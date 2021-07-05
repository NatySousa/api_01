using ProjetoAPI_01.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI_01.Interfaces
{
    public interface IEmpresaRepository
    {
        void Inserir(Empresa empresa);
        void Alterar(Empresa empresa);
        void Excluir(Empresa empresa);

        List<Empresa> Consultar();

        Empresa ObterPorId(Guid idEmpresa);
        Empresa ObterPorCnpj(string cnpj);

        int ObterQuantidadeFuncionarios(Guid idEmpresa);
    }
}
