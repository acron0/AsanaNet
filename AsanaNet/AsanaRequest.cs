using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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
        /// </summary> <Task>
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

        private string GetResponseContent(WebResponse response)
        {
            Encoding enc = Encoding.GetEncoding(65001);
            var stream = new StreamReader(response.GetResponseStream(), enc);
            string output = stream.ReadToEnd();
            stream.Close();
            return output;
        }
    }
}
