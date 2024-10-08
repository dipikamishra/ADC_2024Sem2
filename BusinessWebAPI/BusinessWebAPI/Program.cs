using BusinessWebAPI.Models;
var builder = WebApplication.CreateBuilder(args);

// Register HttpClient
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddControllers();


var app = builder.Build();

app.UseCors();  // Apply the CORS policy

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//StudentList.Generate();
app.Run();
