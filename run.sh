#!/usr/bin/env bash
set -euo pipefail

export DOTNET_ROOT="$HOME/.dotnet"
export PATH="$HOME/.dotnet:$PATH"
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

# Check if dotnet is working (not just present); wipe and reinstall if broken
NEEDS_INSTALL=false
if ! command -v dotnet >/dev/null 2>&1; then
  NEEDS_INSTALL=true
  echo "dotnet not found"
else
  # Verify the runtime is intact by checking for libhostpolicy.so
  if ! find "$HOME/.dotnet/shared/Microsoft.NETCore.App" -name "libhostpolicy.so" 2>/dev/null | grep -q .; then
    NEEDS_INSTALL=true
    echo "dotnet runtime is broken (libhostpolicy.so missing) — reinstalling"
    rm -rf "$HOME/.dotnet"
  fi
fi

if [ "$NEEDS_INSTALL" = "true" ]; then
  chmod +x dotnet-install.sh
  ./dotnet-install.sh --channel 10.0 --install-dir "$HOME/.dotnet"
  export PATH="$HOME/.dotnet:$PATH"
fi

echo "dotnet version: $(dotnet --version)"

export PORT=${PORT:-5000}
export ASPNETCORE_URLS="http://0.0.0.0:${PORT}"

dotnet restore "Employee Admin Portal.csproj"
dotnet publish "Employee Admin Portal.csproj" -c Release -o publish

cd publish

echo "Starting app on port $PORT"

exec dotnet "Employee_Admin_Portal.dll"
