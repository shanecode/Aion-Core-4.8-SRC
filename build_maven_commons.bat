@ECHO off
SET MODE=clean package
SET TITLE=Build
CLS
TITLE InsaneAion Commons - %TITLE%ing Commons
CD ./AC-Commons
call mvn %MODE%
pause
)

