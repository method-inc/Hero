# Hero

Ability based authorization for .Net projects. It will generate a Nuget package that can be downloaded as a 3rd party dll via Visual Studio.

# Installation

You can install this module via Nuget. The Nuget package is available from the [Skookum Nuget Server](http://skookum.cloudapp.net/guestAuth/app/nuget/v1/FeedService.svc/)

# License

This project is licensed under the [MIT License](http://opensource.org/licenses/MIT)

# TODO

+ Write unit tests
+ Setup pub sub for registration
+ Add attributes for methods and classes
+ Add some ability to specify sub method level abilities?
+ Add fluent interface to configure abilities and roles
+ Add database support for roles and abilities?
+ Add ability to generate current roles/ability matrix data structure
+ Do we even want to save abilities in the database? Should we always just generate it via reflection?

# Examples

See tests for examples right now. Will add offical examples later.