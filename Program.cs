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

CancellationTokenSource postCT = new CancellationTokenSource();

app.MapGet("/api/messages", async (HttpRequest request, CancellationToken ct) =>
{
    Console.WriteLine("Received GET /api/messages request.");
    request.Headers.TryGetValue("X-Poll", out var pollValue);
    if (pollValue == "yes")
    {
        try
        {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                ct, 
                app.Lifetime.ApplicationStopping,
                postCT.Token);
            await Task.Delay(TimeSpan.FromSeconds(30), linkedCts.Token);
        }
        catch (TaskCanceledException)
        {
            return Results.Json(messages);
            Console.WriteLine("Polling request cancelled.");
        }
    }
    return Results.Json(messages);
});

app.MapPost("/api/messages", (ChatMessage newMessage) =>
{
    postCT.Cancel();
    postCT.Dispose();
    postCT = new CancellationTokenSource();
    messages.Add(newMessage);
    return Results.Created($"/api/messages/{newMessage.User}", newMessage);
});

app.Run("http://localhost:3000");
