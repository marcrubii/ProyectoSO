using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Threading;



namespace Trivial
{
    public partial class FormPrincipal : Form
    {
        Socket server;
        Thread atender;
        FormInv invitacion;

        int conn = 0;
        int x = 0;

        string username;

        delegate void DelegadoParaEscribir(string[] conectados);

        List<string> invitados;

        List<FormJuego> tableros;

        FormConsultas formConsultas;

        public FormPrincipal()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Bitmap inicio = new Bitmap(Application.StartupPath + @"\trivia.jpg");
            this.BackgroundImage = inicio;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            Bitmap host = new Bitmap(Application.StartupPath + @"\JugAzul.png");
            Bitmap jug1 = new Bitmap(Application.StartupPath + @"\JugLila.png");
            Bitmap jug2 = new Bitmap(Application.StartupPath + @"\JugVerde.png");
            Bitmap jug3 = new Bitmap(Application.StartupPath + @"\JugRojo.png");

            List<Bitmap> fichas = new List<Bitmap>();
            Bitmap[] bitlist = new Bitmap[] { host, jug1, jug2, jug3 };
            fichas.AddRange(bitlist);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        //Thread que modifica los objetos
        public void ListaConectadosGridView(string[] conectados)
    {
            //Varias columnas del DataGridView las desactivamos, hasta que no se haga el log in/ registre no las activamos
            labelConectados.Visible = true;
            listaconGridView.Visible = true;
            listaconGridView.ColumnCount = 1;
            listaconGridView.RowCount = conectados.Length;
            listaconGridView.ColumnHeadersVisible = false;
            listaconGridView.RowHeadersVisible = false;
            listaconGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            listaconGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            listaconGridView.BackgroundColor = Color.White;
            
            listaconGridView.SelectAll();

            int totalRowHeight = listaconGridView.ColumnHeadersHeight;
            for (int i = 0; i < conectados.Length; i++)
            {
                listaconGridView.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                listaconGridView.Rows[i].Cells[0].Value = conectados[i];
                totalRowHeight += listaconGridView.Rows[i].Height;
            }
            listaconGridView.Height = totalRowHeight;
            listaconGridView.Show();
        }

        //Obtener la posicion en la lista de tableros
        private int DamePosicionLista(List<FormJuego> tableros, int idPartida)
        {
            //Retorna el tablero asignado a la partida, también controlamos erroes por si da errores
            bool encontrado = false;
            int i = 0;
            while (i < tableros.Count && encontrado == false)
            {
                if (tableros[i].DameIdPartida() == idPartida)
                    encontrado = true;
                else
                    i = i + 1;
            }
            if (encontrado == true)
                return i;
            else
                return -1;
        }

