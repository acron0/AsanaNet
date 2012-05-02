using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;

using MiniJSON;

namespace AsanaNet
{
    public delegate void AsanaResponseEventHandler(IAsanaObject response);
    public delegate void AsanaCollectionResponseEventHandler(IAsanaObjectCollection response);

    public class Asana
    {        
        #region Variables

        private string _baseUrl;

        #endregion

        #region Properties

        /// <summary>
        /// The API Key assigned object
        /// </summary>
        public string APIKey { get; private set; }

        /// <summary>
        /// The API Key, but base-64 encoded
        /// </summary>
        public string EncodedAPIKey { get; private set; }

        #endregion        

        #region REST Functions

        private AsanaFunction Users     = new AsanaFunction("users",        "GET");
        private AsanaFunction UsersMe   = new AsanaFunction("users/me",     "GET");
        private AsanaFunction UsersId   = new AsanaFunction("users/{0}",    "GET");

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new Asana entry point.
        /// </summary>
        /// <param name="apiKey">The API key for the account we intend to access</param>
        public Asana(string apiKey)
        {   
            _baseUrl = "https://app.asana.com/api/1.0/";

            APIKey = apiKey;
            EncodedAPIKey = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(apiKey + ":"));
        }

        /// <summary>
        /// Creates a base request object with authorization data. 
        /// </summary>
        /// <param name="append">The string we want to append to the request</param>
        /// <returns></returns>
        private AsanaRequest GetBaseRequest(AsanaFunction function, params object[] args)
        {
            string url = _baseUrl + string.Format(function.Url, args);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers["Authorization"] = "Basic " + EncodedAPIKey;
            request.Method = function.Method;
            return new AsanaRequest(request);
        }

        /// <summary>
        /// Returns the user data for the current user.
        /// </summary>
        /// <returns></returns>
        public void GetMyUserData(AsanaResponseEventHandler callback)
        {
            var request = GetBaseRequest(UsersMe);
            request.Go((o,h) =>
            {
                var u = new AsanaUser();
                u.Parse(GetDataAsDict(o));

                callback( u );
            });
        }

        /// <summary>
        /// Returns the user data for all visible users.
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <param name="callback"></param>
        public void GetUserData(AsanaCollectionResponseEventHandler callback)
        {
            var request = GetBaseRequest(Users);
            request.Go((o, h) =>
            {
                var k = GetDataAsDictArray(o);
                AsanaObjectCollection collection = new AsanaObjectCollection();
                foreach (var j in k)
                {
                   var u = new AsanaUser();
                   u.Parse(j);
                   collection.Add(u);
                }

                callback(collection);
                //
            });
        }

        /// <summary>
        /// Returns the user data for a given user.
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <param name="callback"></param>
        public void GetUserData(Int64 id, AsanaResponseEventHandler callback)
        {
            var request = GetBaseRequest(UsersId, id);
            request.Go((o, h) =>
            {
                var u = new AsanaUser();
                u.Parse(GetDataAsDict(o));

                callback(u);
            });
        }

        /// <summary>
        /// Converts the raw string into dictionary format
        /// </summary>
        /// <param name="dataString"></param>
        /// <returns></returns>
        private Dictionary<string, object> GetDataAsDict(string dataString)
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
        private Dictionary<string, object>[] GetDataAsDictArray(string dataString)
        {
            var data = Json.Deserialize(dataString) as Dictionary<string, object>;
            var data2 = data["data"] as List<object>;
            var data3 = new Dictionary<string, object>[data2.Count];
            for (int i = 0; i < data2.Count; ++i)
                data3[i] = data2[i] as Dictionary<string, object>;
            return data3;
        }

        #endregion
    }
}
