# AsanaNet
A .NET implementation of the Asana REST API
http://developer.asana.com/

This open-source project uses the MIT license.

##Using AsanaNet
To use AsanaNet, start by creating an instance of the Asana service. You can find your API key [here](http://app.asana.com/-/account_api).

     var asana = new Asana(YOUR_API_KEY, AuthenticationType.Basic, errorCallback);

All 'Get' requests are asynchronous and can be used in async methods.
To use them in non-async methods, add .Wait(); to the call.

For example, to get the current user's information:

    var me = await asana.GetMe;
    Console.WriteLine("Hello, " + me.Name);

To get a list of workspaces the current user has access to:

    var workspaces = await asana.GetWorkspaces();

    foreach (var workspace in workspaces)
    {
        Console.WriteLine("Workspace: " + workspace.Name);
    }

To create a new task:

    AsanaTask newTask1 = new AsanaTask(workspace);
    newTask1.Name = "Pick up the milk!";
    newTask1.Notes = "Proper semi-skimmed milk. None of that UHT rubbish.";
    newTask1.Assignee = me;
    newTask1.DueOn = DateTime.Now.AddHours(24);
    await newTask1.Save(asana);

For more examples take a look at the AsanaNetTests project.

###Error callback method
The error callback method may be in the following form:

    static void errorCallback(string s1, string s2, string s3)
    {

    }

If you don't want handling anything, you can just pass an empty lambda into the constructor:

    _asana = new Asana(_apiKey, AuthenticationType.Basic, (s1, s2, s3) => {});

For backwards compatibility, callback methods are still supported.

###Work is on-going! Please contribute!
