
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjetoAPI_01.Repositories;
using ProjetoAPI_01.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAPI_01
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. 
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //configurando as classes do repositorio, passando para eles
            //o parametro da connectionstring do banco de dados..

            var connectionstring = Configuration.GetConnectionString("BDProjetoAPI_01");

            services.AddTransient(map => new EmpresaRepository(connectionstring));
            services.AddTransient(map => new FuncionarioRepository(connectionstring));
            services.AddTransient(map => new UsuarioRepository(connectionstring));

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

            //Configuração do CORS - Cross Origin Resource Sharing
            services.AddCors(
                s => s.AddPolicy("DefaultPolicy", builder =>
                {
                    //permitindo que qualquer servidor faça requisições 
                    //para a API
                    builder.AllowAnyOrigin()
                           //permitindo que qualquer método da API 
                           //seja executado (POST, PUT, DELETE, GET etc)
                           .AllowAnyMethod()
                           //permitindo que qualquer cabeçalho 
                           //seja enviado para a API (Token por exemplo)
                           .AllowAnyHeader();
                }));

            //Configuração para a autenticação (JWT BEARER AUTHENTICATION)
            var settingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(settingsSection);

            var appSettings = settingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(
                    auth =>
                    {
                        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                ).AddJwtBearer(

                    bearer =>
                    {
                        bearer.RequireHttpsMetadata = false;
                        bearer.SaveToken = true;
                        bearer.TokenValidationParameters = new TokenValidationParameters{ValidateIssuerSigningKey = true,    IssuerSigningKey = new SymmetricSecurityKey(key), ValidateIssuer = false, ValidateAudience = false};
                    }
                );

            services.AddTransient(map => new TokenService(appSettings));
        }

        // This method gets called by the runtime. 
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //Configuração do CORS - Cross Origin Resource Sharing
            app.UseCors("DefaultPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            //Configuração da documentação do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint
                ("/swagger/v1/swagger.json", "API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }

}

