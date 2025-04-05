using EventEaseWebApp.Data; // Reference to the data context (EF Core)
using Microsoft.EntityFrameworkCore; // Needed for UseSqlServer()

// Create a builder to configure app services and pipeline
var builder = WebApplication.CreateBuilder(args);

// Debug output: prints the Azure SQL connection string to console
// Useful for confirming database connection at runtime
Console.WriteLine("Connecting to: " + builder.Configuration.GetConnectionString("DefaultConnection"));

// Register MVC services for controller and view rendering
builder.Services.AddControllersWithViews();

// Register ApplicationDbContext and configure it to use SQL Server
// The connection string is read from appsettings.json (or Azure environment)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Build the web application
var app = builder.Build();

// Configure error handling for production
// If not in Development, show a friendly error page (instead of stack traces)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Uses HomeController's Error view
}

// Serve static files (CSS, JS, images, etc.) from wwwroot/
app.UseStaticFiles();

// Enable request routing (matches URLs to controller/actions)
app.UseRouting();

// Enable authorization middleware
app.UseAuthorization();

// Define default route pattern (e.g., /Home/Index or /Venues/Details/3)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Start the app and begin listening for HTTP requests
app.Run();
