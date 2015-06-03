CREATE TABLE [dbo].[Licao]
(
	[idLicao] INT NOT NULL, 
	[NumExpl] INT NOT NULL,
	[Tipo] INT NOT NULL,
	[Administrador] INT NULL,
	[Texto] TEXT,
	[Video] VARCHAR(500),
	[Imagem] VARBINARY(MAX),
	[TempoLicao] INT NOT NULL,
	PRIMARY KEY ([idLicao], [NumExpl]), 
	CONSTRAINT [FK_Licao_ToTable] FOREIGN KEY ([Tipo]) REFERENCES [Tipo]([IdTipo]),
	CONSTRAINT [FK_Licao_ToTable2] FOREIGN KEY ([Administrador]) REFERENCES [Administrador]([IdAdministrador])
	 

)