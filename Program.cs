using FurnitureStore3.Data;
using FurnitureStore3.Domain.Entities;
using FurnitureStore3.Domain.Services;
using FurnitureStore3.Infrastructure;
using FurnitureStore3.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services
.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(opt =>
{
    opt.ExpireTimeSpan = TimeSpan.FromHours(1);
    opt.Cookie.Name = "store_session";
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.LoginPath = "/User/Login";
});

builder.Services.AddDbContext<FurnitureContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("local")));
builder.Services.AddScoped<IRepository<User>, EFRepository<User>>();
builder.Services.AddScoped<IRepository<Role>, EFRepository<Role>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRepository<Product>, EFRepository<Product>>();
builder.Services.AddScoped<IRepository<Category>, EFRepository<Category>>();
builder.Services.AddScoped<IProductsReader, ProductsReader>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IRepository<Order>, EFRepository<Order>>();
builder.Services.AddScoped<IOrdersReader,OrdersReader>();
builder.Services.AddScoped<IOrdersService,OrdersService>();

DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("local"));
//using (var context = new FurnitureContext(optionsBuilder.Options))
//{
//    EFInitialSeed.Seed(context);
//}

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseProductsProtection(); 
app.UseStaticFiles();

app.MapControllerRoute("default", "{Controller=Products}/{Action=Index}");
app.MapControllerRoute("default", "{Controller=Orders}/{Action=MyOrders}");
app.Run();