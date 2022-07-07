using Microsoft.EntityFrameworkCore;
using Infrastructure.EF;
using Services.Interfaces;
using Services;
using Model;
using Infrastructure.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<UniversityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSUniverDB")));

builder.Services.AddScoped<IRepository<Course>, CourseRepository>();
builder.Services.AddScoped<IRepository<Group>, GroupRepository>();
builder.Services.AddScoped<IRepository<Student>, StudentRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOperationsViewModel, OperationsViewModel>();

builder.Services.AddScoped<ICRUD<Course>, CourseService>();
builder.Services.AddScoped<ICRUD<Group>, GroupService>();
builder.Services.AddScoped<ICRUD<Student>, StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Course}/{action=AllCourses}/{id?}");

app.Run();

public partial class Program { }