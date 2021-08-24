using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResultChecker.Models;
using ResultChecker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.NamingConventions;
using Microsoft.AspNetCore.Http;

namespace ResultChecker
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            Env = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContextPool<AppDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("ResultCheckerDbCon")));
            //services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ResultCheckerDbCon")));
            services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql( Configuration.GetConnectionString("ResultCheckerDbCon"), b => b.MigrationsAssembly("ResultChecker")).UseLowerCaseNamingConvention());

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;

                //options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
                //account lockout settings
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            var builder = services.AddRazorPages();
            if (Env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
                options.AppendTrailingSlash = true;
            });
            services.AddScoped<IResultRepository, SQLResultRepository>();
            services.AddScoped<IMailService, MailService>();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/admin/accessdenied");
            });

            services.AddAuthorization(options =>
            {
                //Claims Policy
                options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role", "true"));
                //.RequireClaim("Create Role"));//only Delete Role claim required, dot RequireClaim means both claim required.

                options.AddPolicy("EditRolePolicy", policy => policy.RequireClaim("Edit Role", "true")); //personal role edit

                //options.AddPolicy("EditRolePolicy", policy => policy.RequireAssertion(context=>context.User.IsInRole("Admin")&& 
                //                                                                        context.User.HasClaim(claim=>claim.Type=="Edit Role" && claim.Value=="true")||
                //                                                                        context.User.IsInRole("Super Admin"))); //both (Admin && Edit Role) || Super Admin
                options.AddPolicy("CreateRolePolicy", policy => policy.RequireClaim("Create Role", "true"));

                //Role Policy
                options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole("Admin", "HOD", "Examiner")); // dot RequireRole("Admin","User","Manager") for multiple roles in the policy
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
