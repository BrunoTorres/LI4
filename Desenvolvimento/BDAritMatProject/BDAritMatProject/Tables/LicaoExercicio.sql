CREATE TABLE [dbo].[LicaoExercicio]
(
	[Licao] INT NOT NULL , 
	[Explicacao] INT NOT NULL , 
    [Exercicio] INT NOT NULL, 
    PRIMARY KEY ([Licao], [Exercicio], [Explicacao]), 
    CONSTRAINT [FK_LicaoExercicio_ToTable_1] FOREIGN KEY ([Licao],[Explicacao]) REFERENCES [Licao]([IdLicao],[NumExpl]), 
    CONSTRAINT [FK_LicaoExercicio_ToTable_2] FOREIGN KEY ([Exercicio]) REFERENCES [Exercicio]([IdExercicio])
)