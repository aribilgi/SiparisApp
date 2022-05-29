using BL;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            // Bu metot içerisinde projede kullanacaðýmýz servisleri tanýmlýyoruz
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSession(); // Projede session kullanabilmek için
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer()); // .net core da dbcontext i servis olarak bu þekilde eklememiz gerekiyor
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Dependency Injection ile projemize IRepository ile nesne oluþurulursa oraya Repository classýndan bir örnek göndermesini söyledik

            //Diðer Dependency Injection yöntemleri :
            // AddSingleton : Uygulama ayaða kalkarken çalýþan ConfigureServices metodunda bu yöntem ile tanýmladýðýmýz her sýnýftan sadece bir örnek oluþturulur. Kim nereden çaðýrýrsa çaðýrsýn kendisine bu örnek gönderilir. Uygulama yeniden baþlayana kadar yenisi üretilmez.
            // AddTransient : Uygulama çalýþma zamanýnda belirli koþullarda üretilir veya varolan örneði kullanýr. 
            // AddScoped : Uygulama çalýþýrken her istek için ayrý ayrý nesne üretilir.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Bu metot içerisinde app parametresiyle uygulamayla alakalý ayarlar yapýlandýrýlýr.

            if (env.IsDevelopment()) // Eðer uygulama development yani geliþtirme modundaysa
            {
                app.UseDeveloperExceptionPage(); // geliþtirme hata sistemini kullan
            }
            else // deðilse yani uygulama publish edilerek canlýya alýnmýþsa
            {
                app.UseExceptionHandler("/Home/Error"); // hata oluþtuðunda buradaki sayfayý çalýþtýr
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection(); // uygulama https ile güvenli baðlantýyý kullansýn
            app.UseStaticFiles(); // uygulamada statik dosyalarý (yani wwwroot klasöründeki css, js, resim gibi) kullanýmýný aktif et

            app.UseRouting(); // uygulamada routing i aktif et

            app.UseAuthorization(); // güvenliði aktif et, oturum açma iþlemlerini

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "Admin", // Admin paneline giriþ iþin bu kodlarý ekledik. Admin de buradaki url yapýsý kullanýlacak
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute( // .net core da endpoints-uç nokta sistemi ile routing kullanýlýyor
                    name: "default", // route adýmýz
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // route çalýþma sistemi
                                                                        // {controller=Home} buraya controller adý gelecek, gelmezse varsayýlan olarak home u kullan demek, /{action=Index} action gelecek, gönderilmezse Index actionunu çalýþtýr demek, /{id?} ise buraya id deðeri gelecek ama ? iþareti ile buranýn opsiyonel-yani boþ geçilebileceðini belirttik
            });
        }
    }
}
