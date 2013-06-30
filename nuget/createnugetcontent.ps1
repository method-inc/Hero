Copy-Item "$args\Hero.Frontend\AbilitiesController.cs" "$args\nuget\Content\Controllers"
Remove-Item "$args\nuget\Content\Controllers\AbilitiesController.cs.pp"
Rename-Item -Force "$args\nuget\Content\Controllers\AbilitiesController.cs" "$args\nuget\Content\Controllers\AbilitiesController.cs.pp"

Copy-Item "$args\Hero.Frontend\RoleController.cs" "$args\nuget\Content\Controllers"
Remove-Item "$args\nuget\Content\Controllers\RoleController.cs.pp"
Rename-Item -Force "$args\nuget\Content\Controllers\RoleController.cs" "$args\nuget\Content\Controllers\RoleController.cs.pp"

Copy-Item "$args\Hero.Frontend\UserController.cs" "$args\nuget\Content\Controllers"
Remove-Item "$args\nuget\Content\Controllers\UserController.cs.pp"
Rename-Item -Force "$args\nuget\Content\Controllers\UserController.cs" "$args\nuget\Content\Controllers\UserController.cs.pp"

Copy-Item "$args\Hero.Frontend\hero.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\hero.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\hero.js" "$args\nuget\Content\Scripts\hero.js.pp"

Copy-Item "$args\Hero.Frontend\craft.min.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\craft.min.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\craft.min.js" "$args\nuget\Content\Scripts\craft.min.js.pp"
    
$original_file = "$args\nuget\Content\Controllers\AbilitiesController.cs.pp"
$destination_file = "$args\nuget\Content\Controllers\AbilitiesController.cs.pp"
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