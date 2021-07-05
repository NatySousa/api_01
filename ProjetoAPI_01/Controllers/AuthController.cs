using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI_01.Models;
using ProjetoAPI_01.Repositories;
using ProjetoAPI_01.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(AuthModel model,
            [FromServices] UsuarioRepository usuarioRepository,
            [FromServices] TokenService tokenService)
        {
            try
            {
                //consultando o usuario no banco de dados atraves do email e senha
                var usuario = usuarioRepository.Obter(model.Email, model.Senha);

                //verificar se existe no banco de dados o usuario
                //com o email e senha informado..
                if (usuario != null)
                {
                    //autenticar o usuario!
                    return Ok(
                        new
                        {
                            Mensagem = "Usuário autenticado com sucesso",
                            AccessToken = tokenService.GerarToken(usuario.Email),
                            usuario //dados do usuário
                        }
                        );
                }
                else
                {
                    return StatusCode(401, "Acesso negado, usuário inválido.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }
    }
}
