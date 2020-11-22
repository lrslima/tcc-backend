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
    `Status` VARCHAR(255) NULL,
    `Sentimento` VARCHAR(255) NULL,
    `Privacidade` VARCHAR(255) NULL,
    `QrCode` LONGBLOB NULL,
    `DataCriacao` DATE NOT NULL,
    CONSTRAINT `PK_Mensagem` PRIMARY KEY (`Id`)
);

CREATE TABLE `Pessoa` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Nome` VARCHAR(255) NULL,
    `SobreNome` VARCHAR(255) NULL,
    `CPF` VARCHAR(255) NULL,
    `RG` VARCHAR(255) NULL,
    `Email` VARCHAR(255) NULL,
    CONSTRAINT `PK_Pessoa` PRIMARY KEY (`Id`)
);

CREATE TABLE `MensagemModerada` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `IdMensagem` INT NOT NULL,
    `DataAcao` DATETIME NOT NULL,
    `Status` VARCHAR(255) NOT NULL,
    `IdAlteradoPor` INT NOT NULL,
    CONSTRAINT `PK_MensagemModerada` PRIMARY KEY (`Id`),
    FOREIGN KEY (IdAlteradoPor) REFERENCES  Pessoa(Id) ON UPDATE CASCADE 
);

CREATE TABLE `Privacidade` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Descricao` VARCHAR(255) NOT NULL,
    `Ativo` INT NOT NULL,
    CONSTRAINT `PK_Privacidade` PRIMARY KEY (`Id`)
);

CREATE TABLE `Sentimento` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Descricao` VARCHAR(255) NOT NULL,
    `Ativo` INT NOT NULL,
    CONSTRAINT `PK_Sentimento` PRIMARY KEY (`Id`)
);

CREATE TABLE `Status` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Descricao` VARCHAR(50) NOT NULL,
    `Ativo` INT NOT NULL,
    CONSTRAINT `PK_Status` PRIMARY KEY (`Id`)
);

CREATE TABLE `Usuario` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Nome` VARCHAR(100) NOT NULL,
    `Sobrenome` VARCHAR(100) NOT NULL,
    `Email` VARCHAR(100) NOT NULL,
    `Ativo` INT NOT NULL,
    `Senha` VARCHAR(100) NOT NULL,
    `ConfirmarSenha` VARCHAR(255) NULL,
    CONSTRAINT `PK_Usuario` PRIMARY KEY (`Id`)
);

CREATE TABLE `Vitima` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `Nome` VARCHAR(255) NULL,
    `SobreNome` VARCHAR(255) NULL,
    `CPF` VARCHAR(255) NULL,
    `RG` VARCHAR(255) NULL,
    `Rua` VARCHAR(255) NULL,
    `Cidade` VARCHAR(255) NULL,
    `Estado` VARCHAR(255) NULL,
    `Fotografia` VARCHAR(255) NULL,
    CONSTRAINT `PK_Vitima` PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20201121183834_InitialCreate', '3.1.10');