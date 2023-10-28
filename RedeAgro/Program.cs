using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using RedeAgro;
using RedeAgro.Config;
using RedeAgro.Intefaces;
using RedeAgro.Models;
using RedeAgro.Repositories;
using RedeAgro.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

MongoDbConfig? mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

//https://github.com/matteofabbri/AspNetCore.Identity.Mongo/blob/master/README.md
//https://csharp.hotexamples.com/pt/examples/-/ConventionPack/-/php-conventionpack-class-examples.html
//https://stackoverflow.com/questions/65589674/mongodocument-object-only-gets-partially-populated-when-querying
var pack = new ConventionPack()
{
    new CamelCaseElementNameConvention(),
    new IgnoreExtraElementsConvention(true)
};


ConventionRegistry.Register("DatabaseConventions", pack, t => true);

//https://stackoverflow.com/questions/44513786/error-on-mongodb-authentication
//https://stackoverflow.com/questions/63648217/asp-net-core-web-api-and-mongodb-with-multiple-collections
//https://stackoverflow.com/questions/57311614/how-create-indexes-for-2-fields-to-make-their-unique-in-collection
builder.Services
    .AddIdentity<UserApp, RoleApp>()
    .AddRoles<RoleApp>()
    .AddMongoDbStores<UserApp, RoleApp, Guid>
    (
        mongoDbSettings.ConnectionString, mongoDbSettings.NameDb
    );

builder.Services.AddControllersWithViews();

var config = builder.Configuration.GetSection(nameof(MongoDbConfig));
builder.Services.Configure<MongoDbConfig>(config);

builder.Services.AddScoped<ICredenciadoService, CredenciadoService>();
builder.Services.AddScoped<ICredenciadoRepository, CredenciadoRepository>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.AccessDeniedPath = "/Account/AccessDenied";
    opt.LoginPath = "/Account/Login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapRazorPages();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    List<RoleApp> roles = new List<RoleApp>() {
        new RoleApp()
        {
            Name = "ADMIN"
        },
        new RoleApp()
        {
            Name = "USER"
        }
    };

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleApp>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserApp>>();

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role.Name))
        {
            await roleManager.CreateAsync(role);
        }
    }

    DataInitializer.CadastrarPrimeirosUsuarios(userManager);
}

app.Run();
