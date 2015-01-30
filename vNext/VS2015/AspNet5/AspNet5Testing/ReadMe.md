# Asp.Net 5 Testing

A quick and dirty project to illustrate Asp.Net 5 testing
using Visual Studio 2015 Preview CTP 5 (or greater)¨.
- These tests use XUnit
- Install the XUnit extension for R# 9

- Follow instructions here to set up environment: https://github.com/aspnet/Home
  + Run from an elevated command prompt:
    @powershell -NoProfile -ExecutionPolicy unrestricted -Command "iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/aspnet/Home/master/kvminstall.ps1'))"
  + Install the K Runtime Environment (KRE):
    kvm upgrade

- Install the required packages for this project
  + Add the following NuGet package source:
    AspNetVNext: https://www.myget.org/F/aspnetvnext/api/v2
  + Make sure NuGet.org is also checked
  + Let VS 2015 restore packages, or run kpm restore from the command line

- R# test glyphs light up
  + But they don't seem to show up in R# Unit Tests Explorer
  + Running them from the glyph results in inconclusive results

- Tests will light up in R# test runner, but they all are inconclusive

- Tests are supposed to show up in Test Explorer after building
  + This seems to be broken in CTP 5
  
- It is also possible to run the "test" command from the toolbar in VS
  + Ctrl-F5 shows the console running the k test command
  + F5 allows debugging tests and setting breakpoints