# Hero

Ability based authorization for .Net projects. It will generate a Nuget package that can be downloaded as a 3rd party dll via Visual Studio.

# Status

<a href="http://skookum.cloudapp.net/viewType.html?buildTypeId=bt8&guest=1">
    <img src="http://skookum.cloudapp.net/app/rest/builds/buildType:(id:bt8)/statusIcon"/>
</a>

# Installation

You can install this module via Nuget. The Nuget package is available from the [Skookum Nuget Server](http://skookum.cloudapp.net/guestAuth/app/nuget/v1/FeedService.svc/)

# License

This project is licensed under the [MIT License](http://opensource.org/licenses/MIT)

# TODO

## Infrastructure
+ The developer may also want to define an role => ability relationship via code. 
    - This should be done in a centralized configuration section...Done
    - This would be the default configuration essentially...Done
    - Need to setup Nuget to generate this file and give instructions on how to use it or inject in the correct place depending on the project...Done
    - Defining roles would require its own permissions (Admin)...Done
    - Need to give them a place to put their own config without overwriting it on update...Done
+ Create attributes for methods and classes. This will be how developers can set authorization to code based on roles.
    - Need to create a single attribute that would access the central authorization service
    - Might want a central place like a WebAttribute instead because really want to assume they don't have access by default.
+ Should be able to get all permissions for a set of roles
+ Will need to understand role and ability precedence (i.e want the user to understand exactly what someone has access to)

## User has configuration choices
+ The most flexible model would allow the user to define roles
+ The developer must specify abilities, but could expose those in an admin interface via seed data
    - Would need to select a convention to save in database and to read it in
+ This would allow the user to create new roles and assign abilities to that new role
+ This would override the default configuration set in the code
+ Users cannot add abilities though. This would not make sense

## Working with the web
+ For frontend application there would be two approaches
    - Setup one route that returns all role and ability information that the frontend must refer to
       + This has potential security concerns but the backend could cover them
    - Could setup an authorization route for each ability and for each element check whether a user should see or gray out a particular button
    - This route would also define how the admin interface interacted with roles and abilities

## Additional (optional) support
+ Need to support conditions like "read if they own it" or "write if they are the owner". 
    -Conditions would correspond to attributes on the resource.
+ Do we want to add role inheiritance? 
    - Need to consider proxies. 
    - Idea of effective permissions
+ Will need to test using different Role classes with different Id types

## Unknowns
+ Do we need to handle authentication in this module too or just authorization?

# Examples

See tests for examples right now. Will add offical examples later.
