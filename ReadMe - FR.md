<img alt="Me Logo" src="Medias/favicon.ico" width="32">  __Me.WCFTPSyncWrapper solution__

L'objectif de cette solution est d'automatiser la synchronisation entre un boitier de propagation (seedbox) localisé
chez un hébergeur et votre NAS domestique de vos séries TV.
Cela suppose que vous ayez déja mis en place chez votre hébergeur une automatisation du rapatriement de vos séries cultes.
Le cas échéant vous pouvez vous référer à ce [lien](http://www.crazyws.fr/internet/alternatives-megaupload-ou-vpn-seedbox-newsgroup-17QG4.html).
Des solutions existent déja [sous linux](http://www.crazyws.fr/dev/systeme/synchroniser-votre-seedbox-avec-votre-nas-ou-votre-ordinateur-6NGGE.html),
celle que je vous propose utilise l'environnement Windows.

Cette solution repose donc sur l'outil de synchronisation de [CuberKiko Tools](http://www.cyberkiko.com/page/ftpsync/) "FTPSync" que vous pouvez télécharger
[ici](http://cdn.cyberkiko.com/Download/Tools/FTPSync.zip), avec sa [documentation en ligne](http://cyberkiko.com/Docs/FTPSync29/). Elle peut être considérée
comme un Helper ou un Wrapper de __FTPSync__ , permettant de faciliter le paramètrage de l'objectif final qui est de récupérer vos séries sur la seedbox et de les
ranger correctement dans votre NAS domestique.

Le mieux est de vous expliquer l'architecture que j'ai mis en place à mon domicile.

Je dispose dans la pièce qui me sert de bureau d'un NAS sous Windows 10 connecté à Internet et dans mon salon d'un boîtier "Gigabyte GB-BXI3-5010 Barebone Noir"
aussi sous Windows 10, connectés en Ethernet 1Gbs.
La solution _Me.WCFTPSyncWrapper_ est installée sur le lecteur N: du NAS qui est partagé.
Le boîtier Gigabyte exécute qu'en à lui Kodi (ex XBMC) et utilise le partage du NAS installé sur un lecteur N: (lettre de lecteur non obligatoirement identique, mais pratique).

####Structure des répertoires sur le NAS et principaux fichiers:
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
Il est important que le nom des répertoires correspondent au début du nom des fichiers ex: fichier Better.Call.Saul.S01E01.VOSTFR.HDTV.XviD-ATeam.avi
répertoire Better.Call.Saul, pour le classement automatisé. En effet, un nouveau fichier est placé temporairement dans le répertoire _TempDownload_
avant d'être déplacé dans le sous-réperoire _Videos/Series/Nom-Serie_ qui lui correspond en se basant sur le début du nom de fichier.

##Paramètres de FTPSync:

Ils sont stockés dans un fichier ini, dont le nom est passé en paramètre à l'exécutable (ex: seedbox.ini).
Les principaux paramètres qui resteront fixes sont les suivants:
```
[Source]
Server=YOUR_SEEDBOX_SERVER_IP
Port=YOUR_SEEDBOX_SERVER_PORT
User=YOUR_SEEDBOX_SERVER_USERNAME
Pass=YOUR_SEEDBOX_SERVER_PASSWORD
Dir=YOUR_SEEDBOX_SERVER_PATH
```
et
```
[Destination]
Dir=N:\Videos\TempDownload\
```
Les autres paramètres variables sont:
```
[Source]
ExcludeDir=_*;alaakaazaam;degling;dollar;flo;lardon;nunur;plop;throna
ExcludeFile=_*;*.log;*.nfo
IncludeFile=Better.Call.Saul.*;The.Walking.Dead.*;House.of.Cards.*;Gotham.*;Ray.Donovan.*;True.Detective.*;State.of.Affairs.*;Banshee.*
```

Cf. [documentation de ini](http://www.cyberkiko.com/Docs/FTPSync29/INIFile.htm)

##Paramètres de Me.WCFTPSyncWrapper:

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

Les 4 premiers, sont des paramètres ligne de commande pour FTPSync.
Cf. [documentation paramètres](http://www.cyberkiko.com/Docs/FTPSync29/CmdLine.htm?MenuState=XXAAAAAAAAAAAAAAAAAAAAVFAAAAUA)
Les 5 derniers doivent être adaptés en fonction de votre environnement.
_DownloadedListFileLog.txt_ contient la liste des séries mises à jour.

La solution dispose:

- d'une version en ligne de commande **CFTPSyncWrapper.exe**
- d'une version WinForm **WFTPSyncWrapper.exe**

Pour cette première version 1.0.0.0, le projet WinForm se contente d'afficher les paramètres et permet d'exécuter FTPSync.

On utilisera plutôt la version ligne de commande avec "Task scheduler" pour déclencher le processus en mode "Quiet".

Les deux versions disposent de paramètres identiques.

>Copyright (c) Jose ALVAREZ. All rights reserved.
>Licensed under the MIT license. See LICENSE.MD file in the solution root for full license information.
