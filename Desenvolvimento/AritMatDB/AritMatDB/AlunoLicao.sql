USE AritMatDB;
CREATE TABLE [dbo].[AlunoLicao]
(
	[Aluno] INT NOT NULL , 
    [Licao] INT NOT NULL, 
    [Explicacao] INT NOT NULL, 
    [Data] DATE NOT NULL, 
    [RespErradas] INT NULL, 
    PRIMARY KEY ([Aluno], [Explicacao], [Licao]), 
    CONSTRAINT [FK_AlunoLicao_ToTable] FOREIGN KEY ([Aluno]) REFERENCES [Aluno]([IdAluno]), 
    CONSTRAINT [FK_AlunoLicao_ToTable_1] FOREIGN KEY ([Licao],[Explicacao]) REFERENCES [Licao]([IdLicao],[NumExpl] )
)
