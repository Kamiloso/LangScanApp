@echo off
setlocal

:menu
cls
echo.
echo  --- VENV MANAGER ---
echo.
echo  1. Setup venv
echo  2. Open cmd in venv
echo  3. Open project in venv
echo  4. Freeze dependencies
echo  0. Exit
echo.
echo  Select an option:
set /p "choice=>> "

if "%choice%"=="1" goto setup_env
if "%choice%"=="2" goto open_terminal
if "%choice%"=="3" goto open_code
if "%choice%"=="4" goto freeze_env
if "%choice%"=="0" goto end

echo.
echo Invalid option.
goto finish

:setup_env
if exist .venv\ (
    echo.
    echo Venv is already installed.
    goto finish
)
python -m venv .venv
call ".venv\Scripts\activate.bat"
echo.
echo Venv has been installed.
if not exist requirements.txt (
	goto finish
)
pip install -r requirements.txt
echo.
echo Dependencies have been installed.
goto finish

:open_terminal
if not exist ".venv\Scripts\activate.bat" (
    echo.
    echo Venv not found.
    goto finish
)
start "Python Venv" cmd /k "call .venv\Scripts\activate.bat"
goto end

:open_code
if not exist ".venv\Scripts\activate.bat" (
    echo.
    echo Venv not found.
    goto finish
)
call ".venv\Scripts\activate.bat"
start "" code .
goto end

:freeze_env
if not exist ".venv\Scripts\activate.bat" (
    echo.
    echo Venv not found.
    goto finish
)
call ".venv\Scripts\activate.bat"
pip freeze > requirements.txt
echo.
echo Dependencies have been freezed.
goto finish

:finish
echo.
pause
goto menu

:end
endlocal
exit /b 0