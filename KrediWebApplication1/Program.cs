using Microsoft.EntityFrameworkCore;
using KrediWebApplication1.Data;
using KrediWebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantý dizesini yapýlandýr
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// UserRepository'yi Dependency Injection konteynerine ekleyin
builder.Services.AddScoped<UserRepository>();

// Diðer servisleri yapýlandýr
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware ve endpoint'leri yapýlandýr
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
