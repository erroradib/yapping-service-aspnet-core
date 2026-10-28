using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
var app = builder.Build();


app.MapGet("/", async (IHttpClientFactory factory) =>
{
    string anime, character, content;
    try
    {
        var client = factory.CreateClient();
        client.Timeout = TimeSpan.FromSeconds(5);

        var json = await client.GetFromJsonAsync<JsonElement>(
            "https://api.animechan.io/v1/quotes/random");

        var data = json.GetProperty("data");
        anime = data.GetProperty("anime").GetProperty("name").GetString() ?? "Unknown";
        character = data.GetProperty("character").GetProperty("name").GetString() ?? "Unknown";
        content = data.GetProperty("content").GetString() ?? "";
    }
    catch (Exception)
    {
        anime = "Oops!";
        character = "Quote service unavailable T_T";
        content = "Couldn't fetch a quote right now. Refresh in a bit.";
    }

    var html = $"""
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1">
            <title>Anime Quotes</title>
        </head>
        <body style="background:#101218; color:#e5e7eb; font-family:Arial,sans-serif; padding:40px;">
            <p style="color:#9ca3af; text-align:center;">ANIME QUOTE</p>
            <hr>
            <h1 style="color:white;">{WebUtility.HtmlEncode(anime)}</h1>
            <h3 style="color:#a78bfa;">{WebUtility.HtmlEncode(character)}</h3>
            <blockquote style="font-size:22px; line-height:1.7;">
                <i>"{WebUtility.HtmlEncode(content)}"</i>
            </blockquote>
        </body>
        </html>
        """;

    return Results.Content(html, "text/html; charset=utf-8");
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "80";
app.Run($"http://0.0.0.0:{port}");