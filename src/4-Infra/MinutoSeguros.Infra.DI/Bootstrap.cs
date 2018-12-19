using Microsoft.Extensions.DependencyInjection;
using MinutoSeguros.Application.AppServices;
using MinutoSeguros.Application.Interfaces.AppServices;
using MinutoSeguros.Application.Mappers;
using MinutoSeguros.Domain.Interfaces.DomainServices;
using MinutoSeguros.Domain.Interfaces.Https;
using MinutoSeguros.Domain.Services;
using MinutoSeguros.Domain.Validations;
using MinutoSeguros.Infra.Http.Https;

namespace MinutoSeguros.Infra.DI {
    public class Bootstrap {
        
        public static void Configure (IServiceCollection services) {

            //--- validation
            services.AddSingleton<BlogValidation> ();

            //--- mapper
            services.AddSingleton<BlogMapper> ();

            //--- http
            services.AddSingleton<IBlogHttp, BlogHttp> ();

            //--- service
            services.AddSingleton<IBlogService, BlogService> ();

            //--- app service
            services.AddSingleton<IBlogAppService, BlogAppService> ();
        }
    }
}