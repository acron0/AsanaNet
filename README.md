# AsanaNet
A .NET implementation of the Asana REST API
http://developer.asana.com/

This open-source project uses the MIT license.

##Using AsanaNet
To use AsanaNet, start by creating an instance of the Asana service. You can find your API key [here](http://app.asana.com/-/account_api).


     var asana = new Asana(YOUR_API_KEY, AuthenticationType.Basic, errorCallback);

All 'Get' requests are asynchronous and so must be accompanied by a callback.
For example, to get the current user's information:

    asana.GetMe(o =>
    {
            var user = o as AsanaUser;
            Console.WriteLine("Hello, " + user.Name);
    });


It is also possible to make them synchronize, by adding ```.Wait();```.

Additionally, since the methods always return a task, you can ```await``` them within an ```async``` method, or another task.

    asana.GetMe(o =>
    {
            var user = o as AsanaUser;
            Console.WriteLine("Hello, " + user.Name);
    }).Wait();

To get a list of workspaces the current user has access to:

    asana.GetWorkspaces(o =>
    {
        foreach (AsanaWorkspace workspace in o)
        {
            Console.WriteLine("Workspace: " + workspace.Name);
        }
    });

To create a new task:

    AsanaTask newTask1 = new AsanaTask(workspace);
    newTask1.Name = "Pick up the milk!";
    newTask1.Notes = "Proper semi-skimmed milk. None of that UHT rubbish.";
    newTask1.Assignee = me;
    newTask1.DueOn = DateTime.Now.AddHours(2);
    newTask1.Save(asana);

###Error callback method
The error callback method may be in the following form:

    static void errorCallback(string s1, string s2, string s3)
    {

    }

Also, if you don't want handling anything, you can just pass an empty lambda into the constructor:

    _asana = new Asana(_apiKey, AuthenticationType.Basic, (s1, s2, s3) => {});

###Work is on-going! Please contribute!
