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
    public class EmpresasController : ControllerBase
    {
        [HttpPost] //Cadastro
        public IActionResult Post(EmpresaCadastroModel model,
            [FromServices] EmpresaRepository empresaRepository)
        {
            try
            {
                var empresa = new Empresa();

                empresa.NomeFantasia = model.NomeFantasia;
                empresa.RazaoSocial = model.RazaoSocial;
                empresa.Cnpj = model.Cnpj;

                empresaRepository.Inserir(empresa);

                return Ok("Empresa cadastrada com sucesso.");
            }

            catch (Exception e)
            {
                //retornando uma resposta de erro na API..
                //500 -> código indicando ocorreu um erro no serviço
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut] //Atualização
        public IActionResult Put(EmpresaEdicaoModel model,
            [FromServices] EmpresaRepository empresaRepository)
        {
            try
            {
                var empresa = new Empresa();

                empresa.IdEmpresa = model.IdEmpresa;
                empresa.NomeFantasia = model.NomeFantasia;
                empresa.RazaoSocial = model.RazaoSocial;
                empresa.Cnpj = model.Cnpj;

                empresaRepository.Alterar(empresa);

                return Ok("Empresa atualizada com sucesso.");
            }
            catch (Exception e)
            {
                //retornando uma resposta de erro na API..
                //500 -> código indicando ocorreu um erro no serviço
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{idEmpresa}")] //Exclusão
        public IActionResult Delete(Guid idEmpresa,
            [FromServices] EmpresaRepository empresaRepository)
        {
            try
            {
                //buscar a empresa no banco de dados atraves do id..
                var empresa = empresaRepository.ObterPorId(idEmpresa);

                //excluir a empresa
                empresaRepository.Excluir(empresa);

                //retornando mensagem de sucesso
                return Ok("Empresa excluída com sucesso.");
            }
            catch (Exception e)
            {
                //retornando uma resposta de erro na API..
                //500 -> código indicando ocorreu um erro no serviço
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet] //Consulta
        public IActionResult GetAll(
[FromServices] EmpresaRepository empresaRepository)
        {

            try
            {
                //consultando todas as empresas no banco de dados
                var empresas = empresaRepository.Consultar();

                //lista da classe model para retornar 
                //os dados da consulta na api..
                var model = new List<EmpresaConsultaModel>();

                //transferir os dados das empresas obtidos no banco
                //para a lista de EmpresaConsultaModel
                foreach (var item in empresas)
                {
                    model.Add(new EmpresaConsultaModel
                    {
                        IdEmpresa = item.IdEmpresa,
                        NomeFantasia = item.NomeFantasia,
                        RazaoSocial = item.RazaoSocial,
                        Cnpj = item.Cnpj
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

        [HttpGet("{idEmpresa}")] //Consulta
        public IActionResult GetById(Guid idEmpresa,
            [FromServices] EmpresaRepository empresaRepository)
        {
            try
            {
                //consultar no banco de dados a empresa pelo id..
                var empresa = empresaRepository.ObterPorId(idEmpresa);

                //transferir os dados da empresa para o model..
                var model = new EmpresaConsultaModel
                {
                    IdEmpresa = empresa.IdEmpresa,
                    NomeFantasia = empresa.NomeFantasia,
                    RazaoSocial = empresa.RazaoSocial,
                    Cnpj = empresa.Cnpj
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
    }
}
