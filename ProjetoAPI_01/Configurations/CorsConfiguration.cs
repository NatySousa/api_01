using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI_01.Configurations
{
    public class CorsConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            //Configuração do CORS - Cross Origin Resource Sharing
            services.AddCors(
                s => s.AddPolicy("DefaultPolicy", builder =>
                {
                    //permitindo que qualquer servidor faça requisições para a API
                    builder.AllowAnyOrigin()
                    //permitindo que qualquer método da API seja executado (POST, PUT, DELETE, GET etc)
                           .AllowAnyMethod()
                    //permitindo que qualquer cabeçalho seja enviado para a API (Token por exemplo)
                           .AllowAnyHeader();
                }));
        }
    }
}
