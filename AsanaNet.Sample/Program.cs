using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsanaNet.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute().Wait();
        }

        private static async Task Execute()
        {
            Console.WriteLine("# Asana Sync #");
            var asana = new Asana(@"YOUR_API_TOKEN", AuthenticationType.Basic, errorCallback);

            var me = await asana.GetMeAsync();
            Console.WriteLine("Hello, " + me.Name);

            asana.GetWorkspaces(o =>
            {
                foreach (AsanaWorkspace workspace in o)
                {
                    Console.WriteLine("Workspace: " + workspace.Name);

                    var asanaProjects = new List<AsanaProject>();
                    asana.GetProjectsInWorkspace(workspace, projects =>
                    {
                        foreach (AsanaProject project in projects)
                            asanaProjects.Add(project);
                    }).Wait();

                    var projectTarefas = asanaProjects.FirstOrDefault(p =>
                        p.Name.Equals("Tarefas", StringComparison.InvariantCultureIgnoreCase));

                    // Times
                    asana.GetTeamsInWorkspace(workspace, teams =>
                    {
                        foreach (AsanaTeam team in teams)
                        {
                            if (team.Name != "Meio Homologado")
                                continue;

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
            Console.WriteLine("Fim");
            Console.ReadLine();
        }

        private static void errorCallback(string arg1, string arg2, string arg3)
        {            
        }
    }
}