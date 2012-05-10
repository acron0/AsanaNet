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

        /// <summary>
        /// The URL we use to prefix all the requests
        /// e.g. https://app.asana.com/api/1.0
        /// </summary>
        private string _baseUrl;

        /// <summary>
        /// An error callback for the outside world
        /// </summary>
        private Action<string, string> _errorCallback;

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

        #region GET

        private AsanaFunction Users             = new AsanaFunction("/users", "GET");
        private AsanaFunction UsersMe           = new AsanaFunction("/users/me", "GET");
        private AsanaFunction UsersId           = new AsanaFunction("/users/{0}", "GET");

        private AsanaFunction WorkspaceUsers    = new AsanaFunction("/workspaces/{0}/users", "GET");
        private AsanaFunction WorkspaceTasks    = new AsanaFunction("/workspaces/{0}/tasks?&assignee=me", "GET");

        private AsanaFunction TaskId            = new AsanaFunction("/tasks/{0}", "GET");
        private AsanaFunction TaskStories       = new AsanaFunction("/tasks/{0}/stories", "GET");
        private AsanaFunction TaskProjects      = new AsanaFunction("/tasks/{0}/projects", "GET");
        
        private AsanaFunction StoryId           = new AsanaFunction("/stories/{0}", "GET");

        private AsanaFunction ProjectId         = new AsanaFunction("/projects/{0}", "GET");        

        #endregion        

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new Asana entry point.
        /// </summary>
        /// <param name="apiKey">The API key for the account we intend to access</param>
        public Asana(string apiKey, Action<string, string> errorCallback)
        {   
            _baseUrl = "https://app.asana.com/api/1.0";
            _errorCallback = errorCallback;

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

        /// <summary>
        /// The callback for response errors
        /// </summary>
        /// <param name="error"></param>
        internal void ErrorCallback(string requestString, string error)
        {
            _errorCallback(requestString, error);
        }

        /// <summary>
        /// Packs the data and into a collection object and sends it to the callback
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        internal void PackAndSendResponseCollection<T>(string rawData, AsanaCollectionResponseEventHandler callback) where T : IAsanaObject, new()
        {
            var k = GetDataAsDictArray(rawData);
            AsanaObjectCollection collection = new AsanaObjectCollection();
            foreach (var j in k)
            {
                var t = new T();
                t.Parse(j);
                collection.Add(t);
            }

            callback(collection);
        }

        /// <summary>
        /// Packs the data and into a response object and sends it to the callback
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        internal void PackAndSendResponse<T>(string rawData, AsanaResponseEventHandler callback) where T : IAsanaObject, new()
        {
            var u = new T();
            u.Parse(GetDataAsDict(rawData));
            callback(u);
        }

        #endregion

        #region Function Impl

        /// <summary>
        /// Returns the user data for the current user.
        /// </summary>
        /// <returns></returns>
        public void GetMyUser(AsanaResponseEventHandler callback)
        {
            var request = GetBaseRequest(UsersMe);
            request.Go((o, h) => PackAndSendResponse<AsanaUser>(o, callback), ErrorCallback);
        }

        /// <summary>
        /// Returns the user data for all visible users.
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <param name="callback"></param>
        public void GetUsers(AsanaCollectionResponseEventHandler callback)
        {
            var request = GetBaseRequest(Users);
            request.Go((o, h) => PackAndSendResponseCollection<AsanaUser>(o, callback), ErrorCallback);
        }

        /// <summary>
        /// Returns the user data for a given user.
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <param name="callback"></param>
        public void GetUser(Int64 id, AsanaResponseEventHandler callback)
        {
            var request = GetBaseRequest(UsersId, id);
            request.Go((o, h) => PackAndSendResponse<AsanaUser>(o, callback), ErrorCallback);
        }

        /// <summary>
        /// Returns the workspace data for a given workspace
        /// </summary>
        /// <param name="id">ID of the workspace</param>
        /// <param name="callback"></param>
        public void GetWorkspaceUsers(Int64 id, AsanaCollectionResponseEventHandler callback)
        {
            var request = GetBaseRequest(WorkspaceUsers, id);
            request.Go((o, h) => PackAndSendResponseCollection<AsanaUser>(o, callback), ErrorCallback);
        }

        /// <summary>
        /// Returns the tasks for the current user
        /// </summary>
        /// <param name="workspaceId">ID of the workspace</param>
        /// <param name="callback"></param>
        public void GetMyWorkspaceTasks(Int64 workspaceId, AsanaCollectionResponseEventHandler callback)
        {
            var request = GetBaseRequest(WorkspaceTasks, workspaceId);
            request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
        }

        /// <summary>
        /// Returns the task data for a given task.
        /// </summary>
        /// <param name="id">ID of the task</param>
        /// <param name="callback"></param>
        public void GetTask(Int64 id, AsanaResponseEventHandler callback)
        {
            var request = GetBaseRequest(TaskId, id);
            request.Go((o, h) => PackAndSendResponse<AsanaTask>(o, callback), ErrorCallback);
        }    

        /// <summary>
        /// Returns the story data for a given task.
        /// </summary>
        /// <param name="id">ID of the task</param>
        /// <param name="callback"></param>
        public void GetTaskStories(Int64 id, AsanaCollectionResponseEventHandler callback)
        {
            var request = GetBaseRequest(TaskStories, id);
            request.Go((o, h) => PackAndSendResponseCollection<AsanaStory>(o, callback), ErrorCallback);
        }  

        /// <summary>
        /// Returns the story data for a given id.
        /// </summary>
        /// <param name="id">ID of the story</param>
        /// <param name="callback"></param>
        public void GetStory(Int64 id, AsanaResponseEventHandler callback)
        {
            var request = GetBaseRequest(StoryId, id);
            request.Go((o, h) => PackAndSendResponse<AsanaStory>(o, callback), ErrorCallback);
        }  
 
        /// <summary>
        /// Returns the project data for a given task.
        /// </summary>
        /// <param name="id">ID of the task</param>
        /// <param name="callback"></param>
        public void GetTaskProjects(Int64 id, AsanaCollectionResponseEventHandler callback)
        {
            var request = GetBaseRequest(TaskProjects, id);
            request.Go((o, h) => PackAndSendResponseCollection<AsanaProject>(o, callback), ErrorCallback);
        }

        /// <summary>
        /// Returns the project data for a given id.
        /// </summary>
        /// <param name="id">ID of the project</param>
        /// <param name="callback"></param>
        public void GetProject(Int64 id, AsanaResponseEventHandler callback)
        {
            var request = GetBaseRequest(ProjectId, id);
            request.Go((o, h) => PackAndSendResponse<AsanaProject>(o, callback), ErrorCallback);
        } 
        

        #endregion
    }
}
