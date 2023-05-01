using Microsoft.EntityFrameworkCore;
using Portal.Core.Interface;
using Portal.Core.Mapper;
using Portal.Core.Repository;
using Portal.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();

#region Connection String Configuration

var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

#endregion

#region Auto Mapper Configuration

builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

#endregion

#region Services

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDepartmentRep, DepartmentRep>();
builder.Services.AddScoped<IEmployeeRep, EmployeeRep>();
builder.Services.AddScoped<ICountryRep, CountryRep>();
builder.Services.AddScoped<ICityRep, CityRep>();
builder.Services.AddScoped<IDistrictRep, DistrictRep>();

#endregion
#region CORS
builder.Services.AddCors();
#endregion
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options => options
            //serverData
            .AllowAnyOrigin()
            //control ("GET,POST,PUT,DELETE")
            .AllowAnyMethod()
            // control ("XML,JSON")
            .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
