using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AsanaNet;

namespace AsanaNetTests
{
    class Program
    {
        private static string YOUR_API_KEY  = @"0";
        private static Int64 YOUR_WORKSPACE   = 0; // TARGET_TEST
        private static Asana Asana;
        static async Task TestGetMe()
        {
            var me = await Asana.GetMe();

            Console.WriteLine(me.Name);
            Console.ReadLine();
        }

        private static async Task TestCreateTask(AsanaWorkspace workspace)
        {
            var taskParent = new AsanaTask(workspace) { Name = "Parent Test" };
            taskParent = await taskParent.Save(Asana);

            Console.WriteLine(taskParent.Name);
            Console.ReadLine();

            var taskChild = new AsanaSubtask(workspace, taskParent) { Name = "Subtask Test" };
            taskChild = await taskChild.Save(Asana);

            var test = taskChild as AsanaTask;

            Console.WriteLine(taskChild.Name);
            Console.ReadLine();

            // clean-up
            await taskChild.Delete(Asana);
            await taskParent.Delete(Asana);
            Console.WriteLine("Tasks deleted.");
            Console.ReadLine();
        }

        private static async Task TestGetProjects(AsanaWorkspace workspace)
        {
            var projects = await Asana.GetProjectsInWorkspace(workspace);

            foreach (var project in projects)
            {
                Console.WriteLine("{1}: {0}", project.Name, project.Team);
            }
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Asana = new Asana(YOUR_API_KEY, AuthenticationType.Basic, ErrorCallback);

            var workspace = Asana.GetWorkspaceById(YOUR_WORKSPACE).Result;
            Console.WriteLine(workspace.Name);

            TestGetMe().Wait();
            TestCreateTask(workspace).Wait();
            TestGetProjects(workspace).Wait();
        }
        private static void ErrorCallback(string s1, string s2, string s3)
        {
            Console.WriteLine("Error: ");
            Console.WriteLine(s1);
            Console.WriteLine(s2);
            Console.WriteLine(s3);
        }
    }
}
