@echo off
cd ..\Onion.Repositories\
set /p DbContextName="Enter DbContext name: "
dotnet ef migrations list --verbose -c %DbContextName%DbContext
pause