using DigitalProductsApi.src.Api.Middleware;
using DigitalProductsApi.src.Application.Interfaces;
using DigitalProductsApi.src.Application.Mapping;
using DigitalProductsApi.src.Application.Services;
using DigitalProductsApi.src.Application.Validators.AuthValidators;
using DigitalProductsApi.src.Application.Validators.CategoryValidators;
using DigitalProductsApi.src.Application.Validators.ProductValidatos;
using DigitalProductsApi.src.Application.Validators.PurchaseValidators;
using DigitalProductsApi.src.Infrastructure.Data;
using DigitalProductsApi.src.Infrastructure.Identity;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

// DB + Identity
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// JWT and add authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Secret"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };

});
// add authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("role", "Admin"));
});
// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ProductProfile>();
    cfg.AddProfile<CategoryProfile>();
    cfg.AddProfile<UserProfile>();
    cfg.AddProfile<PurchaseProfile>();
});

builder.Services.AddControllers();
// FluentValidation 
builder.Services.AddFluentValidationAutoValidation();
// use and build the AddFluentValidationClientsideAdapters if the frontend comes  
//builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(CreateProductDtoValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(UpdateProductDtoValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(CreateCategoryDtoValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(UpdateCategoryDtoValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(RegisterUserDtoValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(CreatePurchaseDtoValidator).Assembly);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Development-Swagger
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Global Exception Handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
