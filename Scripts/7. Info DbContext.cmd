@echo off
cd ..\Onion.Repositories\
set /p DbContextName="Enter DbContext name: "
dotnet ef dbcontext info -c %DbContextName%DbContext
pause