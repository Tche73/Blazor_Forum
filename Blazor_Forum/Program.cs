using Blazor_Forum.Components;
using Blazor_Forum.Repositories;
using Blazor_Forum.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure le client HTTP pour communiquer avec l'API
builder.Services.AddHttpClient<IForumService, ForumService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7024");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();