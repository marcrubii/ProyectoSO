#include <stdio.h>
#include <mysql/mysql.h>
#include <stdlib.h>

void consultar_ganadas(const char *nombre_jugador) {
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;

	// Configuración de la conexión
	const char *server = "localhost";
	const char *user = "root";
	const char *password = "mysql";
	const char *database = "bd";

	// Inicializar conexión
	conn = mysql_init(NULL);
	if (conn == NULL) {
		fprintf(stderr, "mysql_init() failed\n");
		return;
	}

	// Conectar a la base de datos
	conn = mysql_real_connect(conn, server, user, password, database, 0, NULL, 0);
	if (conn == NULL) {
		printf("Error al inicializar la conexion: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
	}

	printf("Conexión exitosa a la base de datos.\n");

	// Consulta SQL para obtener el número de partidas ganadas por el jugador
	char query[512];
	snprintf(query, sizeof(query), "SELECT COUNT(*) FROM partida p JOIN jugador j ON p.ganador = j.id WHERE j.nombre = '%s';", nombre_jugador);

	if (mysql_query(conn, query)) {
		fprintf(stderr, "Error en la consulta: %s\n", mysql_error(conn));
		mysql_close(conn);
		return;
	}

	res = mysql_store_result(conn);
	if (res == NULL) {
		fprintf(stderr, "Error al obtener resultados: %s\n", mysql_error(conn));
		mysql_close(conn);
		return;
	}

	// Mostrar resultado
	row = mysql_fetch_row(res);
	if (row != NULL) {
		printf("El jugador %s ha ganado %s partidas.\n", nombre_jugador, row[0]);
	} else {
		printf("No se encontraron datos para el jugador %s.\n", nombre_jugador);
	}

	// Liberar memoria y cerrar conexión
	mysql_free_result(res);
	mysql_close(conn);
}

int main() {
	char nombre[20];
	printf("Ingrese el nombre del jugador: ");
	scanf("%19s", nombre);
	consultar_ganadas(nombre);
	return 0;
}
