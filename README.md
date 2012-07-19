# AsanaNet
A .NET implementation of the Asana REST API
http://developer.asana.com/

##Using AsanaNet
To use AsanaNet, start by creating an instance of the Asana service. You can find your API key [here](http://app.asana.com/-/account_api).
<code>
     var asana = new Asana(YOUR_API_KEY, errorCallback);
</code>

All 'Get' requests are asynchronous and so must be accompanied by a callback.
For example, to get the current user's information:
<code>
    asana.GetMe(o =>
    {
            var user = o as AsanaUser;
            Console.WriteLine("Hello, " + u.Name);
    });
</code>

To get a list of workspaces the current user has access to:

<code>
asana.GetWorkspaces(o =>
    {
        foreach (AsanaWorkspace workspace in o)
        {
            Console.WriteLine("Workspace: " + workspace.Name);
        }
    });
</code>

To create a new task:

<code>
AsanaTask newTask1 = new AsanaTask(workspace);
newTask1.Name = "Pick up the milk!";
newTask1.Notes = "Proper semi-skimmed milk. None of that UHT rubbish.";
newTask1.Assignee = me;
newTask1.DueOn = DateTime.Now.AddHours(2);
newTask1.Save(asana);
</code>

###Work is on-going! Please contribute!