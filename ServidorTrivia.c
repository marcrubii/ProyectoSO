#include <stdio.h>
#include <string.h>
#include <stdlib.h> //Necesario para atof
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <unistd.h>
#include <ctype.h>
#include <mysql.h>
#include <pthread.h>
#include <time.h>
#include <math.h>

//Variables globales
	//Para la BBDD
MYSQL *conn;

//Esctructura necesaria para acceso autoexcluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

	//Lista de conectados (formada por usuarios)
typedef struct{
	char nombre[25];
	int socket;
}Usuario;

typedef struct{
	Usuario conectados[50];
	int num;
}ListaConectados;

typedef struct{
	int estado;
	int numInvitados;
	char host[25];
	int puntosHost[6];
	char jug2[25];
	int puntosJug2[6];
	char jug3[25];
	int puntosJug3[6];
	char jug4[25];
	int puntosJug4[6];
	char horaInicio;
}Partidas;


//Iniciamos la lista de conectados
ListaConectados listaC;
int len_tablaP=100;
Partidas tablaP[100];



//Funciones del servidor


//Iniciar sesi�n - codigo 1
int InicioSesion(char nombre[25], char password[20]){
	//Devuelve un 0 si todo va bien, un 1 si el user no se ha registrado, 2 si no coincide la contrase�a y -1 si algo falla en la consulta.
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	
	char consulta[80];
	strcpy(consulta,"SELECT contrasenya FROM jugadores WHERE nombre='");
	strcat(consulta,nombre);
	strcat(consulta,"'");
	
	
	err=mysql_query(conn,consulta);
	if (err != 0){
		printf("Error al consultar la BBDD %u %s",mysql_errno(conn),mysql_error(conn));
		return -1;
	}
	else{
		//Recibimos el resultado de la consulta
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		if (row == NULL)
			return 1;
		else
		{
			if (strcmp(password,row[0])==0){
				int i = 0;
				int encontrado = 0;
				while (i<listaC.num && encontrado == 0){
					if (strcmp(listaC.conectados[i].nombre,nombre)==0)
						encontrado = 1;
					else
						i=i+1;
				}
				if (encontrado==1)
					return 3;
				else
					return 0;
			}
			else
				return 2;
			
		}
		
	}
}

//Registrarse - codigo 2
int Registro(char nombre[25], char password[20], char mail[100]){
	//Retorna un 0 si todo va bien, un 1 si ya existe ese usuario y un -1 si algo va mal en la consulta
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	
	//Busca si ya hay un usuario con ese nombre
	char consulta[80];
	strcpy(consulta, "SELECT nombre FROM jugadores WHERE nombre='");
	strcat(consulta, nombre);
	strcat(consulta, "'");
	
	err = mysql_query(conn, consulta);
	if (err!=0)
		return -1;
	
	else {
		resultado = mysql_store_result(conn);
		row =  mysql_fetch_row(resultado);
		
		//Se registra UNICAMENTE si no hay nadie con el mismo nombre
		if (row==NULL) {
			
			err=mysql_query(conn, "SELECT * FROM jugadores WHERE id=(SELECT max(id) FROM jugadores);");
			if (err!=0){
				return -1;
			}
			resultado = mysql_store_result(conn);
			row =  mysql_fetch_row(resultado);
			int id = atoi(row[0])+1;
			sprintf(consulta,"INSERT INTO jugadores VALUES ('%d','%s','%s','%s');", id, nombre, password, mail);
			err= mysql_query(conn, consulta);
			if (err!=0)
				return -1;						
			else
				return 0; 
		}
		else
			return 1;  
		
	}
}

//Contrincantes - codigo 3
int DameContrincantes(char nombre[25], char contrincantes[500]){
	//Retorna los contrincantes separados por asteriscos o bien 0 si no hay ninguno
	//Resultado de la funcion: 1 si todo va bien, -1 si hay error en la BBDD y 0 si no hay contrincantes
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	
	strcpy(contrincantes,"");
	
	char consulta[500];
	char consulta1[500];
	sprintf(consulta1,"SELECT registro.idP FROM (jugadores,registro) WHERE jugadores.nombre='%s' AND jugadores.id=registro.idJ",nombre);
	sprintf(consulta,"SELECT DISTINCT jugadores.nombre FROM (jugadores,registro) WHERE jugadores.id=registro.idJ AND registro.idP IN (%s) AND jugadores.nombre!='%s'",consulta1,nombre);
	err=mysql_query(conn,consulta);
	if (err!=0)
		return -1;
	else{
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		
		if (row == NULL){
			strcpy(contrincantes,"0");
			return 0;
		}
		else{
			while(row!=NULL){
				sprintf(contrincantes,"%s%s*",contrincantes,row[0]);
				row = mysql_fetch_row(resultado);
			}
			contrincantes[strlen(contrincantes)-1]='\0';
			return 1;
		}
	}
}

//Informaci�n sobre las partidas con X contrincante/s - codigo 4
int DamePartidasContrincantes(char nombre[25], char contrincantes[500],char respuesta[1000]){
	//Retorna el nombre, el id de la partida y el id ganador separados por comas y separados por asteriscos del resto de partidas
	//Resultado de la funci�n: 0 si todo va bien y -1 si hay error en la BBDD
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	
	strcpy(respuesta,"");
	int entra = 0;
	int contr = 0;
	
	char *p = strtok(contrincantes,"/");
	while(p!=NULL){
		char contrincante[25];
		strcpy(contrincante,p);
		char consulta[500];
		char consulta1[500];
		sprintf(consulta1,"SELECT registro.idP FROM (registro,jugadores) WHERE jugadores.nombre='%s' AND jugadores.id = registro.idJ",nombre);
		sprintf(consulta,"SELECT partidas.id,partidas.ganador FROM (partidas,registro,jugadores) WHERE jugadores.nombre='%s' AND jugadores.id=registro.idJ AND registro.idP IN (%s) AND registro.idP=partidas.id",contrincante,consulta1);
		err=mysql_query (conn,consulta);
		if(err!=0){
			printf("No hago consulta\n");
			return -1;
		}
		else{
			resultado = mysql_store_result(conn);
			row = mysql_fetch_row(resultado);
			if(row==NULL){
				entra = entra + 1;
				printf("Soy null\n");
			}
			else if (row!=NULL){				
				while(row!=NULL){
					if(strcmp(respuesta,"")==0){
						printf("Contrincante: %s ; IdPartida: %s ; GanadorPartida: %s\n",contrincante,row[0],row[1]);
						sprintf(respuesta,"%s,%s,%s*",contrincante,row[0],row[1]);
					}
					else{
						printf("Contrincante: %s ; IdPartida: %s ; GanadorPartida: %s (else)\n",contrincante,row[0],row[1]);
						sprintf(respuesta,"%s%s,%s,%s*",respuesta,contrincante,row[0],row[1]);
					}
					row=mysql_fetch_row(resultado);
				}
			}				
		}
		contr=contr+1;
		
		p=strtok(NULL,"/");
	}
	if (entra==contr)
		strcpy(respuesta,"0");
	else{
		respuesta[strlen(respuesta)-1]='\0';
	}
	return 0;
	
}

