using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RezervasyonUcak.Areas.Employees.Models;
using RezervasyonUcak.Areas.Employees.Models.Repository;
using RezervasyonUcak.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
byte[] key = Encoding.UTF8.GetBytes("secret-key32er242rdfecg45t34rfgt4fef34f3f");
//var jwtOptions= builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();


builder.Services.AddControllersWithViews().AddJsonOptions(x =>
				x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var connectionString = builder.Configuration.GetConnectionString("MyWebApiConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(


    options =>
    {
        options.Cookie.Name = "auth";
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/Login";

    }
    );

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.WriteIndented = true;
    });



//builder.Services.AddSingleton(jwtOptions);
/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.Cookie.Name = "MyProjectCookie";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set your desired expiration time (e.g., 30 minutes)
    options.SlidingExpiration = true;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
    };
});

// Add services to the container.

*/


//builder.Services.AddScoped<ITokenHandler, RezervasyonUcak.Models.Token.TokenHandler>();
builder.Services.AddScoped<ImusteriRepository,MusteriRepository>();
builder.Services.AddScoped< UserRepository>();
builder.Services.AddScoped<IUcusSeferRepository, UcuSeferRepositroy>();

//builder.Services.AddScoped<DataController>();


var app = builder.Build();


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();


app.UseAuthorization();


app.UseEndpoints(endpoints =>

{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
       areaName: "Admin",
        pattern: "Admin/{controller=AdminPanel}/{action=Anasayfa}/{id?}"
        );
    endpoints.MapAreaControllerRoute(
          name: "Employees",
          areaName: "Employees",
          pattern: "Employees/{controller=Musteri}/{action=Anasayfa}/{id?}"
          ); endpoints.MapDefaultControllerRoute();

}
);


app.MapControllerRoute(
   name: "default",
   pattern: "Admin/{controller=AdminPanel}/{action=Anasayfa}/{id?}"
//  pattern: "{controller=Auth}/{action=Login}/{id?}");
);
app.Run();
