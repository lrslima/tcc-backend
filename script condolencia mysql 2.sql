CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` VARCHAR(95) NOT NULL,
    `ProductVersion` VARCHAR(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Mensagem` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `IdPessoa` INT NOT NULL,
    `IdVitima` INT NOT NULL,
    `Texto` MEDIUMTEXT NOT NULL,
    `Status` varchar(255) NULL,
    `Sentimento` varchar(255) NULL,
    `Privacidade` varchar(255) NULL,
    `QrCode` BIGINT NOT NULL,
    CONSTRAINT `PK_Mensagem` PRIMARY KEY (`Id`)
);

CREATE TABLE `MensagemModerada` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `IdMensagem` INT NOT NULL,
    `DataAcao` DATETIME NOT NULL,
    `Status` varchar(255) NOT NULL,
    CONSTRAINT `PK_MensagemModerada` PRIMARY KEY (`Id`)
);

CREATE TABLE `Pessoa` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Nome` varchar(255) NULL,
    `SobreNome` varchar(255) NULL,
    `CPF` varchar(255) NULL,
    `RG` varchar(255) NULL,
    `Email` varchar(255) NULL,
    CONSTRAINT `PK_Pessoa` PRIMARY KEY (`Id`)
);

CREATE TABLE `Privacidade` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Descricao` varchar(255) NOT NULL,
    `Ativo` INT NOT NULL,
    CONSTRAINT `PK_Privacidade` PRIMARY KEY (`Id`)
);

CREATE TABLE `Sentimento` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Descricao` varchar(255) NOT NULL,
    `Ativo` INT NOT NULL,
    CONSTRAINT `PK_Sentimento` PRIMARY KEY (`Id`)
);

CREATE TABLE `Status` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Descricao` varchar(50) NOT NULL,
    `Ativo` INT NOT NULL,
    CONSTRAINT `PK_Status` PRIMARY KEY (`Id`)
);

CREATE TABLE `Usuario` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Nome` varchar(100) NOT NULL,
    `Sobrenome` varchar(100) NOT NULL,
    `Email` varchar(100) NOT NULL,
    `Ativo` INT NOT NULL,
    `Senha` varchar(100) NOT NULL,
    `ConfirmarSenha` varchar(255) NULL,
    CONSTRAINT `PK_Usuario` PRIMARY KEY (`Id`)
);

CREATE TABLE `Vitima` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Nome` varchar(255) NULL,
    `SobreNome` varchar(255) NULL,
    `CPF` varchar(255) NULL,
    `RG` varchar(255) NULL,
    `Rua` varchar(255) NULL,
    `Cidade` varchar(255) NULL,
    `Estado` varchar(255) NULL,
    `Fotografia` varchar(255) NULL,
    CONSTRAINT `PK_Vitima` PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20201121183834_InitialCreate', '3.1.10');