//JMV (Jugador M�s Valioso) - codigo 5
int DameMVP(char nombre[25]){
	//Devuelve un 0 y el nombre del MVP si todo va bien, un -1 si hay un error en la conssulta y un -2 si no hay jugadores en la base de datos
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char consulta[200];
	
	err=mysql_query (conn, "SELECT jugadores.nombre FROM (jugadores, registro) WHERE registro.puntos=(SELECT MAX(registro.puntos) FROM registro) AND registro.idJ=jugadores.id");
	if (err!=0){
		strcpy(nombre,"\0");
		return -1;
	}
	else{
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		
		if (row == NULL){
			strcpy(nombre,"\0");
			return -2;
		}
		else{
			strcpy(nombre,row[0]); 
			sprintf(consulta,"SELECT SUM(registro.puntos) FROM (registro, jugadores) WHERE registro.idJ=jugadores.id AND jugadores.nombre='%s'",nombre);
			err=mysql_query(conn,consulta);
			if(err!=0){
				return -1;
			}
			else{
				resultado = mysql_store_result(conn);
				row = mysql_fetch_row(resultado);
				return atoi(row[0]);
			}
		}
	}
	
}
//Dame lista conectados - Codigo 6
int DameListaConectados(char lista[512]){
	//Retorna 0 --> Todo OK(+ char con la lista de conectados : user1/user2/user3/ etc) ; -1 --> Lista vacia (+ lista = '\0')
	
	lista[0]='\0';
	if (listaC.num != 0){
		int i;
		for (i=0;i<listaC.num;i++)
			sprintf(lista,"%s%s*",lista,listaC.conectados[i].nombre);
		lista[strlen(lista)-1]='\0';
		
		return 0;
	}
	else
		return -1;
	
}
//Retorna la fecha y hora actuales
int DameTiempo(char fecha[12],char hora[10]){
	//Retorna la hora en segundos
	
	time_t tiempo = time(0);
	struct tm *tlocal = localtime(&tiempo);
	strftime(fecha,10,"%d/%m/%y",tlocal);
	strftime(hora,10,"%H:%M:%S",tlocal);
	//printf("%s %s\n",fecha,hora);
	
	//Transformamos la hora a segundos
	int segundos;
	char horaSeg[10];
	strcpy(horaSeg,hora);
	char *p = strtok(horaSeg,":");
	segundos = atoi(p)*3600;
	p=strtok(NULL,":");
	segundos = segundos+atoi(p)*60;
	p=strtok(NULL,":");
	segundos=segundos+atoi(p);
	
	return segundos;
	
}
//A�adir a la lista de conectados
int AnadirAListaConectados (char nombre[25],int socket){
	//Retorna la posicion de la lista donde se ha a�adido (-1 si no se ha a�adido)
	int posicionLista = -1;
	if (nombre!=NULL && socket != NULL){
		//Creamos un nuevo usuario que a�adir a la lista
		Usuario nuevoUsuario;
		strcpy(nuevoUsuario.nombre,nombre);
		nuevoUsuario.socket = socket;
		
		//Lo a�adimos
		listaC.conectados[listaC.num]=nuevoUsuario;
		posicionLista = listaC.num;
		listaC.num = listaC.num+1;
		printf("Socket %d a�adido\n", socket);
	}
	return posicionLista;
}
//A�adir un nombre un socket de la lista de conectados
int AnadirNombreListaConectados(char nombre[25], int socket){
	//Retorna la posicion de la lista donde se han hecho los cambios (-1 si no se han hecho)
	int n = 0;
	int encontrado = 0;
	while(n<listaC.num && encontrado==0){
		if(listaC.conectados[n].socket==socket){
			strcpy(listaC.conectados[n].nombre,nombre);
			encontrado=1;
		}
		else{
			n=n+1;
		}
	}
	if(encontrado==1){
		return n;
	}
	else{
		return -1;
	}
}
//Retirar de la lista de conectados
void RetirarDeListaConectados (char nombre[25],int socket) {
	
	if (nombre != NULL && socket != NULL){
		int n = 0;
		int encontrado = 0;
		
		while(n<listaC.num && encontrado==0){
			if (listaC.conectados[n].socket == socket){
				encontrado = 1;
			}
			else
				n++;
		}
		if (encontrado==1){
			while(n<listaC.num-1){
				listaC.conectados[n]=listaC.conectados[n+1];
				n++;
			}
			listaC.num = listaC.num-1;
		}
	}
	else if (socket != NULL){
		int n = 0;
		int encontrado = 0;
		
		while(n<listaC.num && encontrado==0){
			if (listaC.conectados[n].socket==socket){
				encontrado = 1;
			}
			else
				n++;
		}
		if (encontrado==1){
			while(n<listaC.num){
				listaC.conectados[n]=listaC.conectados[n+1];
				n++;
			}
			listaC.num = listaC.num-1;
		}
	}
}
//Notificar actualizacion de la lista de conectados
void NotificarNuevaListaConectados(){
	
	char lista[512];
	char notificacion[512];
	
	//pthread_mutex_lock(&mutex);
	int res = DameListaConectados(lista);
	//pthread_mutex_unlock(&mutex);
	
	printf("Nueva Lista de Conectados:\n");
	if (res == 0){
		printf("Nuevos Datos: %s\n",lista);
		sprintf(notificacion,"6/%s",lista);
	}
	else{
		printf("Lista de conectados vacia\n");
		sprintf(notificacion,"6/%s",lista);
	}
	
	//Enviamos la actualizacion generada a todos los socket
	int j;
	for (j=0;j<listaC.num;j++){
		write(listaC.conectados[j].socket,notificacion,strlen(notificacion));
	}
	
}
//Retorna el socket del nombre buscado en la lista de conectados.
int DameSocketConectado(char nombre[25]){
	//Retorna -1 --> El usuario buscado no se ha encontrado conectado.
	//Retorna n�mero socket--> Se ha encontrado el resultado conectado.
	int i=0;
	int encontrado=0;
	while (i<listaC.num && !encontrado){
		if (strcmp(listaC.conectados[i].nombre,nombre)==0)
			encontrado=1;
		else
			i++;
	}
	if (encontrado==1)
	{
		return listaC.conectados[i].socket;
	}
	else 
		return -1;
}
//Invitar a los conectados seleccionados a una partida
int Invitar(char invitados[500], char nombre[25], char noDisponibles[500],int partida) {
	//invitados; nombre: quien invita
	//Retorna numero de invitados --> Todo OK (+ notifica a los invitados (8/persona_que_le_ha_invitado*id_partida))
	//       -1 --> Alguno de los usuarios invitados se ha desconectado (+nombres de los desconectados en noDisponibles)
	
	strcpy(noDisponibles,"\0");
	int error = 0;
	char *p = strtok(invitados,"*");
	int numInvitados = 0;
	
	while (p != NULL) {
		int encontrado = 0;
		int i = 0;
		while ((i<listaC.num)&&(encontrado == 0)) {
			if (strcmp(listaC.conectados[i].nombre,p) == 0) {
				char invitacion[512];
				sprintf(invitacion, "8/%s*%d", nombre,partida);
				//Invitacion: 8/quien invita*id_partida
				printf("Invitacion: %s\n",invitacion);
				write(listaC.conectados[i].socket, invitacion, strlen(invitacion));
				encontrado = 1;
				numInvitados = numInvitados +1;
			}
			else
				i = i + 1;
		}
		if (encontrado == 0){
			error = -1;
			sprintf(noDisponibles,"%s%s*",noDisponibles,p);
			noDisponibles[strlen(noDisponibles)-1] = '\0';
		}
		p = strtok(NULL, "*");		
	}
	if(error == 0){
		error = numInvitados;
		pthread_mutex_lock(&mutex);
		tablaP[partida].numInvitados = numInvitados;
		pthread_mutex_unlock(&mutex);
	}
	
	return error;
}
//Busca una partida disponible en la tabla de partidas y le asocia el host
int CrearPartida(char nombre[25]){
//int CrearPartida(char invitados[500], char nombre[25]){
	//Retorna -1 --> si no hay ning�n espacio libre en la tabla de partidas.
	//Retorna id de la partida-->  si se ha asignado.
	int i=0;
	int encontrado=0;
	while((i<len_tablaP)&&!encontrado)
	{	
		pthread_mutex_lock(&mutex);
		if(tablaP[i].estado==0)
		{
			tablaP[i].estado=1;
			strcpy(tablaP[i].host,nombre);
			int n;
			for(n=0;n<6;n++)
				tablaP[i].puntosHost[n] = 0;
			strcpy(tablaP[i].jug2,"0");
			for(n=0;n<6;n++)
				tablaP[i].puntosJug2[n] = 0;
			strcpy(tablaP[i].jug3,"0");
			for(n=0;n<6;n++)
				tablaP[i].puntosJug3[n] = 0;
			strcpy(tablaP[i].jug4,"0");
			for(n=0;n<6;n++)
				tablaP[i].puntosJug4[n] = 0;
			tablaP[i].horaInicio = 0;			
			encontrado=1;
		}
		else 
		   i++;
		pthread_mutex_unlock(&mutex);
		
	}
	if(encontrado==0)
		return -1;
	else{
		return i;
	}
}
//A�ade un jugador a la partida indicada
int AnadirJugador(char nombre[25],int partida){
	//Retorna numero de jugadores que faltan para aceptar
	//Cliente ya no permite que se inviten a mas de 3 personas
	printf("A�adir jugador: %s\n",nombre);
	pthread_mutex_lock(&mutex);
	if(strcmp(tablaP[partida].jug2,"0")==0){
		strcpy(tablaP[partida].jug2,nombre);
	}
	else if (strcmp(tablaP[partida].jug3,"0")==0)
		strcpy(tablaP[partida].jug3,nombre);
	else if(strcmp(tablaP[partida].jug4,"0")==0)
		strcpy(tablaP[partida].jug4,nombre);

	tablaP[partida].numInvitados = tablaP[partida].numInvitados-1;
	int res = tablaP[partida].numInvitados;
	pthread_mutex_unlock(&mutex);
	return res;	
}
//Con la confirmacion de todos los invitados notificamos a los jugadores
//del inicio de la partida y su rol.
void IniciarPartida (int partida, char jugadores_partida[500]){
	char ini[500];
	sprintf(ini, "9/%d*%s", partida, jugadores_partida);
	printf("Iniciar partida: %s\n",ini);
	int socket1 = DameSocketConectado(tablaP[partida].host);
	if(socket1!=-1){
		char ini2[500];
		sprintf(ini2,"%s*%s",ini,"host");
		printf("%s\n",ini2);
		write(socket1,ini2,strlen(ini2));
	}
	int socket2 = DameSocketConectado(tablaP[partida].jug2);
	if(socket2!=-1){
		char ini2[500];
		sprintf(ini2,"%s*%s",ini,"jug2");
		printf("%s\n",ini2);
		write(socket2,ini2,strlen(ini2));
	}
	
	if (strcmp(tablaP[partida].jug3,"0")!=0){
		int socket3=DameSocketConectado(tablaP[partida].jug3);
		if(socket3!=-1){
			char ini2[500];
			sprintf(ini2,"%s*%s",ini,"jug3");
			printf("%s\n",ini2);
			write(socket3,ini2,strlen(ini2));
		}
	}
	if (strcmp(tablaP[partida].jug4,"0")!=0){
		int socket4=DameSocketConectado(tablaP[partida].jug4);
		if(socket4!=-1){
			char ini2[500];
			sprintf(ini2,"%s*%s",ini,"jug4");
			printf("%s\n",ini2);
			write(socket4,ini2,strlen(ini2));
		}
	}
	
	//Determinamos la hora de inicio de la partida
	char a[10]; //unused
	char b[10]; //unused
	int horaInicio = DameTiempo(a,b); //en segundos
	tablaP[partida].horaInicio = horaInicio;
	
}
//Envia notificacion a todos menos al que notifica (si queremos que envie a todos en el parametro socket introducimos -1)
void EnviaNotificacion(char notificacion[500],int partida,int socket){
	int socket1=DameSocketConectado(tablaP[partida].host);
	if((socket1!=-1) && (socket1!=socket)){
		write(socket1,notificacion,strlen(notificacion));
		printf("Notificaci�n: %s enviada a socket:%d \n",notificacion,socket1);
	}
	int socket2=DameSocketConectado(tablaP[partida].jug2);
	if ((socket2!=-1) && (socket2!=socket)){
		write(socket2,notificacion,strlen(notificacion));
		printf("Notificaci�n: %s enviada a socket:%d \n",notificacion,socket2);
	}
	
	if (strcmp(tablaP[partida].jug3,"0")!=0){
		int socket3=DameSocketConectado(tablaP[partida].jug3);
		if((socket3!=-1) && (socket3!=socket))
		{
			write(socket3,notificacion,strlen(notificacion));
			printf("Notificaci�n: %s enviada a socket:%d \n",notificacion,socket3);
		}	
	}
	if (strcmp(tablaP[partida].jug4,"0")!=0){
		int socket4=DameSocketConectado(tablaP[partida].jug4);
		if((socket4!=-1) && (socket4!=socket)){
			write(socket4,notificacion,strlen(notificacion));
			printf("Notificaci�n: %s enviada a socket:%d \n",notificacion,socket4);
		}
	}	
}
//Notifica a los invitados que han aceptado que alguien ha rechazado i que por tanto no se inicia la partida
void PartidaRechazada(int partida,char rechazador[25],int socket){
	char rechazada[500];
	sprintf(rechazada,"16/%d*%s",partida,rechazador);
	EnviaNotificacion(rechazada,partida,socket);
}
//Notifica a los jugadores de la tabla que ha acabado la partida.
void FinPartida(int partida, char finalizador[25]){
	char fin[500];
	sprintf(fin,"10/%d*%s", partida,finalizador);
	EnviaNotificacion(fin,partida,-1); //-1 en socket para enviar a todos
}
//Eliminia una partida de la tabla de partidas.
void EliminarPartida(int partida){
	tablaP[partida].estado=0;
	tablaP[partida].numInvitados=-1;
	strcpy(tablaP[partida].host,"0");
	int n;
	for(n=0;n<6;n++)
		tablaP[partida].puntosHost[n] = 0;
	strcpy(tablaP[partida].jug2,"0");
	for(n=0;n<6;n++)
		tablaP[partida].puntosJug2[n] = 0;
	strcpy(tablaP[partida].jug3,"0");
	for(n=0;n<6;n++)
		tablaP[partida].puntosJug3[n] = 0;
	strcpy(tablaP[partida].jug4,"0");
	for(n=0;n<6;n++)
		tablaP[partida].puntosJug4[n] = 0;
	tablaP[partida].horaInicio = 0;
}
//Retorna los jugadores de una partida recibida como parametro en jugadores i el numero total de jugadores
int DameJugadoresPartida(int partida, char jugadores[500]){
	sprintf(jugadores,"%s*%s",tablaP[partida].host,tablaP[partida].jug2);
	int numJugadores = 2;
	if (strcmp(tablaP[partida].jug3,"0")!=0){
		sprintf(jugadores,"%s*%s",jugadores,tablaP[partida].jug3);
		numJugadores=numJugadores+1;
		if(strcmp(tablaP[partida].jug4,"0")!=0){
			sprintf(jugadores,"%s*%s",jugadores,tablaP[partida].jug4);
			numJugadores=numJugadores+1;
		}
	}
	return numJugadores;
}
//Retorna los id de las partidas en las que participa el jugador recibido como par�metro
void DamePartidasJugador(char nombre[25], char partidas[100]){
	int i=0;
	strcpy(partidas,"");
	while(i<len_tablaP)
	{
		if((strcmp(nombre,tablaP[i].host)==0)||(strcmp(nombre,tablaP[i].jug2)==0)||(strcmp(nombre,tablaP[i].jug3)==0)||(strcmp(nombre,tablaP[i].jug4)==0))
			sprintf(partidas,"%s%d/",partidas,i);
		i=i+1;
	}
	partidas[strlen(partidas)-1]='\0';
}
//Retorna el rol de la persona que le toca tirar en nuevoTurno
void DameSiguienteTurno(char anteriorTurno[10], int numJugadores, char nuevoTurno[10]){
	if (strcmp(anteriorTurno,"host")==0)
		strcpy(nuevoTurno,"jug2");
	else if (strcmp(anteriorTurno,"jug2")==0){
		if(numJugadores==2)
			strcpy(nuevoTurno,"host");
		else
			strcpy(nuevoTurno,"jug3");
	}
	else if (strcmp(anteriorTurno,"jug3")==0){
		if (numJugadores==3)
			strcpy(nuevoTurno,"host");
		else
			strcpy(nuevoTurno,"jug4");
	}
	else{
		strcpy(nuevoTurno,"host");
	}
	printf("Anterior turno: %s\n",anteriorTurno);
	printf("Siguiente turno: %s\n",nuevoTurno);
	printf("Jugadores: %d\n",numJugadores);
}
//Suma los puntos de cada jugadores
int TotalPuntos(int puntos[6]){
	int n;
	int suma = 0;
	for(n=0;n<6;n++){
		if(puntos[n]==1)
			suma = suma+1;
	}
	return suma;
}
//Notifica al resto de jugadores el resultado de la tirada del dado de un jugadores
void NotificaResultadoDado(int idPartida, int resDado, char tirador[25], int socket){
	//construimos el mensaje a enviar "11/idPartida/resDado/nombre_tirador"
	char notificacion[500];
	sprintf(notificacion,"11/%d*%d*%s",idPartida,resDado,tirador);
	EnviaNotificacion(notificacion,idPartida,socket); //no nos enviamos a nosotros mismos
}
//Suma puntos al jugador que ha acertado en una casilla de quesito
int SumaPuntos(int idPartida, char rol[10],char categoria[100]){
	//Retorna 1 si despues de sumar alguien llega a 6 quesitos (GANA), 0 si no.
	int ganador = 0;
	//Sumamos el punto a la categoria correspondiente
	if (strcmp(rol,"host")==0){
		if (strcmp(categoria,"Ciencia")==0){
			tablaP[idPartida].puntosHost[0] = 1;			
		}
		
		else if (strcmp(categoria,"Geografia")==0){
			tablaP[idPartida].puntosHost[1] = 1;
		}
		
		else if (strcmp(categoria,"Historia")==0){
			tablaP[idPartida].puntosHost[2] = 1;
		}

		else if (strcmp(categoria,"Entretenimiento")==0){
			tablaP[idPartida].puntosHost[3] = 1;
		}

		else if (strcmp(categoria,"Deportes")==0){
			tablaP[idPartida].puntosHost[4] = 1;
		}
		
		else{
			tablaP[idPartida].puntosHost[5] = 1;
		}
		
		int suma= TotalPuntos(tablaP[idPartida].puntosHost);
		if (suma >= 6)
			ganador = 1;
	}
	else if (strcmp(rol,"jug2")==0){
		if (strcmp(categoria,"Ciencia")==0)
			tablaP[idPartida].puntosJug2[0] = 1;
		
		else if (strcmp(categoria,"Geografia")==0)
			tablaP[idPartida].puntosJug2[1] = 1;
		
		else if (strcmp(categoria,"Historia")==0)
			tablaP[idPartida].puntosJug2[2] = 1;
		
		else if (strcmp(categoria,"Entretenimiento")==0)
			tablaP[idPartida].puntosJug2[3] = 1;
		
		else if (strcmp(categoria,"Deportes")==0)
			tablaP[idPartida].puntosJug2[4] = 1;
		
		else
			tablaP[idPartida].puntosJug2[5] = 1;
		
		//Comprovamos si ya estan los 6 quesitos
		int suma= TotalPuntos(tablaP[idPartida].puntosJug2);		
		if (suma >= 6)
			ganador = 1;
	}
	else if (strcmp(rol,"jug3")==0){
		if (strcmp(categoria,"Ciencia")==0)
			tablaP[idPartida].puntosJug3[0] = 1;
		
		else if (strcmp(categoria,"Geografia")==0)
			tablaP[idPartida].puntosJug3[1] = 1;
		
		else if (strcmp(categoria,"Historia")==0)
			tablaP[idPartida].puntosJug3[2] = 1;
		
		else if (strcmp(categoria,"Entretenimiento")==0)
			tablaP[idPartida].puntosJug3[3] = 1;
		
		else if (strcmp(categoria,"Deportes")==0)
			tablaP[idPartida].puntosJug3[4] = 1;
		
		else
			tablaP[idPartida].puntosJug3[5] = 1;
		
		//Comprovamos si ya estan los 6 quesitos
		int suma= TotalPuntos(tablaP[idPartida].puntosJug3);		
		if (suma >= 6)
			ganador = 1;
	}
	else{
		if (strcmp(categoria,"Ciencia")==0)
			tablaP[idPartida].puntosJug4[0] = 1;
		
		else if (strcmp(categoria,"Geografia")==0)
			tablaP[idPartida].puntosJug4[1] = 1;
		
		else if (strcmp(categoria,"Historia")==0)
			tablaP[idPartida].puntosJug4[2] = 1;
		
		else if (strcmp(categoria,"Entretenimiento")==0)
			tablaP[idPartida].puntosJug4[3] = 1;
		
		else if (strcmp(categoria,"Deportes")==0)
			tablaP[idPartida].puntosJug4[4] = 1;
		
		else
			tablaP[idPartida].puntosJug4[5] = 1;
		
		//Comprovamos si ya estan los 6 quesitos
		int suma= TotalPuntos(tablaP[idPartida].puntosJug4);		
		if (suma >= 6)
			ganador = 1;
	}
	
	return ganador;
}
//Retorna el id de la BBDD (Historico) que le corresponde a la partida
int DameIdHistorico(){
	//Retorna idHistorico-> Historico guardado correctamente; -1-> Error en de BBDD
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	
	//Buscamos la ultima fila
	int idHistorico;
	err=mysql_query (conn, "SELECT MAX(id) FROM partidas");
	if (err!=0){
		return -1;
	}
	else{
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		
		if (row==NULL){
			idHistorico = 0;
		}
		else{
			idHistorico = atoi(row[0])+1;
		}
		printf("Id historico: %d\n",idHistorico);
		return idHistorico;
	}
	
	
}
//Guarda los registros de los jugadores en una partida
int GuardarRegistros(int idPartida, int idHistorico){
	//Retorna 0-> todo OK ; -1 -> Error BBDD
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	//A�adimos los resultados de los jugadores a la tabla registros
	char consulta[500];
	
	//Host
	sprintf(consulta,"SELECT id FROM jugadores WHERE nombre='%s';",tablaP[idPartida].host);
	err=mysql_query (conn, consulta);
	if (err!=0){
		return -1;		
	}
	else{
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		int puntos= TotalPuntos(tablaP[idPartida].puntosHost);
		printf("%s\n",row[0]);
		sprintf(consulta,"INSERT INTO registro VALUES ('%d','%d','%d');",atoi(row[0]),idHistorico,puntos);
		printf("%s\n",consulta);
		err=mysql_query (conn, consulta);
		if (err!=0){
			return -1;
		}
	}
	
	//Jug2
	sprintf(consulta,"SELECT id FROM jugadores WHERE nombre='%s';",tablaP[idPartida].jug2);
	err=mysql_query (conn, consulta);
	if (err!=0){
		return -1;
	}
	else{
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		int puntos= TotalPuntos(tablaP[idPartida].puntosJug2);
		sprintf(consulta,"INSERT INTO registro VALUES ('%d',%d,%d);",atoi(row[0]),idHistorico,puntos);
		err=mysql_query (conn, consulta);
		if (err!=0){
			return -1;
		}
	}
	
	//Jug3 (si hay)
	if (strcmp(tablaP[idPartida].jug3,"0")!=0){
		sprintf(consulta,"SELECT id FROM jugadores WHERE nombre='%s';",tablaP[idPartida].jug3);
		err=mysql_query (conn, consulta);
		if (err!=0){
			return -1;
		}
		else{
			resultado = mysql_store_result(conn);
			row = mysql_fetch_row(resultado);
			int puntos= TotalPuntos(tablaP[idPartida].puntosJug3);
			sprintf(consulta,"INSERT INTO registro VALUES ('%d',%d,%d);",atoi(row[0]),idHistorico,puntos);
			err=mysql_query (conn, consulta);
			if (err!=0){
				return -1;
			}
		}
		
		//Jug4 (si hay)
		if (strcmp(tablaP[idPartida].jug4,"0")!=0){
			sprintf(consulta,"SELECT id FROM jugadores WHERE nombre='%s';",tablaP[idPartida].jug4);
			err=mysql_query (conn, consulta);
			if (err!=0){
				return -1;
			}
			else{
				resultado = mysql_store_result(conn);
				row = mysql_fetch_row(resultado);
				int puntos= TotalPuntos(tablaP[idPartida].puntosJug4);
				sprintf(consulta,"INSERT INTO registro VALUES ('%d',%d,%d);",atoi(row[0]),idHistorico,puntos);
				err=mysql_query (conn, consulta);
				if (err!=0){
					return -1;
				}
			}
		}
	}
	
}
//Retorna la duracion de una partida en minutos
int DameDuracion(int horaInicial){
	//Retorna la duracion de la partida en minutos
	char fecha[12]; //unused
	char hora[10]; //unused
	int horaFinal = DameTiempo(fecha,hora);
	int duracion;
	
	if (horaInicial>horaFinal){
		//Si la partida se juega entre antes de las 00 i despues.
		duracion = 24*3600-horaInicial+horaFinal;
	}
	else{
		duracion = horaFinal-horaInicial;
	}
	
	//Cambio de segundos a minutos
	duracion = round(duracion/60);
	return duracion;
}
//Guardar datos partida
int GuardarPartida(int idTabla, int idHistorico, int duracion ,char ganador[25]){
	//Retorna 0-> Todo ok : -1 -> Error BBDD
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	
	//Llenamos la fila correspondiente al idHistorico
	//Obtenemos la fecha de la partida
	char fecha[12];
	char hora[10];
	char winner[25];
	strcpy(winner,"");
	int winnerfound=0;
	int segundos = DameTiempo(fecha,hora);	
	//Comprovamos que el ganador no sea NULL (la partida se ha terminado antes de tiempo)
	if (strcmp(ganador,"0")==0){
		//Buscamos al jugador con mas puntos
		//En caso de empate se anotaran todos los ganadores separados por barras
		//Para considerarse ganador hay que conseguir almenos un quesito
		//Si nadie consigue un quesito el ganador sera '-'
		int max = 1;
		int puntos=TotalPuntos(tablaP[idTabla].puntosHost);
		printf("Puntos host: %d\n",puntos);
		if(puntos>max){
			sprintf(winner,"%s/",tablaP[idTabla].host);
			max = puntos;
			winnerfound=1;
		}
		else if(puntos == max){
			sprintf(winner,"%s%s/",winner,tablaP[idTabla].host);
			winnerfound=1;
		}
		puntos=TotalPuntos(tablaP[idTabla].puntosJug2);
		printf("Puntos jug2: %d\n",puntos);
		if(puntos>max){
			sprintf(winner,"%s/",tablaP[idTabla].jug2);
			max = puntos;
			winnerfound=1;
		}
		else if(puntos== max){
			sprintf(winner,"%s%s/",winner,tablaP[idTabla].jug2);
			winnerfound=1;
		}
		if(strcmp(tablaP[idTabla].jug3,"0")!=0){
			puntos=TotalPuntos(tablaP[idTabla].puntosJug3);
			printf("Puntos jug3: %d\n",puntos);
			if(puntos>max){
				sprintf(winner,"%s/",tablaP[idTabla].jug3);
				max = puntos;
				winnerfound=1;
			}
			else if(puntos == max){
				sprintf(winner,"%s%s/",winner,tablaP[idTabla].jug3);
				winnerfound=1;
			}
			
			if(strcmp(tablaP[idTabla].jug4,"0")!=0){
				puntos=TotalPuntos(tablaP[idTabla].puntosJug4);
				printf("Puntos jug4: %d\n",puntos);
				if(puntos>max){
					sprintf(winner,"%s/",tablaP[idTabla].jug4);
					max = puntos;
					winnerfound=1;
				}
				else if(puntos== max){
					sprintf(winner,"%s%s/",winner,tablaP[idTabla].jug4);
					winnerfound=1;
				}
			}
		}
		if(winnerfound==0){
			strcpy(winner,"-");
		}
		winner[strlen(winner)-1]='\0';
	}
	else{
		strcpy(winner,ganador);
	}
	printf("%s\n",winner);
	//Insertamos los datos
	char consulta[500];
	sprintf(consulta,"INSERT INTO partidas VALUES (%d,'%s','%s',%d);",idHistorico,fecha,winner,duracion);
	printf("%s\n",consulta);
	err=mysql_query (conn, consulta);
	if (err!=0){
		return -1;
	}
	else{
		return 0;
	}
	
}
//Gurdar historico de la partida
int GuardarHistorico(int idPartida, char ganador[25]){
	//Retorna idHistorico-> Historico guardado correctamente; -1-> Error en de BBDD
	
	//Guardamos una fila en la tabla partidas para nuestra partida
	int idHistorico = DameIdHistorico();
	if (idHistorico==-1)
		return -1;
		
	//Guardamos los datos de la partida en la BBDD
	int duracion = DameDuracion(tablaP[idPartida].horaInicio);
	int res = GuardarPartida(idPartida,idHistorico,duracion,ganador);
	if (res==-1)
		return -1;	
	
	//Guardamos los registros de los jugadores de la Partidas
	res = GuardarRegistros(idPartida, idHistorico);
	if (res==0){
		return idHistorico;
	}
	else{
		return -1;
	}

}
//Eliminar a jugador de la BBDD
int EliminarJugadorBBDD(char nombre[25]){
	//Retorna 0-> Eliminado Correctamente , -1 -> Error BBDD
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	
	//Buscamos las partidas donde este jugador ha jugado 
	char consulta[1000];
	char partidas[500];
	sprintf(consulta,"SELECT partidas.id FROM (partidas, registro, jugadores) WHERE jugadores.nombre='%s' AND jugadores.id=registro.idJ AND registro.idP=partidas.id",nombre);
	err=mysql_query (conn, consulta);
	if (err!=0){
		printf("ERROR SELECT");
		return -1;
	}
	else{
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		while(row!=NULL)
		{	
			sprintf(consulta,"DELETE FROM partidas WHERE partidas.id=%d",atoi(row[0]));
			err=mysql_query(conn, consulta);
			if (err!=0){
				printf("ERROR DELETE");
				return -1;
			}
			else{
				row=mysql_fetch_row(resultado);
			}	
		
		}
	}
	
	//Eliminamos al jugador (i los registros de registros)
	sprintf(consulta,"DELETE FROM jugadores WHERE jugadores.nombre='%s';",nombre);
	err=mysql_query(conn, consulta);
	if (err!=0){
		return -1;
	}
	else{
		return 0;
	}
}
//Para obtener las partidas que se han jugado en una fecha determinada
int DamePartidasFecha(char fecha[12], char respuesta[500]){
	//Retorna en respuesta idPartida1,duracion1*idPartida2,duracion2 (0 si no hay partidas)
	//Resultado de la funci�n: -1-> Error BBDD, 0->No hay partidas esa fecha; 1-> Partidas entregadas en respuesta
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	strcpy(respuesta,"");
	
	char consulta[1000];
	sprintf(consulta,"SELECT id,duracion FROM partidas WHERE fecha='%s';",fecha);
	err=mysql_query(conn, consulta);
	if (err!=0){
		return -1;
	}
	else{
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		if(row==NULL){
			strcpy(respuesta,"0");
			return 0;
		}
		else{
			while(row!=NULL){
				sprintf(respuesta,"%s%s,%s*",respuesta,row[0],row[1]);
				row = mysql_fetch_row(resultado);
			}
			respuesta[strlen(respuesta)-1]='\0';
			return 1;
		}
		
	}
}

