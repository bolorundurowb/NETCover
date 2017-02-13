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



