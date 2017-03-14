using System.Collections.Generic;
using System.Linq;

using LinqToSqlBasic.Model;
using System;
using System.Data;

namespace LinqToSqlBasic.Model
{
    public static class FuncionarioDataAcess
    {
        public static void Inserir(Funcionario funcionario)
        {
            try
            {
                //Cria uma instancia do banco de dados
                LinqToSqlBasic_DataClassesDataContext oDB = new LinqToSqlBasic_DataClassesDataContext();
                
                //Método para inserir um item
                oDB.Funcionarios.InsertOnSubmit(funcionario);
                //Realiza as mudanças
                oDB.SubmitChanges();
                //Libera a conexao
                oDB.Dispose();

            }
            catch
            {
                throw;
            }
        }

        public static void Deletar(Funcionario funcionario)
        {
            try
            {
                LinqToSqlBasic_DataClassesDataContext oDB = new LinqToSqlBasic_DataClassesDataContext();

                Funcionario fun = (from f in oDB.Funcionarios
                                   where f.Id == funcionario.Id
                                   select f).SingleOrDefault();

                oDB.Funcionarios.DeleteOnSubmit(fun);
                oDB.SubmitChanges();
                oDB.Dispose();
            }
            catch
            {
                throw;
            }
        }

        public static void Alterar(Funcionario funcionario)
        {
            try
            {
                LinqToSqlBasic_DataClassesDataContext oDB = new LinqToSqlBasic_DataClassesDataContext();
                //dataGridView1.DataSource = oDB.Funcionarios;
                Funcionario func = (from f in oDB.Funcionarios
                                    where f.Id == funcionario.Id
                                    select f).Single<Funcionario>();                
                
                func.Nome = funcionario.Nome;
                func.Sexo = funcionario.Sexo;
                func.Telefone = funcionario.Telefone;
                func.Salario = funcionario.Salario;
                func.DataNascimento = funcionario.DataNascimento;
                func.Ativo = funcionario.Ativo;
                func.UltimaAtualizacao = funcionario.UltimaAtualizacao;
                                
                oDB.SubmitChanges();
                oDB.Dispose();
            }
            catch
            {
                throw;
            }
        }

        #region Consultas, Métodos Sobrecarregados
        //consulta por ID
        public static Funcionario ObterFuncionario(int IDFuncionario)
        {
            LinqToSqlBasic_DataClassesDataContext oDB = new LinqToSqlBasic_DataClassesDataContext();
            
            Funcionario funcionario = (from f in oDB.Funcionarios
                                             where f.Id == IDFuncionario
                                       select f).Single<Funcionario>();
            
            return funcionario;
        }
        
        //consulta por Nome
        public static List<Funcionario> ObterFuncionario(string nomeFuncionario)
        {
            LinqToSqlBasic_DataClassesDataContext oDB = new LinqToSqlBasic_DataClassesDataContext();

            List<Funcionario> funcionarios = (from f in oDB.Funcionarios
                                              where f.Nome.Contains(nomeFuncionario)
                                              select f).ToList<Funcionario>();
            return funcionarios;
        }

        //top 20 ordenado pelo Nome
        public static List<Funcionario> ObterFuncionario()
        {
            LinqToSqlBasic_DataClassesDataContext oDB = new LinqToSqlBasic_DataClassesDataContext();
           

            List<Funcionario> funcionarios = (from f in oDB.Funcionarios
                                              orderby f.Id //Opcional, retornara em ordenaçao
                                              select f).Take(20).ToList<Funcionario>();
            return funcionarios;
        }

        //StoredProcedure test
        public static List<Funcionario> ObterTodosFuncionario()
        {
            LinqToSqlBasic_DataClassesDataContext oDB = new LinqToSqlBasic_DataClassesDataContext();
            
            //arquivo.dbml > properties do metodo procedure > ReturnType = Funcionario
            List<Funcionario> funcionarios = oDB.sp_BuscarTodosFuncionarios().ToList();

            return funcionarios;            
        }
        #endregion
    }
}
