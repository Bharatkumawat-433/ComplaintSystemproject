// ===== Program.cs का पूरा नया कोड (SQLite के लिए) =====
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ComplaintSystem.Data; // हमारे ApplicationDbContext और ApplicationUser के लिए

var builder = WebApplication.CreateBuilder(args);

// ----- SQLite कनेक्शन -----
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)); // <-- हम SQLite का उपयोग कर रहे हैं

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ----- स्टेप 7.A: Roles इनेबल करना और ApplicationUser का उपयोग -----
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false) // <-- बदला हुआ: IdentityUser की जगह ApplicationUser
    .AddRoles<IdentityRole>() // <-- जोड़ा गया: Roles को इनेबल करने के लिए
    .AddEntityFrameworkStores<ApplicationDbContext>();
    
builder.Services.AddRazorPages();

var app = builder.Build();

// ----- स्टेप 7.B: डिफ़ॉल्ट Admin और Roles बनाने का कोड (Seeding) -----
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        // यह सुनिश्चित करता है कि डेटाबेस बना हुआ है
        context.Database.EnsureCreated(); 
        
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        
        // Roles बनाएँ
        string[] roles = { "Admin", "User" };
        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // एक डिफ़ॉल्ट Admin बनाएँ
        var adminEmail = "admin@test.com";
        var adminPassword = "Admin@123"; 

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                Name = "Admin User", 
                Role = "Admin",     
                EmailConfirmed = true 
            };
            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}
// ----- Seeding कोड यहाँ खत्म होता है -----


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 

app.UseRouting();

app.UseAuthorization(); // Login/Logout के लिए ज़रूरी

app.MapRazorPages(); 

app.Run();
// ======================================================