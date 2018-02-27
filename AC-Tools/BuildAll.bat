:######################################################################## 
:# File name: BuildAll.bat
:# Edited Last By: Alcapwnd 
:# V 2.0
:######################################################################## 

@ECHO off
@COLOR 0B
SET MODE=clean package
SET TITLE=Build
TITLE AionCoreEmulator - %TITLE% Panel
:MENU
CLS
ECHO                AionCoreEmulator - %TITLE% Panel 
ECHO                                  ''~``
ECHO                                 ( o o )
ECHO    ------------------------.oooO--(_)--Oooo.------------------------
ECHO    .             1 - Build GameServer server                       .
ECHO    .             2 - Build LoginServer server                      .
ECHO    .             3 - Build ChatServer server                       .
ECHO    .             4 - Build Commons                                 .
ECHO    .             5 - Build All server                              . 
ECHO    .             6 - Quit                                          .
ECHO    .                         .oooO                                 .
ECHO    .                         (   )   Oooo.                         .
ECHO    ---------------------------\ (----(   )--------------------------
ECHO                                \_)    ) /
ECHO                                      (_/
ECHO.
:ENTER
SET /P Ares= Type your option and press ENTER:
IF %Ares%==1 GOTO GameServer
IF %Ares%==2 GOTO LoginServer
IF %Ares%==3 GOTO ChatServer
IF %Ares%==4 GOTO Commons
IF %Ares%==5 GOTO FULL
IF %Ares%==6 GOTO QUIT
:FULL
cd ..\AC-Commons 
start /WAIT /B ..\tools\Ant\bin\ant clean dist
cd ..\AC-Game
start /WAIT /B ..\tools\Ant\bin\ant clean dist
cd ..\AC-Login
start /WAIT /B ..\tools\Ant\bin\ant clean dist
cd ..\AC-Chat
start /WAIT /B ..\tools\Ant\bin\ant clean dist
GOTO :QUIT

:Commons
GOTO :QUIT

:GameServer
cd ..\AC-Game
start /WAIT /B ..\tools\Ant\bin\ant clean dist
GOTO :QUIT

:LoginServer
cd ..\AC-Login
start /WAIT /B ..\tools\Ant\bin\ant clean dist
GOTO :QUIT

:ChatServer
cd ..\AC-Chat
start /WAIT /B ..\tools\Ant\bin\ant clean dist
GOTO :QUIT

:QUIT
exit
