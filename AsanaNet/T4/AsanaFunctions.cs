using AsanaNet.Objects;
using System;
using System.Threading.Tasks;
/*
* THIS FILE IS GENERATED! DO NOT EDIT!
* REFER TO AsanaFunctionDefinitions.xml
*/
namespace AsanaNet
{
		// Enums for all functions
		public enum Function
		{
			GetUsers,
			GetMe,
			GetUserById,
			GetWorkspaces,
			GetWorkspaceById,
			GetUsersInWorkspace,
			GetTasksInWorkspace,
			GetProjectsInWorkspace,
			GetTagsInWorkspace,
			GetTaskById,
			GetStoriesInTask,
			GetProjectsOnATask,
			GetTasksByTag,
			GetStoryById,
			GetProjectById,
			GetJobById,
			GetTeamById,
			GetTasksInAProject,
			GetTagById,
			GetTeamsInWorkspace,
			GetProjectsInTeam,
			GetEventsInAProject,
			GetEventsInTask,
			CreateWorkspaceTask,
			AddProjectToTask,
			RemoveProjectFromTask,
			AddStoryToTask,
			AddTagToTask,
			RemoveTagFromTask,
			CreateWorkspaceProject,
			CreateWorkspaceTag,
			UpdateTask,
			UpdateTag,
			UpdateProject,
			UpdateWorkspace,
			DeleteTask,
			DeleteProject,
			DuplicateProject
		}

