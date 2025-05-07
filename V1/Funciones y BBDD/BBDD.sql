DROP DATABASE IF EXISTS bd;
CREATE DATABASE bd;
USE bd;

CREATE TABLE jugador(
 id int,
 nombre varchar(20),
 contraseña varchar(20),
 oro int,
 PRIMARY KEY (id)
)ENGINE=InnoDB;

CREATE TABLE partida(
 id int,
 jugadores int,
 ganador int, -- Ahora guarda el ID del jugador que ganó
 PRIMARY KEY (id)
)ENGINE=InnoDB;

CREATE TABLE historial(
 idp int,
 idj int,
 tiempo int,
 fecha varchar(11),
 ganador_partida int,
 FOREIGN KEY (idp) REFERENCES partida(id),
 FOREIGN KEY (idj) REFERENCES jugador(id),
 FOREIGN KEY (ganador_partida) REFERENCES partida(id)
)ENGINE=InnoDB;

-- Inserción de datos en jugador
INSERT INTO jugador VALUES (1,'Marc','1234',100);
INSERT INTO jugador VALUES (2,'Alex','2222',200);
INSERT INTO jugador VALUES (3,'Juan','1010',150);
INSERT INTO jugador VALUES (4,'David','9999',150);

-- Inserción de datos en partida con IDs en lugar de nombres
INSERT INTO partida VALUES (1,2,2); -- Ganador: Alex (ID 2)
INSERT INTO partida VALUES (2,2,1); -- Ganador: Marc (ID 1)
INSERT INTO partida VALUES (3,2,4); -- Ganador: David (ID 4)
INSERT INTO partida VALUES (4,1,3); -- Ganador: Juan (ID 3)

-- Inserción de datos en historial (sin cambios)
INSERT INTO historial VALUES (1,2,16,'07/01/2025',1);
INSERT INTO historial VALUES (2,3,16,'06/01/2025',2);
INSERT INTO historial VALUES (2,1,7,'06/01/2025',2);
INSERT INTO historial VALUES (3,3,16,'06/01/2025',3);
INSERT INTO historial VALUES (3,2,15,'06/01/2025',3);
INSERT INTO historial VALUES (3,4,6,'06/01/2025',3);
INSERT INTO historial VALUES (3,1,6,'06/01/2025',3);
INSERT INTO historial VALUES (4,3,16,'27/12/2024',4);
INSERT INTO historial VALUES (4,4,9,'27/12/2024',4);
INSERT INTO historial VALUES (4,3,16,'27/12/2024',4);
INSERT INTO historial VALUES (4,2,9,'27/12/2024',4);
