@echo off
cd /d "%~dp0src\KisiselHarcamaTakip.Web"
set ASPNETCORE_URLS=http://localhost:5080
echo Baslatiliyor: %ASPNETCORE_URLS%
dotnet run --no-launch-profile
pause
