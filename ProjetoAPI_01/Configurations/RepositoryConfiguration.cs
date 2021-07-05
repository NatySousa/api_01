using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetoAPI_01.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI_01.Configurations
{
    public class RepositoryConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration Configuration)
        {
            var connectionstring = Configuration.GetConnectionString("BDProjetoAPI_01");

            services.AddTransient(map => new EmpresaRepository(connectionstring));
            services.AddTransient(map => new FuncionarioRepository(connectionstring));
            services.AddTransient(map => new UsuarioRepository(connectionstring));
        }
    }
}
