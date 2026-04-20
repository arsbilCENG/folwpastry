using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using PastryFlow.API.Middleware;
using PastryFlow.Application.Interfaces;
using PastryFlow.Application.Mappings;
using PastryFlow.Application.Services;
using PastryFlow.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<PastryFlowDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPastryFlowDbContext>(provider => provider.GetRequiredService<PastryFlowDbContext>());

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

// Application Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IDemandService, DemandService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IDayClosingService, DayClosingService>();
builder.Services.AddScoped<IWasteService, WasteService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// JWT Authentication
var jwtSecret = builder.Configuration["Jwt:Secret"] ?? throw new InvalidOperationException("Jwt:Secret is missing");
var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Auto-migration & Seeding
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PastryFlowDbContext>();
    try
    {
        // Sprint 2 one-time fixup: move products from EKMEK to FIRIN BEFORE migration
        // to avoid Foreign Key violation during Migration's SeedData deletion.
        var ekmekId = "22222222-2222-2222-2222-222222222202";
        var firinId = "22222222-2222-2222-2222-222222222207";
        
        // Use raw SQL to ensure it runs independently of the current model state
        context.Database.ExecuteSqlRaw($@"
            UPDATE ""Products"" 
            SET ""CategoryId"" = '{firinId}' 
            WHERE ""CategoryId"" = '{ekmekId}'");
        
        Console.WriteLine("[Sprint2] Pre-migration fixup: Products moved from EKMEK to FIRIN.");

        context.Database.Migrate();

        // One-time deletion of EKMEK category if it still exists (after products moved)
        context.Database.ExecuteSqlRaw($@"
            DELETE FROM ""Categories"" 
            WHERE ""Id"" = '{ekmekId}'");

        // Fix category SortOrders if they have old values
        var categories = context.Categories.ToList();
        var sortMap = new Dictionary<string, int> {
            { "KEK", 1 }, { "MAYALILAR", 2 }, { "KURABİYE", 3 },
            { "PASTALAR", 4 }, { "İÇECEK", 5 }, { "FIRIN", 6 }, { "HAMMADDE", 7 }
        };
        foreach (var cat in categories)
        {
            if (sortMap.TryGetValue(cat.Name, out var expectedOrder) && cat.SortOrder != expectedOrder)
                cat.SortOrder = expectedOrder;
        }
        context.SaveChanges();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration/Fixup Error: {ex.Message}");
    }
}

// Ensure uploads folder exists
var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
if (!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads"
});

// Swagger available in all environments for testing
app.UseSwagger();
app.UseSwaggerUI();


app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
