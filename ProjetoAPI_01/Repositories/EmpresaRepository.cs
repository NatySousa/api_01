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
    public class EmpresaRepository : IEmpresaRepository
    {
        //atributo privado da classe..
        private readonly string _connectionstring;

        //método construtor da classe que írá receber a connectionstring
        public EmpresaRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void Inserir(Empresa empresa)
        {
            var query = @"
                    INSERT INTO EMPRESA(IDEMPRESA, NOMEFANTASIA, RAZAOSOCIAL, CNPJ)
                    VALUES(NEWID(), @NomeFantasia, @RazaoSocial, @Cnpj)
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, empresa);
            }
        }

        public void Alterar(Empresa empresa)
        {
            var query = @"
                    UPDATE EMPRESA SET 
                        NOMEFANTASIA = @NomeFantasia, 
                        RAZAOSOCIAL = @RazaoSocial,
                        CNPJ = @Cnpj
                    WHERE
                        IDEMPRESA = @IdEmpresa
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, empresa);
            }
        }

        public void Excluir(Empresa empresa)
        {
            var query = @"
                    DELETE FROM EMPRESA WHERE IDEMPRESA = @IdEmpresa
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, empresa);
            }
        }

        public List<Empresa> Consultar()
        {
            var query = @"
                    SELECT * FROM EMPRESA ORDER BY NOMEFANTASIA
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                        .Query<Empresa>(query)
                        .ToList();
            }
        }

        public Empresa ObterPorId(Guid idEmpresa)
        {
            var query = @"
                    SELECT * FROM EMPRESA WHERE IDEMPRESA = @IdEmpresa
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                        .Query<Empresa>(query, new { idEmpresa })
                        .FirstOrDefault();
            }
        }

        public Empresa ObterPorCnpj(string cnpj)
        {
            var query = @"
                    SELECT * FROM EMPRESA WHERE CNPJ = @Cnpj
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                        .Query<Empresa>(query, new { cnpj })
                        .FirstOrDefault();
            }
        }

        public int ObterQuantidadeFuncionarios(Guid idEmpresa)
        {
            var query = @"
                        SELECT COUNT(*) FROM FUNCIONARIO
                        WHERE IDEMPRESA = @IdEmpresa
                    ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                        .Query<int>(query, new { idEmpresa })
                        .FirstOrDefault();
            }
        }
    }
}

