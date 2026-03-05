using _vningsuppgift_Discord_klon_grupp_;
using System.Text.Json;

var messages = new List<ChatMessage>
{
    new ChatMessage { User = "Alice", Message = "Hej!" },
    new ChatMessage { User = "Bob", Message = "Tjena!" }
};

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/messages", () =>
{
    return Results.Json(messages);
});

app.MapPost("/api/messages", (ChatMessage newMessage) =>
{
    messages.Add(newMessage);
    return Results.Created($"/api/messages/{newMessage.User}", newMessage);
});

app.Run("http://localhost:3000");
