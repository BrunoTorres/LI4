CREATE TABLE [dbo].[Aprendizagem]
(
	[IdAprendizagem] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Aluno] INT NOT NULL, 
    [Data] DATE NOT NULL, 
    [Estado] FLOAT NOT NULL, 
    [Tipo] INT NOT NULL, 
    CONSTRAINT [FK_Aprendizagem_ToTable] FOREIGN KEY ([Aluno]) REFERENCES [Aluno]([IdAluno]),
)