CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20201125084955_25_11_2020_leandro') THEN

    CREATE TABLE `Mensagem` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `IdPessoa` int NOT NULL,
        `IdVitima` int NOT NULL,
        `Texto` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `Status` longtext CHARACTER SET utf8mb4 NULL,
        `Sentimento` longtext CHARACTER SET utf8mb4 NULL,
        `Privacidade` int NOT NULL,
        `PoliticaPrivacidade` tinyint(1) NOT NULL,
        `QrCode` longblob NULL,
        `DataCriacao` datetime(6) NOT NULL,
        CONSTRAINT `PK_Mensagem` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20201125084955_25_11_2020_leandro') THEN

    CREATE TABLE `MensagemModerada` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `IdMensagem` int NOT NULL,
        `DataAcao` datetime(6) NOT NULL,
        `Status` longtext CHARACTER SET utf8mb4 NOT NULL,
        `IdAlteradoPor` int NOT NULL,
        CONSTRAINT `PK_MensagemModerada` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20201125084955_25_11_2020_leandro') THEN

    CREATE TABLE `Pessoa` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Nome` longtext CHARACTER SET utf8mb4 NULL,
        `SobreNome` longtext CHARACTER SET utf8mb4 NULL,
        `CPF` longtext CHARACTER SET utf8mb4 NULL,
        `RG` longtext CHARACTER SET utf8mb4 NULL,
        `Email` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Pessoa` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20201125084955_25_11_2020_leandro') THEN

    CREATE TABLE `Privacidade` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Descricao` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `Ativo` int NOT NULL,
        CONSTRAINT `PK_Privacidade` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20201125084955_25_11_2020_leandro') THEN

    CREATE TABLE `Sentimento` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Descricao` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `Ativo` int NOT NULL,
        CONSTRAINT `PK_Sentimento` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20201125084955_25_11_2020_leandro') THEN

    CREATE TABLE `Status` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Descricao` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `Ativo` int NOT NULL,
        CONSTRAINT `PK_Status` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20201125084955_25_11_2020_leandro') THEN

    CREATE TABLE `Usuario` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Nome` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        `Sobrenome` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        `Email` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        `Ativo` int NOT NULL,
        `Senha` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        `ConfirmarSenha` longtext CHARACTER SET utf8mb4 NULL,
        `TipoUsuario` int NOT NULL,
        CONSTRAINT `PK_Usuario` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20201125084955_25_11_2020_leandro') THEN

    CREATE TABLE `Vitima` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Nome` longtext CHARACTER SET utf8mb4 NULL,
        `SobreNome` longtext CHARACTER SET utf8mb4 NULL,
        `CPF` longtext CHARACTER SET utf8mb4 NULL,
        `RG` longtext CHARACTER SET utf8mb4 NULL,
        `Rua` longtext CHARACTER SET utf8mb4 NULL,
        `Cidade` longtext CHARACTER SET utf8mb4 NULL,
        `Estado` longtext CHARACTER SET utf8mb4 NULL,
        `Fotografia` longblob NULL,
        CONSTRAINT `PK_Vitima` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20201125084955_25_11_2020_leandro') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20201125084955_25_11_2020_leandro', '3.1.10');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