		// Function definitions specifically for the GET functions.
		public partial class Asana
		{
			public Task GetUsers(AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUsers));
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaUser>(o, callback), ErrorCallback);
			}

            public Task<IAsanaObjectCollection<AsanaUser>> GetUsersAsync()
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUsers));
                return request.GoCollectionAsync<AsanaUser>();
            }

			public Task GetMe(AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetMe));
				return request.Go((o, h) => PackAndSendResponse<AsanaUser>(o, callback), ErrorCallback);
			}

            public Task<AsanaUser> GetMeAsync()
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetMe));
                return request.GoAsync<AsanaUser>();
            }

			public Task GetUserById(Int64 int64, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUserById), int64);
				return request.Go((o, h) => PackAndSendResponse<AsanaUser>(o, callback), ErrorCallback);
			}

            public Task<AsanaUser> GetUserByIdAsync(Int64 int64)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUserById), int64);
                return request.GoAsync<AsanaUser>();
            }

			public Task GetWorkspaces(AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetWorkspaces));
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaWorkspace>(o, callback), ErrorCallback);
			}

            public Task<IAsanaObjectCollection<AsanaWorkspace>> GetWorkspacesAsync()
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetWorkspaces));
                return request.GoCollectionAsync<AsanaWorkspace>();
            }

			public Task GetWorkspaceById(Int64 int64, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetWorkspaceById), int64);
				return request.Go((o, h) => PackAndSendResponse<AsanaWorkspace>(o, callback), ErrorCallback);
			}

            public Task<AsanaWorkspace> GetWorkspaceByIdAsync(Int64 int64)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetWorkspaceById), int64);
                return request.GoAsync<AsanaWorkspace>();
            }

			public Task GetUsersInWorkspace(AsanaWorkspace asanaWorkspace, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), asanaWorkspace);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaUser>(o, callback), ErrorCallback);
			}

            public Task<IAsanaObjectCollection<AsanaUser>> GetUsersInWorkspaceAsync(AsanaWorkspace asanaWorkspace)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), asanaWorkspace);
                return request.GoCollectionAsync<AsanaUser>();
            }

			public Task GetTasksInWorkspace(AsanaWorkspace asanaWorkspace, AsanaUser asanaUser, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), asanaWorkspace, asanaUser);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
			}

            public Task<IAsanaObjectCollection<AsanaTask>> GetTasksInWorkspaceAsync(AsanaWorkspace asanaWorkspace, AsanaUser asanaUser)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), asanaWorkspace, asanaUser);
                return request.GoCollectionAsync<AsanaTask>();
            }

			public Task GetProjectsInWorkspace(AsanaWorkspace asanaWorkspace, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), asanaWorkspace);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaProject>(o, callback), ErrorCallback);
			}

            public Task<IAsanaObjectCollection<AsanaProject>> GetProjectsInWorkspaceAsync(AsanaWorkspace asanaWorkspace)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), asanaWorkspace);
                return request.GoCollectionAsync<AsanaProject>();
            }

			public Task GetTagsInWorkspace(AsanaWorkspace asanaWorkspace, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), asanaWorkspace);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaTag>(o, callback), ErrorCallback);
			}

            public Task<IAsanaObjectCollection<AsanaTag>> GetTagsInWorkspaceAsync(AsanaWorkspace asanaWorkspace)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), asanaWorkspace);
                return request.GoCollectionAsync<AsanaTag>();
            }

			public Task GetTaskById(Int64 int64, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTaskById), int64);
				return request.Go((o, h) => PackAndSendResponse<AsanaTask>(o, callback), ErrorCallback);
			}

            public Task<AsanaTask> GetTaskByIdAsync(Int64 int64)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTaskById), int64);
                return request.GoAsync<AsanaTask>();
            }

			public Task GetStoriesInTask(AsanaTask asanaTask, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetStoriesInTask), asanaTask);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaStory>(o, callback), ErrorCallback);
			}

            public Task<IAsanaObjectCollection<AsanaStory>> GetStoriesInTaskAsync(AsanaTask asanaTask)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetStoriesInTask), asanaTask);
                return request.GoCollectionAsync<AsanaStory>();
            }

			public Task GetProjectsOnATask(AsanaTask asanaTask, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsOnATask), asanaTask);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaProject>(o, callback), ErrorCallback);
			}

            public Task<IAsanaObjectCollection<AsanaProject>> GetProjectsOnATaskAsync(AsanaTask asanaTask)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsOnATask), asanaTask);
                return request.GoCollectionAsync<AsanaProject>();
            }

			public Task GetTasksByTag(AsanaTag asanaTag, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksByTag), asanaTag);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
			}

            public Task<IAsanaObjectCollection<AsanaTask>> GetTasksByTagAsync(AsanaTag asanaTag)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksByTag), asanaTag);
                return request.GoCollectionAsync<AsanaTask>();
            }

			public Task GetStoryById(Int64 int64, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetStoryById), int64);
				return request.Go((o, h) => PackAndSendResponse<AsanaStory>(o, callback), ErrorCallback);
			}

            public Task<AsanaStory> GetStoryByIdAsync(Int64 int64)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetStoryById), int64);
                return request.GoAsync<AsanaStory>();
            }

			public Task GetProjectById(Int64 int64, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectById), int64);
				return request.Go((o, h) => PackAndSendResponse<AsanaProject>(o, callback), ErrorCallback);
			}

			public Task GetJobById(Int64 int64, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetJobById), int64);
				return request.Go((o, h) => PackAndSendResponse<AsanaJob>(o, callback), ErrorCallback);
			}

			public Task GetTeamById(Int64 int64, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTeamById), int64);
				return request.Go((o, h) => PackAndSendResponse<AsanaTeam>(o, callback), ErrorCallback);
			}

			public Task DuplicateProjectById(Int64 int64, AsanaDuplicateProjectSettings settings, AsanaResponseEventHandler callback)
			{
				var data = Parsing.Serialize(settings, true, false);
				var request = GetBaseRequestWithParams(AsanaFunction.GetFunction(Function.DuplicateProject), data, int64);
				return request.Go((o, h) => PackAndSendResponse<AsanaDuplicateProjectJob>(o, callback), ErrorCallback);
			}

            public Task<AsanaProject> GetProjectByIdAsync(Int64 int64)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectById), int64);
                return request.GoAsync<AsanaProject>();
            }

			public Task GetTasksInAProject(AsanaProject asanaProject, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksInAProject), asanaProject);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
			}

            public Task<IAsanaObjectCollection<AsanaTask>> GetTasksInAProjectAsync(AsanaProject asanaProject)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksInAProject), asanaProject);
                return request.GoCollectionAsync<AsanaTask>();
            }

			public Task GetTagById(Int64 int64, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTagById), int64);
				return request.Go((o, h) => PackAndSendResponse<AsanaTag>(o, callback), ErrorCallback);
			}

            public Task<AsanaTag> GetTagByIdAsync(Int64 int64)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTagById), int64);
                return request.GoAsync<AsanaTag>();
            }

			public Task GetTeamsInWorkspace(AsanaWorkspace asanaWorkspace, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), asanaWorkspace);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaTeam>(o, callback), ErrorCallback);
			}

            public Task<IAsanaObjectCollection<AsanaTeam>> GetTeamsInWorkspaceAsync(AsanaWorkspace asanaWorkspace)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), asanaWorkspace);
                return request.GoCollectionAsync<AsanaTeam>();
            }

			public Task GetProjectsInTeam(AsanaTeam asanaTeam, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsInTeam), asanaTeam);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaProject>(o, callback), ErrorCallback);
			}

            public Task<IAsanaObjectCollection<AsanaProject>> GetProjectsInTeamAsync(AsanaTeam asanaTeam)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsInTeam), asanaTeam);
                return request.GoCollectionAsync<AsanaProject>();
            }

			public Task GetEventsInAProject(AsanaProject asanaProject, String string1, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetEventsInAProject), asanaProject, string1);
				return request.Go((o, h) => PackAndSendResponse<AsanaEvent>(o, callback), ErrorCallback);
			}

            public Task<AsanaEvent> GetEventsInAProjectAsync(AsanaProject asanaProject, String string1)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetEventsInAProject), asanaProject, string1);
                return request.GoAsync<AsanaEvent>(true);
            }

			public Task GetEventsInTask(AsanaTask asanaTask, String string1, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetEventsInTask), asanaTask, string1);
				return request.Go((o, h) => PackAndSendResponse<AsanaEvent>(o, callback), ErrorCallback);
			}

            public Task<AsanaEvent> GetEventsInTaskAsync(AsanaTask asanaTask, String string1)
            {
                var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetEventsInTask), asanaTask, string1);
                return request.GoAsync<AsanaEvent>(true);
            }




			public Task Get<AsanaT>(AsanaCollectionResponseEventHandler callback) where AsanaT : AsanaObject
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(typeof(AsanaT) == typeof(AsanaUser))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUsers));
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaUser>(o, callback), ErrorCallback);
				}

								
				if(typeof(AsanaT) == typeof(AsanaWorkspace))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetWorkspaces));
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaWorkspace>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + typeof(AsanaT).Name);
			}


			public Task Get<AsanaT>(AsanaResponseEventHandler callback) where AsanaT : AsanaObject
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(typeof(AsanaT) == typeof(AsanaUser))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetMe));
					return request.Go((o, h) => PackAndSendResponse<AsanaUser>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + typeof(AsanaT).Name);
			}


			public Task Get<AsanaT>(Int64 arg1, AsanaResponseEventHandler callback) where AsanaT : AsanaObject
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(typeof(AsanaT) == typeof(AsanaUser))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUserById), arg1);
					return request.Go((o, h) => PackAndSendResponse<AsanaUser>(o, callback), ErrorCallback);
				}

								
				if(typeof(AsanaT) == typeof(AsanaWorkspace))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetWorkspaceById), arg1);
					return request.Go((o, h) => PackAndSendResponse<AsanaWorkspace>(o, callback), ErrorCallback);
				}

								
				if(typeof(AsanaT) == typeof(AsanaTask))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTaskById), arg1);
					return request.Go((o, h) => PackAndSendResponse<AsanaTask>(o, callback), ErrorCallback);
				}

								
				if(typeof(AsanaT) == typeof(AsanaStory))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetStoryById), arg1);
					return request.Go((o, h) => PackAndSendResponse<AsanaStory>(o, callback), ErrorCallback);
				}

								
				if(typeof(AsanaT) == typeof(AsanaProject))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectById), arg1);
					return request.Go((o, h) => PackAndSendResponse<AsanaProject>(o, callback), ErrorCallback);
				}

								
				if(typeof(AsanaT) == typeof(AsanaTag))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTagById), arg1);
					return request.Go((o, h) => PackAndSendResponse<AsanaTag>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + typeof(AsanaT).Name);
			}


			public Task Get<AsanaT>(AsanaWorkspace arg1, AsanaCollectionResponseEventHandler callback) where AsanaT : AsanaObject
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(typeof(AsanaT) == typeof(AsanaUser))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaUser>(o, callback), ErrorCallback);
				}

								
				if(typeof(AsanaT) == typeof(AsanaProject))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaProject>(o, callback), ErrorCallback);
				}

								
				if(typeof(AsanaT) == typeof(AsanaTag))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaTag>(o, callback), ErrorCallback);
				}

								
				if(typeof(AsanaT) == typeof(AsanaTeam))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaTeam>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + typeof(AsanaT).Name);
			}


			public Task Get<AsanaT>(AsanaWorkspace arg1,  AsanaUser arg2, AsanaCollectionResponseEventHandler callback) where AsanaT : AsanaObject
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(typeof(AsanaT) == typeof(AsanaTask))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), arg1, arg2);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + typeof(AsanaT).Name);
			}


			public Task Get<AsanaT>(AsanaTask arg1, AsanaCollectionResponseEventHandler callback) where AsanaT : AsanaObject
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(typeof(AsanaT) == typeof(AsanaStory))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetStoriesInTask), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaStory>(o, callback), ErrorCallback);
				}

								
				if(typeof(AsanaT) == typeof(AsanaProject))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsOnATask), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaProject>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + typeof(AsanaT).Name);
			}


			public Task Get<AsanaT>(AsanaTag arg1, AsanaCollectionResponseEventHandler callback) where AsanaT : AsanaObject
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(typeof(AsanaT) == typeof(AsanaTask))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksByTag), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + typeof(AsanaT).Name);
			}


			public Task Get<AsanaT>(AsanaProject arg1, AsanaCollectionResponseEventHandler callback) where AsanaT : AsanaObject
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(typeof(AsanaT) == typeof(AsanaTask))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksInAProject), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + typeof(AsanaT).Name);
			}


			public Task Get<AsanaT>(AsanaTeam arg1, AsanaCollectionResponseEventHandler callback) where AsanaT : AsanaObject
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(typeof(AsanaT) == typeof(AsanaProject))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsInTeam), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaProject>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + typeof(AsanaT).Name);
			}


			public Task Get<AsanaT>(AsanaProject arg1,  String arg2, AsanaResponseEventHandler callback) where AsanaT : AsanaObject
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(typeof(AsanaT) == typeof(AsanaEvent))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetEventsInAProject), arg1, arg2);
					return request.Go((o, h) => PackAndSendResponse<AsanaEvent>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + typeof(AsanaT).Name);
			}


			public Task Get<AsanaT>(AsanaTask arg1,  String arg2, AsanaResponseEventHandler callback) where AsanaT : AsanaObject
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(typeof(AsanaT) == typeof(AsanaEvent))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetEventsInTask), arg1, arg2);
					return request.Go((o, h) => PackAndSendResponse<AsanaEvent>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + typeof(AsanaT).Name);
			}



	}

		// Binds the enums, formations and methods
		public partial class AsanaFunction
		{
			static internal void InitFunctions()
			{
				Functions.Add(Function.GetUsers, new AsanaFunction("/users", "GET"));
				Functions.Add(Function.GetMe, new AsanaFunction("/users/me", "GET"));
				Functions.Add(Function.GetUserById, new AsanaFunction("/users/{0}", "GET"));
				Functions.Add(Function.GetWorkspaces, new AsanaFunction("/workspaces", "GET"));
				Functions.Add(Function.GetWorkspaceById, new AsanaFunction("/workspaces/{0}", "GET"));
				Functions.Add(Function.GetUsersInWorkspace, new AsanaFunction("/workspaces/{0:ID}/users", "GET"));
				Functions.Add(Function.GetTasksInWorkspace, new AsanaFunction("/workspaces/{0:ID}/tasks?assignee={1:ID}", "GET"));
				Functions.Add(Function.GetProjectsInWorkspace, new AsanaFunction("/workspaces/{0:ID}/projects", "GET"));
				Functions.Add(Function.GetTagsInWorkspace, new AsanaFunction("/workspaces/{0:ID}/tags", "GET"));
				Functions.Add(Function.GetTaskById, new AsanaFunction("/tasks/{0}", "GET"));
				Functions.Add(Function.GetStoriesInTask, new AsanaFunction("/tasks/{0:ID}/stories", "GET"));
				Functions.Add(Function.GetProjectsOnATask, new AsanaFunction("/tasks/{0:ID}/projects", "GET"));
				Functions.Add(Function.GetTasksByTag, new AsanaFunction("/tags/{0:ID}/tasks", "GET"));
				Functions.Add(Function.GetStoryById, new AsanaFunction("/stories/{0}", "GET"));
				Functions.Add(Function.GetProjectById, new AsanaFunction("/projects/{0}", "GET"));
				Functions.Add(Function.GetJobById, new AsanaFunction("/jobs/{0}", "GET"));
				Functions.Add(Function.GetTeamById, new AsanaFunction("/teams/{0}", "GET"));
				Functions.Add(Function.GetTasksInAProject, new AsanaFunction("/projects/{0:ID}/tasks", "GET"));
				Functions.Add(Function.GetTagById, new AsanaFunction("/tags/{0}", "GET"));
				Functions.Add(Function.GetTeamsInWorkspace, new AsanaFunction("/organizations/{0:ID}/teams", "GET"));
				Functions.Add(Function.GetProjectsInTeam, new AsanaFunction("/teams/{0:ID}/projects", "GET"));
				Functions.Add(Function.GetEventsInAProject, new AsanaFunction("/projects/{0:ID}/events?sync={1}", "GET"));
				Functions.Add(Function.GetEventsInTask, new AsanaFunction("/tasks/{0:ID}/events?sync={1}", "GET"));
				Functions.Add(Function.CreateWorkspaceTask, new AsanaFunction("/tasks", "POST"));
				Functions.Add(Function.AddProjectToTask, new AsanaFunction("/tasks/{0:ID}/addProject", "POST"));
				Functions.Add(Function.RemoveProjectFromTask, new AsanaFunction("/tasks/{0:ID}/removeProject", "POST"));
				Functions.Add(Function.AddStoryToTask, new AsanaFunction("/tasks/{0:Target}/stories", "POST"));
				Functions.Add(Function.AddTagToTask, new AsanaFunction("/tasks/{0:ID}/addTag", "POST"));
				Functions.Add(Function.RemoveTagFromTask, new AsanaFunction("/tasks/{0:ID}/removeTag", "POST"));
				Functions.Add(Function.CreateWorkspaceProject, new AsanaFunction("/projects", "POST"));
				Functions.Add(Function.CreateWorkspaceTag, new AsanaFunction("/tags", "POST"));
				Functions.Add(Function.UpdateTask, new AsanaFunction("/tasks/{0:ID}", "PUT"));
				Functions.Add(Function.UpdateTag, new AsanaFunction("/tags/{0:ID}", "PUT"));
				Functions.Add(Function.UpdateProject, new AsanaFunction("/projects/{0:ID}", "PUT"));
				Functions.Add(Function.UpdateWorkspace, new AsanaFunction("/workspaces/{0:ID}", "PUT"));
				Functions.Add(Function.DeleteTask, new AsanaFunction("/tasks/{0:ID}", "DELETE"));
				Functions.Add(Function.DeleteProject, new AsanaFunction("/projects/{0:ID}", "DELETE"));
				Functions.Add(Function.DuplicateProject, new AsanaFunction("/projects/{0}/duplicate", "POST"));


				Associations.Add(typeof(AsanaWorkspace), new AsanaFunctionAssociation(null, GetFunction(Function.UpdateWorkspace), null));
				Associations.Add(typeof(AsanaTask), new AsanaFunctionAssociation(GetFunction(Function.CreateWorkspaceTask), GetFunction(Function.UpdateTask), GetFunction(Function.DeleteTask)));
				Associations.Add(typeof(AsanaProject), new AsanaFunctionAssociation(GetFunction(Function.CreateWorkspaceProject), GetFunction(Function.UpdateProject), GetFunction(Function.DeleteProject)));
				Associations.Add(typeof(AsanaTag), new AsanaFunctionAssociation(GetFunction(Function.CreateWorkspaceTag), GetFunction(Function.UpdateTag), null));
				Associations.Add(typeof(AsanaStory), new AsanaFunctionAssociation(GetFunction(Function.AddStoryToTask), null, null));
		
			}
		}
}