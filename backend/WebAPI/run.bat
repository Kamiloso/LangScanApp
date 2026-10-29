@echo off
setlocal

set "TARGET_DIR=LangScan"
set "CMD_RUN=dotnet run --project %TARGET_DIR%"

:menu
cls
echo.
echo  --- APP RUNNER ---
echo.
echo  1. Run application
echo  2. Run application (--run-it-tests)
echo  0. Exit
echo.
echo  Select an option:
set /p "choice=>> "

if "%choice%"=="1" goto run_app
if "%choice%"=="2" goto run_it_tests
if "%choice%"=="0" goto end

echo.
echo Invalid option.
goto finish

:run_app
echo.
%CMD_RUN%
goto finish

:run_it_tests
echo.
%CMD_RUN% -- --run-it-tests
goto finish

:finish
echo.
pause
goto menu

:end
endlocal
exit /b 0