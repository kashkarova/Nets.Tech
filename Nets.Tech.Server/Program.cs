using Nets.Tech.Server.Components;
using Nets.Tech.Server.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IBroadcastService, BroadcastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

var webSocketOptions = new WebSocketOptions()
{
   // KeepAliveInterval = TimeSpan.FromSeconds(5),
};

app.UseWebSockets(webSocketOptions);

app.Map("/ws", async (HttpContext _, IBroadcastService _broadcastService) => 
{
    if (_.WebSockets.IsWebSocketRequest)
    {
        using var webSocket = await _.WebSockets.AcceptWebSocketAsync();
        await _broadcastService.ConnectAsync(webSocket);
    }
    else
    {
        _.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
