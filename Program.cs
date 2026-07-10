using Microsoft.EntityFrameworkCore;
using OrganizasyonSitesi.Data;
using OrganizasyonSitesi.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;  // sembol zorunlu olmasın
    options.Password.RequireUppercase = true;
    options.Lockout.MaxFailedAccessAttempts = 5;      // 5 yanlış deneme = kilit
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
})
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Giris";        // korumalı sayfaya girmeye çalışan buraya yönlenir
    options.AccessDeniedPath = "/Admin/Giris";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
});

builder.Services.AddScoped<IHizmetService, HizmetService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Hata");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/BulunamayanSayfa");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// İlk açılışta admin kullanıcısını oluştur (yoksa)
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var adminEmail = builder.Configuration["AdminUser:Email"];
    var adminSifre = builder.Configuration["AdminUser:Sifre"];

    if (!string.IsNullOrEmpty(adminEmail) && !string.IsNullOrEmpty(adminSifre))
    {
        var mevcut = await userManager.FindByEmailAsync(adminEmail);
        if (mevcut == null)
        {
            var admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(admin, adminSifre);
        }
    }
}

app.Run();
