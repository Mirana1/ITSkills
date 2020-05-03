namespace ITSkills.Web
{
    using System;
    using System.Reflection;

    using ITSkills.Data;
    using ITSkills.Data.Common;
    using ITSkills.Data.Common.Repositories;
    using ITSkills.Data.Models;
    using ITSkills.Data.Repositories;
    using ITSkills.Data.Seeding;
    using ITSkills.Services.Data;
    using ITSkills.Services.Mapping;
    using ITSkills.Services.Messaging;
    using ITSkills.Web.InputModels;
    using ITSkills.Web.ViewModels;
    using ITSkills.Web.ViewModels.Administration.Categories;
    using ITSkills.Web.ViewModels.Administration.Courses;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = this.configuration.GetConnectionString("DefaultConnection");
                options.SchemaName = "dbo";
                options.TableName = "CacheRecords";
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddResponseCaching();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(configure =>
            {
                configure.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        IConfigurationSection googleAuthNSection =
                            this.configuration.GetSection("Authentication:Google");

                        options.ClientId = googleAuthNSection["ClientId"];
                        options.ClientSecret = googleAuthNSection["ClientSecret"];
                    });

            services.AddAntiforgery(options => { options.HeaderName = "X-CSRF-TOKEN"; });
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(x =>
            new SendGridEmailSender(this.configuration["SendGridAPIKey"]));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ICoursesService, CoursesService>();
            services.AddTransient<ILectionsService, LectionsService>();
            services.AddTransient<IMyCoursesService, MyCoursesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(ErrorViewModel).GetTypeInfo().Assembly,
                typeof(CategoryEditInputModel).GetTypeInfo().Assembly,
                typeof(AllCategoriesViewModel).GetTypeInfo().Assembly,
                typeof(CategoryDetailsViewModel).GetTypeInfo().Assembly,
                typeof(DeleteCategoryViewModel).GetTypeInfo().Assembly,
                typeof(AllCoursesViewModel).GetTypeInfo().Assembly,
                typeof(CategoryViewModel).GetTypeInfo().Assembly,
                typeof(UsersViewModel).GetTypeInfo().Assembly,
                typeof(CreateCourseViewModel).GetTypeInfo().Assembly,
                typeof(CategoryDropDownViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseResponseCompression();
            app.UseResponseCaching();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("myCoursePayment", "/Course/Payment/{id}", new { controller = "MyCourses", action = "Payment" });
                        endpoints.MapControllerRoute("courseSearch", "/Course/Index/{seachWord}", new { controller = "Courses", action = "Index" });
                        endpoints.MapControllerRoute("courseLection", "/Course/ById/{id}", new { controller = "Courses", action = "ById" });
                        endpoints.MapControllerRoute("lectionView", "/Lection/ById/{id}", new { controller = "Lections", action = "ById" });
                        endpoints.MapControllerRoute("courseCategory", "/Category/{name:minlength(3)}", new { controller = "Categories", action = "ByName" });
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
