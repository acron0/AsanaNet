using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AsanaNet.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteParallelAsync().Wait();
//            ExecuteAsync().Wait();
//            ExecuteWithTasks();
        }
        
        /// <summary>
        /// New API format
        /// </summary>
        /// <returns></returns>
        private static async Task ExecuteAsync()
        {
            // CONFIGURE YOUR ASANA API TOKEN IN APPSETTINGS.CONFIG FILE
            var startTime = DateTime.Now;
            Console.WriteLine("# Asana - Async Method #");
            var apiToken = GetApiToken();
            var asana = new Asana(apiToken, AuthenticationType.Basic, errorCallback);

            var me = await asana.GetMeAsync();
            Console.WriteLine("Hello, " + me.Name);

            var workspaces = await asana.GetWorkspacesAsync();                       
            foreach (var workspace in workspaces)
            {
                Console.WriteLine("Workspace: " + workspace.Name);
                
                var teams = await asana.GetTeamsInWorkspaceAsync(workspace);                
                foreach (var team in teams)
                {
//                    if (team.Name != "Projetos Especiais")
//                        continue;

                    Console.WriteLine("  Team: " + team.Name);
                    
                    // Projects
                    var projects = await asana.GetProjectsInTeamAsync(team);                    
                    foreach (AsanaProject project in projects)
                    {
                        Console.WriteLine("    Project: " + project.Name);

                        var tasks = await asana.GetTasksInAProjectAsync(project);                        
                        foreach (AsanaTask task in tasks)
                            Console.WriteLine("      Task: " + task.Name);
                    }                    
                }
            }


            Console.WriteLine();
            Console.WriteLine("Execution time " + (DateTime.Now - startTime));
            Console.ReadLine();
        }
        
        /// <summary>
        /// New API format - Parallel execution
        /// </summary>
        /// <returns></returns>
        private static async Task ExecuteParallelAsync()
        {                        
            // CONFIGURE YOUR ASANA API TOKEN IN APPSETTINGS.CONFIG FILE
            var startTime = DateTime.Now;
            Console.WriteLine("# Asana - Async Method #");
            var apiToken = GetApiToken();
            var asana = new Asana(apiToken, AuthenticationType.Basic, errorCallback);

            // Parallel tasks
            var meTask = asana.GetMeAsync();
            
            var workspaces = await asana.GetWorkspacesAsync();
//            var workspacesConcurrentList = new ConcurrentQueue<AsanaWorkspace>(workspaces);
            var workspaceTasks = workspaces.Select(async workspace =>
            {
                var workSpaceInfo = new HierarchicalParallelExecutionData
                {
                    Info = "Workspace: " + workspace.Name,
                    Object = workspace
                };
                
                // Teams
                var teams = await asana.GetTeamsInWorkspaceAsync(workspace);
//                var teamsConcurrentList = new ConcurrentQueue<AsanaTeam>(teams);
                var teamTasks = teams.Select(async team =>
                {
//                    if (team.Name != "Projetos Especiais")
//                        return;
                    
                    var teamInfo = new HierarchicalParallelExecutionData
                    {
                        Info = "  Team: " + team.Name,
                        Object = team
                    };
                    workSpaceInfo.Items.Add(teamInfo);
                    
                    // Projects
                    var projects = await asana.GetProjectsInTeamAsync(team);
//                    var projectsConcurrentList = new ConcurrentQueue<AsanaProject>(projects);
                    var projectTasks = projects.Select(async project =>
                    {
                        var projectInfo = new HierarchicalParallelExecutionData
                        {
                            Info = "    Project: " + project.Name,
                            Object = team
                        };
                        teamInfo.Items.Add(projectInfo);
                        
                        // Taks
                        var tasks = await asana.GetTasksInAProjectAsync(project);                        
                        foreach (var task in tasks)
                        {
                            var taskInfo = new HierarchicalParallelExecutionData
                            {
                                Info = "      Task: " + task.Name,
                                Object = team
                            };
                            projectInfo.Items.Add(taskInfo);
                        }                        

                    });                    
                    await Task.WhenAll(projectTasks);
                });
                await Task.WhenAll(teamTasks);
                
                return workSpaceInfo;
            });
            var hierarchicalCall = await Task.WhenAll(workspaceTasks);                        
            
            var me = meTask.Result;
            Console.WriteLine("Hello, " + me.Name);

            foreach (var item in hierarchicalCall)
                item.WriteToConsole();
           
            Console.WriteLine();
            Console.WriteLine("Execution time " + (DateTime.Now - startTime));
            Console.ReadLine();
        }
        
        
        /// <summary>
        /// Old API format
        /// </summary>
        /// <returns></returns>
        private static void ExecuteWithTasks()
        {
            // CONFIGURE YOUR ASANA API TOKEN IN APPSETTINGS.CONFIG FILE
            var startTime = DateTime.Now;
            Console.WriteLine("# Asana - Task Method #");
            var apiToken = GetApiToken();
            var asana = new Asana(apiToken, AuthenticationType.Basic, errorCallback);

            asana.GetMe(response =>
            {
                var me = (AsanaUser) response;
                Console.WriteLine("Hello, " + me.Name);
            }).Wait();

            asana.GetWorkspaces(o =>
            {
                foreach (AsanaWorkspace workspace in o)
                {
                    Console.WriteLine("Workspace: " + workspace.Name);

                    // Times
                    asana.GetTeamsInWorkspace(workspace, teams =>
                    {
                        foreach (AsanaTeam team in teams)
                        {
//                            if (team.Name != "Projetos Especiais")
//                                continue;

                            Console.WriteLine("  Team: " + team.Name);

                            // Projetos
                            asana.GetProjectsInTeam(team, projects =>
                            {
                                foreach (AsanaProject project in projects)
                                {
                                    Console.WriteLine("    Project: " + project.Name);

                                    asana.GetTasksInAProject(project, tasks =>
                                    {
                                        foreach (AsanaTask task in tasks)
                                        {
                                            Console.WriteLine("      Task: " + task.Name);
                                        }
                                    }).Wait();
                                }
                            }).Wait();
                        }
                    }).Wait();
                }
            }).Wait();


            Console.WriteLine();
            Console.WriteLine("Execution time " + (DateTime.Now - startTime));
            Console.ReadLine();
        }

        private static void errorCallback(string arg1, string arg2, string arg3)
        {            
        }
        
        private static string GetApiToken()
        {
            var configs = new Microsoft.Extensions.Configuration.ConfigurationBuilder()                
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return configs.GetSection("ApiToken").Value;
        }
    }
}