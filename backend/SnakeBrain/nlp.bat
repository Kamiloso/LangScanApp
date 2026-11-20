@echo off
setlocal

set "CMD_START=docker compose up -d --build"
set "CMD_STOP=docker compose down"
set "CMD_STATUS=docker compose ps"
set "CMD_LOGS=docker compose logs --tail 100 app"

:menu
cls
echo.
echo  --- DOCKER NLP MANAGER ---
echo.
echo  1. Start containers    ^| %CMD_START%
echo  2. Stop containers     ^| %CMD_STOP%
echo  3. Show status         ^| %CMD_STATUS%
echo  4. Show app logs       ^| %CMD_LOGS%
echo  0. Exit
echo.
echo  Select an option:
set /p "choice=>> "

if "%choice%"=="1" goto start_container
if "%choice%"=="2" goto stop_container
if "%choice%"=="3" goto show_status
if "%choice%"=="4" goto show_logs
if "%choice%"=="0" goto end

echo.
echo Invalid option.
goto finish

:start_container
echo.
%CMD_START%
echo.
echo App is ready when status shows (healthy). Check with option 3.
goto finish

:stop_container
echo.
%CMD_STOP%
goto finish

:show_status
echo.
%CMD_STATUS%
goto finish

:show_logs
echo.
%CMD_LOGS%
goto finish

:finish
echo.
pause
goto menu

:end
endlocal
exit /b 0