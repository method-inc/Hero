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

Copy-Item "$args\Hero.Frontend\Scripts\hero-admin.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\hero-admin.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\hero-admin.js" "$args\nuget\Content\Scripts\hero-admin.js.pp"

Copy-Item "$args\Hero.Frontend\Scripts\craft.min.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\craft.min.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\craft.min.js" "$args\nuget\Content\Scripts\craft.min.js.pp"

Copy-Item "$args\Hero.Frontend\Scripts\backbone-forms.min.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\backbone-forms.min.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\backbone-forms.min.js" "$args\nuget\Content\Scripts\backbone-forms.min.js.pp"

Copy-Item "$args\Hero.Frontend\Css\metro-bootstrap.css" "$args\nuget\Content\Content"
Remove-Item "$args\nuget\Content\Content\metro-bootstrap.css.pp"
Rename-Item -Force "$args\nuget\Content\Content\metro-bootstrap.css" "$args\nuget\Content\Content\metro-bootstrap.css.pp"

Copy-Item "$args\Hero.Frontend\Images\glyphicons-halflings-white.png" "$args\nuget\Content\Images"
Remove-Item "$args\nuget\Content\Images\glyphicons-halflings-white.png.pp"
Rename-Item -Force "$args\nuget\Content\Images\glyphicons-halflings-white.png" "$args\nuget\Content\Images\glyphicons-halflings-white.png.pp"

Copy-Item "$args\Hero.Frontend\Images\glyphicons-halflings.png" "$args\nuget\Content\Images"
Remove-Item "$args\nuget\Content\Images\glyphicons-halflings.png.pp"
Rename-Item -Force "$args\nuget\Content\Images\glyphicons-halflings.png" "$args\nuget\Content\Images\glyphicons-halflings.png.pp"

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