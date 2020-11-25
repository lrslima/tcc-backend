/*
SQLyog Trial v13.1.7 (64 bit)
MySQL - 8.0.19 : Database - condolencia
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`condolencia` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `condolencia`;

/*Table structure for table `__efmigrationshistory` */

DROP TABLE IF EXISTS `__efmigrationshistory`;

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `__efmigrationshistory` */

insert  into `__efmigrationshistory`(`MigrationId`,`ProductVersion`) values 
('20201121183834_InitialCreate','3.1.10'),
('20201125014931_politica_privacidade','3.1.10');

/*Table structure for table `mensagem` */

DROP TABLE IF EXISTS `mensagem`;

CREATE TABLE `mensagem` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdPessoa` int NOT NULL,
  `IdVitima` int NOT NULL,
  `Texto` longtext NOT NULL,
  `Status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Sentimento` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Privacidade` int NOT NULL,
  `QrCode` longblob,
  `DataCriacao` date NOT NULL,
  `PoliticaPrivacidade` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `mensagem` */

insert  into `mensagem`(`Id`,`IdPessoa`,`IdVitima`,`Texto`,`Status`,`Sentimento`,`Privacidade`,`QrCode`,`DataCriacao`,`PoliticaPrivacidade`) values 
(1,3,2,'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum rhoncus ligula vel risus aliquam, hendrerit pretium orci malesuada. Fusce molestie consectetur turpis, eget tincidunt odio hendrerit vel. Praesent porta lacus velit. Quisque finibus neque eget mattis lacinia. Donec commodo lobortis urna, id maximus urna blandit vel. Aliquam congue elementum neque, ut aliquet lorem tempor quis. Suspendisse tincidunt consequat leo, vel pharetra erat sodales eget. Etiam nulla nisl, malesuada id mauris gravida, ullamcorper pulvinar enim. Ut condimentum libero et porttitor pharetra. Proin iaculis iaculis augue, sed viverra leo elementum ac. Aliquam ac nisl posuere, ornare ex nec, egestas sapien. Interdum et malesuada fames ac ante ipsum primis in faucibus. Nunc vel malesuada diam.','pendente',NULL,2,NULL,'2020-11-24',1);

/*Table structure for table `mensagemmoderada` */

DROP TABLE IF EXISTS `mensagemmoderada`;

CREATE TABLE `mensagemmoderada` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdMensagem` int NOT NULL,
  `DataAcao` datetime(6) NOT NULL,
  `Status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IdAlteradoPor` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `mensagemmoderada_ibfk_1` (`IdAlteradoPor`),
  CONSTRAINT `mensagemmoderada_ibfk_1` FOREIGN KEY (`IdAlteradoPor`) REFERENCES `pessoa` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `mensagemmoderada` */

/*Table structure for table `pessoa` */

DROP TABLE IF EXISTS `pessoa`;

CREATE TABLE `pessoa` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SobreNome` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `CPF` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `RG` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `pessoa` */

insert  into `pessoa`(`Id`,`Nome`,`SobreNome`,`CPF`,`RG`,`Email`) values 
(3,'Nome','Sobrenome','999.999.999-99','99.999.999-9','teste@teste.com');

/*Table structure for table `privacidade` */

DROP TABLE IF EXISTS `privacidade`;

CREATE TABLE `privacidade` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Ativo` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `privacidade` */

insert  into `privacidade`(`Id`,`Descricao`,`Ativo`) values 
(1,'Sigilosa',1),
(2,'Sigilosa - Somente o texto será colocado na cápsula do tempo sem identificação de sua autoria',1),
(3,'Parcialmente pública - Será relevado o seu conteúdo e autoria daqui dez anos',1),
(4,'Pública - Constará no blog do projeto em memória das vítimas do COVID-19',1);

/*Table structure for table `sentimento` */

DROP TABLE IF EXISTS `sentimento`;

CREATE TABLE `sentimento` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Ativo` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `sentimento` */

insert  into `sentimento`(`Id`,`Descricao`,`Ativo`) values 
(1,'Saudades',1),
(2,'Inconformismo',1),
(3,'Fé',1),
(4,'Esperança',1);

/*Table structure for table `status` */

DROP TABLE IF EXISTS `status`;

CREATE TABLE `status` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Ativo` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `status` */

insert  into `status`(`Id`,`Descricao`,`Ativo`) values 
(1,'Pendente',1),
(2,'Aprovado',1),
(3,'Reprovado',1);

/*Table structure for table `usuario` */

DROP TABLE IF EXISTS `usuario`;

CREATE TABLE `usuario` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Sobrenome` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Ativo` int NOT NULL,
  `Senha` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConfirmarSenha` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `usuario` */

/*Table structure for table `vitima` */

DROP TABLE IF EXISTS `vitima`;

CREATE TABLE `vitima` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SobreNome` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `CPF` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `RG` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Rua` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Cidade` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Estado` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Fotografia` longblob,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `vitima` */

insert  into `vitima`(`Id`,`Nome`,`SobreNome`,`CPF`,`RG`,`Rua`,`Cidade`,`Estado`,`Fotografia`) values 
(2,'Nome','Sobrenome','99.999.999-9',NULL,'R. Augusta','São Paulo','São Paulo',NULL);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
