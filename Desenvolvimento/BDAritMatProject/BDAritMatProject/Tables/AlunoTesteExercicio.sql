CREATE TABLE [dbo].[AlunoTesteExercicio]
(
	[Aluno] INT NOT NULL , 
    [Teste] INT NOT NULL, 
    [Exercicio] INT NOT NULL, 
    [Data] DATE NOT NULL, 
    [Nota] FLOAT NOT NULL, 
    PRIMARY KEY ([Aluno], [Exercicio], [Teste]), 
    CONSTRAINT [FK_AlunoTesteExercicio_ToTable] FOREIGN KEY ([Aluno]) REFERENCES [Aluno]([IdAluno]), 
    CONSTRAINT [FK_AlunoTesteExercicio_ToTable_1] FOREIGN KEY ([Teste]) REFERENCES [Teste]([IdTeste]), 
    CONSTRAINT [FK_AlunoTesteExercicio_ToTable_2] FOREIGN KEY ([Exercicio]) REFERENCES [Exercicio]([IdExercicio])
)