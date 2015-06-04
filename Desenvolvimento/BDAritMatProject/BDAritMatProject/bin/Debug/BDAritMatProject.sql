﻿/*
Deployment script for BDAritMatProject

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "BDAritMatProject"
:setvar DefaultFilePrefix "BDAritMatProject"
:setvar DefaultDataPath "C:\Users\John\AppData\Local\Microsoft\VisualStudio\SSDT\BDAritMatProject"
:setvar DefaultLogPath "C:\Users\John\AppData\Local\Microsoft\VisualStudio\SSDT\BDAritMatProject"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE,
                DISABLE_BROKER 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Creating [dbo].[Administrador]...';


GO
CREATE TABLE [dbo].[Administrador] (
    [IdAdministrador] INT           IDENTITY (1, 1) NOT NULL,
    [DataNasc]        DATE          NULL,
    [Username]        VARCHAR (75)  NOT NULL,
    [Password]        VARCHAR (75)  NOT NULL,
    [Nome]            VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdAdministrador] ASC)
);


GO
PRINT N'Creating [dbo].[Aluno]...';


GO
CREATE TABLE [dbo].[Aluno] (
    [IdAluno]    INT           IDENTITY (1, 1) NOT NULL,
    [Nome]       VARCHAR (150) NOT NULL,
    [Username]   VARCHAR (75)  NOT NULL,
    [Password]   VARCHAR (75)  NOT NULL,
    [DataNasc]   DATE          NULL,
    [Dica]       TINYINT       NOT NULL,
    [Tema]       INT           NOT NULL,
    [Explicacao] TINYINT       NOT NULL,
    [Pontuacao]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([IdAluno] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC),
    CONSTRAINT [AK_USERNAME] UNIQUE NONCLUSTERED ([Username] ASC)
);


GO
PRINT N'Creating [dbo].[AlunoExercicioLicao]...';


GO
CREATE TABLE [dbo].[AlunoExercicioLicao] (
    [Aluno]      INT        NOT NULL,
    [Licao]      INT        NOT NULL,
    [Explicacao] INT        NOT NULL,
    [Exercicio]  INT        NOT NULL,
    [Data]       DATE       NOT NULL,
    [Resposta]   FLOAT (53) NULL,
    PRIMARY KEY CLUSTERED ([Exercicio] ASC, [Aluno] ASC, [Licao] ASC, [Explicacao] ASC)
);


GO
PRINT N'Creating [dbo].[AlunoLicao]...';


GO
CREATE TABLE [dbo].[AlunoLicao] (
    [Aluno]       INT  NOT NULL,
    [Licao]       INT  NOT NULL,
    [Explicacao]  INT  NOT NULL,
    [Data]        DATE NOT NULL,
    [RespErradas] INT  NULL,
    PRIMARY KEY CLUSTERED ([Aluno] ASC, [Explicacao] ASC, [Licao] ASC)
);


GO
PRINT N'Creating [dbo].[AlunoTesteExercicio]...';


GO
CREATE TABLE [dbo].[AlunoTesteExercicio] (
    [Aluno]     INT        NOT NULL,
    [Teste]     INT        NOT NULL,
    [Exercicio] INT        NOT NULL,
    [Data]      DATE       NOT NULL,
    [Nota]      FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([Aluno] ASC, [Exercicio] ASC, [Teste] ASC)
);


GO
PRINT N'Creating [dbo].[Aprendizagem]...';


GO
CREATE TABLE [dbo].[Aprendizagem] (
    [IdAprendizagem] INT        IDENTITY (1, 1) NOT NULL,
    [Aluno]          INT        NOT NULL,
    [Data]           DATE       NOT NULL,
    [Estado]         FLOAT (53) NOT NULL,
    [Tipo]           INT        NULL,
    PRIMARY KEY CLUSTERED ([IdAprendizagem] ASC)
);


GO
PRINT N'Creating [dbo].[Dica]...';


GO
CREATE TABLE [dbo].[Dica] (
    [IdDica]    INT             NOT NULL,
    [Exercicio] INT             NOT NULL,
    [Texto]     TEXT            NULL,
    [Imagem]    VARBINARY (MAX) NULL,
    PRIMARY KEY CLUSTERED ([IdDica] ASC)
);


GO
PRINT N'Creating [dbo].[Exercicio]...';


GO
CREATE TABLE [dbo].[Exercicio] (
    [IdExercicio]   INT             IDENTITY (1, 1) NOT NULL,
    [Tipo]          INT             NOT NULL,
    [Administrador] INT             NULL,
    [Texto]         TEXT            NULL,
    [Dificuldade]   INT             NOT NULL,
    [Imagem]        VARBINARY (MAX) NULL,
    [TempoEx]       TIME (7)        NOT NULL,
    PRIMARY KEY CLUSTERED ([IdExercicio] ASC)
);


GO
PRINT N'Creating [dbo].[Licao]...';


GO
CREATE TABLE [dbo].[Licao] (
    [idLicao]       INT             NOT NULL,
    [NumExpl]       INT             NOT NULL,
    [Tipo]          INT             NOT NULL,
    [Administrador] INT             NOT NULL,
    [Texto]         TEXT            NULL,
    [Video]         VARCHAR (500)   NULL,
    [Imagem]        VARBINARY (MAX) NULL,
    [TempoLicao]    INT             NULL,
    PRIMARY KEY CLUSTERED ([idLicao] ASC, [NumExpl] ASC)
);


GO
PRINT N'Creating [dbo].[LicaoExercicio]...';


GO
CREATE TABLE [dbo].[LicaoExercicio] (
    [Licao]      INT NOT NULL,
    [Explicacao] INT NOT NULL,
    [Exercicio]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Licao] ASC, [Exercicio] ASC, [Explicacao] ASC)
);


GO
PRINT N'Creating [dbo].[Resposta]...';


GO
CREATE TABLE [dbo].[Resposta] (
    [IdResposta] INT  IDENTITY (1, 1) NOT NULL,
    [Exercicio]  INT  NOT NULL,
    [Pontuacao]  INT  NOT NULL,
    [Texto]      TEXT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdResposta] ASC)
);


GO
PRINT N'Creating [dbo].[Teste]...';


GO
CREATE TABLE [dbo].[Teste] (
    [IdTeste]     INT        IDENTITY (1, 1) NOT NULL,
    [Dificuldade] FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdTeste] ASC)
);


GO
PRINT N'Creating [dbo].[TesteExercicio]...';


GO
CREATE TABLE [dbo].[TesteExercicio] (
    [Teste]     INT NOT NULL,
    [Exercicio] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Exercicio] ASC, [Teste] ASC)
);


GO
PRINT N'Creating [dbo].[Tipo]...';


GO
CREATE TABLE [dbo].[Tipo] (
    [IdTipo] INT           IDENTITY (1, 1) NOT NULL,
    [Area]   VARCHAR (150) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdTipo] ASC)
);


GO
PRINT N'Creating unnamed constraint on [dbo].[Aluno]...';


GO
ALTER TABLE [dbo].[Aluno]
    ADD DEFAULT 1 FOR [Dica];


GO
PRINT N'Creating unnamed constraint on [dbo].[Aluno]...';


GO
ALTER TABLE [dbo].[Aluno]
    ADD DEFAULT 1 FOR [Tema];


GO
PRINT N'Creating unnamed constraint on [dbo].[Aluno]...';


GO
ALTER TABLE [dbo].[Aluno]
    ADD DEFAULT 1 FOR [Explicacao];


GO
PRINT N'Creating unnamed constraint on [dbo].[Aluno]...';


GO
ALTER TABLE [dbo].[Aluno]
    ADD DEFAULT 0 FOR [Pontuacao];


GO
PRINT N'Creating [dbo].[FK_AlunoExercicioLicao_ToTable]...';


GO
ALTER TABLE [dbo].[AlunoExercicioLicao] WITH NOCHECK
    ADD CONSTRAINT [FK_AlunoExercicioLicao_ToTable] FOREIGN KEY ([Aluno]) REFERENCES [dbo].[Aluno] ([IdAluno]);


GO
PRINT N'Creating [dbo].[FK_AlunoExercicioLicao_ToTable_1]...';


GO
ALTER TABLE [dbo].[AlunoExercicioLicao] WITH NOCHECK
    ADD CONSTRAINT [FK_AlunoExercicioLicao_ToTable_1] FOREIGN KEY ([Licao], [Explicacao]) REFERENCES [dbo].[Licao] ([idLicao], [NumExpl]);


GO
PRINT N'Creating [dbo].[FK_AlunoExercicioLicao_ToTable_3]...';


GO
ALTER TABLE [dbo].[AlunoExercicioLicao] WITH NOCHECK
    ADD CONSTRAINT [FK_AlunoExercicioLicao_ToTable_3] FOREIGN KEY ([Exercicio]) REFERENCES [dbo].[Exercicio] ([IdExercicio]);


GO
PRINT N'Creating [dbo].[FK_AlunoLicao_ToTable]...';


GO
ALTER TABLE [dbo].[AlunoLicao] WITH NOCHECK
    ADD CONSTRAINT [FK_AlunoLicao_ToTable] FOREIGN KEY ([Aluno]) REFERENCES [dbo].[Aluno] ([IdAluno]);


GO
PRINT N'Creating [dbo].[FK_AlunoLicao_ToTable_1]...';


GO
ALTER TABLE [dbo].[AlunoLicao] WITH NOCHECK
    ADD CONSTRAINT [FK_AlunoLicao_ToTable_1] FOREIGN KEY ([Licao], [Explicacao]) REFERENCES [dbo].[Licao] ([idLicao], [NumExpl]);


GO
PRINT N'Creating [dbo].[FK_AlunoTesteExercicio_ToTable]...';


GO
ALTER TABLE [dbo].[AlunoTesteExercicio] WITH NOCHECK
    ADD CONSTRAINT [FK_AlunoTesteExercicio_ToTable] FOREIGN KEY ([Aluno]) REFERENCES [dbo].[Aluno] ([IdAluno]);


GO
PRINT N'Creating [dbo].[FK_AlunoTesteExercicio_ToTable_1]...';


GO
ALTER TABLE [dbo].[AlunoTesteExercicio] WITH NOCHECK
    ADD CONSTRAINT [FK_AlunoTesteExercicio_ToTable_1] FOREIGN KEY ([Teste]) REFERENCES [dbo].[Teste] ([IdTeste]);


GO
PRINT N'Creating [dbo].[FK_AlunoTesteExercicio_ToTable_2]...';


GO
ALTER TABLE [dbo].[AlunoTesteExercicio] WITH NOCHECK
    ADD CONSTRAINT [FK_AlunoTesteExercicio_ToTable_2] FOREIGN KEY ([Exercicio]) REFERENCES [dbo].[Exercicio] ([IdExercicio]);


GO
PRINT N'Creating [dbo].[FK_Aprendizagem_ToTable]...';


GO
ALTER TABLE [dbo].[Aprendizagem] WITH NOCHECK
    ADD CONSTRAINT [FK_Aprendizagem_ToTable] FOREIGN KEY ([Aluno]) REFERENCES [dbo].[Aluno] ([IdAluno]);


GO
PRINT N'Creating [dbo].[FK_Dica_ToTable_1]...';


GO
ALTER TABLE [dbo].[Dica] WITH NOCHECK
    ADD CONSTRAINT [FK_Dica_ToTable_1] FOREIGN KEY ([Exercicio]) REFERENCES [dbo].[Exercicio] ([IdExercicio]);


GO
PRINT N'Creating [dbo].[FK_Exercicio_ToTable]...';


GO
ALTER TABLE [dbo].[Exercicio] WITH NOCHECK
    ADD CONSTRAINT [FK_Exercicio_ToTable] FOREIGN KEY ([Tipo]) REFERENCES [dbo].[Tipo] ([IdTipo]);


GO
PRINT N'Creating [dbo].[FK_Exercicio_ToTable_1]...';


GO
ALTER TABLE [dbo].[Exercicio] WITH NOCHECK
    ADD CONSTRAINT [FK_Exercicio_ToTable_1] FOREIGN KEY ([Administrador]) REFERENCES [dbo].[Administrador] ([IdAdministrador]);


GO
PRINT N'Creating [dbo].[FK_Licao_ToTable]...';


GO
ALTER TABLE [dbo].[Licao] WITH NOCHECK
    ADD CONSTRAINT [FK_Licao_ToTable] FOREIGN KEY ([Tipo]) REFERENCES [dbo].[Tipo] ([IdTipo]);


GO
PRINT N'Creating [dbo].[FK_Licao_ToTable2]...';


GO
ALTER TABLE [dbo].[Licao] WITH NOCHECK
    ADD CONSTRAINT [FK_Licao_ToTable2] FOREIGN KEY ([Administrador]) REFERENCES [dbo].[Administrador] ([IdAdministrador]);


GO
PRINT N'Creating [dbo].[FK_LicaoExercicio_ToTable_1]...';


GO
ALTER TABLE [dbo].[LicaoExercicio] WITH NOCHECK
    ADD CONSTRAINT [FK_LicaoExercicio_ToTable_1] FOREIGN KEY ([Licao], [Explicacao]) REFERENCES [dbo].[Licao] ([idLicao], [NumExpl]);


GO
PRINT N'Creating [dbo].[FK_LicaoExercicio_ToTable_2]...';


GO
ALTER TABLE [dbo].[LicaoExercicio] WITH NOCHECK
    ADD CONSTRAINT [FK_LicaoExercicio_ToTable_2] FOREIGN KEY ([Exercicio]) REFERENCES [dbo].[Exercicio] ([IdExercicio]);


GO
PRINT N'Creating [dbo].[FK_Resposta_ToTable]...';


GO
ALTER TABLE [dbo].[Resposta] WITH NOCHECK
    ADD CONSTRAINT [FK_Resposta_ToTable] FOREIGN KEY ([Exercicio]) REFERENCES [dbo].[Exercicio] ([IdExercicio]);


GO
PRINT N'Creating [dbo].[FK_TesteExercicio_ToTable]...';


GO
ALTER TABLE [dbo].[TesteExercicio] WITH NOCHECK
    ADD CONSTRAINT [FK_TesteExercicio_ToTable] FOREIGN KEY ([Teste]) REFERENCES [dbo].[Teste] ([IdTeste]);


GO
PRINT N'Creating [dbo].[FK_TesteExercicio_ToTable_1]...';


GO
ALTER TABLE [dbo].[TesteExercicio] WITH NOCHECK
    ADD CONSTRAINT [FK_TesteExercicio_ToTable_1] FOREIGN KEY ([Exercicio]) REFERENCES [dbo].[Exercicio] ([IdExercicio]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[AlunoExercicioLicao] WITH CHECK CHECK CONSTRAINT [FK_AlunoExercicioLicao_ToTable];

ALTER TABLE [dbo].[AlunoExercicioLicao] WITH CHECK CHECK CONSTRAINT [FK_AlunoExercicioLicao_ToTable_1];

ALTER TABLE [dbo].[AlunoExercicioLicao] WITH CHECK CHECK CONSTRAINT [FK_AlunoExercicioLicao_ToTable_3];

ALTER TABLE [dbo].[AlunoLicao] WITH CHECK CHECK CONSTRAINT [FK_AlunoLicao_ToTable];

ALTER TABLE [dbo].[AlunoLicao] WITH CHECK CHECK CONSTRAINT [FK_AlunoLicao_ToTable_1];

ALTER TABLE [dbo].[AlunoTesteExercicio] WITH CHECK CHECK CONSTRAINT [FK_AlunoTesteExercicio_ToTable];

ALTER TABLE [dbo].[AlunoTesteExercicio] WITH CHECK CHECK CONSTRAINT [FK_AlunoTesteExercicio_ToTable_1];

ALTER TABLE [dbo].[AlunoTesteExercicio] WITH CHECK CHECK CONSTRAINT [FK_AlunoTesteExercicio_ToTable_2];

ALTER TABLE [dbo].[Aprendizagem] WITH CHECK CHECK CONSTRAINT [FK_Aprendizagem_ToTable];

ALTER TABLE [dbo].[Dica] WITH CHECK CHECK CONSTRAINT [FK_Dica_ToTable_1];

ALTER TABLE [dbo].[Exercicio] WITH CHECK CHECK CONSTRAINT [FK_Exercicio_ToTable];

ALTER TABLE [dbo].[Exercicio] WITH CHECK CHECK CONSTRAINT [FK_Exercicio_ToTable_1];

ALTER TABLE [dbo].[Licao] WITH CHECK CHECK CONSTRAINT [FK_Licao_ToTable];

ALTER TABLE [dbo].[Licao] WITH CHECK CHECK CONSTRAINT [FK_Licao_ToTable2];

ALTER TABLE [dbo].[LicaoExercicio] WITH CHECK CHECK CONSTRAINT [FK_LicaoExercicio_ToTable_1];

ALTER TABLE [dbo].[LicaoExercicio] WITH CHECK CHECK CONSTRAINT [FK_LicaoExercicio_ToTable_2];

ALTER TABLE [dbo].[Resposta] WITH CHECK CHECK CONSTRAINT [FK_Resposta_ToTable];

ALTER TABLE [dbo].[TesteExercicio] WITH CHECK CHECK CONSTRAINT [FK_TesteExercicio_ToTable];

ALTER TABLE [dbo].[TesteExercicio] WITH CHECK CHECK CONSTRAINT [FK_TesteExercicio_ToTable_1];


GO
PRINT N'Update complete.';


GO