        //Función que ejecuta el Thread de las respuestas del servidor
        private void AtenderServidor()
        {
            bool fin = false;
            while (fin==false)
            {
                try
                {
                    //Respuesta servidor 
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                    int codigo = Convert.ToInt32(trozos[0]);
                    string mensaje = trozos[1].Split('\0')[0];
                    switch (codigo)
                    {
                        case 1: //Usuario Inicio de sesión
                            if (mensaje == "0")
                            {
                                tableros = new List<FormJuego>();

                                //Establecemos fondo y el dataGridView, una vez ha sido iniciado, ponemos la imagen trivia2.png
                                Bitmap fondo = new Bitmap(Application.StartupPath + @"\trivia2.png");
                                this.BackgroundImage = fondo;
                                this.BackgroundImageLayout = ImageLayout.Stretch;

                                inv_lbl.Visible = true;
                                inv_lbl.Text = username + ", pulsa sobre el nombre del jugador para invitarlo";
                                consultasButton.Visible = true;
                                accederBox.Visible = false;
                                registroBox.Visible = false;
                                listaconGridView.Visible = true;
                                labelConectados.Visible = true;


                                conexion.Text = "Desconectar";
                                conexion.Visible = true;
                                conn = 1;

                                regLabel.Visible = false;
                                regVisible.Visible = false;
                                inicio.Visible = false;
                                eliminarLbl.Visible = false;
                                eliminarCuenta.Visible = false;
                            }
                            //Controlar errores
                            else if (mensaje == "1")
                            {
                                MessageBox.Show("Usuario incorecto");
                                NameBox.Clear();
                                PasswordBox.Clear();
                                fin = true;
                            }
                            else if (mensaje == "2")
                            {
                                MessageBox.Show("Contraseña incorrecta");
                                PasswordBox.Clear();
                                fin = true;
                            }
                            else if (mensaje == "3")
                            {
                                MessageBox.Show("Usuario ya conectado");
                                NameBox.Clear();
                                PasswordBox.Clear();
                                fin = true;
                            }
                            else
                            {
                                MessageBox.Show("Error en la consulta, vuelva a intentarlo");
                                fin = true;
                            }
                            break;

                        case 2: //Registrar el usuario
                            if (mensaje == "0")
                            {
                                MessageBox.Show("Se ha registrado correctamente");
                                registroBox.Visible = false;
                                inicio.Visible = false;
                                regVisible.Visible = true;
                                accederBox.Visible = true;
                                regLabel.Visible = true;
                                eliminarLbl.Visible = true;
                                eliminarCuenta.Visible = true;
                            }
                            //Controlar errores
                            else if (mensaje == "1")
                                MessageBox.Show("Este nombre de usuario ya existe.");
                            else
                                MessageBox.Show("Error en la consulta, vuelva a intentarlo");
                            userBox.Clear();
                            password2Box.Clear();
                            mailBox.Clear();
                            fin = true;
                            break;

                        case 3: // Consultar contrincantes recientes
                            if (mensaje == "-1")
                                MessageBox.Show("Error en la consulta, vuelva a intentarlo");
                            else
                            {
                                this.formConsultas = new FormConsultas();
                                this.formConsultas.SetPregunta(ConQuienLbl.Text);
                                this.formConsultas.SetDataGrid(codigo, mensaje);
                                this.formConsultas.ShowDialog();
                            }
                            break;

                        case 4: //(Consultar el resultado de la partida con un número de jugadores
                            if (mensaje == "-1")
                                MessageBox.Show("Error en la consulta, vuelva a intentarlo");
                            else
                            {
                                this.formConsultas = new FormConsultas();
                                this.formConsultas.SetPregunta(companyia.Text);
                                this.formConsultas.SetDataGrid(codigo, mensaje);
                                this.formConsultas.ShowDialog();
                            }
                            break;

                        case 5: // Consultar el MVP (jugador con más puntos en total)
                            if (mensaje == "-1")
                                MessageBox.Show("Error en la consulta, vuelva a intentarlo");
                            else {
                                this.formConsultas = new FormConsultas();
                                this.formConsultas.SetPregunta(jugMaxBtn.Text);
                                this.formConsultas.SetDataGrid(codigo, mensaje);
                                this.formConsultas.ShowDialog();
                            }
                            break;

                        case 6: //Notificación (actualización de la lista de conectados)
                            if (mensaje == "-1")
                                MessageBox.Show("No hay usuarios en línea");
                            else
                            {
                                //Separamos los conectados
                                string[] conectados = mensaje.Split('*');

                                //Modificamos el DataGridView
                                DelegadoParaEscribir delegado = new DelegadoParaEscribir(ListaConectadosGridView);
                                listaconGridView.Invoke(delegado, new object[] { conectados });
                            }
                            break;

                        case 7: //Petición de invitación a un usuario
                            if (mensaje == "0")
                                MessageBox.Show("Invitaciones enviadas con éxito");
                            else
                            {
                                string[] idle = mensaje.Split('*');
                                string show = "";
                                for (int n = 0; n < idle.Length; n++)
                                    show = show + idle[n] + ",";
                                show = show.Remove(show.Length - 1);
                                MessageBox.Show("Invitaciones enviadas con éxito\n excepto las de: " + show + "\nInténtalo de nuevo");
                            }
                            break;

                        case 8: //Notificar de invitación de partida
                            string[] split = mensaje.Split('*');
                            invitacion = new FormInv();
                            invitacion.SetHost(split[0]);
                            invitacion.ShowDialog();
                            string respuesta = "7/" + invitacion.GetRespuesta() + "/" + split[1] + "\0";
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(respuesta);
                            server.Send(msg);

                            break;

                        case 9: //Notificación para iniciar la partida
                            //Thread de la partida
                            ThreadStart ts = delegate { NuevaPartida(mensaje); };
                            Thread T = new Thread(ts);
                            T.Start();
                            break;

                        case 10:// Notificamos el final de la partida
                            int numTablero = DamePosicionLista(tableros, Convert.ToInt32(mensaje.Split('*')[0]));
                            if (numTablero >= 0)
                            {
                                tableros[numTablero].FinalizarPartida();
                                tableros[numTablero].Close();
                            }
                            if (mensaje.Split('*')[1] != username)
                                MessageBox.Show(mensaje.Split('*')[1] + " ha finalizado \nla partida " + mensaje.Split('*')[0]);
                            break;

                        case 11: //Notificar el resultado del dado del jugador que ha tirado
                            int idPartida = Convert.ToInt32(mensaje.Split('*')[0]);
                            numTablero = DamePosicionLista(tableros, idPartida);
                            tableros[numTablero].NuevoMovimiento(mensaje);
                            break;

                        case 12: //Notificar el movimiento de la casilla del jugador

                            idPartida = Convert.ToInt32(mensaje.Split('*')[0]);
                            numTablero = DamePosicionLista(tableros, idPartida);
                            tableros[numTablero].setCasillaJugador(mensaje);
                            break;

                        case 13: //Notificacion del resultado de un jugador (0 - incorrecta, 1 - correcta, 2 - correcta y queso)

                            idPartida = Convert.ToInt32(mensaje.Split('*')[0]);
                            numTablero = DamePosicionLista(tableros, idPartida);
                            tableros[numTablero].ActualizarResultadoPregunta(mensaje);
                            break;

                        case 14: //Notificar que jugador ha conseguido todos los 6 quesos

                            idPartida = Convert.ToInt32(mensaje.Split('*')[0]);
                            numTablero = DamePosicionLista(tableros, idPartida);
                            tableros[numTablero].Ganador(mensaje);
                            break;

                        case 15: //Notificación de mensaje en el chat del juego

                            idPartida = Convert.ToInt32(mensaje.Split('*')[0]);
                            numTablero = DamePosicionLista(tableros, idPartida);
                            tableros[numTablero].NuevoMensajeChat(mensaje);
                            break;

                        case 16: // Notificación de rechazo de la invitación a la partida

                            idPartida = Convert.ToInt32(mensaje.Split('*')[0]);
                            MessageBox.Show(mensaje.Split('*')[1] + " ha rechazado la partida\nNo se iniciará");
                            break;

                        case 17: //Eliminar cuenta de la BBDD

                            if (mensaje == "0")
                            {
                                MessageBox.Show("Usuario eliminado con éxito");
                                eliminarBox.Visible = false;
                                accederBox.Visible = true;
                                regVisible.Visible = true;
                                regLabel.Visible = true;
                                volverLbl.Visible = false;
                                eliminarLbl.Visible = true;
                                eliminarCuenta.Visible = true;
                                usuarioEliminado.Clear();
                                contrasenyaEliminado.Clear();
                            }
                            else if (mensaje == "-1")

                                MessageBox.Show("Error al eliminar el usuario");
                    
                            else if (mensaje == "1")
                            {
                                MessageBox.Show("El usuario que quiere eliminar no existe");
                                usuarioEliminado.Clear();
                                contrasenyaEliminado.Clear();
                            }
                            else if (mensaje == "2")
                            {
                                MessageBox.Show("Contraseña incorrecta");
                                contrasenyaEliminado.Clear();
                            }
                            else
                            {
                                MessageBox.Show("El usuario esta conectado.\n Para eliminar un usuario, este no puede estar online");
                                usuarioEliminado.Clear();
                                contrasenyaEliminado.Clear();
                            } 
                            fin = true;
                            break;
                        case 18: //Retorna las partidas jugadas en la fecha y su duración
                            if (mensaje == "-1")
                                MessageBox.Show("Error en la consulta, vuelva a intentarlo");
                            else
                            {
                                this.formConsultas = new FormConsultas();
                                this.formConsultas.SetPregunta(fechaBtn.Text);
                                this.formConsultas.SetDataGrid(codigo, mensaje);
                                this.formConsultas.ShowDialog();
                            }
                            
                            break;
                    }
                }
                catch (SocketException)
                {
                    MessageBox.Show("Server desconectado");
                }
            }
            //Desconexión
            byte[] msg0 = System.Text.Encoding.ASCII.GetBytes("0/");
            server.Send(msg0);

            //Desconexión del servidor
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        //Ejecutar un nuevo thread con el formulario de una partida
        private void NuevaPartida(string mensaje)
        {
            //Creación del nuevo formilario para la nueva partida
            FormJuego tablero = new FormJuego();
            tablero.SetPartida(mensaje,this.server,this.username);
            tableros.Add(tablero);
            tablero.ShowDialog();
            tableros.Remove(tablero);

          
        }

        //Iniciamos Form
        private void Acceso_Load(object sender, EventArgs e)
        {
            //Si no se ha iniciado/registrado, varias funciones están ocultas hasta que el usuario finalmente entre en la aplicación
            registroBox.Visible = false;
            consultaBox.Visible = false;
            consultasButton.Visible = false;
            listaconGridView.Visible = false;
            labelConectados.Visible = false;
            invitarButton.Visible = false;
            conexion.Visible = false;
            invitadosGridView.Visible = false;
            label6.Visible = false;
            inicio.Visible = false;
            eliminarBox.Visible = false;
            inv_lbl.Visible = false;

            //Fondos
            candadoBox.Image = Image.FromFile(".\\candadoCerrado.jpg");
            candadoBox.SizeMode = PictureBoxSizeMode.StretchImage;

            //Fondos
            candadoEliminado.Image = Image.FromFile(".\\candadoCerrado.jpg");
            candadoEliminado.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        //Botón de conexión/desconexión
        private void conexion_Click(object sender, EventArgs e)
        {
            //Estamos conectados y queremos desconectarnos de la aplicación
            if (conn==1)
            {
                try
                {
                    //Cerramos los tableros abiertos
                    for (int i = 0; i < tableros.Count; i++)
                        tableros[i].Close();
                    tableros.Clear();

                    //Mensaje para avisar de desconexión
                    string mensaje = "0/";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                   
                    atender.Abort();

                    //Desconexión
                    server.Shutdown(SocketShutdown.Both);
                    server.Close();
                    conexion.Visible=false;
                    conn = 0;

                  
                    this.BackColor = Color.DarkSlateGray;

                    //Pantalla inicial
                    Bitmap portada = new Bitmap(Application.StartupPath + @"\trivia.jpg");
                    this.BackgroundImage = portada;
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                    consultaBox.Visible = false;
                    consultasButton.Visible = false;
                    accederBox.Visible = true;
                    registroBox.Visible = false;
                    listaconGridView.Visible = false;
                    labelConectados.Visible = false;
                    invitarButton.Text = "Invitar";
                    invitarButton.Visible = false;
                    regLabel.Visible = true;
                    regVisible.Visible = true;
                    eliminarLbl.Visible = true;
                    eliminarCuenta.Visible = true;
                    invitadosGridView.Visible = false;
                    label6.Visible = false;
                    inv_lbl.Visible = false;    


                    //Vaciamos las casillas por si habian quedado rellenadas
                    NameBox.Clear();
                    PasswordBox.Clear();

                }
                catch (Exception)
                {
                    MessageBox.Show("Ya estás desconectado.");
                }
            }
        }
        //Iniciar sesion
        private void Login_Click(object sender, EventArgs e)
        {
            try
            {
                //Se conecta al servidor sólo al entrar 

                IPAddress direc = IPAddress.Parse("10.4.119.5");
                IPEndPoint ipep = new IPEndPoint(direc, 50026);

                //Creamos el socket 
                this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
               
                server.Connect(ipep);

                //Iniciamos el Thread para atender a los mensajes de los clientes
                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
                
                username = NameBox.Text;
                //Codigo 1 - Inicio sesion
                string mensaje = "1/" + NameBox.Text + "/" + PasswordBox.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
            catch (SocketException)
            {
                MessageBox.Show("No he podido conectar con el servidor");
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: Compruebe que está conectado al servidor.");
            }

        }
        //Botón para registrarse 
        private void Registrarme_Click(object sender, EventArgs e)
        {
            try
            {
                //Creamos el socket y nos conectamos
                IPAddress direc = IPAddress.Parse("10.4.119.5");
                IPEndPoint ipep = new IPEndPoint(direc, 50026);
                this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect(ipep);

                //Abrimos el thread
                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();

                //Codigo 2 - Registrarse
                if ((userBox.Text != "0") && (password2Box.Text != "0"))
                {
                    string mensaje = "2/" + userBox.Text + "/" + password2Box.Text + "/" + mailBox.Text;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else
                    MessageBox.Show("Ningún campo puede ser 0");              

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: Compruebe que está conectado al servidor.");
            }
        }
        

       
        //Mostrar y ocultar las contraseñas.
        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {
            PasswordBox.UseSystemPasswordChar = true;
            candadoBox.Image = Image.FromFile(".\\candadoCerrado.jpg");
        }

        //Cambiamos pictureBox, si quiere mostrar/ocultar contraseña
        private void candadoBox_Click(object sender, EventArgs e)
        {
            //Poder ver la contraseña
            if (PasswordBox.UseSystemPasswordChar == true)
            {
                PasswordBox.UseSystemPasswordChar = false;
                candadoBox.Image = Image.FromFile(".\\candadoAbierto.jpg");
            }

            //Ocultar la contraseña
            else
            {
                PasswordBox.UseSystemPasswordChar = true;
                candadoBox.Image = Image.FromFile(".\\candadoCerrado.jpg");
            }
        }
        //Desplegar/esconder posibles consultas
        private void consultasButton_Click(object sender, EventArgs e)
        {
            //Si las consultas estan desplegadas y queremos esconderlas
            if (x==0)
            {
                consultaBox.Visible = false;
                consultasButton.Text = "Mostrar\n consultas";
                x = 1;
            }
            //Si las consultas estas escondidas y queremos desplegarlas
            else
            {
                consultaBox.Visible = true;
                consultasButton.Text = "Ocultar\n consultas";
                x = 0;
            }
        }
        //Si cerramos el form directamente tambien tenemos que desconectar el socket y detener el thread
        private void Acceso_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (conn == 1) {

                    //Mensaje de desconexion
                    string mensaje = "0/";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    atender.Abort();

                    //Desconexión
                    server.Shutdown(SocketShutdown.Both);
                    server.Close();

                    //Cerramos todos los tableros abiertos
                    for (int i = 0; i < tableros.Count; i++)
                        tableros[i].Close();
                    tableros.Clear();
                }
            }
            catch (Exception)
            {
             
            }
        }

        private void ConectadosGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string invitado = listaconGridView.CurrentCell.Value.ToString();

                //Comprobamos que no somos nosotros mismos
                if (invitado == username)
                {
                    MessageBox.Show("No te puedes autoinvitar");
                }
                else
                {
                    if (invitadosGridView.Visible == false)
                    {
                        invitados = new List<string>();
                        invitadosGridView.Visible = true;
                        invitarButton.Visible = true;
                        label6.Visible = true;
                    }

                    if (invitados.Count <= 3)
                    {

                        //Comprobamos que no este ya en la lista para añadirlo
                        int i = 0;
                        bool encontrado = false;
                        while ((i < invitados.Count) && (encontrado == false))
                        {
                            if (invitado == invitados[i])
                                encontrado = true;

                            else
                                i = i + 1;
                        }
                        if (encontrado == false)
                        {
                            invitados.Add(invitado);
                            CrearInvitadosGridView(invitados);
                        }
                    }
                    else
                        MessageBox.Show("El numero máximo de invitados es 3");
                }
                listaconGridView.SelectAll();


            }
            catch (NullReferenceException)
            {

            }   
            
        }

