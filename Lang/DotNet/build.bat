@echo off
if exist Bin rd /S /Q Bin
del /F /Q Wrapper\*.cs
rem swig -csharp -c++ -nodefaultctor -nodefaultdtor -namespace OSGeo.FDO -outdir "%CD%\Wrapper" -o "%CD%\FdoDotNet\fdo_wrap.cpp" -I"%CD%\..\..\Fdo\Inc" fdo.i
swig -csharp -c++ -nodefaultctor -nodefaultdtor -namespace OSGeo.FDO -outdir "%CD%\Wrapper" -o "%CD%\FdoDotNet\fdo_wrap.cpp" -I"%CD%\..\..\Fdo\Inc" fdo.i 1> swig_build.log 2>&1
msbuild FdoDotNet.sln /p:Configuration=Release