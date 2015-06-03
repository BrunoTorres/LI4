CREATE TABLE [dbo].[Aluno] (
    [IdAluno]    INT           IDENTITY (1, 1) NOT NULL,
    [Nome]       VARCHAR (150) NOT NULL,
    [Username]   VARCHAR (75)  NOT NULL,
    [Password]   VARCHAR (75)  NOT NULL,
    [DataNasc]   DATE          NULL,
    [Dica]       TINYINT       DEFAULT ((1)) NOT NULL,
    [Tema]       INT           DEFAULT ((1)) NOT NULL,
    [Explicacao] TINYINT       DEFAULT ((1)) NOT NULL,
    [Pontuacao]  INT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdAluno] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC),
    CONSTRAINT [AK_USERNAME] UNIQUE NONCLUSTERED ([Username] ASC)
);


