using MeterMaintenance.EF;
using MeterMaintenance.Services.MeterMaintenanceService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("SqlServerExpressConnection");

// Add services to the container.
builder.Services.AddDbContext<IMeterMaintenanceContext, MeterMaintenanceContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddTransient<IMeterMaintenanceService, MeterMaintenanceService>();
builder.Services.AddHttpContextAccessor();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "apartments",
    pattern: "{controller=Apartment}/{action=AllApartments}");

app.MapControllerRoute(
    name: "meters",
    pattern: "{controller=Meter}/{action=CheckRequired}/{street}/{house}");

app.Run();