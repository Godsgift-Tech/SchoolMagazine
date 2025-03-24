using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.AppService;
using SchoolMagazine.Application.AppUsers;
using SchoolMagazine.Application.Email_Messaging;
using SchoolMagazine.Application.Mappings;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.UserRoleInfo;
using SchoolMagazine.Infrastructure.Data;
using SchoolMagazine.Infrastructure.Data.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<ISchoolRepository, SchoolRepository>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IAdvertRepository, AdvertRepository>();
builder.Services.AddScoped<IAdvertService, AdvertService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
//builder.Services.AddScoped<ITokenService, TokenService>();
//builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<RoleManager<Role>>();



builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
builder.Services.AddSingleton<IEmailService, EmailService>();

//builder.Services.AddScoped<IEmailService, EmailService>(); // Add this line

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure EF Core with SQL Server and connection string from appsettings.json
builder.Services.AddDbContext<MagazineContext>(x => x.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    
));


// Load appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Retrieve JWT Secret
var jwtSecret = builder.Configuration["JwtSettings:Secret"];

//if (string.IsNullOrEmpty(jwtSecret))
//{
//    throw new ArgumentNullException("JwtSettings:Secret", "JWT Secret cannot be null. Check appsettings.json or environment variables.");
//}

// Add services

//builder.Services.AddIdentity<User, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();



// Configure Identity
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<MagazineContext>()
.AddDefaultTokenProviders();




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>

//     options.SaveToken = true,
//options.RequireHttpsMetadata = false,
options.TokenValidationParameters = new TokenValidationParameters
    {
       ValidateIssuer = true,
        ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
          ValidIssuer = builder.Configuration["Jwt: Issuer"],
          ValidAudience = builder.Configuration["Jwt: Audience"],
          IssuerSigningKey= new SymmetricSecurityKey (
              Encoding.UTF8.GetBytes(builder.Configuration["Jwt: Secret"]))

    });


var app = builder.Build();


// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Ensure this is before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();
