using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI_01.Entities;
using ProjetoAPI_01.Models;
using ProjetoAPI_01.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(AccountModel model,
            [FromServices] UsuarioRepository usuarioRepository)
        {
            try
            {
                //verificar se o email informado ja esta cadastrado no banco..
                if (usuarioRepository.Obter(model.Email) != null)
                {
                    //retornando erro HTTP 422
                    return UnprocessableEntity("O email informado já encontra-se cadastrado. Tente outro.");
                }
                else
                {
                    var usuario = new Usuario();

                    usuario.Nome = model.Nome;
                    usuario.Email = model.Email;
                    usuario.Senha = model.Senha;

                    //gravando o usuario no banco de dados
                    usuarioRepository.Inserir(usuario);

                    //retornar resposta de sucesso HTTP 200
                    return Ok("Usuário cadastrado com sucesso.");
                }
            }
            catch (Exception e)
            {
                //INTERNAL SERVER ERROR
                return StatusCode(500, e.Message);
            }
        }
    }
}
