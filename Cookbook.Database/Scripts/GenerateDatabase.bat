@echo off

set LogFileName=GenerateDatabase.log

set ExecPath=%~dp0
set DataPath=%ExecPath%..\Data

for /F "usebackq tokens=1,2 delims==" %%i in (`wmic os get LocalDateTime /VALUE 2^>NUL`) do if '.%%i.'=='.LocalDateTime.' set CurrentTime=%%j
set CurrentTime=%CurrentTime:~0,4%-%CurrentTime:~4,2%-%CurrentTime:~6,2% %CurrentTime:~8,2%:%CurrentTime:~10,2%:%CurrentTime:~12,6%

echo Début de génération de la base à %CurrentTime% > %LogFileName%
echo. >> %LogFileName%

echo Création de la base >> %LogFileName%
sqlcmd -S DESKTOP-9HSO3TR\SQL2014 -E -i CreateDatabase.sql >> %LogFileName%
echo. >> %LogFileName%

echo Ajout des données >> %LogFileName%
sqlcmd -S DESKTOP-9HSO3TR\SQL2014 -E -v DataPath ="%DataPath%" -i PopulateDatabase.sql >> %LogFileName%
echo. >> %LogFileName%

for /F "usebackq tokens=1,2 delims==" %%i in (`wmic os get LocalDateTime /VALUE 2^>NUL`) do if '.%%i.'=='.LocalDateTime.' set CurrentTime=%%j
set CurrentTime=%CurrentTime:~0,4%-%CurrentTime:~4,2%-%CurrentTime:~6,2% %CurrentTime:~8,2%:%CurrentTime:~10,2%:%CurrentTime:~12,6%
echo Fin de la génération de la base à %CurrentTime% >> %LogFileName%