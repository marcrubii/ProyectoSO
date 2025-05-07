#include <stdio.h>
#include <mysql/mysql.h>
#include <string.h>
#include <stdlib.h>

int main() {
    MYSQL *conn;
    int err;
    MYSQL_RES *resultado;
    MYSQL_ROW row;
    char consulta[256];
    char id[10];

    // Conectamos con el servidor MySQL
    conn = mysql_init(NULL);
    if (conn == NULL) {
        printf("Error al crear la conexi\u00f3n: %u %s\n", mysql_errno(conn), mysql_error(conn));
        exit(1);
    }

    // Inicializamos la conexión con la base de datos 'bd'
    conn = mysql_real_connect(conn, "localhost", "root", "mysql", "bd", 0, NULL, 0);
    if (conn == NULL) {
        printf("Error al inicializar la conexi\u00f3n: %u %s\n", mysql_errno(conn), mysql_error(conn));
        exit(1);
    }

    // Pedimos al usuario el ID de la partida
    printf("Escribe la ID de la partida para conocer sus jugadores:\n");
    if (scanf("%9s", id) != 1) { // Limitamos la entrada para evitar desbordamientos
        printf("Error al introducir los datos\n");
        mysql_close(conn);
        exit(1);
    }

    // Construimos la consulta SQL con medidas de seguridad
    snprintf(consulta, sizeof(consulta), 
             "SELECT DISTINCT jugador.nombre FROM historial JOIN jugador ON historial.idj = jugador.id WHERE historial.idp = '%s'", id);

    // Ejecutamos la consulta
    err = mysql_query(conn, consulta);
    if (err != 0) {
        printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
        mysql_close(conn);
        exit(1);
    }

    // Obtenemos y mostramos los resultados
    resultado = mysql_store_result(conn);
    if (resultado == NULL) {
        printf("Error al obtener los resultados de la consulta\n");
        mysql_close(conn);
        exit(1);
    }

    if (mysql_num_rows(resultado) == 0) {
        printf("No se encontraron jugadores para la partida con ID %s.\n", id);
    } else {
        printf("Los jugadores que han jugado en la partida %s son:\n", id);
        while ((row = mysql_fetch_row(resultado)) != NULL) {
            printf("- %s\n", row[0]);
        }
    }

    // Liberamos memoria y cerramos conexión
    mysql_free_result(resultado);
    mysql_close(conn);

    return 0;
}