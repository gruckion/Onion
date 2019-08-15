@echo off
cd ..\Onion.Repositories\
set /p Confirmation="Enter Yes to confirm database drop: "
set /p DbContextName="Enter DbContext name: "
IF "%IF "%var%"=="Yes" (
	dotnet ef migrations add %MigrationsName% -o Data/Migrations -c %DbContextName%DbContext
) ELSE (
	echo No action taken.
)


pause