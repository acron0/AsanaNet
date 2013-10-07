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
        /// Begins the request
        /// </summary>
        public Task Go(Action<string, WebHeaderCollection> callback, Action<string, string, string> error)
        {
            _callback = callback;
            _error = error;
            Task<Task<WebResponse>> requestTask;

            try
            {
                requestTask = new Task<Task<WebResponse>>(async () =>
                {
                    return await Task.Factory.FromAsync<WebResponse>(
                                    _request.BeginGetResponse,
                                    _request.EndGetResponse,
                                    null);
                });
                requestTask.Start();
            }
            catch (System.Net.WebException ex)
            {
                string responseContent = GetResponseContent(ex.Response);
                _error(ex.Response.ResponseUri.AbsoluteUri, ex.Message, responseContent);
                return new Task(() => { });
            }

            HttpWebRequest request = (HttpWebRequest)_request;
            AsanaRequest state = (AsanaRequest)requestTask.AsyncState;
            WebResponse result;
            return Task.Run( async () =>
            {
                requestTask.Wait();
                // unwrap waits for the result
                result = await requestTask.Unwrap();
                string output = GetResponseContent(result);
                _callback(output, result.Headers);
            });
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
