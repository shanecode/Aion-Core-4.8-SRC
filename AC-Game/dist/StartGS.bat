@ECHO off
TITLE InsaneAion 4.8 - GameServer Panel
color 4E
SET PATH=C:\Program Files\Java\jdk1.7.0_79\bin
:START
CLS

echo.

echo Starting Insane Aion Game Server.

echo.

REM -------------------------------------  
REM Default parameters for a basic server.
java -Xms1280m -Xmx8192m -XX:MaxHeapSize=8192m -Xdebug -XX:MaxNewSize=24m -XX:NewSize=24m -XX:+UseParNewGC -XX:+CMSParallelRemarkEnabled -XX:+UseConcMarkSweepGC -XX:-UseSplitVerifier -ea -javaagent:./libs/ac-commons-1.3.jar -cp ./libs/*;./libs/Ac-Game.jar com.aionemu.gameserver.GameServer
REM -------------------------------------
SET CLASSPATH=%OLDCLASSPATH%

if ERRORLEVEL 2 goto restart
if ERRORLEVEL 1 goto error
if ERRORLEVEL 0 goto end

REM Restart...
:restart
echo.
echo Administrator Restart ...
echo.
goto start

REM Error...
:error
echo.
echo Server terminated abnormaly ...
echo.
goto end

REM End...
:end
echo.
echo Server terminated ...
echo.
pause
