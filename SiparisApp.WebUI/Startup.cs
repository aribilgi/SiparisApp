using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiparisApp.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        // Bu metot i�erisinde app parametresiyle uygulamayla alakal� ayarlar yap�land�r�l�r.

            if (env.IsDevelopment()) // E�er uygulama development yani geli�tirme modundaysa
            {
                app.UseDeveloperExceptionPage(); // geli�tirme hata sistemini kullan
            }
            else // de�ilse yani uygulama publish edilerek canl�ya al�nm��sa
            {
                app.UseExceptionHandler("/Home/Error"); // hata olu�tu�unda buradaki sayfay� �al��t�r
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection(); // uygulama https ile g�venli ba�lant�y� kullans�n
            app.UseStaticFiles(); // uygulamada statik dosyalar� (yani wwwroot klas�r�ndeki css, js, resim gibi) kullan�m�n� aktif et

            app.UseRouting(); // uygulamada routing i aktif et

            app.UseAuthorization(); // g�venli�i aktif et, oturum a�ma i�lemlerini

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute( // .net core da endpoints-u� nokta sistemi ile routing kullan�l�yor
                    name: "default", // route ad�m�z
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // route �al��ma sistemi
                                                                        // {controller=Home} buraya controller ad� gelecek, gelmezse varsay�lan olarak home u kullan demek, /{action=Index} action gelecek, g�nderilmezse Index actionunu �al��t�r demek, /{id?} ise buraya id de�eri gelecek ama ? i�areti ile buran�n opsiyonel-yani bo� ge�ilebilece�ini belirttik
            });
        }
    }
}
