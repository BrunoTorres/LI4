CREATE TABLE [dbo].[AlunoExercicioLicao]
(
	[Aluno] INT NOT NULL , 
    [Licao] INT NOT NULL, 
    [Explicacao] INT NOT NULL, 
    [Exercicio] INT NOT NULL, 
    [Data] DATETIME NOT NULL, 
    [Resposta] FLOAT NULL, 
    PRIMARY KEY ([Exercicio], [Aluno], [Licao], [Explicacao]), 
    CONSTRAINT [FK_AlunoExercicioLicao_ToTable] FOREIGN KEY ([Aluno]) REFERENCES [Aluno]([IdAluno]), 
    CONSTRAINT [FK_AlunoExercicioLicao_ToTable_1] FOREIGN KEY ([Licao],[Explicacao]) REFERENCES [Licao]([IdLicao],[NumExpl]),
    CONSTRAINT [FK_AlunoExercicioLicao_ToTable_3] FOREIGN KEY ([Exercicio]) REFERENCES [Exercicio]([IdExercicio])
)