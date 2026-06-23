# Deploying Employee-Admin-Portal to Replit (free)

This repository includes a simple runner (run.sh) and a .replit file so you can deploy the app to Replit for free without entering any payment details.

How to run on Replit

1. Open https://replit.com/
2. Click "Create" → "Import from GitHub" → paste the repository URL: https://github.com/iamshagunsingh16-dot/Employee-Admin-Portal
3. Replit will import the repo. In the Repl sidebar the run command is already set to run.sh via the included .replit file.
4. Click the green "Run" button. Replit will:
   - Install the .NET SDK (using the dotnet-install script) if needed
   - Restore and publish the project
   - Run the published app and bind it to the PORT provided by Replit
5. Once started, open the webview (the Replit browser preview) or click the "Open in new tab" link — this will show the app root (/). You can also test the API at /api/employees.

Notes and troubleshooting

- The runner installs .NET 10.x from the official Microsoft dotnet-install script. This should work on Replit's Ubuntu-based containers.
- If the install fails due to network/timeout, open the Replit console, and re-run the run.sh command manually to see the failure details.
- The app falls back to SQLite for the database if no SQL Server connection string is provided, and it applies EF migrations automatically on startup. That means you don't need to configure a database to get a working demo.
- If you see a port error, Replit sets the PORT environment variable; the run script respects that. If running locally, you can simulate PORT: `PORT=5000 bash run.sh`.

Local test (optional)

You can also run the app locally using the run script (it will install dotnet locally into $HOME/.dotnet):

  bash run.sh

Or publish and run directly if you already have dotnet SDK installed:

  dotnet publish "Employee Admin Portal.csproj" -c Release -o publish
  PORT=5000 ASPNETCORE_URLS="http://0.0.0.0:5000" dotnet publish/Employee_Admin_Portal.dll

