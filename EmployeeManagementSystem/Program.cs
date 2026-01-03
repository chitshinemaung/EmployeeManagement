using EmployeeManagementSystem.Database.EmployeeDB;
using EmployeeManagementSystem.Middlewares;
using EmployeeManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ILoginUserRepositories,LoginUserRepositories>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddDistributedMemoryCache(); 

builder.Services.AddSession(options =>

{
    options.IdleTimeout = TimeSpan.FromHours(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}); 

builder.Services.AddDbContext<EmployeeDBContxt>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDatabaseString"));
}, ServiceLifetime.Transient);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization(); 

app.UseSession(); 
app.UseSessionMiddleware(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=UserLoginIndex}/{id?}");

app.Run();