@echo off
setlocal

set "TARGET_DIR=LangScan"
set "SECRETS_ENV=.secrets.env"
set "CMD_RUN=dotnet run --project %TARGET_DIR%"

:menu
cls
echo.
echo  --- APP RUNNER ---
echo.
echo  1. Run application
echo  2. Run application (--run-it-tests)
echo  3. Create secrets template
echo  0. Exit
echo.
echo  Select an option:
set /p "choice=>> "

if "%choice%"=="1" goto run_app
if "%choice%"=="2" goto run_it_tests
if "%choice%"=="3" goto create_secrets_template
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

:create_secrets_template
echo.
if not exist "%SECRETS_ENV%" (
	for %%a in (GEMINI OPENAI GROK) do (
		echo AI_KEY_%%a=>>"%SECRETS_ENV%"
	)
	echo Done!
) else (
	echo File "%SECRETS_ENV%" already exists.
)
goto finish

:finish
echo.
pause
goto menu

:end
endlocal
exit /b 0