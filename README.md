# NET Cover

[![Build Status](https://travis-ci.org/bolorundurowb/NETCover.svg?branch=master)](https://travis-ci.org/bolorundurowb/NETCover)

## About
This project aims to develop a cross-platform code coverage tool for .NET languages _(starting with C#)_ that would generate coverage reports in different formats _(starting with LCOV)_ compatible with different coverage reporting services. 

As at now, it works with MSBuild generated PDB files and workis going on in ernest to make it compatible with XBuild generated MDB files.

This work is a derivative created by Sergiy Sakharov, for his original work visit [https://www.codeproject.com/Articles/41722/Building-NET-Coverage-Tool](https://www.codeproject.com/Articles/41722/Building-NET-Coverage-Tool)

## Usage
```bash
coverage.exe {<AssemblyPaths>} [{-[<FilterType>:]NameFilter}] [<commands>[<commandArgs>]]
```

_AssemblyPaths_ - file system masks for DLL/EXE files, `i.e.: "C:\Temp\Libs\NHibernate* C:\Temp\NInject\NInject.Core*"`

_Filter Types:_
> f: - exclude files by name

> s: - exclude assemblies by name (This could be useful if strong name for some assembly should be weakened, however, coverage report is redundant for it)

> t: - exclude types by full name

> m: - exclude methods by full name

> a: or nothing - exclude members by their custom attribute names

_Commands:_
> /r - If this command is selected - instrumented assemblies replace existing ones. Old assemblies are backed up along with corresponding pdb files

> /x <coverage file path> - Path to a coverage XML file

## Usage Example
```bash
coverage.exe C:\Temp\myapp.exe C:\Temp\myapp.lib.dll -CodeGeneratedAttribute -t:Test /r /x C:\Temp\coverage2.xml 
```

This will generate instrumented `myapp.exe` and `myapp.lib.dll`, moving old assemblies into `myapp.bak.exe` and `myapp.lib.bak.dll` respectively. Members marked by attributes that contain `'CodeGeneratedAttribute'` in their name as well as types that contain `'Test'` in their full names will be excluded from the report.

## Possible Improvements
There are a couple of things that come to my mind (except making the tool to be bug-free).

- Remove duplicate instruments - and therefore improve performance and precision
- Make flushing of hit counts to XML file immediate
- Add support for Mono mdb files and port the tool to Linux (I am not sure that this is necessary because there is already a monocov tool for Mono)
- Create pdb[/mdb] files for instrumented assemblies, so that it will be possible to debug even those
- Calculate coverage without pdb files - this will require syntax analysis of the IL code. As for this point, I had some thoughts to reuse `nop` operators as indicators of code branching, however, it requires DLL do be built using debug configuration and therefore is not likely to be used (because debug-built assemblies usually have pdb-s with them)
- Mixed mode - reuse pdbs of different builds as a reference