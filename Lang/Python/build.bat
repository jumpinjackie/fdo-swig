@echo off
SET PLAT=Win32
SET CONFIG=Release
if "%1" == "x64" SET PLAT=x64
if exist "Bin\%PLAT%\%CONFIG%" del /Q /F "Bin\%PLAT%\%CONFIG%\_PyFDO.*"
if exist "Bin\%PLAT%\%CONFIG%" del /Q /F "Bin\%PLAT%\%CONFIG%\PyFDO.*"
mkdir "Bin\%PLAT%\%CONFIG%"
swig -python -c++ -nodefaultctor -nodefaultdtor -outdir "%CD%\Bin\%PLAT%\%CONFIG%" -o "%CD%\FdoPython\fdo_wrap.cpp" -I"%CD%\..\..\Fdo\Inc" fdo.i 1> swig_build.log 2>&1
msbuild FdoPython.sln /p:Configuration=Release;Platform=%PLAT%