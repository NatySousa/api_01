using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI_01.Configurations
{
    public class SwaggerConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            //Configuração da documentação da API (Swagger)
            services.AddSwaggerGen(
                swagger =>
                {
                    swagger.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "API Controle de Empresas e Funcionários",
                        Description = "API REST desenvolvida em .NET CORE com Dapper",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "API",
                            Url = new Uri("https://www.jornalcontabil.com.br/wp-content/uploads/2020/04/empresario-nomeia-um-lider-para-o-chefe-da-equipe-criacao-de-equipes-efetivas-de-especialistas_72572-1300.jpg"),
                            Email = "#"
                        }
                    });
                }
                );
        }
    }
}
