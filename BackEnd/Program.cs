using System.Text;
using CorsIssue;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var allowedHosts = builder.Configuration
                          .GetSection("AllowedCORS")
                          .Get<string[]>();
builder.Services
       .AddAuthorization()
       .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(opt =>
       {
           opt.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = false,
               ValidateAudience = false,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               IssuerSigningKey =
                   new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes("TheB3st0fs3cr3tk3ys"))
           };
       });

builder.Services
       .AddGraphQLServer()
       .AddQueryType()
           .AddTypeExtension<GraphQuery>()
       .AddAuthorization()
       .InitializeOnStartup();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(b =>
    {
        b.WithOrigins(allowedHosts)
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials();
    });
});

var app = builder.Build();

app.UseAuthentication()
   .UseAuthorization()
   .UseCors();

app.MapGraphQLHttp()
   // If you uncomment this line the CORS will stops work
   //.RequireAuthorization()
   ;
app.Run();
