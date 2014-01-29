﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Web;
using System.Threading;
using System.IO;
using System.Xml;
using System.Threading.Tasks;

using MiniJSON;

namespace AsanaNet
{
    public delegate void AsanaResponseEventHandler(AsanaObject response);
    public delegate void AsanaCollectionResponseEventHandler(IAsanaObjectCollection response);

	public enum AuthenticationType
	{
		Basic,
		OAuth
	}

    [Serializable]
    public partial class Asana
    {        
        #region Variables

        /// <summary>
        /// The URL we use to prefix all the requests
        /// e.g. https://app.asana.com/api/1.0
        /// </summary>
        private string _baseUrl;

        /// <summary>
        /// An error callback for the outside world
        /// </summary>
        private Action<string, string, string> _errorCallback;

        #endregion

        #region Properties

		/// <summary>
		/// The Authentication Type used for API access
		/// </summary>
		public AuthenticationType AuthType { get; private set; }

        /// <summary>
        /// The API Key assigned object
        /// </summary>
        public string APIKey { get; private set; }

        /// <summary>
        /// The API Key, but base-64 encoded
        /// </summary>
        public string EncodedAPIKey { get; private set; }

		/// <summary>
		/// The OAuth Bearer Token assigned object
		/// </summary>
		public string OAuthToken { get; set; }

        #endregion        

        #region Methods

        /// <summary>
        /// Creates a new Asana entry point.
        /// </summary>
		/// <param name="apiKeyOrBearerToken">The API key (for Basic authentication) or Bearer Token (for OAuth authentication) for the account we intend to access</param>
		public Asana(string apiKeyOrBearerToken, AuthenticationType authType, Action<string, string, string> errorCallback)
        {   
            _baseUrl = "https://app.asana.com/api/1.0";
            _errorCallback = errorCallback;

			AuthType = authType;
			if (AuthType == AuthenticationType.OAuth) {
				OAuthToken = apiKeyOrBearerToken;
			} else {
				APIKey = apiKeyOrBearerToken;
				EncodedAPIKey = Convert.ToBase64String (System.Text.Encoding.ASCII.GetBytes(apiKeyOrBearerToken + ":"));
			}

            var defaultAuth = new System.Net.Http.Headers.AuthenticationHeaderValue(AuthType == AuthenticationType.OAuth ? "Bearer" : "Basic", AuthType == AuthenticationType.OAuth ? OAuthToken : EncodedAPIKey);
            _baseHttpClient.DefaultRequestHeaders.Authorization = defaultAuth;
			_baseHttpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AsanaNet", "1.1-async"));
            _baseHttpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            AsanaFunction.InitFunctions();
        }

        internal readonly HttpClient _baseHttpClient = new HttpClient();

        private Uri GetBaseUri(AsanaFunction function, params object[] obj)
        {
            var uri = _baseUrl + string.Format(new PropertyFormatProvider(), function.Url, obj);
            return new Uri(uri);
        }

        private Uri GetBaseUriWithParams(AsanaFunction function, Dictionary<string, object> args, params object[] obj)
        {
            var uri = _baseUrl + string.Format(new PropertyFormatProvider(), function.Url, obj);
            if (!ReferenceEquals(args, null) && args.Count > 0)
            {
                uri += "?";
                foreach (var kv in args)
                    uri += kv.Key + "=" + Uri.EscapeUriString(kv.Value.ToString()) + "&";
                uri = uri.TrimEnd('&');
            }
            return new Uri(uri);
        }

        /// <summary>
        /// Creates a base request object with authorization data. 
        /// </summary>
        /// <param name="append">The string we want to append to the request</param>
        /// <returns></returns>
        // depracated
        private AsanaRequest GetBaseRequest(AsanaFunction function, params object[] obj)
        {
            string url = _baseUrl + string.Format(new PropertyFormatProvider(), function.Url, obj);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			if(AuthType == AuthenticationType.Basic)
				request.Headers["Authorization"] = "Basic " + EncodedAPIKey;
			else if(AuthType == AuthenticationType.OAuth)
				request.Headers["Authorization"] = "Bearer " + OAuthToken;
            request.Method = function.Method;
            request.UserAgent = "AsanaNet (github.com/acron0/AsanaNet)";
            return new AsanaRequest(request);
        }