        private void CrearInvitadosGridView(List<string> invitados)
        {
            invitadosGridView.ColumnCount = 1;
            invitadosGridView.RowCount = invitados.Count;
            invitadosGridView.ColumnHeadersVisible = false;
            invitadosGridView.RowHeadersVisible = false;
            invitadosGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            invitadosGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            invitadosGridView.SelectAll();
            invitadosGridView.BackgroundColor = Color.White;

            int totalRowHeight = invitadosGridView.ColumnHeadersHeight;
            for (int i = 0; i < invitados.Count; i++)
            {
                invitadosGridView.Rows[i].Cells[0].Value = invitados[i];
                totalRowHeight += invitadosGridView.Rows[i].Height;
            }
            invitadosGridView.Height = totalRowHeight;
            invitadosGridView.Height = invitadosGridView.Height + 5;

        }
        private void invitarButton_Click(object sender, EventArgs e)
        {
            string mensaje = "6/";
            for (int i = 0; i < invitados.Count; i++)
            {
                mensaje = mensaje + invitados[i] + "*";
            }
            mensaje = mensaje.Remove(mensaje.Length - 1);

            //Codigo 6 - Invitar a jugadores
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            label6.Visible = false;
            invitadosGridView.Visible = false;
            invitarButton.Visible = false;
        }

