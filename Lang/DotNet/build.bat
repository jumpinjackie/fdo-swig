@echo off
del Wrapper\*.cs
swig -csharp -c++ -nodefaultctor -nodefaultdtor -namespace OSGeo.FDO -outdir "%CD%\Wrapper" -o "%CD%\FdoDotNet\fdo_wrap.cpp" -I"%CD%\..\..\Fdo\Inc" fdo.i
rem swig -csharp -c++ -nodefaultctor -nodefaultdtor -namespace OSGeo.FDO -outdir "%CD%\Wrapper" -o "%CD%\FdoDotNet\fdo_wrap.cpp" -I"%CD%\..\..\Fdo\Inc" fdo.i 1> swig_build.log 2>&1
msbuild FdoDotNet\FdoDotNet.sln /p:Configuration=Release