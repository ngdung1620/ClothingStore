using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStoreBackend.Models;
using ClothingStoreBackend.Services;
using ClothingStoreBackend.Services.Impl;
using ClothingStoreBackend.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ClothingStoreBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string _CORS = "_CORS";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // khai baos scope
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IGroupCategoryService, GroupCategoryService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            // cau hinh DB, Cau hinh services
            services.AddDbContext<MasterDbContext>(options =>
                options.UseNpgsql(Configuration["PostgresSQL:ConnectionString"])
            );
            //Add Authentication
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<MasterDbContext>()
                .AddDefaultTokenProviders();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            //Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DefaultApplication.SecretKey)),
                    ClockSkew = TimeSpan.Zero,
                };
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Thi???t l???p v??? Password
                options.Password.RequireDigit = false; // Kh??ng b???t ph???i c?? s???
                options.Password.RequireLowercase = false; // Kh??ng b???t ph???i c?? ch??? th?????ng
                options.Password.RequireNonAlphanumeric = true; //  b???t k?? t??? ?????c bi???t
                options.Password.RequireUppercase = true; // b???t bu???c ch??? in
                options.Password.RequiredLength = 4; // S??? k?? t??? t???i thi???u c???a password
                options.Password.RequiredUniqueChars = 1; // S??? k?? t??? ri??ng bi???t
            });
            //CORS Config
            var cors = Configuration.GetSection("CORS").GetChildren().Select(x => x.Value).ToArray();
            services.AddCors(options =>
            {
                options.AddPolicy(_CORS,
                    builder =>
                    {
                        builder.WithOrigins(cors)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClothingStoreBackend", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,MasterDbContext context)
        {
            context.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClothingStoreBackend v1"));
            }
            app.UseCors(_CORS);
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}