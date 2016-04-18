# Me.WCFTPSyncWrapper solution changelog for developpers

    <copyright file="ChangeLogForDev.md" company="Me">
    Copyright (c) Jose ALVAREZ. All rights reserved.
    Licensed under the MIT license.
    See LICENSE.md file in the solution root for full license information.
    </copyright>
    <author>Jose ALVAREZ [10000]</author>
    <email>jose.alvarez54@live.fr</email>
    <member> [20000]</member>
    <member> [30000]</member>
    <date>2016-04-14</date>
    <summary> 

    </summary>

**Internet Ref. projects**

> *"Render therefore unto Caesar the things which are Caesar's; and unto God the things that are God's."*

* [CyberKiko Tools FTPSync](http://www.cyberkiko.com/page/ftpsync/)
* [FTPSync download](http://cdn.cyberkiko.com/Download/Tools/FTPSync.zip)
* [Synchronisation FTP grâce à FtpSync](https://mikaelkrief.wordpress.com/2011/11/04/c-synchronisation-ftp-grace-a-ftpsync/)
* [Inno Setup to unzip a file](http://www.scriptkitties.com/innounzip/)
* [Inno-Setup: Post Install open link: antivirus alert while opening a link](http://stackoverflow.com/questions/5375210/inno-setup-post-install-open-link-antivirus-alert-while-opening-a-link)
* [innounp, the Inno Setup Unpacker](http://innounp.sourceforge.net/)
* [Console Notify icon](https://bluehouse.wordpress.com/2006/01/24/how-to-create-a-notify-icon-in-c-without-a-form/)
* [Redirect console output to textbox](http://stackoverflow.com/questions/14802876/what-is-a-good-way-to-direct-console-output-to-text-box-in-windows-form)

# TODO OPENED


##### [xxxxx] IMPLEMENT: MoveDuplicatedFiles()
    ==> open

============================================================================================================================
Sections model: 

    ## Version 0.0.0.0
    ##### [xxxxx] 
        [xxxxx-xxx]  
        ==> open
    ##### ==> open
    ##### [xxxxx] 
        ==> open

    _BEFORE COMMIT:_
    *Change projects assembly version number in AssemblyInfo.cs, setup.iss*
    *Compile the solution and then setup.iss*
============================================================================================================================

##### [10004] Version 1.1.0.0.alpha
 
    2016-04-18 Commit on GitHub. 

##### [10003] CFTPSyncWrapper and WFTPSyncWrapper parameters
[FTPSync parameters](http://www.cyberkiko.com/docs/FTPSync29/CmdLine.htm)
[10003-001]  ADD: Options.cs in Me.Common
[10003-002]  ADD: object options = null parmeter in FTPSyncWrapper constructor
[10003-003]  ADD: in CFTPSyncWrapper and WFTPSyncWrapper same command line parameters than FTPSync
    those parameters will superseed app.settings parameters
        

##### [10002] Version 1.0.0.1.alpha
 
    2016-04-16 Commit on GitHub. 

##### [10001] ADD: /INIT option & ADD: Push-Location $PSScriptRoot in InitForDevs.ps1
    [Avoiding initial transfer of all files](http://www.cyberkiko.com/post/ftpsync-avoiding-initial-transfer-of-all-files/)

##### [10000] Version 1.0.0.0.alpha
 
    2016-04-14 Commit on GitHub. 


