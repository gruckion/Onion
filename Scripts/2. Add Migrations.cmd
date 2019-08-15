@echo off
cd ..\Onion.Repositories\
set /p DbContextName="Enter DbContext name: "
set /p MigrationsName="Enter migrations name: "
dotnet ef migrations add %MigrationsName% -o Data/%DbContextName%/Migrations -c %DbContextName%DbContext
pause

