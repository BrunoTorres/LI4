CREATE TABLE [dbo].[TesteExercicio]
(
	[Teste] INT NOT NULL , 
    [Exercicio] INT NOT NULL, 
    PRIMARY KEY ([Exercicio], [Teste]), 
    CONSTRAINT [FK_TesteExercicio_ToTable] FOREIGN KEY ([Teste]) REFERENCES [Teste]([IdTeste]), 
    CONSTRAINT [FK_TesteExercicio_ToTable_1] FOREIGN KEY ([Exercicio]) REFERENCES [Exercicio]([IdExercicio])
)