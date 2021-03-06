@echo off
SET PLAT=x86
SET BUILD_ALL=0
if "%1" == "x64" SET PLAT=x64
if "%1" == "all" SET BUILD_ALL=1
if exist Bin rd /S /Q Bin
del /F /Q Wrapper\*.cs
rem swig -csharp -c++ -nodefaultctor -nodefaultdtor -namespace OSGeo.FDO -outdir "%CD%\Wrapper" -o "%CD%\FdoDotNet\fdo_wrap.cpp" -I"%CD%\..\..\Fdo\Inc" fdo.i
..\..\Tools\swigwin-3.0.2\swig.exe -csharp -c++ -namespace OSGeo.FDO -outdir "%CD%\Wrapper" -o "%CD%\FdoDotNet\fdo_wrap.cpp" -I"%CD%\..\..\Fdo\Inc" fdo.i 1> swig_build.log 2>&1
if "%BUILD_ALL%" == "1" (
    msbuild FdoDotNet.sln /p:Configuration=Release;Platform=x86
    msbuild FdoDotNet.sln /p:Configuration=Release;Platform=x64
) else (
    msbuild FdoDotNet.sln /p:Configuration=Release;Platform=%PLAT%
)