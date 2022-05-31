using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using session1_protectAPI_CRUD.Repositories;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration);

// Enable PII for logging
IdentityModelEventSource.ShowPII = true;

// Configure middleware events
builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = ctx =>
        {
            var accessToken = ctx.SecurityToken;
            Debug.WriteLine("[OnTokenVaidated]: I can do stuff here! ");
            //Debug.WriteLine("[OnTokenVaidated]: " + accessToken);
            //Debug.WriteLine("[OnTokenVaidated]: Claims of the user context");
            //Debug.WriteLine("[OnTokenVaidated]: "+ ctx.Principal.Claims);
            return Task.CompletedTask;
        },
        OnMessageReceived = ctx =>
        {
            Debug.WriteLine("[OnMessageReceived]: I can do stuff here! ");
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = ctx =>
        {
            Debug.WriteLine("[OnAuthenticationFailed]: Authentication failed with the following error: ");
            Debug.WriteLine(ctx.Exception);
            return Task.CompletedTask;
        },
        OnChallenge = ctx =>
        {
            Debug.WriteLine("[OnChallenge]: I can do stuff here! ");
            return Task.CompletedTask;
        }
    };
});

// Registering the repository as a singleton 
builder.Services.AddSingleton<IWeatherForecastsRepository,InMemWeatherForecastsRepository>();

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
