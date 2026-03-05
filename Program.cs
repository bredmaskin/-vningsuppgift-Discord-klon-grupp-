var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    WebRootPath = "public"
});

var app = builder.Build();
app.UseFileServer();

app.Run("http://localhost:3000");