CREATE TABLE [dbo].[AlunoLicao] (
    [Aluno]       INT      NOT NULL,
    [Licao]       INT      NOT NULL,
    [Explicacao]  INT      NOT NULL,
    [Data]        DATETIME NOT NULL,
    [RespErradas] INT      NULL,
    [RespCertas] INT NULL, 
    PRIMARY KEY CLUSTERED ([Aluno] ASC, [Explicacao] ASC, [Licao] ASC),
    CONSTRAINT [FK_AlunoLicao_ToTable] FOREIGN KEY ([Aluno]) REFERENCES [dbo].[Aluno] ([IdAluno]),
    CONSTRAINT [FK_AlunoLicao_ToTable_1] FOREIGN KEY ([Licao], [Explicacao]) REFERENCES [dbo].[Licao] ([idLicao], [NumExpl])
);

