#!/usr/bin/env bash
set -euo pipefail

# Ensure dotnet is on PATH (installed at $HOME/.dotnet)
export DOTNET_ROOT="$HOME/.dotnet"
export PATH="$HOME/.dotnet:$PATH"
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

if ! command -v dotnet >/dev/null 2>&1; then
  echo "dotnet not found — installing dotnet via dotnet-install.sh"
  chmod +x dotnet-install.sh
  ./dotnet-install.sh --channel 10.0 --install-dir "$HOME/.dotnet"
fi

echo "dotnet version: $(dotnet --version)"

export PORT=${PORT:-5000}
export ASPNETCORE_URLS="http://0.0.0.0:${PORT}"

dotnet restore "Employee Admin Portal.csproj"
dotnet publish "Employee Admin Portal.csproj" -c Release -o publish

cd publish

echo "Starting app on port $PORT"

exec dotnet Employee_Admin_Portal.dll