//Atencion a los diferentes clientes (threads)
int *AtenderCliente(void *socket){

	int ret;
	int sock_conn;
	int *s;
	s = (int *) socket;
	sock_conn = *s;
	
	//Datos de este cliente
	char nombre[25];
	char password[20];
	
	//Variable para saber si se tiene que desconectar
	int terminar = 0;
	while (terminar==0)
	{
		
		char buff[512];
		char buff2[512];
		
		// Informacion recibida almacenada en buff
		ret=read(sock_conn,buff, sizeof(buff));
		printf ("[%d] Recibido\n",sock_conn);
		
		// Tenemos que a�adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		buff[ret]='\0';
		
		//Escribimos en consola lo que nos ha llegado (buff)
		printf("[%d] Mensaje Recibido: %s\n",sock_conn,buff);
		
		//Obtenemos el codigo que nos indica el tipo de petici�n.
		char *p = strtok(buff,"/");
		int codigo = atoi(p);
		
		//Codigo 0 --> Desconexi�n
		if (codigo == 0){
			//Mensaje en buff: 0/
			//Return en buff2: -
			terminar = 1;
			
			pthread_mutex_lock(&mutex);
			RetirarDeListaConectados(nombre,sock_conn);
			
			
			if((strcmp(nombre,"0")!=0) && (strcmp(nombre,"\0")!=0)){
				printf("[%d]\n",sock_conn);
				NotificarNuevaListaConectados();
				printf("-------\n");
				
				char partidas[100];
				DamePartidasJugador(nombre,partidas);
				printf("Nombre: %s ; Partidas: %s\n",nombre,partidas);
				char *p=strtok(partidas,"/");
				while(p!=NULL)
				{	
					int partida=atoi(p);
					FinPartida(partida,nombre);
					//GuardarHistorico(partida,NULL);
					printf("[%d] Fin Partida %d\n",sock_conn, partida);
					EliminarPartida(partida);
					p=strtok(NULL,"/");
				}
			}
			
			
			pthread_mutex_unlock(&mutex);
			
			
		}
		else {
			//Codigo 1 --> Comprovaci�n para el Login
			if (codigo == 1){
				//Mesnaje en buff: 1/username/contrasenya
				//Return en buff2: 0--> Todo OK ; 1 --> Usuario no existe ; 2 --> Contrasenya no coincide ; -1 --> Error de consulta
				
				p = strtok(NULL,"/");
				strcpy(nombre,p);
				p = strtok(NULL,"/");
				strcpy(password,p);
				
				int res = InicioSesion(nombre,password);
				
				sprintf(buff2,"1/%d", res);
				
				//A�adimos a la lista de conectados si la comprovacion ha sido correcta
				if (res == 0){
					pthread_mutex_lock(&mutex);  //Autoexclusion
					printf("[%d]\n",sock_conn);
					AnadirNombreListaConectados(nombre,sock_conn);
					NotificarNuevaListaConectados();
					printf("--------------\n");
					pthread_mutex_unlock(&mutex);
					
				}
				else{
					strcpy(nombre,"0");
				}
				
			}
			
			//Codigo 2 --> Insert de nuevos jugadores
			else if (codigo==2){ 
				//Mensaje en buff: 2/username/contrasenya/mail
				//Return en buff2: 0--> Todo OK ; 1 --> Usuario ya existente ; -1 --> Error de consulta
				
				char nom[25];
				char password[20];
				char mail[100];
				
				p = strtok(NULL, "/");
				strcpy(nom, p);
				p = strtok (NULL, "/");
				strcpy (password, p);
				p = strtok(NULL,"/");
				strcpy(mail, p);
				
				int res = Registro(nom,password,mail);
				sprintf(buff2,"2/%d",res);
				
			}
			
			
			//Codigo 3 --> Dame mis contrincantes 
			else if (codigo == 3){
				//Mensaje en buff: 3/usuario
				//Return en buff2: 3/0 -> No hay contrincantes ; 3/contrincante1*contrincante2*....
				
				p = strtok(NULL,"/");
				char usuario[25];
				strcpy(usuario,p);
				char contrincantes[500];
				
				int res = DameContrincantes(usuario,contrincantes);
				if (res ==1)
					sprintf(buff2,"3/%s",contrincantes);
				else
					strcpy(buff2,"3/0");
				
			}
			
			//Codigo 4 --> Dame Informaci�n partidas con contrincantes como parametro
			else if (codigo == 4){
				//Mensaje en buff: 4/nombre1(/nombre2/nombre3/...)
				//Return en buff2: 4/0 -> Ningun resultado; 4/nombre1,idPartida1,ganadorPartida1*nombre2,idPartida2,ganadorPartida2*...
				char contrincantes[500];
				strcpy(contrincantes,"");
				p = strtok(NULL,"/");
				while(p!=NULL){
					sprintf(contrincantes,"%s%s/",contrincantes,p);
					p = strtok(NULL,"/");
				}
				char respuesta[1000];
				strcpy(respuesta,"");
				int res = DamePartidasContrincantes(nombre,contrincantes,respuesta);
				if (res==0)
					sprintf(buff2,"4/%s",respuesta);
				else
					strcpy(buff2,"4/0");
				
			}
			
			//Codigo 5 --> Obtener jugador con m�s puntos
			else if (codigo==5){
				//Mensaje en buff: 5/
				//Return en buff 2: nombre jugador con mas puntos --> Todo OK ; -1 --> Error de consulta ; -2 --> No hay jugadores en BBDD
				
				int puntos = DameMVP(nombre);
				if (puntos >= 0)
					sprintf(buff2,"5/%s*%d",nombre,puntos);
				else
					sprintf(buff2,"5/%d",puntos);
				
			}
			
			//Codigo 6 --> Invitar a otros usuarios conectados a una partida
			else if (codigo ==  6) {
				//Mensaje en buff: 6/invitado1*invitado2*...
				//Return en buff2: 7/0 (Todo OK) ; 7/invitado_no_disponible1/... (si hay invitados que se han desconectado)
				
				p = strtok(NULL, "/");
				char invitados[500];
				printf("[%d] Invitados: %s\n", sock_conn, invitados);
				char noDisponibles[500];
				strcpy(invitados, p);
			
				printf("[%d]\n",sock_conn);
				int partida=CrearPartida(nombre);
				printf("---------\n");
				if (partida==-1)
					sprintf(buff2,"7/-1");
				else{
					printf("[%d]\n",sock_conn);
					int res = Invitar(invitados, nombre, noDisponibles,partida);
					printf("----------\n");
					
					if (res == -1){
						sprintf(buff2,"7/%s",noDisponibles);
					}
					else{
						strcpy(buff2,"7/0");						 
					}
				}
				
			}
			//Codigo 7 --> Respuesta a una invitacion de partida
			else if (codigo ==  7) {
				//Mensaje en buff: 7/respuesta(SI/NO)/id_partida
				//Mensaje en buff2: -
				
				p = strtok(NULL,"/");
				char respuesta[3];
				strcpy(respuesta,p);
				p = strtok(NULL,"/");
				int id_partida;
				id_partida = atoi(p);
				if (strcmp(respuesta,"NO")==0){
					printf("[%d]\n",sock_conn);
					PartidaRechazada(id_partida,nombre,sock_conn);
					pthread_mutex_lock(&mutex);
					EliminarPartida(id_partida);
					pthread_mutex_unlock(&mutex);
					printf("-------------\n");
				}
				else{
					int res = AnadirJugador(nombre,id_partida);
					if (res == 0){
						printf("[%d] A�adido a la partida %s\n", sock_conn, nombre);
						char jugadores_partida[500];
						DameJugadoresPartida(id_partida,jugadores_partida);
						printf("[%d]\n",sock_conn);
						IniciarPartida(id_partida,jugadores_partida);
						printf("-----------\n");
					}
				}
				
			}
			//Codigo 8 --> Resultado de lanzar el dado
			else if (codigo == 8){
				//Mensaje en buff: 8/idPartida/resDado/rol/nombre
				//Mensaje en buff2: -
				
				p = strtok(NULL,"/");
				int partida = atoi(p);
				p = strtok(NULL,"/");
				int resDado = atoi(p);
				char rol[10];
				p = strtok(NULL,"/");
				strcpy(rol,p);
				char tirador[25];
				p = strtok(NULL,"/");
				strcpy(tirador,p);
				
				printf("[%d]\n",sock_conn);
				NotificaResultadoDado(partida,resDado,tirador,sock_conn);
				printf("----------\n");
			}
			//Codigo 9 --> Finalizar y eliminar una partida.
			else if(codigo==9){
				//Mensaje en buff: 9/idPartida
				//Mensaje en buff2: -
				
				p=strtok(NULL,"/");
				int partida=atoi(p);
				FinPartida(partida,nombre);
				pthread_mutex_lock(&mutex);
				GuardarHistorico(partida,"0");
				EliminarPartida(partida);
				pthread_mutex_unlock(&mutex);
			}
			//Codigo 10 --> Nuevo movimiento del Jugador
			else if(codigo==10){
				//Mensaje en buff: 10/idPartida/idNuevaCasilla/rol
				//Mensaje en buff2: -

				p=strtok(NULL,"/");
				int partida = atoi(p);
				p=strtok(NULL,"/");
				int nuevaCasilla = atoi(p);
				p=strtok(NULL,"/");
				char miRol[10];
				strcpy(miRol,p);

				//Notificamos al resto de jugadores el movimiento
				char notificacion[500];
				sprintf(notificacion,"12/%d*%s*%s*%d",partida,nombre,miRol,nuevaCasilla);
				EnviaNotificacion(notificacion,partida,sock_conn);
			}
			//Codigo 11 --> Resultado Pregunta
			else if(codigo==11){
				//Mensaje en buff: 11/idPartida/rol/resultadoPregunta(/quesito ganado) (1-> OK, 2-> OK + quesito, 0-> Mal)
				//Mensaje en buff2: -

				p=strtok(NULL,"/");
				int partida = atoi(p);
				p=strtok(NULL,"/");
				char miRol[10];
				strcpy(miRol,p);
				p=strtok(NULL,"/");
				int resultado = atoi(p);
				char quesito[100];
				char notificacion[500];
				int ganador=0;
				sprintf(notificacion,"13/%d*%s*%d",partida,nombre,resultado);
				if((resultado == 0) || (resultado == 2)){
					char jugadores[500]; //unused
					int numJugadores = DameJugadoresPartida(partida,jugadores);
					char siguienteTurno[10];
					DameSiguienteTurno(miRol,numJugadores,siguienteTurno);
					sprintf(notificacion,"%s*%s",notificacion,siguienteTurno);
					
					if(resultado == 2){
						p = strtok(NULL,"/");
						strcpy(quesito,p);
						sprintf(notificacion,"%s*%s",notificacion,quesito);
						pthread_mutex_lock(&mutex);
						ganador = SumaPuntos(partida,miRol,quesito);						
						pthread_mutex_unlock(&mutex);
											
					}
				}
				if(ganador!=1)
				{
					EnviaNotificacion(notificacion,partida,-1); //Enviamos a todos (tambien al usuario del thread)					
				}
				else{
					//Sumamos quesito (1 punto) en el caso de recibir un 2
					//En el caso que alguien llegeue a 6 puntos (6 quesitos) se acaba la partida porque
					//este jugador ha ganado.
					//Se acaba la partida con un ganador
					sprintf(notificacion,"14/%d*%s",partida,nombre);
					EnviaNotificacion(notificacion,partida,-1);//Enviamos tmb al ganador para que se sepa quien ha ganado
					FinPartida(partida,nombre);
					//Guardamos el historico
					pthread_mutex_lock(&mutex);
					int guardar = GuardarHistorico(partida,nombre);
					pthread_mutex_unlock(&mutex);
					//Eliminamos la partida
					pthread_mutex_lock(&mutex);
					EliminarPartida(partida);
					pthread_mutex_unlock(&mutex);
				}
				

			}
		
			//Codigo 12 -> Nuevo mensaje para el chat
			else if (codigo==12){
				//Mensaje en buff: 12/idPartida/mensaje
				//Mensaje en buff2: -
				
				p = strtok(NULL,"/");
				int partida = atoi(p);
				p = strtok(NULL,"/");
				char mensaje[500];
				strcpy(mensaje,p);
				
				//Notificamos a todos menos a uno mismo (envia el cliente de este thread -> nombre)
				char notificacion[500];
				sprintf(notificacion,"15/%d*%s*%s",partida,nombre,mensaje);
				EnviaNotificacion(notificacion,partida,sock_conn);
				
			}
			//Codigo 13 -> Darse de baja de la BBDD
			else if (codigo==13){
				//Mensaje en buff: 13/usuario/contrase�a
				//Mensaje en buff2: 17/0--> Todo OK ; 17/1 --> Usuario no existe ; 17/2 --> Contrasenya no coincide ; 17/-1 --> Error de consulta ; 17/3 --> Usuario ya conectado
				
				p = strtok(NULL,"/");
				char usuario[25];
				strcpy(usuario,p);
				p = strtok(NULL,"/");
				char contra[20];
				strcpy(contra,p);
				
				//Comprovar que es possible eliminar lo requerido
				int res = InicioSesion(usuario,contra);
				if (res != 0){
					sprintf(buff2,"17/%d",res);
				}
				else{
					//Procedemos a eliminar de la BBDD
					res = EliminarJugadorBBDD(usuario);
					sprintf(buff2,"17/%d",res);
				}
			}
			//Codigo 14-> Solicitud de las partidas en una determinada fecha
			else if(codigo==14){
				//Mensaje en buff: 14/fecha
				//Mensaje en buff2: 18/idPartida1,duracion1*idPartida2,duracion2*....
				
				p = strtok(NULL,"/");
				char fecha[15];
				strcpy(fecha,"");
				while (p!=NULL){
					sprintf(fecha,"%s%s/",fecha,p);
					p=strtok(NULL,"/");
				}
				fecha[strlen(fecha)-1]='\0';
				printf("Fecha: %s\n",fecha);
				char respuesta[500];
				int res = DamePartidasFecha(fecha,respuesta);
				if (res==1){
					sprintf(buff2,"18/%s",respuesta);
				}
				else{
					strcpy(buff2,"18/0");
				}
			}

			// Y lo enviamos
			if (codigo!=0 && codigo!=7 && codigo!=8 && codigo!=9 && codigo!=10 && codigo!=11 && codigo!=12){
				write (sock_conn,buff2, strlen(buff2));
				printf("[%d] Codigo: %d , Resultado: %s\n",sock_conn,codigo,buff2);//Vemos el resultado de la accion.
			}
		}
		
	}
	
	// Se acabo el servicio para este cliente
	close(sock_conn);
	pthread_exit(0);
}


