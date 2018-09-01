using com.chatclube.Repository.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ChatClube.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ConfigureChatClubeContext();
        }

        public static void ConfigureChatClubeContext()
        {
            var dbOptions =
               new DbContextOptionsBuilder<ChatClubeContext>()
               .UseSqlServer(@"Server=localhost;Database=ChatClube;Trusted_Connection=true;");
            ChatClubeContext.dbContextOptions = dbOptions.Options;
        }
    }
}
