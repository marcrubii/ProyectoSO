#include <stdio.h>
#include <mysql/mysql.h>
#include <stdlib.h>

void consultar_partidas_ganadas(int jugador_id) {
    MYSQL *conn;
    MYSQL_RES *res;
    MYSQL_ROW row;

    const char *server = "localhost";
    const char *user = "root";
    const char *password = "mysql";
    const char *database = "bd";

    conn = mysql_init(NULL);
    if (conn == NULL) {
        fprintf(stderr, "mysql_init() failed\n");
        exit(EXIT_FAILURE);
    }

    if (mysql_real_connect(conn, server, user, password, database, 0, NULL, 0) == NULL) {
        fprintf(stderr, "mysql_real_connect() failed: %s\n", mysql_error(conn));
        mysql_close(conn);
        exit(EXIT_FAILURE);
    }

    printf("Conexión exitosa a la base de datos.\n");

    char consulta[512];
    snprintf(consulta, sizeof(consulta), "SELECT COUNT(*) FROM partida WHERE ganador = %d;", jugador_id);

    if (mysql_query(conn, consulta)) {
        fprintf(stderr, "Error en la consulta: %s\n", mysql_error(conn));
        mysql_close(conn);
        exit(EXIT_FAILURE);
    }

    res = mysql_store_result(conn);
    if (res == NULL) {
        fprintf(stderr, "Error al obtener resultados: %s\n", mysql_error(conn));
        mysql_close(conn);
        exit(EXIT_FAILURE);
    }

    row = mysql_fetch_row(res);
    if (row != NULL) {
        printf("El jugador con ID %d ha ganado un total de %s partidas.\n", jugador_id, row[0]);
    } else {
        printf("No se encontraron partidas ganadas para el jugador con ID %d.\n", jugador_id);
    }

    mysql_free_result(res);
    mysql_close(conn);
}

int main() {
    int id_jugador;
    printf("Introduce el ID del jugador: ");
    scanf("%d", &id_jugador);
    consultar_partidas_ganadas(id_jugador);
    return 0;
}
