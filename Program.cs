using System.Text;
using BankSystem.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BankSystem.API.Services;
using SeuProjeto.Extensions;
using BankSystem.Extensions;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BankContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankDatabase")));

//*** JWT
var secretKey = "senha";
var key = Encoding.ASCII.GetBytes(secretKey);
var issuer = builder.Configuration["JwtSettings:Issuer"];
var audience = builder.Configuration["JwtSettings:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    object jwtSettings = null;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience
    };
});

//*


//** 
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();


builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddAuthorization();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {

        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });



var app = builder.Build();
app.UseCustomExceptionHandler();
app.ApplyDatabaseMigrations();


if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();


    app.UseSwagger();
    app.UseSwaggerUI();
}




var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapControllers();

app.MapGet("/health", async (BankContext context) =>
{


    try
    {
        bool canConnect = await context.Database.CanConnectAsync();
        if (canConnect)
        {
            return Results.Ok("O banco está funcionando.");
        }
        else
        {
            return Results.Problem("Não foi possível conectar ao banco de dados.");
        }
    }
    catch (Exception ex)
    {
        return Results.Problem($"Meu banco falhou: {ex.Message}");
    }
});
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
