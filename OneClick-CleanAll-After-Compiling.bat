@ECHO ############################################
@ECHO ########## Cleaning all files ... ##########
@ECHO ########## Execute from ./trunk/  ##########
@ECHO ############################################


@cd AC-Chat
@call ..\AC-Tools\Ant\bin\ant clean
@cd ..

@cd AC-Login
@call ..\AC-Tools\Ant\bin\ant clean

@cd ..

@cd AC-Game
@call ..\AC-Tools\Ant\bin\ant clean
del jar-yguard.xml
del yshrinklog.xml

@cd ..

@ECHO ############################################
@ECHO ################# Completed ################
@ECHO ############################################

@PAUSE