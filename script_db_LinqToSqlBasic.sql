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
CREATE PROCEDURE sp_InserirFuncioanario 
	@nom	VARCHAR(100),
	@sex	CHAR(1),
	@tel	CHAR(11),
	@nas	DATE,
	@sal	SMALLMONEY,
	@atv	BIT
	AS
		INSERT INTO Funcionario VALUES (@nom, @sex, @tel, @nas, @sal, @atv, GETDATE())

GO
CREATE PROCEDURE sp_BuscarTodosFuncionarios
AS
	SELECT Id, Nome, Sexo, Telefone, DataNascimento, Salario, Ativo, UltimaAtualizacao FROM Funcionario
	
	
return
EXEC sp_BuscarTodosFuncionarios