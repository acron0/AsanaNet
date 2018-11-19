using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MiniJSON;

namespace AsanaNet
{
    /// <summary>
    /// Wraps up the request
    /// </summary>
    internal class AsanaRequest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="request"></param>
        /// <param name="host"></param>
        public AsanaRequest(HttpWebRequest request, Asana host)
        {
            _request = request;
            _host = host;
        }
        
        #region Static variables
        
        /// <summary>
        /// Static because all Asana requests will have a common throttle
        /// </summary>
        private static bool _throttling = false;
        private static ManualResetEvent _throttlingWaitHandle = new ManualResetEvent(false); 
        
        #endregion
        
        #region Variables
        
        /// <summary>
        /// Contains the actual request object
        /// </summary>
        private readonly HttpWebRequest _request;

        /// <summary>
        /// Link with the instance owner of the request
        /// </summary>
        private readonly Asana _host;

        /// <summary>
        /// The callback once the response is received.
        /// </summary>
        private Action<string, WebHeaderCollection> _callback;

        /// <summary>
        /// The error callback
        /// </summary>
        private Action<string, string, string> _error;

        #endregion
                     
        #region Task communication pattern
        
        /// <summary>
        /// Begins the request
        /// </summary> <Task>
        public Task Go(Action<string, WebHeaderCollection> callback, Action<string, string, string> error)
        {
            _callback = callback;
            _error = error;

            if (_throttling)            
                _throttlingWaitHandle.WaitOne();

            return Task.Factory.FromAsync<WebResponse>(
                    _request.BeginGetResponse,
                    _request.EndGetResponse,
                    null).ContinueWith( (requestTask) =>
                    {
                        HttpWebRequest request = (HttpWebRequest)_request;
                        AsanaRequest state = (AsanaRequest)requestTask.AsyncState;

                        if (requestTask.IsFaulted)
                        {
                            _error(request.Address.AbsoluteUri, requestTask.Exception.InnerException.Message, "");
                            return;
                        }
                        
                        WebResponse result = requestTask.Result;
                        
                        if (result.Headers["Retry-After"] != null)
                        {
                            string retryAfter = result.Headers["Retry-After"];
                            ThrottleFor(Convert.ToInt32(retryAfter));
                            Go(callback, error);
                            return;
                        }
                        string responseContent = GetResponseContent(result);
                        _callback(responseContent, result.Headers);
                    }
            );
        }               

        private static string GetResponseContent(WebResponse response)
        {
            Encoding enc = Encoding.GetEncoding(65001);
            var stream = new StreamReader(response.GetResponseStream(), enc);
            string output = stream.ReadToEnd();
            stream.Close();
            return output;
        }
        
        #endregion
        
        #region Async/Await communication pattern
        
        /// <summary>
        /// Begins the request
        /// </summary> <Task>
        public async Task<TAsanaObject> GoAsync<TAsanaObject>(bool respectOriginalStructure = false)
            where TAsanaObject : AsanaObject
        {
            if (_throttling)
                _throttlingWaitHandle.WaitOne();
                            
            using (var response = await GetWebResponseAsync(_request))
            {
                if (response.Headers["Retry-After"] != null)
                {
                    var retryAfter = response.Headers["Retry-After"];
                    ThrottleFor(Convert.ToInt32(retryAfter));
                    return await GoAsync<TAsanaObject>();
                }

                using (var dataStream = response.GetResponseStream())
                using (var reader = new StreamReader(dataStream))
                {
                    var responseFromServer = reader.ReadToEnd();
                    if (respectOriginalStructure)
                    {
                        var result = PackOriginalContent<TAsanaObject>(responseFromServer);
                        return result;
                    }
                    else
                    {
                        var result = PackAndSendResponse<TAsanaObject>(responseFromServer);
                        return result;
                    }
                }
            }
        }

        private async Task<WebResponse> GetWebResponseAsync(HttpWebRequest request)
        {
            try
            {
                return await request.GetResponseAsync();
            }
            catch (WebException e)
            {
                return e.Response;
            }
        }

        /// <summary>
        /// Begins the request
        /// </summary> <Task>
        public async Task<IAsanaObjectCollection<TAsanaObject>> GoCollectionAsync<TAsanaObject>()
            where TAsanaObject : AsanaObject
        {
            if (_throttling)            
                _throttlingWaitHandle.WaitOne();

            using (var response = await _request.GetResponseAsync())
            {
                if (response.Headers["Retry-After"] != null)
                {
                    var retryAfter = response.Headers["Retry-After"];
                    ThrottleFor(Convert.ToInt32(retryAfter));
                    return await GoCollectionAsync<TAsanaObject>();
                }
                
                using (var dataStream = response.GetResponseStream())
                using (var reader = new StreamReader(dataStream))
                {
                    var responseFromServer = reader.ReadToEnd();
                    var result = PackAndSendResponseCollection<TAsanaObject>(responseFromServer);
                    return result;
                }
            }
        }
                
        #region Json deserealize utils

        private T PackOriginalContent<T>(string rawData) where T : AsanaObject
        {
            var data = Json.Deserialize(rawData) as Dictionary<string, object>;
            var u = AsanaObject.Create(typeof(T));
            Parsing.Deserialize(data, u, _host);
            return (T) u;
        }
        
        /// <summary>
        /// Packs the data and into a response object and sends it to the callback
        /// </summary>
        /// <typeparam name="T"></typeparam>        
        private T PackAndSendResponse<T>(string rawData) where T : AsanaObject
        {
            var u = AsanaObject.Create(typeof(T));
            Parsing.Deserialize(GetDataAsDict(rawData), u, _host);
            return (T) u;
        }               
        
        /// <summary>
        /// Converts the raw string into dictionary format
        /// </summary>
        /// <param name="dataString"></param>
        /// <returns></returns>
        private static Dictionary<string, object> GetDataAsDict(string dataString)
        {            
            var data = Json.Deserialize(dataString) as Dictionary<string, object>;
            var data2 = data["data"] as Dictionary<string, object>;
            return data2;
        }

        /// <summary>
        /// Packs the data and into a collection object and sends it to the callback
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private IAsanaObjectCollection<T> PackAndSendResponseCollection<T>(string rawData) where T : AsanaObject
        {
            var k = GetDataAsDictArray(rawData);
            AsanaObjectCollection<T> collection = new AsanaObjectCollection<T>();
            foreach (var j in k)
            {
                var t = (T) AsanaObject.Create(typeof(T));
                Parsing.Deserialize(j, t, _host);
                collection.Add(t);
            }

            return collection;            
        }
        
        /// <summary>
        /// Converts the raw string into list of dictionaries format
        /// </summary>
        /// <param name="dataString"></param>
        /// <returns></returns>
        private static Dictionary<string, object>[] GetDataAsDictArray(string dataString)
        {
            var data = Json.Deserialize(dataString) as Dictionary<string, object>;
            var data2 = data["data"] as List<object>;
            var data3 = new Dictionary<string, object>[data2.Count];
            for (int i = 0; i < data2.Count; ++i)
                data3[i] = data2[i] as Dictionary<string, object>;
            return data3;
        }
        
        #endregion
        
        #endregion
        
        #region Methods
        
        private static void ThrottleFor(int seconds)
        {
            _throttling = true;
            Timer timer = null;
            timer = new Timer(s =>
                {
                    _throttling = false;
                    _throttlingWaitHandle.Set();
                    _throttlingWaitHandle = new ManualResetEvent(false);
                    timer.Dispose();
                }, null, 1000 * seconds, Timeout.Infinite);
        }
        
        #endregion
    }
}
