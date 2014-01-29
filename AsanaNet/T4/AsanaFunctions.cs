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
			GetSubtasksInTask,
			GetStoriesInTask,
			GetProjectsOnATask,
			GetTasksByTag,
			GetStoryById,
			GetProjectById,
			GetTasksInAProject,
			GetTagById,
			GetTeamsInWorkspace,
			CreateWorkspaceTask,
			CreateTaskSubtask,
			SetParentTask,
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
		}

		// Function definitions specifically for the GET functions.
		public partial class Asana
		{
			public Task GetUsers(AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUsers));
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaUser>(o, callback), ErrorCallback);
			}
			public async Task<AsanaObjectCollection<AsanaUser>> GetUsers()
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetUsers));
                var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetUsers), uri);
                return AsanaRequest.GetResponseCollection<AsanaUser>(response, this);
			}
			

			public Task GetMe(AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetMe));
				return request.Go((o, h) => PackAndSendResponse<AsanaUser>(o, callback), ErrorCallback);
			}
			public async Task<AsanaUser> GetMe()
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetMe));
				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetMe), uri);
                return AsanaRequest.GetResponse<AsanaUser>(response, this);
			}
			

			public Task GetUserById(Int64 arg1, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUserById), arg1);
				return request.Go((o, h) => PackAndSendResponse<AsanaUser>(o, callback), ErrorCallback);
			}
			public async Task<AsanaUser> GetUserById(Int64 arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetUserById), arg1);
				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetUserById), uri);
                return AsanaRequest.GetResponse<AsanaUser>(response, this);
			}
			

			public Task GetWorkspaces(AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetWorkspaces));
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaWorkspace>(o, callback), ErrorCallback);
			}
			public async Task<AsanaObjectCollection<AsanaWorkspace>> GetWorkspaces()
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetWorkspaces));
                var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetWorkspaces), uri);
                return AsanaRequest.GetResponseCollection<AsanaWorkspace>(response, this);
			}
			

			public Task GetWorkspaceById(Int64 arg1, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetWorkspaceById), arg1);
				return request.Go((o, h) => PackAndSendResponse<AsanaWorkspace>(o, callback), ErrorCallback);
			}
			public async Task<AsanaWorkspace> GetWorkspaceById(Int64 arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetWorkspaceById), arg1);
				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetWorkspaceById), uri);
                return AsanaRequest.GetResponse<AsanaWorkspace>(response, this);
			}
			

			public Task GetUsersInWorkspace(AsanaWorkspace arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), arg1);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaUser>(o, callback), ErrorCallback);
			}
			public async Task<AsanaObjectCollection<AsanaUser>> GetUsersInWorkspace(AsanaWorkspace arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), arg1);
                var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetUsersInWorkspace), uri);
                return AsanaRequest.GetResponseCollection<AsanaUser>(response, this);
			}
			

			public Task GetTasksInWorkspace(AsanaWorkspace arg1,  AsanaUser arg2, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), arg1, arg2);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
			}
			public async Task<AsanaObjectCollection<AsanaTask>> GetTasksInWorkspace(AsanaWorkspace arg1,  AsanaUser arg2)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), arg1, arg2);
                var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTasksInWorkspace), uri);
                return AsanaRequest.GetResponseCollection<AsanaTask>(response, this);
			}
			

			public Task GetProjectsInWorkspace(AsanaWorkspace arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), arg1);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaProject>(o, callback), ErrorCallback);
			}
			public async Task<AsanaObjectCollection<AsanaProject>> GetProjectsInWorkspace(AsanaWorkspace arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), arg1);
                var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), uri);
                return AsanaRequest.GetResponseCollection<AsanaProject>(response, this);
			}
			

			public Task GetTagsInWorkspace(AsanaWorkspace arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), arg1);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaTag>(o, callback), ErrorCallback);
			}
			public async Task<AsanaObjectCollection<AsanaTag>> GetTagsInWorkspace(AsanaWorkspace arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), arg1);
                var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTagsInWorkspace), uri);
                return AsanaRequest.GetResponseCollection<AsanaTag>(response, this);
			}
			

			public Task GetTaskById(Int64 arg1, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTaskById), arg1);
				return request.Go((o, h) => PackAndSendResponse<AsanaTask>(o, callback), ErrorCallback);
			}
			public async Task<AsanaTask> GetTaskById(Int64 arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTaskById), arg1);
				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTaskById), uri);
                return AsanaRequest.GetResponse<AsanaTask>(response, this);
			}
			

			public Task GetSubtasksInTask(AsanaTask arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetSubtasksInTask), arg1);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
			}
			public async Task<AsanaObjectCollection<AsanaTask>> GetSubtasksInTask(AsanaTask arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetSubtasksInTask), arg1);
                var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetSubtasksInTask), uri);
                return AsanaRequest.GetResponseCollection<AsanaTask>(response, this);
			}
			

			public Task GetStoriesInTask(AsanaTask arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetStoriesInTask), arg1);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaStory>(o, callback), ErrorCallback);
			}
			public async Task<AsanaObjectCollection<AsanaStory>> GetStoriesInTask(AsanaTask arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetStoriesInTask), arg1);
                var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetStoriesInTask), uri);
                return AsanaRequest.GetResponseCollection<AsanaStory>(response, this);
			}
			

			public Task GetProjectsOnATask(AsanaTask arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsOnATask), arg1);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaProject>(o, callback), ErrorCallback);
			}
			public async Task<AsanaObjectCollection<AsanaProject>> GetProjectsOnATask(AsanaTask arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetProjectsOnATask), arg1);
                var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetProjectsOnATask), uri);
                return AsanaRequest.GetResponseCollection<AsanaProject>(response, this);
			}
			

			public Task GetTasksByTag(AsanaTag arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksByTag), arg1);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
			}
			public async Task<AsanaObjectCollection<AsanaTask>> GetTasksByTag(AsanaTag arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTasksByTag), arg1);
                var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTasksByTag), uri);
                return AsanaRequest.GetResponseCollection<AsanaTask>(response, this);
			}
			

			public Task GetStoryById(Int64 arg1, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetStoryById), arg1);
				return request.Go((o, h) => PackAndSendResponse<AsanaStory>(o, callback), ErrorCallback);
			}
			public async Task<AsanaStory> GetStoryById(Int64 arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetStoryById), arg1);
				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetStoryById), uri);
                return AsanaRequest.GetResponse<AsanaStory>(response, this);
			}
			

			public Task GetProjectById(Int64 arg1, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectById), arg1);
				return request.Go((o, h) => PackAndSendResponse<AsanaProject>(o, callback), ErrorCallback);
			}
			public async Task<AsanaProject> GetProjectById(Int64 arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetProjectById), arg1);
				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetProjectById), uri);
                return AsanaRequest.GetResponse<AsanaProject>(response, this);
			}
			

			public Task GetTasksInAProject(AsanaProject arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksInAProject), arg1);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
			}
			public async Task<AsanaObjectCollection<AsanaTask>> GetTasksInAProject(AsanaProject arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTasksInAProject), arg1);
                var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTasksInAProject), uri);
                return AsanaRequest.GetResponseCollection<AsanaTask>(response, this);
			}
			

			public Task GetTagById(Int64 arg1, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTagById), arg1);
				return request.Go((o, h) => PackAndSendResponse<AsanaTag>(o, callback), ErrorCallback);
			}
			public async Task<AsanaTag> GetTagById(Int64 arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTagById), arg1);
				var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTagById), uri);
                return AsanaRequest.GetResponse<AsanaTag>(response, this);
			}
			

			public Task GetTeamsInWorkspace(AsanaWorkspace arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), arg1);
				return request.Go((o, h) => PackAndSendResponseCollection<AsanaTeam>(o, callback), ErrorCallback);
			}
			public async Task<AsanaObjectCollection<AsanaTeam>> GetTeamsInWorkspace(AsanaWorkspace arg1)
			{
				var uri = GetBaseUri(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), arg1);
                var response = await AsanaRequest.GoAsync(this, AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), uri);
                return AsanaRequest.GetResponseCollection<AsanaTeam>(response, this);
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

			public Task Get(Type AsanaT, AsanaCollectionResponseEventHandler callback)
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(AsanaT == typeof(AsanaUser))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUsers));
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaUser>(o, callback), ErrorCallback);
				}

								
				if(AsanaT == typeof(AsanaWorkspace))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetWorkspaces));
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaWorkspace>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + AsanaT.Name);
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

			public Task Get(Type AsanaT, AsanaResponseEventHandler callback)
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(AsanaT == typeof(AsanaUser))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetMe));
					return request.Go((o, h) => PackAndSendResponse<AsanaUser>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + AsanaT.Name);
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

			public Task Get(Type AsanaT, Int64 arg1, AsanaResponseEventHandler callback)
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(AsanaT == typeof(AsanaUser))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUserById), arg1);
					return request.Go((o, h) => PackAndSendResponse<AsanaUser>(o, callback), ErrorCallback);
				}

								
				if(AsanaT == typeof(AsanaWorkspace))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetWorkspaceById), arg1);
					return request.Go((o, h) => PackAndSendResponse<AsanaWorkspace>(o, callback), ErrorCallback);
				}

								
				if(AsanaT == typeof(AsanaTask))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTaskById), arg1);
					return request.Go((o, h) => PackAndSendResponse<AsanaTask>(o, callback), ErrorCallback);
				}

								
				if(AsanaT == typeof(AsanaStory))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetStoryById), arg1);
					return request.Go((o, h) => PackAndSendResponse<AsanaStory>(o, callback), ErrorCallback);
				}

								
				if(AsanaT == typeof(AsanaProject))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectById), arg1);
					return request.Go((o, h) => PackAndSendResponse<AsanaProject>(o, callback), ErrorCallback);
				}

								
				if(AsanaT == typeof(AsanaTag))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTagById), arg1);
					return request.Go((o, h) => PackAndSendResponse<AsanaTag>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + AsanaT.Name);
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

			public Task Get(Type AsanaT, AsanaWorkspace arg1, AsanaCollectionResponseEventHandler callback)
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(AsanaT == typeof(AsanaUser))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaUser>(o, callback), ErrorCallback);
				}

								
				if(AsanaT == typeof(AsanaProject))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaProject>(o, callback), ErrorCallback);
				}

								
				if(AsanaT == typeof(AsanaTag))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaTag>(o, callback), ErrorCallback);
				}

								
				if(AsanaT == typeof(AsanaTeam))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTeamsInWorkspace), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaTeam>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + AsanaT.Name);
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

			public Task Get(Type AsanaT, AsanaWorkspace arg1,  AsanaUser arg2, AsanaCollectionResponseEventHandler callback)
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(AsanaT == typeof(AsanaTask))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), arg1, arg2);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + AsanaT.Name);
			}

			public Task Get<AsanaT>(AsanaTask arg1, AsanaCollectionResponseEventHandler callback) where AsanaT : AsanaObject
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(typeof(AsanaT) == typeof(AsanaTask))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetSubtasksInTask), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
				}

								
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

			public Task Get(Type AsanaT, AsanaTask arg1, AsanaCollectionResponseEventHandler callback)
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(AsanaT == typeof(AsanaTask))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetSubtasksInTask), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
				}

								
				if(AsanaT == typeof(AsanaStory))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetStoriesInTask), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaStory>(o, callback), ErrorCallback);
				}

								
				if(AsanaT == typeof(AsanaProject))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsOnATask), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaProject>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + AsanaT.Name);
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

			public Task Get(Type AsanaT, AsanaTag arg1, AsanaCollectionResponseEventHandler callback)
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(AsanaT == typeof(AsanaTask))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksByTag), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + AsanaT.Name);
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

			public Task Get(Type AsanaT, AsanaProject arg1, AsanaCollectionResponseEventHandler callback)
			{
				AsanaRequest request = default(AsanaRequest);
								
				if(AsanaT == typeof(AsanaTask))
				{
					request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksInAProject), arg1);
					return request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
				}

								
                throw new TypeAccessException("Unknown type for this request: " + AsanaT.Name);
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
				Functions.Add(Function.GetSubtasksInTask, new AsanaFunction("/tasks/{0:ID}/subtasks", "GET"));
				Functions.Add(Function.GetStoriesInTask, new AsanaFunction("/tasks/{0:ID}/stories", "GET"));
				Functions.Add(Function.GetProjectsOnATask, new AsanaFunction("/tasks/{0:ID}/projects", "GET"));
				Functions.Add(Function.GetTasksByTag, new AsanaFunction("/tags/{0:ID}/tasks", "GET"));
				Functions.Add(Function.GetStoryById, new AsanaFunction("/stories/{0}", "GET"));
				Functions.Add(Function.GetProjectById, new AsanaFunction("/projects/{0}", "GET"));
				Functions.Add(Function.GetTasksInAProject, new AsanaFunction("/projects/{0:ID}/tasks", "GET"));
				Functions.Add(Function.GetTagById, new AsanaFunction("/tags/{0}", "GET"));
				Functions.Add(Function.GetTeamsInWorkspace, new AsanaFunction("/organizations/{0:ID}/teams", "GET"));
				Functions.Add(Function.CreateWorkspaceTask, new AsanaFunction("/tasks", "POST"));
				Functions.Add(Function.CreateTaskSubtask, new AsanaFunction("/tasks/{0:Parent}/subtasks", "POST"));
				Functions.Add(Function.SetParentTask, new AsanaFunction("/tasks/{0:ID}/setParent", "POST"));
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
		

				Associations.Add(typeof(AsanaWorkspace), new AsanaFunctionAssociation(null, GetFunction(Function.UpdateWorkspace), null));
				Associations.Add(typeof(AsanaTask), new AsanaFunctionAssociation(GetFunction(Function.CreateWorkspaceTask), GetFunction(Function.UpdateTask), GetFunction(Function.DeleteTask)));
				Associations.Add(typeof(AsanaSubtask), new AsanaFunctionAssociation(GetFunction(Function.CreateTaskSubtask), GetFunction(Function.UpdateTask), GetFunction(Function.DeleteTask)));
				Associations.Add(typeof(AsanaProject), new AsanaFunctionAssociation(GetFunction(Function.CreateWorkspaceProject), GetFunction(Function.UpdateProject), GetFunction(Function.DeleteProject)));
				Associations.Add(typeof(AsanaTag), new AsanaFunctionAssociation(GetFunction(Function.CreateWorkspaceTag), GetFunction(Function.UpdateTag), null));
				Associations.Add(typeof(AsanaStory), new AsanaFunctionAssociation(GetFunction(Function.AddStoryToTask), null, null));
		
			}
		}
}