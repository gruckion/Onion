@echo off
cd ..\Onion.Repositories\
set /p DbContextName="Enter DbContext name: "
dotnet ef migrations remove -c %DbContextName%DbContext
pause