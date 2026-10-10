@echo off
setlocal

set "TARGET_DIR=LangScan"
set "ARGS=--project %TARGET_DIR% --startup-project %TARGET_DIR% -- --test-run"

set "CMD_ADD=dotnet ef migrations add"
set "CMD_REMOVE=dotnet ef migrations remove --force"

:menu
cls
echo.
echo  --- EF CORE MANAGER ---
echo.
echo  1. Add migration       ^| %CMD_ADD% ^<name^> ...
echo  2. Remove last         ^| %CMD_REMOVE% ...
echo  3. List local files    ^| (Lists local .cs files only)
echo  4. Reset all           ^| (Deletes Migrations folder)
echo  0. Exit
echo.
echo  Select an option:
set /p "choice=>> "

if "%choice%"=="1" goto add_migration
if "%choice%"=="2" goto remove_migration
if "%choice%"=="3" goto list_migrations
if "%choice%"=="4" goto reset_migrations
if "%choice%"=="0" goto end

echo.
echo Invalid option.
goto finish

:add_migration
echo.
echo Enter migration name:
set /p "mig_name=>> "
%CMD_ADD% "%mig_name%" %ARGS%
goto finish

:remove_migration
echo.
%CMD_REMOVE% %ARGS%
goto finish

:list_migrations
echo.
echo Local migrations in project %TARGET_DIR%:
if exist "%TARGET_DIR%\Migrations\" (
    dir /b "%TARGET_DIR%\Migrations\*.cs" | findstr /v /i "Designer.cs ModelSnapshot.cs"
) else (
    echo Migrations directory does not exist yet.
)
goto finish

:reset_migrations
echo.
echo Deleting all local migrations...
if exist "%TARGET_DIR%\Migrations\" (
    rmdir /s /q "%TARGET_DIR%\Migrations"
    echo Migrations reset successfully.
) else (
    echo Migrations directory does not exist.
)
goto finish

:finish
echo.
pause
goto menu

:end
endlocal
exit /b 0