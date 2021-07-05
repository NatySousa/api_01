using ProjetoAPI_01.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI_01.Interfaces
{
    public interface IUsuarioRepository
    {
        void Inserir(Usuario usuario);

        Usuario Obter(string email);
        Usuario Obter(string email, string senha);
    }
}
