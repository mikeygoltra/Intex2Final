using Intex2Final.Data;
using Intex2Final.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ML.OnnxRuntime;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2Final
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _env = environment;
        }
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment => _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            {
                // Register the Swagger generator, defining 1 or more Swagger documents
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                });
                services.AddControllers();
            }

            //FOR THE API
            services.AddSingleton<InferenceSession>(
                new InferenceSession(
                    Path.Combine(_env.ContentRootPath, "wwwroot", "test.onnx")
                )
            );

            var authConnectionString = Configuration["ConnectionStrings:AuthLink"];
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(authConnectionString));

            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<intex2dbContext>(options => options.UseNpgsql(connectionString));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<IdentityOptions>(options => 
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 20;
                options.Password.RequiredUniqueChars = 5;
            });

            //services.AddAuthentication().AddGoogle(options =>
            //{
            //    IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");

            //    options.ClientId = googleAuthNSection["ClientId"];
            //    options.ClientSecret = googleAuthNSection["ClientSecret"];
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "mummy_api v1"));
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
            //app.Use(async (context, next) =>
            //{
            //    //may need to add more website links or image links; THIS IS THE CSP HEADER
            //    context.Response.Headers.Add("Content - Security - Policy", "default - src 'self'; script - src 'self' 'unsafe-inline'; style - src 'self' https://cdn.jsdelivr.net 'unsafe-inline'; font-src 'self'; img-src 'self' https://www.thetimes.co.uk/imageserver/image/%2Fmethode%2Ftimes%2Fprod%2Fweb%2Fbin%2F7d3dfd74-27d4-11e9-92e2-27eb1cf1c11f.jpg?crop=4675%2C2630%2C33%2C485&resize=1180 mummyicon.png; frame-src 'self'");
            //    await next();
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
