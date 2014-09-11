#!/bin/sh
mkdir -p Bin/x86/Release
/usr/local/bin/swig -python -c++ -outdir "Bin/x86/Release" -o "FdoPython/fdo_wrap.cpp" -I/usr/local/fdo-3.9.0/include fdo.i
pushd FdoPython
make
make install
popd
