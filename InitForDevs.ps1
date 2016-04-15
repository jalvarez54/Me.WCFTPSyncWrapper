if (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))
{   
#"No Administrative rights, it will display a popup window asking user for Admin rights"

$arguments = "& '" + $myinvocation.mycommand.definition + "'"
Start-Process "$psHome\powershell.exe" -Verb runAs -ArgumentList $arguments

break
}
#"After user clicked Yes on the popup, your file will be reopened with Admin rights"

#
#Create directories for developpers
#

$binPath = "..\bin"
$FTPSyncPath = "..\FTPSync"
$DebugPath = "..\bin\Debug"
$ReleasePath = "..\bin\Release"
$StagingPath = "..\bin\Staging"
$SetupPath = "..\bin\Setup"

New-Item "$PSScriptRoot\$binPath" -type directory -Force
New-Item "$PSScriptRoot\$FTPSyncPath" -type directory -Force

New-Item "$PSScriptRoot\$DebugPath" -type directory -Force
New-Item "$PSScriptRoot\$ReleasePath" -type directory -Force
New-Item "$PSScriptRoot\$StagingPath" -type directory -Force
New-Item "$PSScriptRoot\$SetupPath" -type directory -Force

New-Item "$PSScriptRoot\$DebugPath\FTPSync" -type directory -Force
New-Item "$PSScriptRoot\$ReleasePath\FTPSync" -type directory -Force
New-Item "$PSScriptRoot\$StagingPath\FTPSync" -type directory -Force
New-Item "$PSScriptRoot\$DebugPath\logs" -type directory -Force
New-Item "$PSScriptRoot\$ReleasePath\logs" -type directory -Force
New-Item "$PSScriptRoot\$StagingPath\logs" -type directory -Force

#
# Download FTPSync.zip
#
$url = "http://cdn.cyberkiko.com/Download/Tools/FTPSync.zip"
$output = "$PSScriptRoot\FTPSync.zip"
$start_time = Get-Date
$object = New-Object System.Net.WebClient

Write-Output "Downloading $url"
try
{
    $object.DownloadFile($url,$output)
}
catch
{
    $ErrorMessage = $_.Exception.Message
    Write-Output  $ErrorMessage
    break
}
Write-Output "Time taken: $((Get-Date).Subtract($start_time).Seconds) second(s)" 

#
#Unzip FTPSync.zip
#
$start_time = Get-Date
$targetFolder = "$PSScriptRoot\$FTPSyncPath"

if(Test-Path $targetFolder)
{
    Write-Output  "Deleting folder  $targetFolder"
    Remove-Item -Recurse -Force $targetFolder
}   

Write-Output  "Unzipping  $output"
try
{
    [System.Reflection.Assembly]::LoadWithPartialName('System.IO.Compression.FileSystem')
    [System.IO.Compression.ZipFile]::ExtractToDirectory($output, $targetFolder)
}
catch
{
    $ErrorMessage = $_.Exception.Message
    Write-Output  $ErrorMessage
    break
}

$from = "$PSScriptRoot\$FTPSyncPath\*"
$toDebug = "$PSScriptRoot\$DebugPath\FTPSync\"
$toRelease = "$PSScriptRoot\$ReleasePath\FTPSync\"
$toStaging = "$PSScriptRoot\$StagingPath\FTPSync\"

Copy-Item $from $toDebug -Recurse -Force
Copy-Item $from $toRelease -Recurse -Force
Copy-Item $from $toStaging -Recurse -Force

Write-Output "Time taken: $((Get-Date).Subtract($start_time).Seconds) second(s)"

#
#Copy seedbox.ini
#
Write-Output  "Copying seedbox.ini"
$from = "$PSScriptRoot\dist\seedbox.ini"
Copy-Item $from $toDebug -force
Copy-Item $from $toRelease -force
Copy-Item $from $toStaging -force

Write-Output  "Script terminated"