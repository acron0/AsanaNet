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
        public void Go(Action<string, WebHeaderCollection> callback)
        {
            _callback = callback;
            try
            {
                IAsyncResult result = _request.BeginGetResponse(new AsyncCallback(ResponseCallback), null);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
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
            catch (System.Exception ex)
            {
                throw ex;
            }            

            Encoding enc = Encoding.GetEncoding(1252);
            var stream = new StreamReader(response.GetResponseStream(), enc);
            string output = stream.ReadToEnd();

            stream.Close();
            response.Close();

            _callback(output, response.Headers);
        }
    }
}
