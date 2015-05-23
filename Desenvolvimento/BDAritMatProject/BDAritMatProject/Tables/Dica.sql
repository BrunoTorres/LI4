CREATE TABLE [dbo].[Dica]
(
	[IdDica] INT NOT NULL PRIMARY KEY, 
    [Exercicio] INT NOT NULL, 
    [Texto] TEXT NULL, 
    [Imagem] VARBINARY(MAX) NULL,
	CONSTRAINT [FK_Dica_ToTable_1] FOREIGN KEY ([Exercicio]) REFERENCES [Exercicio]([IdExercicio])
)