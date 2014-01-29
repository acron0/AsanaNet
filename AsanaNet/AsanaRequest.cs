using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace AsanaNet
{
    /// <summary>
    /// Wraps up the request
    /// </summary>
    internal class AsanaRequest
    {
        /// <summary>
        /// Contains the actual request object
        /// </summary>
        private HttpWebRequest _request;

        /// <summary>
        /// The callback once the response is received.
        /// </summary>
        private Action<string, WebHeaderCollection> _callback;

        /// <summary>
        /// The error callback
        /// </summary>
        private Action<string, string, string> _error;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="request"></param>
        // depracated
        public AsanaRequest(HttpWebRequest request)
        {
            _request = request;
        }

        /// <summary>
        /// Static because all Asana requests will have a common throttle
        /// </summary>
        private static bool _throttling = false;
        private static ManualResetEvent _throttlingWaitHandle = new ManualResetEvent(false); 

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

        /// <summary>
        /// Begins the request
        /// </summary>
        // depracated
        public Task Go(Action<string, WebHeaderCollection> callback, Action<string, string, string> error)
        {
            _callback = callback;
            _error = error;

            if (_throttling)
            {
                _throttlingWaitHandle.WaitOne();
            }

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

        // depracated
        private string GetResponseContent(WebResponse response)
        {
            Encoding enc = Encoding.GetEncoding(65001);
            var stream = new StreamReader(response.GetResponseStream(), enc);
            string output = stream.ReadToEnd();
            stream.Close();
            return output;
        }

        /// <summary>
        /// Begins the request
        /// </summary>
        public static async Task<string> GoAsync(Asana asana, AsanaFunction function, Uri uri, HttpContent content = null)
        {
            return await Task.Factory.StartNew<string>(() =>
            {
                //_callback = callback;
                //_error = asana.ErrorCallback;

                if (_throttling)
                {
                    _throttlingWaitHandle.WaitOne();
                }

                // Initalise a response object
                HttpResponseMessage response = null;

                if (function.Method != "GET")
                {
                    if (content == null)
                    {
                        //content = new StringContent(null, Encoding.UTF8, "application/json");
                        content = new StringContent("");
                        content.Headers.ContentType =
                            new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    }
                }

                switch (function.Method)
                {
                    default: //GET
                        response = asana._baseHttpClient.GetAsync(uri).Result;
                        break;
                    case "POST":
                        response = asana._baseHttpClient.PostAsync(uri, content).Result;
                        break;
                    case "PUT":
                        response = asana._baseHttpClient.PutAsync(uri, content).Result;
                        break;
                    case "DELETE":
                        response = asana._baseHttpClient.DeleteAsync(uri).Result;
                        break;
                }

                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException e)
                {
                    asana.ErrorCallback(uri.AbsoluteUri, response.StatusCode.ToString(), e.Message);

                    if (!ReferenceEquals(response.Headers.RetryAfter, null) && response.Headers.RetryAfter.Delta.HasValue)
                    {
                        var retryAfter = response.Headers.RetryAfter.Delta.Value.Seconds;
                        ThrottleFor(retryAfter);
                        return GoAsync(asana, function, uri, content).Result;
                    }

                    throw;
                }
                return response.Content.ReadAsStringAsync().Result;

                // Create a content object for the request
                //HttpContent content_ = new System.Net.Http.StringContent(root.ToString(), Encoding.UTF8, "application/json");
            });
        }


        /// <summary>
        /// Begins the request
        /// </summary>
        public static T GetResponse<T>(string responseContent, Asana asana) where T : AsanaObject
        {
            var outputObject = AsanaObject.Create(typeof(T));
            Parsing.Deserialize(Asana.GetDataAsDict(responseContent), outputObject, asana);
            return (T)outputObject;
        }

        /// <summary>
        /// Begins the request
        /// </summary>
        public static AsanaObjectCollection<T> GetResponseCollection<T>(string responseContent, Asana asana) where T : AsanaObject
        {
            var k = Asana.GetDataAsDictArray(responseContent);
            var outputCollection = new AsanaObjectCollection<T>();
            foreach (var outputObjectDict in k)
            {
                var newObject = AsanaObject.Create(typeof(T));
                Parsing.Deserialize(outputObjectDict, newObject, asana);
                outputCollection.Add((T)newObject);
            }
            return outputCollection;
        }
    }
}
