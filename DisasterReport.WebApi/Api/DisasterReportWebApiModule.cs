using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Swashbuckle.Application;
using System.Linq;
using System.Configuration;
using DisasterReport.Api.SwaggerExtensions;
using System;
using System.Web.Http.Cors;

namespace DisasterReport.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(DisasterReportApplicationModule))]
    public class DisasterReportWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(DisasterReportApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

            // 解决跨域
            var cors = new EnableCorsAttribute("*", "*", "*");
            GlobalConfiguration.Configuration.EnableCors(cors);

            var controllerXmlPahts = ConfigurationManager.AppSettings["ControllerXmlPath"].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            Configuration.Modules.AbpWebApi().HttpConfiguration
            .EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "灾情上报API");
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider));

                foreach (var controllerXmlPaht in controllerXmlPahts)
                {
                    c.IncludeXmlComments(string.Format("{0}/bin/{1}.XML", System.AppDomain.CurrentDomain.BaseDirectory, controllerXmlPaht));
                }
            })
            .EnableSwaggerUi(c =>
            {
                c.InjectJavaScript(Assembly.GetAssembly(typeof(DisasterReportWebApiModule)), "DisasterReport.Api.SwaggerExtensions.zh-cn.js");
                c.DisableValidator();
            });

        }
    }
}
