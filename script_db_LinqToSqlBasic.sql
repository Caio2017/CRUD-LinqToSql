USE MASTER
GO
CREATE DATABASE db_LinqToSqlBasic
GO
USE db_LinqToSqlBasic
GO
CREATE TABLE Funcionario (
	Id					INT PRIMARY KEY IDENTITY,
	Nome				VARCHAR(100),
	Sexo				CHAR(1),
	Telefone			CHAR(11),
	DataNascimento		DATE,
	Salario				SMALLMONEY,
	Ativo				BIT,
	UltimaAtualizacao	DATETIME
)

GO
CREATE PROCEDURE sp_BuscarTodosFuncionarios
AS
	SELECT Id, Nome, Sexo, Telefone, DataNascimento, Salario, Ativo, UltimaAtualizacao FROM Funcionario
	
	
return
EXEC sp_BuscarTodosFuncionarios
