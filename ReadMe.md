<img alt="Me Logo" src="Medias/favicon.ico" width="32">  __Me.WCFTPSyncWrapper solution__

The objective of this solution is to automate the synchronization between a propagation case (seedbox) located in a hosting and your
home NAS to your TV sets. This assumes that you have already set up on your web host automation repatriation of your cult series.
If necessary you can refer to this [link](http://www.crazyws.fr/internet/alternatives-megaupload-ou-vpn-seedbox-newsgroup-17QG4.html).
Solutions already exist [under linux](http://www.crazyws.fr/dev/systeme/synchroniser-votre-seedbox-avec-votre-nas-ou-votre-ordinateur-6NGGE.html),
that I propose using the Windows environment.

This solution is based on the synchronization tool [CuberKiko Tools](http://www.cyberkiko.com/page/ftpsync/) "FTPSync" which you can download
[here](http://cdn.cyberkiko.com/Download/Tools/FTPSync.zip), with its [online documentation](http://cyberkiko.com/Docs/FTPSync29/).
It can be seen as a Helper or Wrapper __FTPSync__ , to facilitate the parameterization of the ultimate goal is to recover your series on the 
seedbox and store properly in your home NAS.

It is best to explain the architecture that I set up in my home.

I have in the room that office I use a NAS running Windows 10 with Internet access and in my living room with a housing "Gigabyte GB-BXI3-5010 Barebone Black" 
also in Windows 10, connected Ethernet 1Gbs. The Me.WCFTPSyncWrapper solution is installed on drive N: NAS is shared. The Gigabyte housing that runs to him Kodi 
(formerly XBMC) and uses sharing NAS installed on an N: drive (drive letter not necessarily identical but practice).

####Directory structure on the NAS and key files:
```
N:
|
|--ME
    |--MeFtpSyncWrapper
            |--FTPSync
                |FTPSync.exe
                |seedbox.ini
                |seedbox.mtb
            |--logs
            |CFTPSyncWrapper.exe
            |CFTPSyncWrapper.exe.config
            |WFTPSyncWrapper.exe
            |WFTPSyncWrapper.exe.config
            |MeCommon.dll
            |WCFTPSyncWrapper.dll
|--Videos
    |--_Duplicated
    |--Documentaires
    |--Films
    |--Series
        |--Better.Call.Saul
        |--Game.of.Thrones
        |--House.of.Cards
        |........
        |
        |etc....
    |--TempDownload
    |DownloadedListFileLog.txt
|
|
|
|
```
It is important that the folder names correspond to the beginning of file names eg Better.Call.Saul.S01E01.VOSTFR.HDTV.XviD-ATeam.avi Better.Call.Saul 
file directory for automated classification. Indeed, a new file is temporarily placed in the directory TempDownload before being moved in the sub-directory
Videos/Series/SerieName corresponding to it based on the beginning of the file name.

##FTPSync parameters:

They are stored in an ini file, whose name is passed as a parameter to the executable (eg seedbox.ini).

The main parameters that will remain fixed are:
```
[Source]
Server=YOUR_SEEDBOX_SERVER_IP
Port=YOUR_SEEDBOX_SERVER_PORT
User=YOUR_SEEDBOX_SERVER_USERNAME
Pass=YOUR_SEEDBOX_SERVER_PASSWORD
Dir=YOUR_SEEDBOX_SERVER_PATH
```
and
```
[Destination]
Dir=N:\Videos\TempDownload\
```
The other variable parameters are:
```
[Source]
ExcludeDir=_*;alaakaazaam;degling;dollar;flo;lardon;nunur;plop;throna
ExcludeFile=_*;*.log;*.nfo
IncludeFile=Better.Call.Saul.*;The.Walking.Dead.*;House.of.Cards.*;Gotham.*;Ray.Donovan.*;True.Detective.*;State.of.Affairs.*;Banshee.*
```

See. [ini documentation](http://www.cyberkiko.com/Docs/FTPSync29/INIFile.htm)

##Me.WCFTPSyncWrapper parameters:

```
<add key="quiet" value="true"/>
<add  key="full" value="false"/>
<add  key="differential" value="false"/>
<add  key="incremental" value="true"/> 
<add key="iniFileName" value="seedbox.ini"/>
<add key="seriesPath" value="N:\Videos\Series"/>
<add key="downloadsPath" value="N:\Videos\TempDownload"/>
<add key="downloadedListFileLog" value="N:\Videos\DownloadedListFileLog.txt"/>
<add key="duplicatedPath" value="N:\Videos\_Duplicated"/>
```

The first 4 are command line parameters for FTPSync.
See. [documentation paramètres](http://www.cyberkiko.com/Docs/FTPSync29/CmdLine.htm?MenuState=XXAAAAAAAAAAAAAAAAAAAAVFAAAAUA)
Last 5 must be adapted to your environment.
_DownloadedListFileLog.txt_ lists the updated series.

The solution provides:

- A command line version **CFTPSyncWrapper.exe** FTPSync Wrapper.exe
- A version WinForm **WFTPSyncWrapper.exe** WFTPSyncWrapper.exe

For this first version 1.0.0.0, the WinForm project only displays the parameters and can execute FTPSync.

Instead, use the command line version with "Task Scheduler" to start the process in "Quiet".

Both versions have identical parameters.

_Google translation_

>Copyright (c) Jose ALVAREZ. All rights reserved.
>Licensed under the MIT license. See LICENSE.MD file in the solution root for full license information.
