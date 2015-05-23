﻿CREATE TABLE [dbo].[Aluno]
(
	[IdAluno] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nome] VARCHAR(150) NOT NULL, 
    [Username] VARCHAR(75) NOT NULL UNIQUE, 
    [Password] VARCHAR(75) NOT NULL, 
    [DataNasc] DATE NULL, 
    [Dica] TINYINT NOT NULL DEFAULT 1, 
    [Tema] INT NOT NULL DEFAULT 1, 
    [Explicacao] TINYINT NOT NULL DEFAULT 1, 
    [Pontuacao] INT NOT NULL DEFAULT 0,
	CONSTRAINT AK_USERNAME UNIQUE (Username)
)
