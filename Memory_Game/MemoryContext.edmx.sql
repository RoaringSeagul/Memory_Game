
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/29/2015 17:04:58
-- Generated from EDMX file: C:\420-GED\Memory_Game\Memory_Game\MemoryContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Memory];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CE_maPartie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jouer] DROP CONSTRAINT [FK_CE_maPartie];
GO
IF OBJECT_ID(N'[dbo].[FK_CE_monEtat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jouer] DROP CONSTRAINT [FK_CE_monEtat];
GO
IF OBJECT_ID(N'[dbo].[FK_CE_monUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jouer] DROP CONSTRAINT [FK_CE_monUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Etat]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Etat];
GO
IF OBJECT_ID(N'[dbo].[Jouer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Jouer];
GO
IF OBJECT_ID(N'[dbo].[Partie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Partie];
GO
IF OBJECT_ID(N'[dbo].[Utilisateur]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Utilisateur];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Etats'
CREATE TABLE [dbo].[Etats] (
    [idEtat] int  NOT NULL,
    [nomEtat] varchar(30)  NOT NULL
);
GO

-- Creating table 'Jouers'
CREATE TABLE [dbo].[Jouers] (
    [idUser] int  NOT NULL,
    [idEtat] int  NOT NULL,
    [idPartie] int  NOT NULL,
    [listeCombine] varchar(200)  NOT NULL
);
GO

-- Creating table 'Parties'
CREATE TABLE [dbo].[Parties] (
    [idPartie] int  NOT NULL,
    [dateHeurePartie] varchar(30)  NOT NULL
);
GO

-- Creating table 'Utilisateurs'
CREATE TABLE [dbo].[Utilisateurs] (
    [idUser] int  NOT NULL,
    [nomUser] varchar(30)  NOT NULL,
    [prenomUser] varchar(30)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idEtat] in table 'Etats'
ALTER TABLE [dbo].[Etats]
ADD CONSTRAINT [PK_Etats]
    PRIMARY KEY CLUSTERED ([idEtat] ASC);
GO

-- Creating primary key on [idUser], [idEtat] in table 'Jouers'
ALTER TABLE [dbo].[Jouers]
ADD CONSTRAINT [PK_Jouers]
    PRIMARY KEY CLUSTERED ([idUser], [idEtat] ASC);
GO

-- Creating primary key on [idPartie] in table 'Parties'
ALTER TABLE [dbo].[Parties]
ADD CONSTRAINT [PK_Parties]
    PRIMARY KEY CLUSTERED ([idPartie] ASC);
GO

-- Creating primary key on [idUser] in table 'Utilisateurs'
ALTER TABLE [dbo].[Utilisateurs]
ADD CONSTRAINT [PK_Utilisateurs]
    PRIMARY KEY CLUSTERED ([idUser] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [idEtat] in table 'Jouers'
ALTER TABLE [dbo].[Jouers]
ADD CONSTRAINT [FK_CE_monEtat]
    FOREIGN KEY ([idEtat])
    REFERENCES [dbo].[Etats]
        ([idEtat])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CE_monEtat'
CREATE INDEX [IX_FK_CE_monEtat]
ON [dbo].[Jouers]
    ([idEtat]);
GO

-- Creating foreign key on [idPartie] in table 'Jouers'
ALTER TABLE [dbo].[Jouers]
ADD CONSTRAINT [FK_CE_maPartie]
    FOREIGN KEY ([idPartie])
    REFERENCES [dbo].[Parties]
        ([idPartie])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CE_maPartie'
CREATE INDEX [IX_FK_CE_maPartie]
ON [dbo].[Jouers]
    ([idPartie]);
GO

-- Creating foreign key on [idUser] in table 'Jouers'
ALTER TABLE [dbo].[Jouers]
ADD CONSTRAINT [FK_CE_monUser]
    FOREIGN KEY ([idUser])
    REFERENCES [dbo].[Utilisateurs]
        ([idUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------