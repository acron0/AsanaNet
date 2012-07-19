using System;
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
			GetTasksInAProject,
			GetTagById,
			CreateWorkspaceTask,
			AddProjectToTask,
			RemoveProjectFromTask,
			AddStoryToTask,
			UpdateTask,
			UpdateTag,
		}

		// Function definitions specifically for the GET functions.
		public partial class Asana
		{   
			public void GetUsers(AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUsers));
				request.Go((o, h) => PackAndSendResponseCollection<AsanaUser>(o, callback), ErrorCallback);
			}
			public void GetMe(AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetMe));
				request.Go((o, h) => PackAndSendResponse<AsanaUser>(o, callback), ErrorCallback);
			}
			public void GetUserById(Int64 arg1, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUserById), arg1);
				request.Go((o, h) => PackAndSendResponse<AsanaUser>(o, callback), ErrorCallback);
			}
			public void GetWorkspaces(AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetWorkspaces));
				request.Go((o, h) => PackAndSendResponseCollection<AsanaWorkspace>(o, callback), ErrorCallback);
			}
			public void GetUsersInWorkspace(AsanaWorkspace arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetUsersInWorkspace), arg1);
				request.Go((o, h) => PackAndSendResponseCollection<AsanaUser>(o, callback), ErrorCallback);
			}
			public void GetTasksInWorkspace(AsanaWorkspace arg1,  AsanaUser arg2, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksInWorkspace), arg1, arg2);
				request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
			}
			public void GetProjectsInWorkspace(AsanaWorkspace arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsInWorkspace), arg1);
				request.Go((o, h) => PackAndSendResponseCollection<AsanaProject>(o, callback), ErrorCallback);
			}
			public void GetTagsInWorkspace(AsanaWorkspace arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTagsInWorkspace), arg1);
				request.Go((o, h) => PackAndSendResponseCollection<AsanaTag>(o, callback), ErrorCallback);
			}
			public void GetTaskById(Int64 arg1, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTaskById), arg1);
				request.Go((o, h) => PackAndSendResponse<AsanaTask>(o, callback), ErrorCallback);
			}
			public void GetStoriesInTask(AsanaTask arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetStoriesInTask), arg1);
				request.Go((o, h) => PackAndSendResponseCollection<AsanaStory>(o, callback), ErrorCallback);
			}
			public void GetProjectsOnATask(AsanaTask arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectsOnATask), arg1);
				request.Go((o, h) => PackAndSendResponseCollection<AsanaProject>(o, callback), ErrorCallback);
			}
			public void GetTasksByTag(AsanaTask arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksByTag), arg1);
				request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
			}
			public void GetStoryById(Int64 arg1, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetStoryById), arg1);
				request.Go((o, h) => PackAndSendResponse<AsanaStory>(o, callback), ErrorCallback);
			}
			public void GetProjectById(Int64 arg1, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetProjectById), arg1);
				request.Go((o, h) => PackAndSendResponse<AsanaProject>(o, callback), ErrorCallback);
			}
			public void GetTasksInAProject(AsanaProject arg1, AsanaCollectionResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTasksInAProject), arg1);
				request.Go((o, h) => PackAndSendResponseCollection<AsanaTask>(o, callback), ErrorCallback);
			}
			public void GetTagById(Int64 arg1, AsanaResponseEventHandler callback)
			{
				var request = GetBaseRequest(AsanaFunction.GetFunction(Function.GetTagById), arg1);
				request.Go((o, h) => PackAndSendResponse<AsanaTag>(o, callback), ErrorCallback);
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
				Functions.Add(Function.GetTasksInAProject, new AsanaFunction("/projects/{0:ID}/tasks", "GET"));
				Functions.Add(Function.GetTagById, new AsanaFunction("/tags/{0}", "GET"));
				Functions.Add(Function.CreateWorkspaceTask, new AsanaFunction("/tasks", "POST"));
				Functions.Add(Function.AddProjectToTask, new AsanaFunction("/tasks/{0:ID}/addProject", "POST"));
				Functions.Add(Function.RemoveProjectFromTask, new AsanaFunction("/tasks/{0:ID}/removeProject", "POST"));
				Functions.Add(Function.AddStoryToTask, new AsanaFunction("/tasks/{0:Target}/stories", "POST"));
				Functions.Add(Function.UpdateTask, new AsanaFunction("/tasks/{0:ID}", "PUT"));
				Functions.Add(Function.UpdateTag, new AsanaFunction("/tags/{0:ID}", "PUT"));
		

				Associations.Add(typeof(AsanaTask), new AsanaFunctionAssociation(GetFunction(Function.CreateWorkspaceTask), GetFunction(Function.UpdateTask)));
				Associations.Add(typeof(AsanaStory), new AsanaFunctionAssociation(GetFunction(Function.AddStoryToTask), null));
		
			}
		}
}