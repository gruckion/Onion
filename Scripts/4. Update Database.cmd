@echo off
cd ..\Onion.Repositories\
set /p DbContextName="Enter DbContext name: "
dotnet ef database update -c %DbContextName%DbContext
pause