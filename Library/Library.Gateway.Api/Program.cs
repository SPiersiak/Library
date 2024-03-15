using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Commons.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile("ocelot.json", false, false);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddJwtAuthentication();

var app = builder.Build();

await app.UseOcelot();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
