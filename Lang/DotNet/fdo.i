%module FDO

%include "wchar.i"
%include "../Common/FdoIgnore.i"
%include "../Common/FdoIncludes.i"
%{
// FDO headers to include into SWIG wrapper
#include "Fdo.h"
%}