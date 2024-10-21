using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Demo.PL.MappingProfiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL
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
            services.AddDbContext<MVCDbContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }); //Allow Dependency injection

            services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            services.AddTransient<IEmployeeRepository,EmployeeRepository>();
            services.AddAutoMapper(M => M.AddProfile(new EmployeeProfiel()));
            services.AddAutoMapper(M => M.AddProfile(new DepartmentProfil()));
            services.AddScoped<IUnitOfWork, UnitOfWrk>();

            services.AddIdentity<ApplicationUser ,IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
				//P@asw0rd

			}
			)
                .AddEntityFrameworkStores<MVCDbContext>();

			//services.AddScoped<UserManager<ApplicationUser>>();
			//services.AddScoped<SignInManager<ApplicationUser>>();
			//services.AddScoped<RoleManager<IdentityRole>>();

			services.AddAuthentication();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Register}/{id?}");
            });
        }
    }
}
