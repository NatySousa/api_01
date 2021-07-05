using ProjetoAPI_01.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI_01.Interfaces
{
    public interface IFuncionarioRepository
    {
        void Inserir(Funcionario funcionario);
        void Alterar(Funcionario funcionario);
        void Excluir(Funcionario funcionario);

        List<Funcionario> Consultar();

        Funcionario ObterPorId(Guid idFuncionario);
        Funcionario ObterPorCpf(string cpf);

        List<Funcionario> ConsultarPorDataAdmissao
            (DateTime dataAdmissaoMin, DateTime dataAdmissaoMax);
    }

}