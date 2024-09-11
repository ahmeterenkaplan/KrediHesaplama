using Microsoft.EntityFrameworkCore;
using KrediWebApplication1.Data;
using KrediWebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantý dizesini yapýlandýr
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// UserRepository'yi Dependency Injection konteynerine ekleyin
builder.Services.AddScoped<UserRepository>();

// CORS ayarlarýný ekleyin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // React uygulamasýnýn çalýþtýðý adres
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Diðer servisleri yapýlandýr
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

// JSON iþlemleri için eðer gerekiyorsa Newtonsoft.Json ekleyebilirsiniz
// builder.Services.AddControllers().AddNewtonsoftJson(); // Eðer kullanmak isterseniz açabilirsiniz

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

// CORS'u middleware'e ekleyin
app.UseCors("AllowReactApp");

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
