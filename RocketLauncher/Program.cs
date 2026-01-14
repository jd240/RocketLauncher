using Services;
using DataTransferObject;
using Service;
using Microsoft.EntityFrameworkCore;
using Entities;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<TenantIService, TenantService>();
builder.Services.AddScoped<UserIService, UserService>();
builder.Services.AddDbContext<UserDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
if(builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
