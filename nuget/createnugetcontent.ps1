Write-Host $args

Copy-Item "$args\Hero.Frontend\Controllers\AuthorizationController.cs" "$args\nuget\Content\Controllers"
Remove-Item "$args\nuget\Content\Controllers\AuthorizationController.cs.pp"
Rename-Item -Force "$args\nuget\Content\Controllers\AuthorizationController.cs" "$args\nuget\Content\Controllers\AuthorizationController.cs.pp"

Copy-Item "$args\Hero.Frontend\Controllers\AbilityController.cs" "$args\nuget\Content\Controllers"
Remove-Item "$args\nuget\Content\Controllers\AbilityController.cs.pp"
Rename-Item -Force "$args\nuget\Content\Controllers\AbilityController.cs" "$args\nuget\Content\Controllers\AbilityController.cs.pp"

Copy-Item "$args\Hero.Frontend\Controllers\RoleController.cs" "$args\nuget\Content\Controllers"
Remove-Item "$args\nuget\Content\Controllers\RoleController.cs.pp"
Rename-Item -Force "$args\nuget\Content\Controllers\RoleController.cs" "$args\nuget\Content\Controllers\RoleController.cs.pp"

Copy-Item "$args\Hero.Frontend\Controllers\UserController.cs" "$args\nuget\Content\Controllers"
Remove-Item "$args\nuget\Content\Controllers\UserController.cs.pp"
Rename-Item -Force "$args\nuget\Content\Controllers\UserController.cs" "$args\nuget\Content\Controllers\UserController.cs.pp"

Copy-Item "$args\Hero.Frontend\Scripts\hero.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\hero.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\hero.js" "$args\nuget\Content\Scripts\hero.js.pp"

Copy-Item "$args\Hero.Frontend\Scripts\hero-ability.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\hero-ability.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\hero-ability.js" "$args\nuget\Content\Scripts\hero-ability.js.pp"

Copy-Item "$args\Hero.Frontend\Scripts\hero-authorization.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\hero-authorization.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\hero-authorization.js" "$args\nuget\Content\Scripts\hero-authorization.js.pp"

Copy-Item "$args\Hero.Frontend\Scripts\hero-role.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\hero-role.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\hero-role.js" "$args\nuget\Content\Scripts\hero-role.js.pp"

Copy-Item "$args\Hero.Frontend\Scripts\hero-user.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\hero-user.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\hero-user.js" "$args\nuget\Content\Scripts\hero-user.js.pp"

Copy-Item "$args\Hero.Frontend\Scripts\restangular.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\restangular.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\restangular.js" "$args\nuget\Content\Scripts\restangular.js.pp"

Copy-Item "$args\Hero.Frontend\Scripts\lodash.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\lodash.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\lodash.js" "$args\nuget\Content\Scripts\lodash.js.pp"

Copy-Item "$args\Hero.Frontend\Templates\user-create.html" "$args\nuget\Content\Templates"
Remove-Item "$args\nuget\Content\Templates\user-create.html.pp"
Rename-Item -Force "$args\nuget\Content\Templates\user-create.html" "$args\nuget\Content\Templates\user-create.html.pp"

Copy-Item "$args\Hero.Frontend\Templates\user-list.html" "$args\nuget\Content\Templates"
Remove-Item "$args\nuget\Content\Templates\user-list.html.pp"
Rename-Item -Force "$args\nuget\Content\Templates\user-list.html" "$args\nuget\Content\Templates\user-list.html.pp"

Copy-Item "$args\Hero.Frontend\Templates\role-create.html" "$args\nuget\Content\Templates"
Remove-Item "$args\nuget\Content\Templates\role-create.html.pp"
Rename-Item -Force "$args\nuget\Content\Templates\role-create.html" "$args\nuget\Content\Templates\role-create.html.pp"

Copy-Item "$args\Hero.Frontend\Templates\role-list.html" "$args\nuget\Content\Templates"
Remove-Item "$args\nuget\Content\Templates\role-list.html.pp"
Rename-Item -Force "$args\nuget\Content\Templates\role-list.html" "$args\nuget\Content\Templates\role-list.html.pp"

Copy-Item "$args\Hero.Frontend\Templates\ability-create.html" "$args\nuget\Content\Templates"
Remove-Item "$args\nuget\Content\Templates\ability-create.html.pp"
Rename-Item -Force "$args\nuget\Content\Templates\ability-create.html" "$args\nuget\Content\Templates\ability-create.html.pp"

Copy-Item "$args\Hero.Frontend\Templates\ability-list.html" "$args\nuget\Content\Templates"
Remove-Item "$args\nuget\Content\Templates\ability-list.html.pp"
Rename-Item -Force "$args\nuget\Content\Templates\ability-list.html" "$args\nuget\Content\Templates\ability-list.html.pp"

Copy-Item "$args\Hero.Frontend\Templates\header.html" "$args\nuget\Content\Templates"
Remove-Item "$args\nuget\Content\Templates\header.html.pp"
Rename-Item -Force "$args\nuget\Content\Templates\header.html" "$args\nuget\Content\Templates\header.html.pp"

$original_file = "$args\nuget\Content\Controllers\AuthorizationController.cs.pp"
$destination_file = "$args\nuget\Content\Controllers\AuthorizationController.cs.pp"
(Get-Content $original_file) | Foreach-Object {
    $_ -replace 'namespace Hero.Frontend', 'namespace $rootnamespace$'
    } | Set-Content $destination_file
    
$original_file = "$args\nuget\Content\Controllers\AbilityController.cs.pp"
$destination_file = "$args\nuget\Content\Controllers\AbilityController.cs.pp"
(Get-Content $original_file) | Foreach-Object {
    $_ -replace 'namespace Hero.Frontend', 'namespace $rootnamespace$'
    } | Set-Content $destination_file

$original_file = "$args\nuget\Content\Controllers\RoleController.cs.pp"
$destination_file = "$args\nuget\Content\Controllers\RoleController.cs.pp"
(Get-Content $original_file) | Foreach-Object {
    $_ -replace 'namespace Hero.Frontend', 'namespace $rootnamespace$'
    } | Set-Content $destination_file

$original_file = "$args\nuget\Content\Controllers\UserController.cs.pp"
$destination_file = "$args\nuget\Content\Controllers\UserController.cs.pp"
(Get-Content $original_file) | Foreach-Object {
    $_ -replace 'namespace Hero.Frontend', 'namespace $rootnamespace$'
    } | Set-Content $destination_file