        /// <summary>
        /// Creates a base request object with authorization data AND POST data.
        /// </summary>
        /// <param name="append">The string we want to append to the request</param>
        /// <returns></returns>
        // depracated
        private AsanaRequest GetBaseRequestWithParams(AsanaFunction function, Dictionary<string, object> args, params object[] obj)
        {
            string url = _baseUrl + string.Format(new PropertyFormatProvider(), function.Url, obj);

            if (args.Count > 0)
            {
                url += "?";
                foreach (var kv in args)
                    url += kv.Key.ToString() + "=" + Uri.EscapeUriString(kv.Value.ToString()) + "&";
                url = url.TrimEnd('&');
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			if(AuthType == AuthenticationType.Basic)
				request.Headers["Authorization"] = "Basic " + EncodedAPIKey;
			else if(AuthType == AuthenticationType.OAuth)
				request.Headers["Authorization"] = "Bearer " + OAuthToken;
            request.Method = function.Method;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = 0;

            return new AsanaRequest(request);
        }

        /// <summary>
        /// Converts the raw string into dictionary format
        /// </summary>
        /// <param name="dataString"></param>
        /// <returns></returns>
        // was private 
        internal static Dictionary<string, object> GetDataAsDict(string dataString)
        {
            var data = Json.Deserialize(dataString) as Dictionary<string, object>;
            var data2 = data["data"] as Dictionary<string, object>;
            return data2;
        }

        /// <summary>
        /// Converts the raw string into list of dictionaries format
        /// </summary>
        /// <param name="dataString"></param>
        /// <returns></returns>
        // was private
        internal static Dictionary<string, object>[] GetDataAsDictArray(string dataString)
        {
            var data = Json.Deserialize(dataString) as Dictionary<string, object>;
            var data2 = data["data"] as List<object>;
            var data3 = new Dictionary<string, object>[data2.Count];
            for (int i = 0; i < data2.Count; ++i)
                data3[i] = data2[i] as Dictionary<string, object>;
            return data3;
        }

        /// <summary>
        /// The callback for response errors
        /// </summary>
        /// <param name="error"></param>
        internal void ErrorCallback(string requestString, string error, string responseContent)
        {
            _errorCallback(requestString, error, responseContent);
        }

        /// <summary>
        /// Packs the data and into a collection object and sends it to the callback
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        // depracated
        internal void PackAndSendResponseCollection<T>(string rawData, AsanaCollectionResponseEventHandler callback) where T : AsanaObject
        {
            var k = GetDataAsDictArray(rawData);
            AsanaObjectCollection collection = new AsanaObjectCollection();
            foreach (var j in k)
            {
                var t = AsanaObject.Create(typeof(T));
                Parsing.Deserialize(j, t, this);
                collection.Add(t);
            }
            callback(collection);
        }

        /// <summary>
        /// Packs the data and into a response object and sends it to the callback
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        internal void PackAndSendResponse<T>(string rawData, AsanaResponseEventHandler callback) where T : AsanaObject
        {
            var u = AsanaObject.Create(typeof(T));
            Parsing.Deserialize(GetDataAsDict(rawData), u, this);
            callback(u);
        }

        /// <summary>
        /// Repacks data into an existing object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rawData"></param>
        /// <param name="?"></param>
        internal void RepackAndCallback<T>(string rawData, T obj) where T : AsanaObject
        {
            Parsing.Deserialize(GetDataAsDict(rawData), obj, this);

            obj.SavingCallback(Parsing.Serialize(obj, false, false));
            obj.SavedCallback();
        }

        /// <summary>
        /// Tells the asana object to save the specified object
        /// </summary>
        internal async Task<T> Save<T>(T obj, AsanaFunction func, Dictionary<string, object> data = null) where T: AsanaObject
        {
            IAsanaData idata = obj as IAsanaData;
            if (idata == null)
                throw new NullReferenceException("All AsanaObjects must implement IAsanaData in order to Save themselves.");

            if (data == null)
				data = Parsing.Serialize(obj, true, !idata.IsObjectLocal);

			// depracated var
			//AsanaRequest request = null;

            AsanaFunctionAssociation afa = AsanaFunction.GetFunctionAssociation(obj.GetType());

            if (func == null)
                func = idata.IsObjectLocal ? afa.Create : afa.Update;

            var uri = GetBaseUriWithParams(func, data, obj);
            var response = await AsanaRequest.GoAsync(this, func, uri);
            return AsanaRequest.GetResponse<T>(response, this);

            // TODO: serialize to JSON
            // http://stackoverflow.com/questions/12458532/suppress-requiredattribute-validation-for-the-jsonmediatypeformatter-in-asp-net
            // http://www.asp.net/web-api/overview/web-api-clients/calling-a-web-api-from-a-net-client

            // depracated version
            //request = GetBaseRequestWithParams(func, data, obj);
            //return request.Go((o, h) => RepackAndCallback(o, obj), ErrorCallback);
        }

        /// <summary>
        /// Tells asana to delete the specified object
        /// </summary>
        /// <param name="obj"></param>
        internal async Task<T> Delete<T>(T obj) where T : AsanaObject
        {
            AsanaFunction func;

            IAsanaData idata = obj as IAsanaData;
            if (idata == null)
                throw new NullReferenceException("All AsanaObjects must implement IAsanaData in order to Delete themselves.");

			// depracated var
			//AsanaRequest request = null;

            AsanaFunctionAssociation afa = AsanaFunction.GetFunctionAssociation(obj.GetType());

            if (idata.IsObjectLocal == false)
                func = afa.Delete;
            else 
                throw new Exception("Object is local, cannot delete.");

            if (Object.ReferenceEquals(func, null)) throw new NotImplementedException("This object cannot delete itself.");

            var uri = GetBaseUriWithParams(func, null, obj);
            var response = await AsanaRequest.GoAsync(this, func, uri);
            return AsanaRequest.GetResponse<T>(response, this);

//            request = GetBaseRequest(func, obj);
//            return request.Go((o, h) => RepackAndCallback(o, obj), ErrorCallback);
        }

        #endregion
    }
}
