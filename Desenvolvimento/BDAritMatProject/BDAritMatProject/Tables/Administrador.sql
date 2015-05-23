CREATE TABLE [dbo].[Administrador]
(
	[IdAdministrador] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DataNasc] DATE NULL, 
    [Username] VARCHAR(75) NOT NULL, 
    [Password] VARCHAR(75) NOT NULL, 
    [Nome] VARCHAR(100) NOT NULL
)