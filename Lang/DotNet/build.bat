del Wrapper\*.cs
swig -csharp -c++ -namespace OSGeo.FDO -outdir "%CD%\Wrapper" -o "%CD%\FdoDotNet\fdo_wrap.cpp" -I"%CD%\..\..\Fdo\Inc" fdo.i
msbuild FdoDotNet\FdoDotNet.sln