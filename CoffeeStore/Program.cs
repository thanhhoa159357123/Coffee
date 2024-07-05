using CoffeeStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CoffeeDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("CoffeesStoreConnection"));
});
builder.Services.AddScoped<ICoffeeRepository, EFCoffeeRepository>();
builder.Services.AddScoped<ICustomerRepository, EFCustomerRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
builder.Services.AddRazorPages();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

app.UseRouting();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

CoffeeData.EnsurePopulated(app);

app.Run();
