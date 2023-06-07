using Application.Commons;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//register custom configuration
var configuration = builder.Configuration.Get<AppConfiguration>();
builder.Services.AddSingleton(configuration);

// Add dbcontext ---> Remember to delete at the end
builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(configuration.ConnectionStrings.DefaultDB));

// Add services to the container.
builder.Services.AddRazorPages();

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

app.Run();
