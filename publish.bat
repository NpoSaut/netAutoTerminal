set progName=%1
set installerName=%progName%.msi

@cd %progName%Installer\bin\Release
%progName%Installer.exe
@cd ..\..\..

@echo off
set /p Publish= Publish on repo.saut.ru [y/n]? 
IF %Publish%==y (
    copy installers\%installerName% "\\repo\ToolsRepo\%progName%\%installerName%" /y
    \\repo\ToolsRepo\%progName%\description.xml
)
