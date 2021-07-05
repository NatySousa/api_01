using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        [HttpPost] //Cadastro
        public IActionResult Post(FuncionarioCadastroModel model,
           [FromServices] FuncionarioRepository funcionarioRepository)
        {
            try
            {
                //verificar se já existe um funcionario cadastrado com o CPF informado..
                if (funcionarioRepository.ObterPorCpf(model.Cpf) != null)
                {
                    //retorando erro HTTP 422
                    return UnprocessableEntity("O CPF informado já encontra-se cadastrado.");
                }

                var funcionario = new Funcionario();

                funcionario.Nome = model.Nome;
                funcionario.Cpf = model.Cpf;
                funcionario.Matricula = model.Matricula;
                funcionario.DataAdmissao = model.DataAdmissao;
                funcionario.IdEmpresa = model.IdEmpresa;

                funcionarioRepository.Inserir(funcionario);

                return Ok("Funcionário cadastrado com sucesso.");
            }
            catch (Exception e)
            {
                //retornando uma resposta de erro na API..
                //500 -> código indicando ocorreu um erro no serviço
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut] //Atualização
        public IActionResult Put(FuncionarioEdicaoModel model,
            [FromServices] FuncionarioRepository funcionarioRepository)
        {
            try
            {
                var funcionario = funcionarioRepository.ObterPorId(model.IdFuncionario);

                //verificando se o funcionário não foi encontrado..
                if (funcionario == null)
                {
                    return UnprocessableEntity("Funcionário não encontrado.");
                }

                funcionario.Nome = model.Nome;
                funcionario.Matricula = model.Matricula;
                funcionario.DataAdmissao = model.DataAdmissao;
                funcionario.IdEmpresa = model.IdEmpresa;

                funcionarioRepository.Alterar(funcionario);

                return Ok("Funcionário atualizado com sucesso.");
            }
            catch (Exception e)
            {
                //retornando uma resposta de erro na API..
                //500 -> código indicando ocorreu um erro no serviço
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{idFuncionario}")] //Exclusão
        public IActionResult Delete(Guid idFuncionario,
            [FromServices] FuncionarioRepository funcionarioRepository)
        {
            try
            {
                var funcionario = funcionarioRepository.ObterPorId(idFuncionario);

                //verificando se o funcionário não foi encontrado..
                if (funcionario == null)
                {
                    return UnprocessableEntity("Funcionário não encontrado.");
                }

                funcionarioRepository.Excluir(funcionario);

                //retornando mensagem de sucesso
                return Ok("Funcionário excluído com sucesso.");
            }
            catch (Exception e)
            {
                //retornando uma resposta de erro na API..
                //500 -> código indicando ocorreu um erro no serviço
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet] //Consulta
        public IActionResult GetAll([FromServices] FuncionarioRepository funcionarioRepository)
        {
            try
            {
                var funcionarios = funcionarioRepository.Consultar();

                var model = new List<FuncionarioConsultaModel>();

                foreach (var item in funcionarios)
                {
                    model.Add(new FuncionarioConsultaModel
                    {
                        IdFuncionario = item.IdFuncionario,
                        Nome = item.Nome,
                        Cpf = item.Cpf,
                        Matricula = item.Matricula,
                        DataAdmissao = item.DataAdmissao,

                        IdEmpresa = item.Empresa.IdEmpresa,
                        NomeFantasia = item.Empresa.NomeFantasia,
                        RazaoSocial = item.Empresa.RazaoSocial,
                        Cnpj = item.Empresa.Cnpj
                    });
                }

                return Ok(model);
            }
            catch (Exception e)
            {
                //retornando uma resposta de erro na API..
                //500 -> código indicando ocorreu um erro no serviço
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{idFuncionario}")] //Consulta
        public IActionResult GetById(Guid idFuncionario,
            [FromServices] FuncionarioRepository funcionarioRepository)
        {
            try
            {
                var funcionario = funcionarioRepository.ObterPorId(idFuncionario);

                var model = new FuncionarioConsultaModel
                {
                    IdFuncionario = funcionario.IdFuncionario,
                    Nome = funcionario.Nome,
                    Cpf = funcionario.Cpf,
                    Matricula = funcionario.Matricula,
                    DataAdmissao = funcionario.DataAdmissao,

                    IdEmpresa = funcionario.Empresa.IdEmpresa,
                    NomeFantasia = funcionario.Empresa.NomeFantasia,
                    RazaoSocial = funcionario.Empresa.RazaoSocial,
                    Cnpj = funcionario.Empresa.Cnpj
                };

                return Ok(model);
            }
            catch (Exception e)
            {
                //retornando uma resposta de erro na API..
                //500 -> código indicando ocorreu um erro no serviço
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{dataAdmissaoMin}/{dataAdmissaoMax}")]
        public IActionResult GetByDataAdmissao(DateTime dataAdmissaoMin, DateTime dataAdmissaoMax,
            [FromServices] FuncionarioRepository funcionarioRepository)
        {
            try
            {
                //consultar os funcionarios por periodo de data de admissão
                var funcionarios = funcionarioRepository.ConsultarPorDataAdmissao(dataAdmissaoMin, dataAdmissaoMax);

                var model = new List<FuncionarioConsultaModel>();

                foreach (var item in funcionarios)
                {
                    model.Add(new FuncionarioConsultaModel
                    {
                        IdFuncionario = item.IdFuncionario,
                        Nome = item.Nome,
                        Cpf = item.Cpf,
                        Matricula = item.Matricula,
                        DataAdmissao = item.DataAdmissao,

                        IdEmpresa = item.Empresa.IdEmpresa,
                        NomeFantasia = item.Empresa.NomeFantasia,
                        RazaoSocial = item.Empresa.RazaoSocial,
                        Cnpj = item.Empresa.Cnpj
                    });
                }

                return Ok(model);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
