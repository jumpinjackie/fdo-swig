CPPFLAGS=-g -m32 -fPIC -Wno-write-strings -I/usr/local/fdo-3.9.0/include -I/usr/include/python2.7 -I../../Common
LDFLAGS=-g -m32 -fPIC -shared
LDLIBS=-L/usr/local/fdo-3.9.0/lib -lFDO -lExpressionEngine

all: _FDO.so

clean:
	rm -rf _FDO.so
	rm -rf *.o

install: _FDO.so
	mkdir -p ../Bin/x86/Release
	cp _FDO.so ../Bin/x86/Release

_FDO.so: fdo_wrap.o utils.o StringBuffer.o
	g++ $(LDFLAGS) -o _FDO.so fdo_wrap.o utils.o StringBuffer.o $(LDLIBS)

fdo_wrap.o: fdo_wrap.cpp
	g++ $(CPPFLAGS) -c fdo_wrap.cpp

utils.o: utils.cpp utils.h
	g++ $(CPPFLAGS) -c utils.cpp

StringBuffer.o: StringBuffer.cpp StringBuffer.h
	g++ $(CPPFLAGS) -c StringBuffer.cpp
