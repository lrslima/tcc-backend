IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Mensagem] (
    [Id] int NOT NULL IDENTITY,
    [IdPessoa] int NOT NULL,
    [IdVitima] int NOT NULL,
    [Texto] nvarchar(255) NOT NULL,
    [Status] nvarchar(max) NULL,
    [Sentimento] nvarchar(max) NULL,
    [Privacidade] nvarchar(max) NULL,
    [QrCode] bigint NOT NULL,
    CONSTRAINT [PK_Mensagem] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MensagemModerada] (
    [Id] int NOT NULL IDENTITY,
    [IdMensagem] int NOT NULL,
    [DataAcao] datetime2 NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_MensagemModerada] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pessoa] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [SobreNome] nvarchar(max) NULL,
    [CPF] nvarchar(max) NULL,
    [RG] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    CONSTRAINT [PK_Pessoa] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Privacidade] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(255) NOT NULL,
    [Ativo] int NOT NULL,
    CONSTRAINT [PK_Privacidade] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Sentimento] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(255) NOT NULL,
    [Ativo] int NOT NULL,
    CONSTRAINT [PK_Sentimento] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Status] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(50) NOT NULL,
    [Ativo] int NOT NULL,
    CONSTRAINT [PK_Status] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Usuario] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(100) NOT NULL,
    [Sobrenome] nvarchar(100) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [Ativo] int NOT NULL,
    [Senha] nvarchar(100) NOT NULL,
    [ConfirmarSenha] nvarchar(max) NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Vitima] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [SobreNome] nvarchar(max) NULL,
    [CPF] nvarchar(max) NULL,
    [RG] nvarchar(max) NULL,
    [Rua] nvarchar(max) NULL,
    [Cidade] nvarchar(max) NULL,
    [Estado] nvarchar(max) NULL,
    [Fotografia] nvarchar(max) NULL,
    CONSTRAINT [PK_Vitima] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201121183834_InitialCreate', N'5.0.0');
GO

COMMIT;
GO

