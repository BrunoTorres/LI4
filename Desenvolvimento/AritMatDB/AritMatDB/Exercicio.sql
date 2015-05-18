CREATE TABLE [dbo].[Exercicio]
(
	[IdExercicio] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Tipo] INT NOT NULL, 
    [Administrador] INT NULL, 
    [Texto] TEXT NULL, 
    [Dificuldade] INT NOT NULL, 
    [Imagem] VARBINARY(MAX) NULL, 
    [TempoEx] TIME NOT NULL, 
    CONSTRAINT [FK_Exercicio_ToTable] FOREIGN KEY ([Tipo]) REFERENCES [Tipo]([IdTipo]), 
    CONSTRAINT [FK_Exercicio_ToTable_1] FOREIGN KEY ([Administrador]) REFERENCES [Administrador]([IdAdministrador])
)
