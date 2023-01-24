using Domain.Logic.Interfaces;
using Domain.UseCases;
using Domain.Logic;
using Domain;
using Database;
using Database.Repository;
using Microsoft.EntityFrameworkCore;
using Domain.Logic.Interfaces;
using Domain.UseCases;
using IT_Project.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
    
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("Host=localhost;Port=5432;Database=backend;" +
"Username=postgres;Password=12345678")));
builder.Services.AddDbContext<ApplicationContext>(options =>
options.EnableSensitiveDataLogging(true));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IScheduleRepository, ScheduleRepository>();
builder.Services.AddTransient<IAppointmentRepository, AppointmentsRepository>();
builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddTransient<UserInteractor>();
builder.Services.AddTransient<DoctorInteractor>();
builder.Services.AddTransient<ScheduleInteractor>();
builder.Services.AddTransient<AppointmentInteractor>();
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidAudience = AuthOptions.AUDIENCE,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
    };
    });





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
app.Run();
