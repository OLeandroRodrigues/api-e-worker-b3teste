create database b3teste_db;
use b3teste_db;

CREATE TABLE `tipo_status` (
  `TipoStatusId` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`TipoStatusId`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ;

INSERT INTO `b3teste_db`.`tipo_status`
(`TipoStatusId`,`Descricao`)
VALUES(1,'Aberto'),
	(2,'Pendente'),
    (3,'Fechado');
    
    CREATE TABLE `tarefa` (
  `TarefaId` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(50) DEFAULT NULL,
  `Data` datetime NOT NULL,
  `TipoStatusId` int NOT NULL,
  PRIMARY KEY (`TarefaId`),
  CONSTRAINT `tarefa_tarefa_TipoStatusId` FOREIGN KEY (`TipoStatusId`) REFERENCES `tipo_status` (`TipoStatusId`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ;