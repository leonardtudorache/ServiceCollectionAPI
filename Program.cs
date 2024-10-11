using ServiceCollectionAPI.Repositories;
using ServiceCollectionAPI.Repositories.Interfaces;
using ServiceCollectionAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ServiceCollectionAPI.Services.Interfaces;
using ServiceCollectionAPI.Services.Interfaces.UserManagement;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = "{YOUR_URL}";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "{YOUR_URL}",
        ValidateAudience = true,
        ValidAudience = "{YOUR_AUDIENCE}}",
        ValidateLifetime = true
    };
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthorization", policy =>
    {
        if (builder.Configuration.GetValue<bool>("IsProduction"))
        {
            policy.RequireAuthenticatedUser();
        }
        else
        {
            policy.Requirements.Add(new AllowAnonymousRequirement());
        }
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, AllowAnonymousHandler>();

// Configuration
ConfigurationManager configuration = builder.Configuration;

//Repositories
builder.Services.AddTransient(typeof(IMongoRepository<>), typeof(MongoRepository<>));

//Services
builder.Services.AddTransient(typeof(IUserService), typeof(UserService));
builder.Services.AddTransient(typeof(ITenantService), typeof(TenantService));
builder.Services.AddTransient(typeof(IRoleService), typeof(RoleService));
builder.Services.AddTransient(typeof(IProductService), typeof(ProductService));
builder.Services.AddTransient(typeof(IEmployeeService), typeof(EmployeeService));
builder.Services.AddScoped(typeof(ITenantContextService), typeof(TenantContextService));
builder.Services.AddScoped(typeof(IEmailHelper), typeof(EmailHelper));
builder.Services.AddTransient(typeof(IPackageService), typeof(PackageService));
builder.Services.AddTransient(typeof(IOfferService), typeof(OfferService));
builder.Services.AddTransient(typeof(IClientOfferService), typeof(ClientOfferService));
builder.Services.AddTransient(typeof(IClientService), typeof(ClientService));
builder.Services.AddTransient(typeof(IInvoiceService), typeof(InvoiceService));
builder.Services.AddTransient(typeof(IContractService), typeof(ContractService));
builder.Services.AddTransient(typeof(IBusinessService), typeof(BusinessService));




builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // Disable the default PascalCase naming policy
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // Make the property names case-insensitive
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Service Collection API", Version = "v0" });
    options.SchemaFilter<LowerCaseSchemaFilter>();
});

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseAuthentication();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Collection API");
});

app.UseAuthorization();
app.MapControllers();
app.Run();
