Copy-Item "$args\Hero.Configuration\HeroConfig.cs" "$args\nuget\Content\App_Start"
Remove-Item "$args\nuget\Content\App_Start\HeroConfig.cs.pp"
Rename-Item -Force "$args\nuget\Content\App_Start\HeroConfig.cs" "$args\nuget\Content\App_Start\HeroConfig.cs.pp"

$original_file = "$args\nuget\Content\App_Start\HeroConfig.cs.pp"
$destination_file = "$args\nuget\Content\App_Start\HeroConfig.cs.pp"
(Get-Content $original_file) | Foreach-Object {
    $_ -replace 'namespace Hero.Configuration', 'namespace $rootnamespace$'
    } | Set-Content $destination_file