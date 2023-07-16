
using Gestion_Rendimiento_Data;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_Entity.Model;
using Gestion_Rendimiento_IService;
using Gestion_Rendimiento_Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend
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


            services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("es-PE")
        };
                options.DefaultRequestCulture = new RequestCulture("es-PE");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.Configure<FormOptions>(options =>
            {
                // options.MultipartBodyLengthLimit = 2147483647;
                // options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = 2147483647;//long.MaxValue; // <-- !!! long.MaxValue
                                                              // options.MultipartBoundaryLengthLimit = int.MaxValue;
                                                              // options.MultipartHeadersCountLimit = int.MaxValue;
                                                              // options.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.AddScoped<IPersonaService, PersonaService>();
            services.AddScoped<IOficinaService, OficinaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IVariableService, VariableService>();
            services.AddScoped<IEvaluadorService, EvaluadorService>();
            services.AddScoped<IEvaluadoService, EvaluadoService>();
            services.AddScoped<IRendimientoService, RendimientoService>();
            services.AddScoped<IConfiguracionRendimientoService, ConfiguracionRendimientoService>();
            services.AddScoped<IProyectoService, ProyectoService>();
            services.AddScoped<IProyectoDetalleService, ProyectoDetalleService>();
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.  
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

          //  services.AddRazorPages().AddRazorRuntimeCompilation();


            services.Configure<ConfiguracionSistemaModel>(Configuration.GetSection("ConfiguracionSistema"));
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

                .AddCookie(options =>
                {

                    options.Cookie.HttpOnly = true;
                    options.Cookie.Name = "TOKEN_EC";
                    options.LoginPath = new PathString("/Login/Iniciar");
                    options.LogoutPath = new PathString("/Login/Salir");
                    options.AccessDeniedPath = new PathString("/Login/NoAutorizado");
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.SlidingExpiration = true;
                    options.Cookie.Path = "/";
                });



            services.AddMvc()
                   //  services.AddMvc(options => options.Filters.Add(new AuthorizeFilter()))
                   .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDistributedMemoryCache();//To Store session in Memory, This is default implementation of IDistributedCache    
            services.AddSingleton<IHttpContextAccessor,
           HttpContextAccessor>();
            //agregado inicio serializar le objeto
            services.AddControllers()
                     .AddJsonOptions(options =>
                     {
                         options.JsonSerializerOptions.PropertyNamingPolicy = null;
                     });
            //agregado final

            services.AddDbContext<DatabaseContext>(options =>
            options.UseOracle(Configuration.GetConnectionString("DefaultConnection"),
            o => o.UseOracleSQLCompatibility("11")));
        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Login/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Login}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