//Programa principal del servidor

int main(int argc, char *argv[]) {
	
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	// INICIALITZACIONES
	//Ponemos a 0 el numeros de conectados en la lista de conectados
	listaC.num=0;
	
	// Abrimos el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0){
		printf("Error creant socket\n");
	}
	// Hacemos el bind al puerto
	memset(&serv_adr, 0, sizeof(serv_adr));// inicializa a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la maquina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// escucharemos en el port 9080
	serv_adr.sin_port = htons(9080);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0){
		printf ("Error al bind\n");
	}
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(sock_listen, 2) < 0){
		printf("Error en el Listen\n");
	}
	
	//Creamos la connexi�n a la BBDD
	conn = mysql_init(NULL);
	if (conn == NULL){
		printf("Error al crear la connexi�n: %u %s\n",mysql_errno(conn),mysql_error(conn));
		exit(1);
	}
	
	conn = mysql_real_connect(conn,"localhost","root","mysql","T02_BBDD",0,NULL,0);
	if (conn==NULL){
		printf("Error al crear la connexi�n: %u %s\n",mysql_errno(conn),mysql_error(conn));
		exit(1);
	}
	
	
	pthread_t thread;
	pthread_mutex_init(&mutex,NULL);
	
	// Bucle infinito
	int i;
	for(i=0;;i++){
		printf ("[Main] Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("[Main] He recibido conexi?n\n");
		//sock_conn --> socket para este cliente
		//Guardamos el socket
		pthread_mutex_lock(&mutex);
		int posicion = AnadirAListaConectados("0",sock_conn);
		pthread_mutex_unlock(&mutex);
		
		//Creamos el thread
		pthread_create (&thread, NULL, AtenderCliente, &listaC.conectados[posicion].socket);
	}
	pthread_mutex_destroy(&mutex);
	exit(0);
}

