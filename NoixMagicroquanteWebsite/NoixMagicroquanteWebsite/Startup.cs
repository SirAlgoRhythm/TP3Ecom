using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace NoixMagicroquanteWebsite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Ajouter la prise en charge de la session
            services.AddSession(options =>
            {
                // Configuration des options de session
                options.Cookie.Name = ".NoixMagicroquanteWebsite.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            // Configuration des services MVC
            services.AddControllersWithViews();

            // Ajouter et configurer le service de contexte de base de données pour Identity
            services.AddDbContext<NoixMagicroquanteWebsiteContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Ajouter le service Identity et configurer les options de Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<NoixMagicroquanteWebsiteContext>()
                    .AddDefaultTokenProviders();

            // Configurations supplémentaires pour Identity ici si nécessaire, par exemple:
            // services.Configure<IdentityOptions>(options => { ... });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Ajouter le middleware de session avant le middleware MVC
            app.UseSession();

            // Activer le middleware pour servir l'authentification
            app.UseAuthentication();

            app.UseRouting();

            // Activer le middleware pour utiliser l'autorisation
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
