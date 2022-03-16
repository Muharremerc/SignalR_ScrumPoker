using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ScrumPokerAPI.Hubs;
using ScrumPokerAPI.Middleware;
using ScrumPokerAPI.Services;
using ScrumPokerAPI.Services.Interfaces;
using ScrumPokerAPI.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI
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
            services.AddCors(o => o.AddDefaultPolicy(p => p.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(or => true)));
           
            services.AddControllers();
            services.AddSignalR();

            services.AddSingleton<IGroupService, GroupService>();
            services.AddSingleton<IBaseGroupService, BaseGroupService>();
            services.AddSingleton<IPointService, PointService>();
            services.AddSingleton<IChatService, ChatService>();
            services.AddAutoMapper(typeof(Startup));

            #region Swagger

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";
                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                 options =>
                 {
                     foreach (var description in provider.ApiVersionDescriptions)
                     {
                         options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                     }
                 });
            app.ConfigureExceptionHandler();
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<GroupHub>("/groupHub");
            });
        }
    }
}
