using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using Microsoft.AspNetCore.Identity;
using InventorySystem.Utils;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<MvcAppContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("MvcAppContext")));
}
else{
    builder.Services.AddDbContext<MvcAppContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionMvcAppContext")));
}
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    // Redirect to your custom login page

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;

    // User settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<MvcAppContext>()
.AddDefaultTokenProviders();

//// Add Identity
//builder.Services.AddDefaultIdentity<IdentityUser>(options => 
//{
//    // Password settings
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    // Redirect to your custom login page
     
//    // Lockout settings
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
//    options.Lockout.MaxFailedAccessAttempts = 5;
    
//    // User settings
//    options.User.RequireUniqueEmail = true;
//})
//.AddEntityFrameworkStores<MvcAppContext>()
//.AddDefaultTokenProviders();

// Add MVC
builder.Services.AddControllersWithViews();


var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("Listing indexes on EquipmentsEntity table:");
    DatabaseSchemaHelper.ListIndexes(builder.Configuration.GetConnectionString("MvcAppContext") ?? "Data Source=MvcAppContext-9de2901f-83de-4eff-8b35-2922b749ba1e.db", "EquipmentsEntity");

    //Console.WriteLine("Dropping index IX_SerialUniqueKey if it exists:");
    //DatabaseSchemaHelper.DropIndex(builder.Configuration.GetConnectionString("MvcAppContext") ?? "Data Source=MvcAppContext-9de2901f-83de-4eff-8b35-2922b749ba1e.db", "IX_SerialUniqueKey");
}
else
{
    // Production environment logic here if needed
}

// Seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        MvcMovie.Models.SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Add static files middleware to serve wwwroot content
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


