using AutoMapper;
using LibraryManager.Api.Mappings;
using LibraryManager.Api.Services;
using LibraryManager.Data.Repositories;
using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;

namespace LibraryManager.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Configure Dependency Injection
            var container = new UnityContainer();
            container.RegisterType<IBookRepository, BookRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IBookService, BookService>();
            container.RegisterType<ICategoryService, CategoryService>();
            config.DependencyResolver = new UnityDependencyResolver(container);

            // AutoMapper configuration
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            container.RegisterInstance(mapper);
        }
    }
}
