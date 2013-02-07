Copy-Item "$args\Hero.Configuration\HeroConfiguration.cs" "$args\nuget\Content\App_Start"
Remove-Item "$args\nuget\Content\App_Start\HeroConfiguration.cs.pp"
Rename-Item -Force "$args\nuget\Content\App_Start\HeroConfiguration.cs" "$args\nuget\Content\App_Start\HeroConfiguration.cs.pp"

$original_file = "$args\nuget\Content\App_Start\HeroConfiguration.cs.pp"
$destination_file = "$args\nuget\Content\App_Start\HeroConfiguration.cs.pp"
(Get-Content $original_file) | Foreach-Object {
    $_ -replace 'namespace Hero.Configuration', 'namespace $rootnamespace$.App_Start'
    } | Set-Content $destination_file