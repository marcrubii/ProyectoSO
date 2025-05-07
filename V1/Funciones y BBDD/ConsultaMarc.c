#include <stdio.h>
#include <stdlib.h>
#include <mysql/mysql.h>

#define SERVER "localhost"
#define USER "root"
#define PASSWORD "mysql" // Asegúrate de usar la contraseña correcta
#define DATABASE "bd"

int main() {
    MYSQL *conn;
    MYSQL_RES *res;
    MYSQL_ROW row;
    
    char query[256];
    int player_id;
    
    printf("Introduce tu ID de jugador: ");
    if (scanf("%d", &player_id) != 1) {
        fprintf(stderr, "Error al leer el ID del jugador.\n");
        return EXIT_FAILURE;
    }
    
    conn = mysql_init(NULL);
    if (conn == NULL) {
        fprintf(stderr, "mysql_init() failed\n");
        return EXIT_FAILURE;
    }
    
    if (mysql_real_connect(conn, SERVER, USER, PASSWORD, DATABASE, 0, NULL, 0) == NULL) {
        fprintf(stderr, "mysql_real_connect() failed: %s\n", mysql_error(conn));
        mysql_close(conn);
        return EXIT_FAILURE;
    }
    
    snprintf(query, sizeof(query),
             "SELECT DISTINCT j.nombre FROM historial h "
             "JOIN jugador j ON h.idj = j.id "
             "JOIN partida p ON h.idp = p.id "
             "WHERE h.idp IN (SELECT idp FROM historial WHERE idj = %d) "
             "AND j.id <> %d;", player_id, player_id);
    
    if (mysql_query(conn, query)) {
        fprintf(stderr, "Query failed: %s\n", mysql_error(conn));
        mysql_close(conn);
        return EXIT_FAILURE;
    }
    
    res = mysql_store_result(conn);
    if (res == NULL) {
        fprintf(stderr, "mysql_store_result() failed: %s\n", mysql_error(conn));
        mysql_close(conn);
        return EXIT_FAILURE;
    }
    
    if (mysql_num_rows(res) == 0) {
        printf("No se encontraron jugadores con los que hayas jugado.\n");
    } else {
        printf("\nJugadores con los que has jugado:\n");
        while ((row = mysql_fetch_row(res))) {
            printf("- %s\n", row[0]);
        }
    }
    
    mysql_free_result(res);
    mysql_close(conn);
    return EXIT_SUCCESS;
}