        private void invitadosGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string eliminado = invitadosGridView.CurrentCell.Value.ToString();
                invitados.Remove(eliminado);
                if (invitados.Count == 0)
                {
                    invitadosGridView.Visible = false;
                    invitarButton.Visible = false;
                    label6.Visible = false;
                }
                else
                    CrearInvitadosGridView(invitados);
            }
            catch (NullReferenceException)
            {

            }
        }

        private void regVisible_Click(object sender, EventArgs e)
        {
            registroBox.Visible = true;
            regVisible.Visible = false;
            accederBox.Visible = false; 
            regLabel.Visible = false;
            inicio.Visible=true;
            eliminarLbl.Visible = false;
            eliminarCuenta.Visible = false;
        }

        private void inicio_Click(object sender, EventArgs e)
        {
            registroBox.Visible = false;
            inicio.Visible = false;
            regVisible.Visible = true;
            accederBox.Visible = true;
            regLabel.Visible = true;
            eliminarLbl.Visible = true;
            eliminarCuenta.Visible = true;
        }

        private void eliminarCuenta_Click(object sender, EventArgs e)
        {
            eliminarBox.Visible = true;
            eliminarLbl.Visible = false;
            eliminarCuenta.Visible = false;
            regVisible.Visible = false;
            accederBox.Visible = false;
            regLabel.Visible = false;
            volverLbl.Visible = true;
        }

        private void volverLbl_Click(object sender, EventArgs e)
        {
            eliminarBox.Visible = false;
            accederBox.Visible = true;
            regVisible.Visible = true;
            regLabel.Visible = true;
            volverLbl.Visible = false;
            eliminarLbl.Visible = true;
            eliminarCuenta.Visible = true;
        }

        //Eliminar usuario de la BBDD
        private void eliminarBtn_Click(object sender, EventArgs e)
        {
            if (usuarioEliminado.Text == "0")
            {
                MessageBox.Show("Nombre de usuario invalido");
            }
            else
            {
                try
                {
                    //Creamos sockets y conectamos
                    IPAddress direc = IPAddress.Parse("10.4.119.5");
                    IPEndPoint ipep = new IPEndPoint(direc, 50026);
                    this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    server.Connect(ipep);

                    //Abrimos Thread
                    ThreadStart ts = delegate { AtenderServidor(); };
                    atender = new Thread(ts);
                    atender.Start();

                    //Enviamos mensaje a la BBD apra eliminar jugador
                    string mensaje = "13/"+usuarioEliminado.Text+"/"+contrasenyaEliminado.Text;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                catch (SocketException)
                {
                    MessageBox.Show("Servidor no disponible");
                }
            }
            
        }

        private void candadoEliminado_Click(object sender, EventArgs e)
        {
            //Usuario quiere poder ver la contraseña
            if (contrasenyaEliminado.UseSystemPasswordChar == true)
            {
                contrasenyaEliminado.UseSystemPasswordChar = false;
                candadoEliminado.Image = Image.FromFile(".\\candadoAbierto.jpg");
            }

            //Usuario quiere ocultar la contraseña
            else
            {
                contrasenyaEliminado.UseSystemPasswordChar = true;
                candadoEliminado.Image = Image.FromFile(".\\candadoCerrado.jpg");
            }
        }

        private void pregBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConQuienLbl.Checked)
                {
                    //Construimos el mensaje y lo enviamos para saber con quien he jugado
                    string mensaje = "3/" + NameBox.Text;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                }
                else if (companyia.Checked)
                {
                    //Construimos el mensaje y lo enviamos para saber qué partidas he jugado con ellos
                    string mensaje = "4/" + nombresBox.Text;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else if (jugMaxBtn.Checked)
                {
                    //Construimos el mensaje y lo enviamos. Queremos saber el JMV
                    string mensaje = "5/";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                else //Construimos el mensaje y lo enviamos para saber las partidas jugadas durante un día en concreto
                {
                    string fecha = dateTimePicker.Value.ToString().Split(' ')[0];
                    string[] fechas = fecha.Split('/');
                    int año = Convert.ToInt32(fechas[2]) - 2000;
                    string mensaje = "14/" + fechas[0] + "/" + fechas[1] + "/" + año.ToString();
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: Compruebe que está conectado al servidor.");
            }
        }

    }
    
}
