using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.AppService;
using SchoolMagazine.Application.AppUsers;
using SchoolMagazine.Application.Mappings;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Infrastructure.Auth;
using SchoolMagazine.Infrastructure.Data;
using SchoolMagazine.Infrastructure.Data.Service;

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

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure EF Core with SQL Server and connection string from appsettings.json
builder.Services.AddDbContext<MagazineContext>(x => x.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    
));

// Add services


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

var app = builder.Build();

// Seed roles

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var roleManager = services.GetRequiredService<RoleManager<Role>>();
//    var userManager = services.GetRequiredService<UserManager<User>>();

//    // Call the SeededRole to seed roles and users
//   await SeededRole.SeedRolesAndUsers(roleManager, userManager);
//}







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
