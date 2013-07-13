# Hero <a href="http://skookum.cloudapp.net/viewType.html?buildTypeId=bt8&guest=1"><img src="http://skookum.cloudapp.net/app/rest/builds/buildType:(id:bt8)/statusIcon"/></a>

## Server Side
Hero is an ability based authorization for .NET MVC and WepAPI projects inspired by the Rails project [CanCan](https://github.com/ryanb/cancan) and by [Derick Bailey](http://lostechies.com/derickbailey/2011/05/24/dont-do-role-based-authorization-checks-do-activity-based-checks/).  Ability based authorization provides a more flexible approach to the traditional .NET authorization technique by decoupling the permissions and code.  By associating a role to a method or action in .NET (through typical Authorization attribute) you are creating a tight coupling between a role and an action.  However, what happens when your role name changes?  Now you have to update all code that references that role name.  By assigning ability names to an action or method you abstract the functionality and the association can be done programmatically.  This allows for a loose coupling of functionality to roles and users.

## Client Side
In addition to a server side component, Hero has a Javascript component as well.  The client side version of Hero allows for the injection of security authorization into javascript functions through AOP techniques.

## HeroAdmin

HeroAdmin is a client side application that is available to any Hero install by default. It allows users, roles, and abilities to be managed via a web interface. By default it uses an in memory repository for persistence, but can be configured to use any persistence layer you like.

# Installation

You can install this module via [NuGet](http://www.nuget.org). Currently what exists in master is automatically pushed out as a NuGet package. Once Hero becomes more stable, this will change.  Hero can be installed via the following command:

````
Install-Package Hero
````

# Dependencies

Hero currently depends on the following (these are installed automatically if you install via NuGet):

+ [angular.js](http://angularjs.org/)
+ [dotnetrepositories](https://github.com/Skookum/dotnetrepositories)
+ [dotnetstandard](https://github.com/Skookum/dotnetstandard)
+ [Twitter Bootstrap](http://twitter.github.io/bootstrap/) (for HeroAdmin)

# Roadmap

1. Create a provider/adapter to allow Hero to pull user and roles from .NET's built in user and role repositories.
2. Make it easier to create user/role/ability combos in the Global.asax.cs
3. Have a mechanism for allowing auto creation of abilities (for example in a simple CRUD application).
4. Look for methods of hooking the server side registrations to the client side automatically.  This may be through naming conventions or making the function registrations go through the server.

# Examples

You can see a fully implemented example at the [HeroGame](https://github.com/Skookum/HeroGame) project.  For quick help on the server side implementation or client side, see below:


##Server Side
The steps to create user, roles, and register abilities are extremely simple and easy to configure.  This code will typically be performed in the Global.asax or similiar application startup code.

The first step is to initialize the authorization services with Hero.

````csharp
IAbilityAuthorizationService service = new AbilityAuthorizationService();
HeroConfig.Initialize(service);
````

Once you have registered a service with Hero the next step is to create an ability.  The following code will create an ability named View.

````csharp 
Ability viewAbility = new Ability("View");
service.AddAbility(viewAbility);
````

Once you have added abilities you can then add roles into the system.

````csharp 
Role basicRole = new Role("Basic");
basicRole.Abilities.Add(viewAbility);
service.AddRole(basicRole);
````

Finally you can add users into the system. Note you can only grant users abilities by adding roles that contain those abilities. You should not add abilities to users directly.

````csharp 
User basicUser = new User("BasicUser");
basicUser.Roles.Add(basicRole);
service.AddUser(basicUser);
````

Once you have created the service and added your roles/users with their abilitites you need to associate an action or method with an ability.  This can be performed through the attributes provided in the Hero.Attributes project.  Hero provides an attribute to be utilized in an ASP.NET MVC or WebAPI project.  Abilities can be registered at the controller or action level.  Your more restrictive abilities should be registered at the action level, while the less restrictive should be applied at the controller level.  In the following example, the View ability is the least restrictive, and Create, Edit, and Delete are at the action level.  The view actions (Index and Details) inherit their abilities from the controller level.

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

Finally you can leverage perform authorization checks in your Razor views

````csharp
<p>
    @if (HeroConfig.Can(User.Identity.Name, "Create"))
    {
        @Html.ActionLink("Create New", "Create", null, new {@class = "createButton"})
    }
</p>
````

This is all it takes to configure you ability based authorization system on the server side. Now your code and your authorization are decoupled. This means you can add new roles, remove roles, rename roles, etc. all from an admin panel instead of redeploying an application.  You can also leverage your registered abilities on the client side as well.  See below for an examples.

## Complex Abilities
  Another feature Hero provides is an ability hierarchy. This makes Hero more flexible and easier to register users with their appropriate abilities.  The example below creates an ability hierarchy called Manage that contains the Edit, Create, and Delete abilities.  When the manage ability is registered with AdminRole, the Edit, Create, and Delete abilities are also available to the AdminRole.

````csharp
Role toDoAdminRole = new Role("ToDoAdmin");
Role toDoEditRole = new Role("ToDoEdit");

Ability toDoCreateAbility = new Ability("Create");
Ability toDoDeleteAbility = new Ability("Delete");
Ability toDoEditContentAbility = new Ability("EditContent");
Ability toDoEditDataAbility = new Ability("EditData");

Ability toDoEditAbility = new Ability("Edit");
todoEditAbility.Add(toDoEditContentAbility);
todoEditAbility.Add(toDoEditDataAbility);

Ability manageAbility = new Ability("Manage");
manageAbility.Abilities.Add(toDoCreateAbility);
manageAbility.Abilities.Add(toDoEditAbility);
manageAbility.Abilities.Add(toDoDeleteAbility);

toDoAdminRole.Abilities.Add(manageAbility);
toDoEditRole.Abilities.Add(toDoEditAbility);
````

##Client Side
Generally speaking providing authorization at the server level is not the entire story.  If a user does not have permission to perform an action, the trigger (button, link, visual container) would not even be visible to a user.  Hero provides a client side implementation to help keep your client ability triggers in sync with the server and provies support for Single Page Applications (SPA).

The following code will create a simple javascript module for managing these triggers.  The module has functions for showing the create, edit, delete, and details button utilized in the ToDo HD application.  By default these buttons are hidden and only if the user is authorized will the module functions run.

````javascript
//Add a module for the buttons on the page.
(function (testModule) {
    var getCreateButton = function() { return document.getElementsByClassName("createButton"); };
    var getDeleteButton = function () { return document.getElementsByClassName("deleteButton"); };
    var getEditButton = function () { return document.getElementsByClassName("editButton"); };
    var getDetailsButton = function () { return document.getElementsByClassName("detailsButton"); };

    var show = function (elem) {
        if (elem) {
            for (var i = 0; i < elem.length; i++) {
                if (elem[i]) elem[i].style.display = 'inline';
            }
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
Hero.init()
    .registerAbility("View", TestModule, TestModule.prototype.showDetailsButton)
    .registerAbility("Create", TestModule, TestModule.prototype.showCreateButton)
    .registerAbility("Edit", TestModule, TestModule.prototype.showEditButton)
    .registerAbility("Delete", TestModule, TestModule.prototype.showDeleteButton);
````

The configuration code provides a fluent syntax for registering a module's public functions with an Ability.  In the previous example, if the user has the Create ability then the showCreateButton will be allowed to run (and thus the button will become visible).  One point to note here is the endpoint.  Hero installs an Abilities controller by default and your Hero javascript configuration will need to point to this controller.

## HeroAdmin Usage

Hero also include HeroAdmin. HeroAdmin is an SPA that was built to ease the management task of creating abilities, roles, and users. If you wish to use HeroAdmin in your application you will need to add the following to your BundleConfig.cs

````csharp
bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));
bundles.Add(new ScriptBundle("~/bundles/herojs").Include(
            "~/Scripts/angular.js",
            "~/Scripts/angular-resource.js",
            "~/Scripts/lodash.js",
            "~/Scripts/restangular.js",
            "~/Scripts/hero.js",
            "~/Scripts/hero-user.js",
            "~/Scripts/hero-role.js",
            "~/Scripts/hero-ability.js",
            "~/Scripts/hero-authorization.js"));
````

Add the following to the page you wish the HeroAdmin console to appear. Typically you can create an Admin MVC controller/view to support this (see HeroGame for example):

````html
@Scripts.Render("~/bundles/herojs")
@Scripts.Render("~/Scripts/init.js")

<div id="admin-console"></div>
````

Finally create an init.js (or similar):

````javascript
(function () {
  Hero.init().buildConsole();
})();
````
You should then see the HeroAdmin console.
![HeroAdmin Console](http://i.snag.gy/qqLp1.jpg "HeroAdmin")


# Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md)

# License

This project is licensed under the [MIT License](http://opensource.org/licenses/MIT)

