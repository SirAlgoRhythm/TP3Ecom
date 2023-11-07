namespace NoixMagicroquanteWebsite
{
    public class Startup
    {
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

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
