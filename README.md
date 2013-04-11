# Hero <a href="http://skookum.cloudapp.net/viewType.html?buildTypeId=bt8&guest=1"><img src="http://skookum.cloudapp.net/app/rest/builds/buildType:(id:bt8)/statusIcon"/></a>

## Server Side
Hero is an ability based authorization for .NET MVC and WepAPI projects inspired by the Rails project [CanCan](https://github.com/ryanb/cancan) and by [Derek Bailey](http://lostechies.com/derickbailey/2011/05/24/dont-do-role-based-authorization-checks-do-activity-based-checks/).  Ability based authorization provides a more flexible approach to the traditional .NET authorization technique by decoupling the permissions and code.  By associating a role to a method or action in .NET (through typical Authorization attribute) you are creating a tight coupling between a role and an action.  However, what happens when your role name changes?  Now you have to update all code that references that role name.  By assigning ability names to an action or method you abstract the functionality and the association can be done programmatically.  This allows for a loose coupling of functionality to roles and users.

## Client Side
In addition to a server side component, Hero has a Javascript component as well.  The client side version of Hero allows for the injection of security authorization into javascript functions through AOP techniques.


# Installation

You can install this module via [NuGet](http://www.nuget.org). Currently what exists in master is automatically pushed out as a NuGet package. Once Hero becomes more stable, this will change.

# Roadmap

1. The next step is to define an administration screen that can be easily integrated in an application.
2. Allow for hierarchical abilities to make naming and maintenance easier.
3. Create a provider to allow Hero to pull user and roles from .NET's built in user and role repositories.
3. Have a mechanism for allowing auto creation of abilities (for example in a simple CRUD application).
4. Look for methods of hooking the server side registrations to the client side automatically.  This may be through naming conventions or making the function registrations go through the server.

# Examples

You can see a fully implemented example in the Samples project in the repository.  For quick help on the server side implementation or client side, see below:

##Server Side
The steps to create user, roles, and register abilities are extremely simple and easy to configure.  This code will typically be performed in the Global.asax or similiar application startup code.

The first step is to initialize the authorization services with Hero.

````csharp
IAbilityAuthorizationService service = new AbilityAuthorizationService();
HeroConfig.Initialize(service);
````

Once you have registered a service with Hero the next step is to register a user or role with an Ability.  The following code will register a role labeled BasicRole with an Ability named View.

````csharp 
HeroConfig.RegisterAbilities(service, new Role("BasicRole"), new Ability("View"));
````

If you want to register a specific user, there is a corresponding registration function for this as well.

````csharp
HeroConfig.RegisterAbilities(service, new User("John Doe"), new Ability("View"));
````

Once you have created the service and registered your roles/users with their abilitites you need to associate an action or method with an ability.  This can be performed through the attributes provided in the Hero.Attributes project.  Hero provides an attribute to be utilized in an ASP.NET MVC or WebAPI project.  Abilities can be registered at the controller or action level.  Your more restrictive abilities should be registered at the action level, while the less restrictive should be applied at the controller level.  In the following example, the View ability is the least restrictive, and Create, Edit, and Delete are at the action level.  The view actions (Index and Details) inherit their abilities from the controller level.

The corresponding Attribute for WebAPI projects is AbilityWebApiAuthorization.

````csharp
[AbilityMvcAuthorization(Ability = "View")]
public class ToDoController : Controller
{
    private ToDoDbContext db = new ToDoDbContext();

    //
    // GET: /ToDo/

    public ActionResult Index()
    {
        return View(db.Items.ToList());
    }

    //
    // GET: /ToDo/Details/5

    public ActionResult Details(int id = 0)
    {
        ToDo todo = db.Items.Find(id);
        if (todo == null)
        {
            return HttpNotFound();
        }
        return View(todo);
    }

    //
    // GET: /ToDo/Create

    [AbilityMvcAuthorization(Ability = "Create")]
    public ActionResult Create()
    {
        return View();
    }

    //
    // POST: /ToDo/Create

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AbilityMvcAuthorization(Ability = "Create")]
    public ActionResult Create(ToDo todo)
    {
        if (ModelState.IsValid)
        {
            db.Items.Add(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(todo);
    }

    //
    // GET: /ToDo/Edit/5
    [AbilityMvcAuthorization(Ability = "Edit")]
    public ActionResult Edit(int id = 0)
    {
        ToDo todo = db.Items.Find(id);
        if (todo == null)
        {
            return HttpNotFound();
        }
        return View(todo);
    }

    //
    // POST: /ToDo/Edit/5

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AbilityMvcAuthorization(Ability = "Edit")]
    public ActionResult Edit(ToDo todo)
    {
        if (ModelState.IsValid)
        {
            db.Entry(todo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(todo);
    }

    //
    // GET: /ToDo/Delete/5
    [AbilityMvcAuthorization(Ability = "Delete")]
    public ActionResult Delete(int id = 0)
    {
        ToDo todo = db.Items.Find(id);
        if (todo == null)
        {
            return HttpNotFound();
        }
        return View(todo);
    }

    //
    // POST: /ToDo/Delete/5

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [AbilityMvcAuthorization(Ability = "Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        ToDo todo = db.Items.Find(id);
        db.Items.Remove(todo);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
        db.Dispose();
        base.Dispose(disposing);
    }
}
````

This is all it takes to configure you ability based authorization system on the server side.  You can also leverage your registered abilities on the client side as well.  See below for an examples.

##Ability Groups
  Another feature Hero provies is the grouping of abilities.  Grouping allows for easier registration of abilities.  The example below creates an ability group called Manage that contains the Edit, Create, and Delete abilities.  When the manage ability is registered with AdminRole, the Edit, Create, and Delete abilities are registered.

````csharp
IRole toDoAdminRole = new Role("ToDoAdmin");
Ability toDoCreateAbility = new Ability("Create");
Ability toDoDeleteAbility = new Ability("Delete");
Ability toDoEditAbility = new Ability("Edit");
Ability manageAbility = new Ability("Manage", new[] { toDoCreateAbility, toDoEditAbility, toDoDeleteAbility, toDoViewAbility });
HeroConfig.RegisterAbilities(service, toDoAdminRole, manageAbility);
````

##Client Side
Generally speaking providing authorization at the server level is not the entire story.  If a user does not have permission to perform an action, the trigger (button, link, visual container) would not even be visible to a user.  Hero provides a client side implementation to help keep your client ability triggers in sync with the server.

The following code will create a simple javascript module for managing these triggers.  The module has functions for showing the create, edit, delete, and details button utilized in the ToDo HD application.  By default these buttons are hidden and only if the user is authorized will the module functions run.

````javascript
//Add a module for the buttons on the page.
(function (testModule) {
    var getCreateButton = function() { return document.getElementsByClassName("createButton"); };
    var getDeleteButton = function () { return document.getElementsByClassName("deleteButton"); };
    var getEditButton = function () { return document.getElementsByClassName("editButton"); };
    var getDetailsButton = function () { return document.getElementsByClassName("detailsButton"); };

    var show = function(elem) {
        if (elem && elem[0]) {
            elem[0].style.display = 'inline';      
        }
    };
    
    testModule.showCreateButton = function showCreateButton() {
        show(getCreateButton());
    };
    
    testModule.showDeleteButton = function showDeleteButton() {
        show(getDeleteButton());
    };
    
    testModule.showEditButton = function showEditButton() {
        show(getEditButton());
    };
    
    testModule.showDetailsButton = function showDetailsButton() {
        show(getDetailsButton());
    };
    
})(window.TestModule = window.TestModule || {});

//on doc ready show/hide the buttons.
$(document).ready(function () {
    TestModule.showCreateButton();
    TestModule.showDeleteButton();
    TestModule.showEditButton();
    TestModule.showDetailsButton();
});


//initialize the ability based authorization utilizing the Hero project
Hero
    .configure({ endpoint: "http://localhost:54573/Abilities/" })
    .registerAbility(Hero.Ability("View"), TestModule, TestModule.showDetailsButton)
    .registerAbility(Hero.Ability("Create"), TestModule, TestModule.showCreateButton)
    .registerAbility(Hero.Ability("Edit"), TestModule, TestModule.showEditButton)
    .registerAbility(Hero.Ability("Delete"), TestModule, TestModule.showDeleteButton);
````

The configuration code provides a fluent syntax for registering a module's public functions with an Ability.  In the previous example, if the user has the Create ability then the showCreateButton will be allowed to run (and thus the button will become visible).  One point to note here is the endpoint.  Hero installs an Abilities controller by default and your Hero javascript configuration will need to point to this controller.

# Contributing

We are very much interested in helping expand the .NET open source community. Please feel free to fork and submit pull requests.

Here's a quick guide:

1. Fork the repo.
2. Run the tests. We only take pull requests with passing tests, and it's great to know that you have a clean slate.
3. Add a test for your change. Only refactoring and documentation changes require no new tests. If you are adding functionality or fixing a bug, we need a test!
4. Make the test pass.
5. Push to your fork and submit a pull request.

At this point you're waiting on us. We like to at least comment on, if not accept, pull requests within three business days (and, typically, one business day). We may suggest some changes or improvements or alternatives.

In terms of syntax please try and follow the conventions that can be seen in the current code base.

You can contribute in a number of ways including:

1. Feature enhancements
2. Writing more unit tests
3. Updating documentation
4. Submitting bug fixes
5. Submitting issues (we expect issue reports through Github issues)

# License

This project is licensed under the [MIT License](http://opensource.org/licenses/MIT)

