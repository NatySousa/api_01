
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI_01.Entities
{
    public class Empresa
    {
        public Guid IdEmpresa { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }

        //Relacionamento de associação (TER-MUITOS)
        public List<Funcionario> Funcionarios { get; set; }
    }
}
