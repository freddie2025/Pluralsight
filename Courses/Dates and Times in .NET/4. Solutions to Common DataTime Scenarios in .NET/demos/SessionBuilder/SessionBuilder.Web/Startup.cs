using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SessionBuilder.Core;
using SessionBuilder.Core.Data;

namespace SessionBuilder.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var connection = @"Server=.\SQLEXPRESS;Database=SessionBuilder;Trusted_Connection=True;ConnectRetryCount=0";

            services.AddDbContext<SessionBuilderContext>(options => options.UseSqlServer(connection));

            services.AddTransient<ISpeakerRepository, FakeSpeakerRepository>();
        }

        public void SetupSampleData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SessionBuilderContext>();

                context.Database.Migrate();

                if (!context.Sessions.Any())
                {
                    var repository = new FakeSpeakerRepository();
                    context.AddRange(repository.Speakers);
                    context.SaveChanges();
                }
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();

            app.UseMvcWithDefaultRoute();

            SetupSampleData(app);
        }
    }
}
