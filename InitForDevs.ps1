#################################################################################
# File: InitForDevs.ps1 
# 2016/04/16
# Create the directory structure in ..\bin Debug, Release and Staging,
# Download and unzip FTPSync.zip in a FTPSync directory for each directory above,
# Copy dist\seedbox.ini in each FTPSync directories of each directory above
# Create logs folder in each each directory above
# JA
####################################################################################################

#
#Create directories for developers
#

# temporarily change to the correct folder
Push-Location $PSScriptRoot

$binPath = "..\bin"
$FTPSyncPath = "..\FTPSync"
$DebugPath = "..\bin\Debug"
$ReleasePath = "..\bin\Release"
$StagingPath = "..\bin\Staging"
$SetupPath = "..\bin\Setup"

New-Item "$binPath" -type directory -Force 
New-Item "$FTPSyncPath" -type directory -Force

New-Item "$DebugPath" -type directory -Force
New-Item "$ReleasePath" -type directory -Force
New-Item "$StagingPath" -type directory -Force
New-Item "$SetupPath" -type directory -Force

New-Item "$DebugPath\FTPSync" -type directory -Force
New-Item "$ReleasePath\FTPSync" -type directory -Force
New-Item "$StagingPath\FTPSync" -type directory -Force
New-Item "$DebugPath\logs" -type directory -Force
New-Item "$ReleasePath\logs" -type directory -Force
New-Item "$StagingPath\logs" -type directory -Force

#
# Download FTPSync.zip
#
$url = "http://cdn.cyberkiko.com/Download/Tools/FTPSync.zip"
$output = "FTPSync.zip"
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
$targetFolder = "$FTPSyncPath"

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

$from = "$FTPSyncPath\*"
$toDebug = "$DebugPath\FTPSync\"
$toRelease = "$ReleasePath\FTPSync\"
$toStaging = "$StagingPath\FTPSync\"

Copy-Item $from $toDebug -Recurse -Force 
Copy-Item $from $toRelease -Recurse -Force
Copy-Item $from $toStaging -Recurse -Force

Write-Output "Time taken: $((Get-Date).Subtract($start_time).Seconds) second(s)"

#
#Copy seedbox.ini
#
Write-Output  "Copying seedbox.ini"
$from = "dist\seedbox.ini"
Copy-Item $from $toDebug -force
Copy-Item $from $toRelease -force
Copy-Item $from $toStaging -force

# now back to previous directory
Pop-Location

Write-Output  "Script terminated"

Write-Host "Press any key to continue ..."
$x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")