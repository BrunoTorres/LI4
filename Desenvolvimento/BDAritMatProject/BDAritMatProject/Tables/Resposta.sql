CREATE TABLE [dbo].[Resposta]
(
	[IdResposta] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Exercicio] INT NOT NULL, 
    [Pontuacao] INT NOT NULL, 
    [Texto] TEXT NOT NULL, 
    CONSTRAINT [FK_Resposta_ToTable] FOREIGN KEY ([Exercicio]) REFERENCES [Exercicio]([IdExercicio])
)