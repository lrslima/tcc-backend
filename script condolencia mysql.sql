CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Mensagem` (
    `Id` int NOT NULL,
    `IdPessoa` int NOT NULL,
    `IdVitima` int NOT NULL,
    `Texto` MEDIUMTEXT NOT NULL,
    `Status` nvarchar(255) NULL,
    `Sentimento` nvarchar(255) NULL,
    `Privacidade` nvarchar(255) NULL,
    `QrCode` bigint NOT NULL,
    CONSTRAINT `PK_Mensagem` PRIMARY KEY (`Id`)
);

CREATE TABLE `MensagemModerada` (
    `Id` int NOT NULL,
    `IdMensagem` int NOT NULL,
    `DataAcao` datetime NOT NULL,
    `Status` nvarchar(255) NOT NULL,
    CONSTRAINT `PK_MensagemModerada` PRIMARY KEY (`Id`)
);

CREATE TABLE `Pessoa` (
    `Id` int NOT NULL,
    `Nome` nvarchar(255) NULL,
    `SobreNome` nvarchar(255) NULL,
    `CPF` nvarchar(255) NULL,
    `RG` nvarchar(255) NULL,
    `Email` nvarchar(255) NULL,
    CONSTRAINT `PK_Pessoa` PRIMARY KEY (`Id`)
);

CREATE TABLE `Privacidade` (
    `Id` int NOT NULL,
    `Descricao` nvarchar(255) NOT NULL,
    `Ativo` int NOT NULL,
    CONSTRAINT `PK_Privacidade` PRIMARY KEY (`Id`)
);

CREATE TABLE `Sentimento` (
    `Id` int NOT NULL,
    `Descricao` nvarchar(255) NOT NULL,
    `Ativo` int NOT NULL,
    CONSTRAINT `PK_Sentimento` PRIMARY KEY (`Id`)
);

CREATE TABLE `Status` (
    `Id` int NOT NULL,
    `Descricao` nvarchar(50) NOT NULL,
    `Ativo` int NOT NULL,
    CONSTRAINT `PK_Status` PRIMARY KEY (`Id`)
);

CREATE TABLE `Usuario` (
    `Id` int NOT NULL,
    `Nome` nvarchar(100) NOT NULL,
    `Sobrenome` nvarchar(100) NOT NULL,
    `Email` nvarchar(100) NOT NULL,
    `Ativo` int NOT NULL,
    `Senha` nvarchar(100) NOT NULL,
    `ConfirmarSenha` nvarchar(255) NULL,
    CONSTRAINT `PK_Usuario` PRIMARY KEY (`Id`)
);

CREATE TABLE `Vitima` (
    `Id` int NOT NULL,
    `Nome` nvarchar(255) NULL,
    `SobreNome` nvarchar(255) NULL,
    `CPF` nvarchar(255) NULL,
    `RG` nvarchar(255) NULL,
    `Rua` nvarchar(255) NULL,
    `Cidade` nvarchar(255) NULL,
    `Estado` nvarchar(255) NULL,
    `Fotografia` nvarchar(255) NULL,
    CONSTRAINT `PK_Vitima` PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20201121183834_InitialCreate', '3.1.10');