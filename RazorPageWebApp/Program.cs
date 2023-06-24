using Application.Commons;
using Application.IServices;
using DataAccess;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RazorPageWebApp;
using RazorPageWebApp.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//register custom configuration

//Add DIs
builder.Services.AddInfrastrucureService();

// Add dbcontext ---> Remember to delete at the end
builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDB")));

//Add web app services
builder.Services.AddWebAppServices();

// Add services to the container.
builder.Services.AddRazorPages()
                .AddMvcOptions(x => x.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true) // allow Model State checking
                .AddJsonOptions(option=>option.JsonSerializerOptions.ReferenceHandler =  ReferenceHandler.IgnoreCycles);// do what it does
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

// use session
app.UseSession();

app.Run();
