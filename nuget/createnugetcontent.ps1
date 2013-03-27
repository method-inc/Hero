Copy-Item "$args\Hero.Configuration\HeroConfig.cs" "$args\nuget\Content\App_Start"
Remove-Item "$args\nuget\Content\App_Start\HeroConfig.cs.pp"
Rename-Item -Force "$args\nuget\Content\App_Start\HeroConfig.cs" "$args\nuget\Content\App_Start\HeroConfig.cs.pp"

Copy-Item "$args\Hero.Frontend\AbilitiesController.cs" "$args\nuget\Content\Controllers"
Remove-Item "$args\nuget\Content\Controllers\AbilitiesController.cs.pp"
Rename-Item -Force "$args\nuget\Content\Controllers\AbilitiesController.cs" "$args\nuget\Content\Controllers\AbilitiesController.cs.pp"

Copy-Item "$args\Hero.Frontend\hero.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\hero.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\hero.js" "$args\nuget\Content\Scripts\hero.js.pp"

Copy-Item "$args\Hero.Frontend\reqwest.min.js" "$args\nuget\Content\Scripts"
Remove-Item "$args\nuget\Content\Scripts\reqwest.min.js.pp"
Rename-Item -Force "$args\nuget\Content\Scripts\reqwest.min.js" "$args\nuget\Content\Scripts\reqwest.min.js.pp"

$original_file = "$args\nuget\Content\App_Start\HeroConfig.cs.pp"
$destination_file = "$args\nuget\Content\App_Start\HeroConfig.cs.pp"
(Get-Content $original_file) | Foreach-Object {
    $_ -replace 'namespace Hero.Configuration', 'namespace $rootnamespace$'
    } | Set-Content $destination_file
    
$original_file = "$args\nuget\Content\Controllers\AbilitiesController.cs.pp"
$destination_file = "$args\nuget\Content\Controllers\AbilitiesController.cs.pp"
(Get-Content $original_file) | Foreach-Object {
    $_ -replace 'namespace Hero.Frontend', 'namespace $rootnamespace$'
    } | Set-Content $destination_file