using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositorys;
using System.Text;
using UnitOfWork;

namespace surfaliancaAPI
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
            services.AddCors();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("surfalianca")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                         .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped(typeof(IStoreRepository), typeof(StoreRepository));
            services.AddScoped(typeof(ITeamRepository), typeof(TeamRepository));
            services.AddScoped(typeof(ITeamImageRepository), typeof(TeamImageRepository));
            services.AddScoped(typeof(IFinSystemRepository), typeof(FinSystemRepository));
            services.AddScoped(typeof(IConstructionRepository), typeof(ConstructionRepository));
            services.AddScoped(typeof(ILaminationRepository), typeof(LaminationRepository));
            services.AddScoped(typeof(ITailRepository), typeof(TailRepository));
            services.AddScoped(typeof(IBottomRepository), typeof(BottomRepository));
            services.AddScoped(typeof(IBoardModelRepository), typeof(BoardModelRepository));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(IPaintRepository), typeof(PaintRepository));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddScoped(typeof(IBlogRepository), typeof(BlogRepository));
            services.AddScoped(typeof(ICupomRepository), typeof(CupomRepository));
            services.AddScoped(typeof(IStateRepository), typeof(StateRepository));
            services.AddScoped(typeof(IStringerRepository), typeof(StringerRepository));
            services.AddScoped(typeof(IShippingCompanyRepository), typeof(ShippingCompanyRepository));
            services.AddScoped(typeof(IShippingCompanyStateRepository), typeof(ShippingCompanyStateRepository));
            services.AddScoped(typeof(IBoardModelDimensionsRepository), typeof(BoardModelDimensionsRepository));

            services.AddScoped(typeof(IOrderEmailRepository), typeof(OrderEmailRepository));
            services.AddScoped(typeof(IOrderTrackingRepository), typeof(OrderTrackingRepository));

            services.AddScoped(typeof(IProductStatusRepository), typeof(ProductStatusRepository));
            services.AddScoped(typeof(IProductTypeRepository), typeof(ProductTypeRepository));
            services.AddScoped(typeof(ITypeBlogRepository), typeof(TypeBlogRepository));

            services.AddScoped(typeof(ITailReinforcementRepository), typeof(TailReinforcementRepository));


            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            var key = Encoding.ASCII.GetBytes(Configuration["secretJwt"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
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
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
