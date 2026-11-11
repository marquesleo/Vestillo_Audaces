using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TemplateAudacesApi.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.IgnoreNullValues = true;
    var settings = new JsonSerializerSettings();

    settings.NullValueHandling =  NullValueHandling.Ignore;
    settings.DefaultValueHandling = DefaultValueHandling.Ignore;
});

var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();

// Força o carregamento das DLLs da Vestillo referenciando tipos conhecidos
_ = typeof(Vestillo.Connection.ProviderFactory).Assembly;
_ = typeof(Vestillo.Lib.Funcoes).Assembly;
_ = typeof(Vestillo.Business.Models.Produto).Assembly;

// Vestillo.Core: carrega pelo diretório das demais (não tem tipo exposto diretamente)
var vestilloDir = Path.GetDirectoryName(typeof(Vestillo.Business.Models.Produto).Assembly.Location)!;
var corePath = Path.Combine(vestilloDir, "Vestillo.Core.dll");
if (File.Exists(corePath))
    System.Reflection.Assembly.LoadFrom(corePath);

// Log das DLLs da Vestillo carregadas em runtime
Console.WriteLine("===== VESTILLO DLLs CARREGADAS =====");
foreach (var asm in AppDomain.CurrentDomain.GetAssemblies()
    .Where(a => a.FullName != null && a.FullName.StartsWith("Vestillo", StringComparison.OrdinalIgnoreCase)))
{
    var location = string.IsNullOrEmpty(asm.Location) ? "(sem localização - carregado de memória)" : asm.Location;
    Console.WriteLine($"  {asm.GetName().Name} v{asm.GetName().Version} => {location}");
}
Console.WriteLine("====================================");





// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

