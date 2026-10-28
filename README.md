# Anime Quotes

A lightweight ASP.NET Core web application that displays a randomly selected anime quote, together with the anime and character names. Each visit to the home page requests a new quote from the [Animechan API](https://animechan.io/).

## Features

- Retrieves a random anime quote when the home page is opened.
- Displays the anime title, character name, and quote in a simple dark-themed page.
- Encodes API-provided text before rendering it in HTML.
- Shows a readable fallback message if the quote service is unavailable.
- Uses the `PORT` environment variable for the listening port.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- An internet connection to retrieve quotes from Animechan

## Run locally

Clone the repository, open a terminal in the project directory, and run:

```powershell
$env:PORT = "5004"
dotnet run
```

Then open [http://localhost:5004](http://localhost:5004). The application listens on port `80` when `PORT` is not set. Choose an available port appropriate for your environment.

On other shells, set `PORT` using the syntax for that shell before running `dotnet run`. For example, on macOS or Linux:

```bash
PORT=5004 dotnet run
```

## Configuration

| Variable | Default | Description |
| --- | --- | --- |
| `PORT` | `80` | TCP port on which the application listens. |

The quote service is requested from `https://api.animechan.io/v1/quotes/random`. Requests have a five-second timeout. If the request fails or the service returns unexpected data, the page displays a fallback message.

## Project structure

| File | Purpose |
| --- | --- |
| `Program.cs` | Configures the web application and serves the quote page. |
| `webapp-github.csproj` | Defines the ASP.NET Core project and .NET target framework. |
| `appsettings.json` | Contains the default application logging configuration. |
| `Properties/launchSettings.json` | Defines local development launch profiles. |
| `starter.bat` | Starts the application with `dotnet run` on Windows. |
