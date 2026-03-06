using _vningsuppgift_Discord_klon_grupp_;
using System.Text.Json;
using Microsoft.Extensions.Hosting;

var messages = new List<ChatMessage>
{
    new ChatMessage { User = "Alice", Message = "Hej!" },
    new ChatMessage { User = "Bob", Message = "Tjena!" }
};

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/messages", async (HttpRequest request, CancellationToken ct) =>
{
    request.Headers.TryGetValue("X-Poll", out var pollValue);
    if (pollValue == "yes")
    {
        try
        {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct, app.Lifetime.ApplicationStopping);
            await Task.Delay(TimeSpan.FromSeconds(30), linkedCts.Token);
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("Polling request cancelled.");
        }
    }
    return Results.Json(messages);
});

app.MapPost("/api/messages", (ChatMessage newMessage) =>
{
    messages.Add(newMessage);
    return Results.Created($"/api/messages/{newMessage.User}", newMessage);
});

app.Run("http://localhost:3000");
