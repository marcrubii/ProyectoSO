#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql/mysql.h>


int main(int argc, char *argv[])
{
    int sock_conn, sock_listen, ret;
    struct sockaddr_in serv_adr;
    char peticion[512];
    char respuesta[512];

    MYSQL *conn;
    MYSQL_RES *res;
    MYSQL_ROW row;

    conn = mysql_init(NULL);
    if (conn == NULL) {
        fprintf(stderr, "mysql_init() failed\n");
        exit(EXIT_FAILURE);
    }

    if (mysql_real_connect(conn, "localhost", "root", "mysql", "bd", 0, NULL, 0) == NULL) {
        fprintf(stderr, "mysql_real_connect() failed: %s\n", mysql_error(conn));
        mysql_close(conn);
        exit(EXIT_FAILURE);
    }

    if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
        printf("Error creando socket\n");

    memset(&serv_adr, 0, sizeof(serv_adr));
    serv_adr.sin_family = AF_INET;
    serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
    serv_adr.sin_port = htons(9030);

    if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
        printf("Error al hacer el bind\n");

    if (listen(sock_listen, 3) < 0)
        printf("Error en el listen\n");

    for (;;) {
        printf("Escuchando\n");
        sock_conn = accept(sock_listen, NULL, NULL);
        printf("Conexion recibida\n");
        int terminar = 0;
        while (terminar == 0) {
            ret = read(sock_conn, peticion, sizeof(peticion));
            peticion[ret] = '\0';
            printf("Peticion: %s\n", peticion);

            char *p = strtok(peticion, "/");
            int codigo = atoi(p);
            char argumento[128];

            if (codigo == 0) {
                terminar = 1;
            } else {
                p = strtok(NULL, "/");
                strcpy(argumento, p);

                char consulta[512];
                respuesta[0] = '\0';

                if (codigo == 1) {
                    snprintf(consulta, sizeof(consulta), "SELECT DISTINCT jugador.nombre FROM historial JOIN jugador ON historial.idj = jugador.id WHERE historial.idp = '%s'", argumento);
                } 
                else if (codigo == 2) {
                    snprintf(consulta, sizeof(consulta), "SELECT DISTINCT jugador.nombre FROM historial JOIN jugador ON historial.idj = jugador.id WHERE historial.idp = '%s'", argumento);
                } 
                else if (codigo == 3) {
                    snprintf(consulta, sizeof(consulta), "SELECT COUNT(*) FROM historial WHERE ganador_partida = %s;", argumento);
                    if (mysql_query(conn, consulta) == 0) {
                        res = mysql_store_result(conn);
                        if (res != NULL && (row = mysql_fetch_row(res)) != NULL) {
                            sprintf(respuesta, "El jugador con ID %s ha ganado %s partidas.", argumento, row[0]);
                        }
                        mysql_free_result(res);
                    }
                }

                if (codigo != 0 && strlen(respuesta) == 0) {
                    if (mysql_query(conn, consulta)) {
                        sprintf(respuesta, "Error en la consulta: %s", mysql_error(conn));
                    } else {
                        res = mysql_store_result(conn);
                        if (res != NULL) {
                            while ((row = mysql_fetch_row(res)) != NULL) {
                                strcat(respuesta, row[0]);
                                strcat(respuesta, "\n");
                            }
                            mysql_free_result(res);
                        }
                    }
                }

                write(sock_conn, respuesta, strlen(respuesta));
            }
        }
        close(sock_conn);
    }

    mysql_close(conn);
    return 0;
}
