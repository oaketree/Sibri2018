using Cms.BLL.category.services;
using Cms.BLL.login.services;
using Cms.BLL.news.service;
using Cms.BLL.pages.service;
using Cms.BLL.upload.service;
using Cms.BLL.upload.ueditor;
using Cms.Contract.category;
using Cms.Contract.login;
using Cms.Contract.news;
using Cms.Contract.pages;
using Cms.Contract.upload;
using Core.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cms.WebPage
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
            //services.AddMemoryCache();
            services.AddDbContext<LoginDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<CategoryDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<NewsDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<UploadDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<PageDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAdminServices, AdminServices>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IUeditorService, UeditorService>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IConfigHandler, ConfigHandler>();
            services.AddScoped<INotSupportedHandler, NotSupportedHandler>();
            services.AddScoped<IEditorConfig, EditorConfig>();
            services.AddScoped<IPictureHelper, PictureHelper>();
            services.AddScoped<IUploadHandler, UploadHandler>();
            services.AddScoped<IListFileHandler, ListFileHandler>();
            services.AddScoped<ICrawler, Crawler>();
            services.AddScoped<ICrawlerHandler, CrawlerHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddScoped<ICategoryService, CategoryService>();
            //services.AddSingleton<ICacheHelper, CacheHelper>();


            services.AddMvc();
            //services.AddMvc(options =>
            //{
            //    options.InputFormatters.Add(new TextPlainInputFormatter());
            //});
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/Login");
                options.AccessDeniedPath = new PathString("/Admin/Denied");
            });

            services.Configure<RemoteSave>(this.Configuration.GetSection("RemoteSave"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
