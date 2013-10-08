using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

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

        private static int requestsCount = 0;
        private static int maxReqPerMinute = 100;
        private static Stopwatch throttlingTimer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="request"></param>
        public AsanaRequest(HttpWebRequest request)
        {
            _request = request;
        }

        public static bool ThrottlingCheck()
        {
            requestsCount++;
            if (throttlingTimer == null || !throttlingTimer.IsRunning)
                throttlingTimer = Stopwatch.StartNew();
            else if (requestsCount >= maxReqPerMinute && (throttlingTimer.ElapsedTicks / Stopwatch.Frequency) < 60)
            {
                throttlingTimer = Stopwatch.StartNew();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Begins the request
        /// </summary> <Task>
        public Task Go(Action<string, WebHeaderCollection> callback, Action<string, string, string> error, int ThrottleSeconds = 0)
        {
            _callback = callback;
            _error = error;

            if (!ThrottlingCheck())
            {
                // Throttling for a minute before completing the task.
                // Console.WriteLine("Throttling");
                Thread.Sleep(1000 * 10);
            }
            if (ThrottleSeconds > 0)
            {
                Task.Delay(1000 * ThrottleSeconds);
            }

            return Task.Factory.FromAsync<WebResponse>(
                    _request.BeginGetResponse,
                    _request.EndGetResponse,
                    null).ContinueWith( (requestTask) =>
                    {
                        HttpWebRequest request = (HttpWebRequest)_request;
                        AsanaRequest state = (AsanaRequest)requestTask.AsyncState;
                        WebResponse result = requestTask.Result;
                        if (result.Headers["Retry-After"] != null)
                        {
                            string retryAfter = result.Headers["Retry-After"];
                            Go(callback, error, Convert.ToInt32(retryAfter));
                            return;
                        }
                        string responseContent = GetResponseContent(result);
                        if (requestTask.IsFaulted)
                        {
                            _error(result.ResponseUri.AbsoluteUri, requestTask.Exception.InnerException.Message, responseContent);
                            return;
                        }
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
