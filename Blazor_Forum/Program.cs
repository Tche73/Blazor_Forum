using Blazor_Forum.Auth;
using Blazor_Forum.Components;
using Blazor_Forum.Repositories;
using Blazor_Forum.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<AuthService>();
// Configure le client HTTP pour communiquer avec l'API
builder.Services.AddHttpClient<IForumService, ForumService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7024");
});

// Ajouter les services d'authentification AVANT builder.Build()
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();

// Construire l'application APRÈS avoir enregistré tous les services
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