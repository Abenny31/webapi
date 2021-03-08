using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using webapi1.Data;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace webapi1
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

            services.AddControllers();
         
            services.AddMvc();
            services.AddDbContext<BlogPostsContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BlogPostsContext")));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
                );
            });
            services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));

            services.AddRouting();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
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
                app.UseHsts();
            }
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints=>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            }
            );


            app.UseSpa(spa =>
            {
              

                spa.Options.SourcePath = @"C:\Radna Mapa\VS\webapi1\ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });


        }
    }
}
