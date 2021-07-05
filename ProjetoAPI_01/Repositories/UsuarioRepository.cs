using Dapper;
using ProjetoAPI_01.Entities;
using ProjetoAPI_01.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI_01.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        //atributo
        private readonly string _connectionstring;

        //construtor
        public UsuarioRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void Inserir(Usuario usuario)
        {
            var query = @$"
                    INSERT INTO USUARIO(
                        IDUSUARIO, 
                        NOME, 
                        EMAIL, 
                        SENHA, 
                        DATACRIACAO)
                    VALUES(
                        NEWID(),
                        @Nome,
                        @Email,
                        CONVERT(VARCHAR(32), HASHBYTES('MD5', '{usuario.Senha}'), 2),
                        GETDATE()
                    )
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, usuario);
            }
        }

        public Usuario Obter(string email)
        {
            var query = @"
                            SELECT * FROM USUARIO
                            WHERE EMAIL = @Email
                        ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Usuario>(query, new { email })
                    .FirstOrDefault();
            }
        }

        public Usuario Obter(string email, string senha)
        {
            var query = @$"
                    SELECT * FROM USUARIO
                    WHERE
                        EMAIL = @Email
                    AND
                        SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', '{senha}'), 2)
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Usuario>(query, new { email })
                    .FirstOrDefault();
            }
        }
    }
}
