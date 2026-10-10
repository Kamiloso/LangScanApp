@echo off
setlocal

set "CMD_START=docker compose up"
set "CMD_STOP=docker compose stop"
set "CMD_CONSOLE=docker compose exec db psql -U root"
set "CMD_LOGS=docker compose logs -f"

:menu
cls
echo.
echo  --- DOCKER DB MANAGER ---
echo.
echo  1. Start container     ^| %CMD_START%
echo  2. Stop container      ^| %CMD_STOP%
echo  3. Open PSQL console   ^| %CMD_CONSOLE%
echo  0. Exit
echo.
echo  Select an option:
set /p "choice=>> "

if "%choice%"=="1" goto start_container
if "%choice%"=="2" goto stop_container
if "%choice%"=="3" goto open_console
if "%choice%"=="0" goto end

echo.
echo Invalid option.
goto finish

:start_container
echo.
%CMD_START%
goto finish

:stop_container
echo.
%CMD_STOP%
goto finish

:open_console
echo.
%CMD_CONSOLE%
goto finish

:finish
echo.
pause
goto menu

:end
endlocal
exit /b 0