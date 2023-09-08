using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineStore.Application;
using OnlineStore.Infrastructure;
using OnlineStore.Persistence;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// AddAsync services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.CustomSchemaIds(type => type.ToString());

    options.CustomOperationIds(apiDescription =>
        apiDescription.TryGetMethodInfo(out var methodInfo)
            ? methodInfo.Name
            : null);

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("https://localhost:5001/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"OnlineStoreApi", "OnlineStore"}
                }
            }
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "OnlineStoreApi");
    })
);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddInfrastructureRegistration();
builder.Services.AddApplicationRegistration();
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DisplayOperationId();
        options.OAuthClientId("online_store_client_api");
        options.OAuthClientSecret("online_store_client_secret");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.UseStaticFiles();

app.Run();
