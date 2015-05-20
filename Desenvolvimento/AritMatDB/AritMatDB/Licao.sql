USE AritMatDB;
CREATE TABLE [dbo].[Licao]
(
	[idLicao] INT NOT NULL, 
	[NumExpl] INT NOT NULL,
	[Tipo] INT NOT NULL,
	[Administrador] INT NOT NULL,
	[Texto] VARCHAR(120),
	[Video] VARCHAR(500),
	[Imagem] VARBINARY,
	[TempoLicao] Time,
	PRIMARY KEY ([idLicao], [NumExpl]), 
	CONSTRAINT [FK_Licao_ToTable] FOREIGN KEY ([Tipo]) REFERENCES [Tipo]([IdTipo]),
	CONSTRAINT [FK_Licao_ToTable2] FOREIGN KEY ([Administrador]) REFERENCES [Administrador]([IdAdministrador])
	 

)