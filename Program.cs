var app = WebApplication.Create(args);
app.MapGet("/", () => "Hello World!");

// Middleware
app.UseCors("");

// Servera statiska filer från wwwroot (index.html, css, js)
app.UseDefaultFiles();  // index.html som standardfil
app.UseStaticFiles();   // css, js, bilder
app.UseAuthorization();

app.Run("http://localhost:3000");