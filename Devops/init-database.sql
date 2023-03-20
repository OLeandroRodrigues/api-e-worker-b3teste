



use b3teste_db;

CREATE TABLE `b3teste_db`.`tarefa_status` (
  `TarefaStatusId` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`TarefaStatusId`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ;

INSERT INTO `b3teste_db`.`tarefa_status`
(`TipoStatusId`,`Descricao`)
VALUES(1,'Aberto'),
	(2,'Pendente'),
    (3,'Fechado');
    
CREATE TABLE `tarefa` (
  `TarefaId` int NOT NULL AUTO_INCREMENT,
  `TarefaStatusId` int NOT NULL,
  `Descricao` varchar(50) DEFAULT NULL,
  `Data` datetime NOT NULL,
  PRIMARY KEY (`TarefaId`),
  CONSTRAINT `tarefa_tarefa_TipoStatusId` FOREIGN KEY (`TarefaStatusId`) REFERENCES `tarefa_status` (`TarefaStatusId`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci ;