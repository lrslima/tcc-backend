CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` VARCHAR(95) NOT NULL,
    `ProductVersion` VARCHAR(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Mensagem` (
    `Id` INT NOT NULL AUTO_INCREMENT,
    `IdPessoa` INT NOT NULL,
    `IdVitima` INT NOT NULL,
    `Texto` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Status` longtext CHARACTER SET utf8mb4 NULL,
    `Sentimento` longtext CHARACTER SET utf8mb4 NULL,

                                                                    << ===== CONFIRMAR COM FRONT END ===== >>    

    `Privacidade` int NOT NULL,
    

    `PoliticaPrivacidade` tinyint(1) NOT NULL,
    `QrCode` LONGBLOB NULL,
    `DataCriacao` DATE NOT NULL,
    CONSTRAINT `PK_Mensagem` PRIMARY KEY (`Id`)
);

CREATE TABLE `Pessoa` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nome` longtext CHARACTER SET utf8mb4 NULL,
    `SobreNome` longtext CHARACTER SET utf8mb4 NULL,
    `CPF` longtext CHARACTER SET utf8mb4 NULL,
    `RG` longtext CHARACTER SET utf8mb4 NULL,
    `Email` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Pessoa` PRIMARY KEY (`Id`)
);

CREATE TABLE `MensagemModerada` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `IdMensagem` int NOT NULL,
    `DataAcao` datetime(6) NOT NULL,
    `Status` longtext CHARACTER SET utf8mb4 NOT NULL,
    `IdAlteradoPor` int NOT NULL,
    CONSTRAINT `PK_MensagemModerada` PRIMARY KEY (`Id`)
);

CREATE TABLE `Privacidade` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Descricao` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Ativo` int NOT NULL,
    CONSTRAINT `PK_Privacidade` PRIMARY KEY (`Id`)
);

CREATE TABLE `Sentimento` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Descricao` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Ativo` int NOT NULL,
    CONSTRAINT `PK_Sentimento` PRIMARY KEY (`Id`)
);

CREATE TABLE `Status` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Descricao` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `Ativo` int NOT NULL,
    CONSTRAINT `PK_Status` PRIMARY KEY (`Id`)
);

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

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20201121183834_InitialCreate', '3.1.10');

INSERT INTO `Status` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Pendente', '1');
INSERT INTO `Status` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Aprovado', '1');
INSERT INTO `Status` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Reprovado', '1');

INSERT INTO `Sentimento` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Boas lembranças', '1');
INSERT INTO `Sentimento` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Saudades', '1');
INSERT INTO `Sentimento` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Fé', '1');
INSERT INTO `Sentimento` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Esperança', '1');
INSERT INTO `Sentimento` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Inconformismo', '1');
INSERT INTO `Sentimento` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Pesar', '1');
INSERT INTO `Sentimento` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Outros sentimentos', '1');

INSERT INTO `Privacidade` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Sigilosa', '1');
INSERT INTO `Privacidade` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Sigilosa - Somente o texto será colocado na cápsula do tempo sem identificação de sua autoria', '1');
INSERT INTO `Privacidade` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Parcialmente pública - Será relevado o seu conteúdo e autoria daqui dez anos', '1');
INSERT INTO `Privacidade` (`Id`, `Descricao`, `Ativo`) VALUES (NULL, 'Pública - Constará no blog do projeto em memória das vítimas do COVID-19', '1');
