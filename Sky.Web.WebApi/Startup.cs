using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sky.Common;
using Sky.Core;
using Sky.RepsonsityService;
using Sky.Web.WebApi.Controllers;
using Sky.Web.WebApi.Jwt;
using Microsoft.Extensions.Caching.Redis;

namespace Sky.Web.WebApi
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
            services.AddTransient(typeof(IResponsitryBase<>), typeof(ResponsitryBase<>));

            //集中注册服务
            foreach (var item in DBUnity.GetClassName("Sky.RepsonsityService"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//注入httpcontext
            services.AddTransient<IJwtAuthorization, JwtAuthorzation>(); //jwt认证注入


            services.AddTransient<MemoryCacheService>(); //内存缓存认证注入 
            //注入Redis
            services.AddSingleton(new RedisCacheService(new RedisCacheOptions()
            {
                InstanceName = Configuration.GetSection("Redis:InstanceName").Value,
                Configuration = Configuration.GetSection("Redis:Connection").Value
            }));

            // 配置日志
            services.AddLogging(logConfig =>
            {
                Logger.LoadLogger();
            });
            
            services.AddCors();//支持跨域

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(jwbearoption =>
           {
               jwbearoption.TokenValidationParameters = new TokenValidationParameters
               {

                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = "issuer",
                   ValidAudience = "audience",
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))

               };
               //jwbearoption.SecurityTokenValidators.Clear();//将SecurityTokenValidators清除掉，否则它会在里面拿验证
           });

            services.AddMvc(option =>
            {
                //全局配置错误日志
                option.Filters.Add<HttpGlobalExceptionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello, World!");
            });


            //强制使用https
            app.UseHttpsRedirection();
            app.UseCors();

            //配置中间件
            app.UseAuthentication();

            app.UseMvc();


        }
    }
}
