using System.Text;
using BankSystem.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BankSystem.API.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
// ...
builder.Services.AddControllers();
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


//var accountInMemoryRepo = new List<Conta>();
//builder.Services.AddSingleton(accountInMemoryRepo);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();


    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");
app.MapControllers();

app.MapGet("/health", async (BankContext context) =>
{

    // try
    // {
    //     // Verifica se o banco de dados pode ser acessado
    //     var canConnect = await context.Database.CanConnectAsync();
    //     if (canConnect)
    //     {

    //         Console.WriteLine("Banco de dados acessível");

    //         return Results.Ok("Healthy");
    //     }
    //     else
    //     {
    //         return Results.StatusCode(503); // Serviço Indisponível
    //     }
    // }
    // catch (Exception ex)
    // {
    //     return Results.Problem($"Erro ao conectar ao banco de dados.  {ex.Message}");
    // }

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
