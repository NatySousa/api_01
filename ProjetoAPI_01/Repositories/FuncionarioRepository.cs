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
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly string _connectionstring;

        public FuncionarioRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void Inserir(Funcionario funcionario)
        {
            var query = @"
                    INSERT INTO FUNCIONARIO(IDFUNCIONARIO, NOME, CPF, MATRICULA, DATAADMISSAO, IDEMPRESA) 
                    VALUES(NEWID(), @Nome, @Cpf, @Matricula, @DataAdmissao, @IdEmpresa)
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, funcionario);
            }
        }

        public void Alterar(Funcionario funcionario)
        {
            var query = @"
                    UPDATE FUNCIONARIO SET 
                        NOME = @Nome,
                        CPF = @Cpf,
                        MATRICULA = @Matricula,
                        DATAADMISSAO = @DataAdmissao,
                        IDEMPRESA = @IdEmpresa
                    WHERE
                        IDFUNCIONARIO = @IdFuncionario
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, funcionario);
            }
        }

        public void Excluir(Funcionario funcionario)
        {
            var query = @"
                    DELETE FROM FUNCIONARIO
                    WHERE IDFUNCIONARIO = @IdFuncionario
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, funcionario);
            }
        }

        public List<Funcionario> Consultar()
        {
            var query = @"
                    SELECT * FROM FUNCIONARIO F
                    INNER JOIN EMPRESA E
                    ON E.IDEMPRESA = F.IDEMPRESA
                    ORDER BY F.NOME
                    ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query(query, (Funcionario funcionario, Empresa empresa) =>
                    {
                        funcionario.Empresa = empresa;
                        return funcionario;
                    },
                    splitOn: "IdEmpresa") //chave estrangeira
                    .ToList();
            }
        }

        public Funcionario ObterPorId(Guid idFuncionario)
        {
            var query = @"
                    SELECT * FROM FUNCIONARIO F
                    INNER JOIN EMPRESA E
                    ON E.IDEMPRESA = F.IDEMPRESA
                    WHERE IDFUNCIONARIO = @IdFuncionario
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query(query, (Funcionario funcionario, Empresa empresa) =>
                    {
                        funcionario.Empresa = empresa;
                        return funcionario;
                    },
                     new { idFuncionario },
                     splitOn: "IdEmpresa") //chave estrangeira
                    .FirstOrDefault();
            }
        }

        public List<Funcionario> ConsultarPorDataAdmissao(DateTime dataAdmissaoMin, DateTime dataAdmissaoMax)
        {
            var query = @"
                    SELECT * FROM FUNCIONARIO F
                    INNER JOIN EMPRESA E
                    ON E.IDEMPRESA = F.IDEMPRESA 
                    WHERE DATAADMISSAO BETWEEN @dataAdmissaoMin AND @dataAdmissaoMax
                    ORDER BY DATAADMISSAO
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                        .Query(query, (Funcionario funcionario, Empresa empresa) =>
                        {
                            funcionario.Empresa = empresa;
                            return funcionario;
                        },
                        new { dataAdmissaoMin, dataAdmissaoMax },
                        splitOn: "IdEmpresa") //chave estrangeira
                        .ToList();
            }
        }

        public Funcionario ObterPorCpf(string cpf)
        {
            var query = @"
                    SELECT * FROM FUNCIONARIO F
                    INNER JOIN EMPRESA E
                    ON E.IDEMPRESA = F.IDEMPRESA
                    WHERE CPF = @Cpf
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                        .Query(query, (Funcionario funcionario, Empresa empresa) =>
                        {
                            funcionario.Empresa = empresa;
                            return funcionario;
                        },
                        new { cpf },
                        splitOn: "IdEmpresa") //chave estrangeira
                        .FirstOrDefault();
            }
        }
    }
}
