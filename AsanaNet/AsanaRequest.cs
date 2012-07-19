using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

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
        public void Go(Action<string, WebHeaderCollection> callback, Action<string, string, string> error)
        {
            _callback = callback;
            _error = error;
            IAsyncResult result = _request.BeginGetResponse(new AsyncCallback(ResponseCallback), null);

        }

        private string GetResponseContent(WebResponse response)
        {
            Encoding enc = Encoding.GetEncoding(1252);
            var stream = new StreamReader(response.GetResponseStream(), enc);
            string output = stream.ReadToEnd();
            stream.Close();
            return output;
        }

        /// <summary>
        /// Callback for the end of the async operation.
        /// </summary>
        /// <param name="result"></param>
        private void ResponseCallback(IAsyncResult result)
        {
            AsanaRequest state = (AsanaRequest)result.AsyncState;
            HttpWebRequest request = (HttpWebRequest)_request;

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.EndGetResponse(result);
            }
            catch (System.Net.WebException ex)
            {
                string responseContent = GetResponseContent(ex.Response);
                _error(ex.Response.ResponseUri.AbsoluteUri, ex.Message, responseContent);
                return;
            }            

            string output = GetResponseContent(response);

            response.Close();

            _callback(output, response.Headers);
        }
    }
}
