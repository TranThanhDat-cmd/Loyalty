using AutoMapper;
using Loyalty.Configuration;
using Loyalty.Core.IRepositories;
using Loyalty.Core.Repositories;
using Loyalty.Data;
using Loyalty.Data.Entities;
using Loyalty.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;




var builder = WebApplication.CreateBuilder(args);



// mapping appsetting with obj introduced in .net 3.1
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
});

// add DI db context 
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LoyaltyDb")));

// add auth using jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwt =>
{
    var secretKey = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"]);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        // tự cấp token
        ValidateIssuer = false,
        ValidateAudience = false,


        // ký vào token
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),


        ValidateLifetime = false,
        RequireExpirationTime = false
    };

});

// add DI repository
builder.Services.AddScoped<IRoleRepository, RoleRepository>();


// Di automapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


// add user manager
builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddRoles<Role>().AddEntityFrameworkStores<MyDbContext>();







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
