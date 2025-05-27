using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Trivial
{
    public partial class FormJuego : Form 
    {
        int partida; 
        int casilla;
        int dice;
        bool end;

        Jugador miJugador;

        bool miTurno;
        bool dadoClick;
        bool tableroClick;

        Socket server;

        Queue<string> chat;

        List<Jugador> jugadors;   //Jugadores de la partida: host, jug2, (jug3, jug4)
        ListaCasillas casillas;   //Casillas tablero
        List<PictureBox> ubicaciones;  //Indicaciones posibles movimientos
        List<PictureBox> piezas;       //Pieza de cada jugador. 
        List<Bitmap> piezasbit;

        // Quesitos
        Bitmap qV; 
        Bitmap qB;
        Bitmap qA;
        Bitmap qL;
        Bitmap qN;
        Bitmap qR;
        List<Bitmap> quesitos;

        int xorigen;
        int yorigen;

        int x;
        int y;

        public FormJuego()
        {
            InitializeComponent();
            PictureBox dado = new PictureBox();
            // El fondo del Form es la imagen del tablero
            Bitmap tablero = new Bitmap(Application.StartupPath + @"\Tablero.png");
            this.BackgroundImage = (Image)tablero;
            this.BackgroundImageLayout = ImageLayout.Center;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            tableroBox.Image = (Image)tablero;
            tableroBox.BackColor = Color.Transparent;
            tableroBox.SizeMode = PictureBoxSizeMode.AutoSize;
            tableroBox.Location = new Point(0, 0);
            this.Size = tableroBox.Size;

            // Imagenes para mostrar posibles movimientos
            Bitmap ubi = new Bitmap(Application.StartupPath + @"\ubicacion.png");
            this.ubicaciones = new List<PictureBox>();
            PictureBox[] ubilist = new PictureBox[] { ubi1Box, ubi2Box, ubi3Box, ubi4Box, ubi5Box, ubi6Box, ubi7Box };
            ubicaciones.AddRange(ubilist);
            foreach(PictureBox pBox in ubicaciones)
            {
                pBox.Image = (Image)ubi;
                pBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pBox.Visible = false;
            }

            // Fichas
            qV = new Bitmap(Application.StartupPath + @"\quesitoVerde.png");
            qB = new Bitmap(Application.StartupPath + @"\quesitoAzul.png");
            qA = new Bitmap(Application.StartupPath + @"\quesitoAmarillo.png");
            qL = new Bitmap(Application.StartupPath + @"\quesitoLila.png");
            qN = new Bitmap(Application.StartupPath + @"\quesitoNaranja.png");
            qR = new Bitmap(Application.StartupPath + @"\quesitoRojo.png");
            this.quesitos = new List<Bitmap>();
            Bitmap[] bitlist = new Bitmap[] { qV, qB, qA, qL, qN, qR };
            quesitos.AddRange(bitlist);
            
            this.xorigen = (tableroBox.Size.Width / 2) + tableroBox.Location.X;
            this.yorigen = tableroBox.Size.Height / 2 + tableroBox.Location.Y;

            casillas = new ListaCasillas(xorigen,yorigen);
            casillas.CalcularMovimientos();

            ChatBox.Multiline = true;
            chat = new Queue<string>();

            end = false;
        }

        private void Tablero_Load(object sender, EventArgs e)
        {
            dado.Image = Image.FromFile("dado1.png");
            username_lbl.Text = "Usuario: " + miJugador.GetNombre();
            partida_lbl.Text = "Partida: " + partida;
            playersGridView.ColumnCount = 2;
            playersGridView.RowCount = this.jugadors.Count;
            playersGridView.ColumnHeadersVisible = true;
            playersGridView.Columns[0].HeaderText = "Jugador";
            playersGridView.Columns[1].HeaderText = "Quesitos";
            playersGridView.RowHeadersVisible = false;
            playersGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            playersGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            int totalRowHeight = playersGridView.ColumnHeadersHeight;       
            playersGridView.RowsDefaultCellStyle.BackColor = Color.White;
            for (int q = 0; q < this.jugadors.Count; q++)
            {
                playersGridView.Rows[q].Cells[0].Value = jugadors[q].GetNombre();
                playersGridView.Rows[q].Cells[1].Value = "0";
                totalRowHeight += playersGridView.Rows[q].Height;
            }
            playersGridView.Height = totalRowHeight;
            playersGridView.Height = playersGridView.Height + 5;

            playersGridView.Rows[0].DefaultCellStyle.BackColor = Color.RoyalBlue;
            playersGridView.ClearSelection();

            //Establecemos el turno inicial
            if (miJugador.GetRol() == "host")
            {
                miTurno = true;
                dadoClick = true;
                notificacionLbl.Text = "Es tu turno";
            }
            else
            {
                miTurno = false;
                dadoClick = false;
                notificacionLbl.Text = "Es el turno de " + jugadors[0].GetNombre();
            }
            tableroClick = false;
            
        }        
        public void SetPartida(string mensaje, Socket server, string userName)
        {
            string[] trozos = mensaje.Split('*'); 
            this.partida = Convert.ToInt32(trozos[0]);
            
            jugadors = new List<Jugador>();
            List<string> roles = new List<string>();
            string[] r = new string[] { "host", "jug2", "jug3", "jug4" };
            roles.AddRange(r);
            for (int i = 1; i < trozos.Length - 1; i++)
            {
                Jugador j = new Jugador(trozos[i], roles[i - 1]);
                jugadors.Add(j);
            }
            miJugador = new Jugador(userName, trozos[trozos.Length - 1]);
            //Empezamos en la casilla central
            casilla = 1000;

            // Colocar las piezas de todos los jugadores
            this.piezas = new List<PictureBox>();
            PictureBox[] emlist = new PictureBox[] { hostBox, jug2Box, jug3Box, jug4Box };
            piezas.AddRange(emlist);

            int k = 0;
            while (k < piezas.Count)
            {
                if (k < jugadors.Count)
                {
                    piezas[k].Image = MakeNewImage(jugadors[k]);
                    piezas[k].SizeMode = PictureBoxSizeMode.CenterImage;
                    piezas[k].BackColor = Color.Transparent;
                    piezas[k].Visible = true;
                    piezas[k].Location = new Point(Convert.ToInt32(xorigen - hostBox.Size.Width / 2), Convert.ToInt32(yorigen - hostBox.Size.Height / 2));
                }
                else
                {
                    piezas[k].Visible = false;
                }
                k++;
            }

            this.server = server;
        }

        public void FinalizarPartida()
        {
            this.end = true;
        }

        //Se recibe un nuevo movimiento
        public void NuevoMovimiento(string mensaje) 
        {
            string[] trozos = mensaje.Split('*');
            if (miTurno == false)
            { 
                dadolbl.Text = trozos[2] + " ha sacado un " + trozos[1];
                dado.Image = Image.FromFile("dado" + trozos[1] + ".png");
            }
        }
        // Recibimos que alguien tiene una nueva casilla 
        public void setCasillaJugador(string mensaje)
        {
            string[] trozos = mensaje.Split('*');
            int j = 0;
            bool encontrado = false;
            while ((j < jugadors.Count) && (encontrado == false))
            {
                if (jugadors[j].GetRol() == trozos[2])
                    jugadors[j].SetCasilla(Convert.ToInt32(trozos[3]));
                j++;
            }
            moverCasillaJugadores();

        }
        // Anuncia el ganador a todos los jugadores
        public void Ganador(string mensaje)
        {
            string[] trozos = mensaje.Split('*');
            if (trozos[1] == miJugador.GetNombre())
                MessageBox.Show("¡¡¡¡¡¡¡¡GANADOR!!!!!!!!!");
            else
                MessageBox.Show("Ganó " + trozos[1] + ".");

        }
   
        public void moverCasillaJugadores()
        {            
            foreach(Jugador j in this.jugadors)
            {
                if (j.GetNombre()!=miJugador.GetNombre())
                {
                    int idc = j.GetCasilla();
                    int x = this.casillas.DameCasilla(idc).GetX();
                    int y = this.casillas.DameCasilla(idc).GetY();
                    Bitmap bitmap = MakeNewImage(j);
                    piezas[j.GetRolNum()].Location = new Point(Convert.ToInt32(x - hostBox.Size.Width / 2), Convert.ToInt32(y - hostBox.Size.Height / 2));
                }
            }
        }
        //Actualizar turno ---> una vez se ha respondido, se notifica el resultado
        public void ActualizarResultadoPregunta(string mensaje) 
        {
            // 0 ---> mal contestada pero se actualiza turno
            // 1 ---> bien contestada pero sin queso
            // 2 ---> bien contestada y con queso
            string[] trozos = mensaje.Split('*');
            if (trozos[1] != miJugador.GetNombre())
            {
                if (trozos[2] == "0")
                    notificacionLbl.Text = trozos[1] + " ha contestado mal";
                else if (trozos[2] == "1")
                    notificacionLbl.Text = trozos[1] + " ha contestado bien, sigue tirando";
                else
                {
                    notificacionLbl.Text = trozos[1] + " ha ganado un quesito!"; 
                    int q = 0;
                    bool f = false;

                    // Cambiar la pieza de dicho jugador
                    q = 0;
                    f = false;
                    //Encontrar el num del jugador que ha ganado el queso
                    while ((q<jugadors.Count)&&(f == false))
                    {
                        if (trozos[1] == jugadors[q].GetNombre())
                        {
                            f = true;
                        }
                        else
                            q++;
                    }
                    //Modificar el vector quesos del jugador
                    int res = jugadors[q].SetQuesitoCat(trozos[4]);
                    //Crear la pieza como siempre
                    Bitmap bitmap = MakeNewImage(jugadors[q]);
             
                    piezas[jugadors[q].GetRolNum()].Image = (Image)bitmap;

                    //Actualizamos el num de quesos en el datGridView
                    if (res == 0)
                        playersGridView.Rows[jugadors[q].GetRolNum()].Cells[1].Value = Convert.ToInt32(playersGridView.Rows[jugadors[q].GetRolNum()].Cells[1].Value) + 1;

                }
            }
            if (((trozos[2] == "0")|| (trozos[2] == "2")))
            {
                string siguienteTurno = trozos[3];
                for (int i = 0; i < playersGridView.RowCount; i++)
                {
                    playersGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    playersGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                switch (siguienteTurno)
                {
                    case "host":
                        playersGridView.Rows[0].DefaultCellStyle.BackColor = Color.RoyalBlue;
                        playersGridView.Rows[0].DefaultCellStyle.ForeColor = Color.White;

                        break;
                    case "jug2":
                        playersGridView.Rows[1].DefaultCellStyle.BackColor = Color.LightPink;
                        playersGridView.Rows[1].DefaultCellStyle.ForeColor = Color.White;
                        break;
                    case "jug3":
                        playersGridView.Rows[2].DefaultCellStyle.BackColor = Color.DarkViolet;
                        playersGridView.Rows[2].DefaultCellStyle.ForeColor = Color.White;

                        break;
                    case "jug4":
                        playersGridView.Rows[3].DefaultCellStyle.BackColor = Color.SeaGreen;
                        playersGridView.Rows[3].DefaultCellStyle.ForeColor = Color.White;
                        break;
                }
                playersGridView.ClearSelection();
                if (trozos[3] == miJugador.GetRol())
                {
                    miTurno = true;
                    dadoClick = true;
                    MessageBox.Show("ES TU TURNO");
                    if (trozos[2]!="2")
                        notificacionLbl.Text = "Es tu turno";
                }
                else
                {
                    miTurno = false;
                    dadoClick = false;
                }
            }
            else
            {
                if (trozos[1] == miJugador.GetNombre())
                {
                    miTurno = true;
                    dadoClick = true;
                    notificacionLbl.Text = "Es tu turno";
                }
                else
                {
                    miTurno = false;
                    dadoClick = false;
                }
            }
        }

        //Obtener IDPartida
        public int DameIdPartida()
        {
            return this.partida;
        }

        //Tirar el dado y mostrar el resultado
        private void dado_Click_1(object sender, EventArgs e)
        {
            if ((miTurno == true) && (dadoClick == true))
            {
                Random dice = new Random();
                this.dice = dice.Next(1, 6);
                if (this.dice == 1)
                    dado.Image = Image.FromFile("dado1.png");
                else if (this.dice == 2)
                    dado.Image = Image.FromFile("dado2.png");
                else if (this.dice == 3)
                    dado.Image = Image.FromFile("dado3.png");
                else if (this.dice == 4)
                    dado.Image = Image.FromFile("dado4.png");
                else if (this.dice == 5)
                    dado.Image = Image.FromFile("dado5.png");
                else
                    dado.Image = Image.FromFile("dado6.png");

                List<int> movimientos = casillas.DameMovimientosPosibles(casilla, this.dice);
                
                int m = 0;
                double xm = ubicaciones[m].Size.Width / 2;
                double ym = ubicaciones[m].Size.Height;
                while (m<movimientos.Count)
                {
                    Casilla c = casillas.DameCasilla(movimientos[m]); 
                    ubicaciones[m].Location = new Point(Convert.ToInt32(c.GetX() - xm), Convert.ToInt32(c.GetY() - ym));
                    ubicaciones[m].Visible = true;
                    ubicaciones[m].BackColor = Color.Transparent;
                   m++;
                }

                dadoClick = false;  
                tableroClick = true; 

                dadolbl.Text = "Avanzo " + this.dice.ToString() + " casillas";

                //Construimos el mensaje para enviar el resultado del dado
                string resDado = "8/" + partida + "/" + this.dice + "/" + miJugador.GetRol() + "/" + miJugador.GetNombre();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(resDado);
                server.Send(msg);
            }
            else
                MessageBox.Show("No es tu turno");
            
        }

        private void Tablero_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (end == false)
            {
                string finpartida = "9/" + partida;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(finpartida);
                server.Send(msg);
            }
        }

        //Mensajear en el chat
        private void ChatBtn_Click(object sender, EventArgs e)
        {
            if (ChatTxt.Text != "")
            {
                string mensaje = "12/" + partida + "/" + ChatTxt.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Escribimos en el chat lo que enviamos
                string mchat = "Yo: " + ChatTxt.Text;
                if (chat.Count >= 9)
                {
                    chat.Dequeue();
                    chat.Enqueue(mchat);

                    ChatBox.Clear();
                    foreach(string msgChat in chat)
                    {
                        ChatBox.Text = ChatBox.Text + msgChat + Environment.NewLine;
                    }
                }
                else if (chat.Count == 8)
                {
                    chat.Enqueue(mchat);
                    ChatBox.Text = ChatBox.Text + mchat;
                }
                else
                {
                    chat.Enqueue(mchat);
                    ChatBox.Text = ChatBox.Text + mchat + Environment.NewLine;
                }
                //Borramos lo escrito una vez enviado
                ChatTxt.Clear();
            }
        }

        //Recibir mensajes del chat
        public void NuevoMensajeChat(string datos)
        {
            string mensaje = datos.Split('*')[1] + ": "+ datos.Split('*')[2];
          
            if (chat.Count >= 9)
            {
                chat.Dequeue();
                chat.Enqueue(mensaje);

                ChatBox.Clear();
                foreach (string msgChat in chat)
                {
                    ChatBox.Text = ChatBox.Text + msgChat + Environment.NewLine;
                }
            }
            else if (chat.Count == 8)
            {
                chat.Enqueue(mensaje);
                ChatBox.Text = ChatBox.Text + mensaje;
            }
            else
            {
                chat.Enqueue(mensaje);
                ChatBox.Text = ChatBox.Text + mensaje + Environment.NewLine;
            }
        }
        // Extraer la pieza del jugador
        private Bitmap MakeNewImage(Jugador jug)        
        {
            List<Bitmap> listBit = new List<Bitmap>();
            listBit.Add(jug.GetEmboltorioBitmap());

            int p = 0;
            while(p < jug.GetQuesitos().Length)
            {
                if (jug.GetQuesitos()[p] == 1)
                    listBit.Add(this.quesitos[p]);
                p++;
            }
            //Sobreponemos los bitmaps y las imagenes
            int i = 1;
            while (i < listBit.Count)
            {
                if (listBit[i] != null)
                {
                    Graphics g = Graphics.FromImage(listBit[0]);
                    g.DrawImage(listBit[i], new Point(0, 0));
                }                
                i++;
            }            
            return listBit[0];
        }
        private void tableroBox_Click(object sender, EventArgs e)
        {

            if ((miTurno == true) && (tableroClick == true)) 
            {
                int idcasilla; 

                MouseEventArgs me = (MouseEventArgs)e;
                Point coordinates = me.Location;
                double xclick = coordinates.X;
                double yclick = coordinates.Y;

                double a = xclick - xorigen;
                double b = yclick - yorigen;

                double distOrigen = Math.Sqrt(a * a + b * b);

                double espesor = 45; 
                double ancho = 25; 
                double radi = 675 / 2;
                double alpha = 8.57;      
                double radians = Math.Atan2(a, b);
                double angle_pos = radians * (180 / Math.PI);  
                double angle_girat = 180 - angle_pos;   

                double r04 = 2 * espesor;
                double r03 = 3 * espesor;
                double r02 = 4 * espesor;
                double r01 = 5 * espesor;
                double r00 = 6 * espesor;
                double rtotal = radi; 

                string piso;
                string riera;

                if (distOrigen < rtotal) // Determinar dentro tablero
                {
                    double multiple = angle_girat / alpha;  

                    if ((distOrigen > r00) && (distOrigen <= rtotal)) // Determinar si estas en el perimetro
                    {
                        if (Math.Abs(a) <= ancho) //Determinar si es la de arriba ---> (0) o abajo ---> (21)
                        {
                            if (b > 0)
                                idcasilla = 21;
                            else
                                idcasilla = 0;
                        }
                        else
                        {
                            double m = Math.Round(multiple, 0);
                            idcasilla = Convert.ToInt32(m);
                        }
                    }
                    else
                    {
                        if (distOrigen <= espesor)
                        {
                            idcasilla = 1000;
                        }
                        else
                        {
                            if ((distOrigen > espesor) && (distOrigen <= r04))
                            {
                                piso = "4";
                            }
                            else if ((distOrigen > r04) && (distOrigen <= r03))
                            {
                                piso = "3";
                            }
                            else if ((distOrigen > r03) && (distOrigen <= r02))
                            {
                                piso = "2";
                            }
                            else if ((distOrigen > r02) && (distOrigen <= r01))
                            {
                                piso = "1";
                            }
                            else
                            {
                                piso = "0";
                            }
                            if (Math.Abs(a) <= ancho) //Determinar si es la de arriba ---> (0) o abajo ---> (21)
                            {
                                if (b > 0)
                                    riera = "13";
                                else
                                    riera = "10";
                                string c = riera + piso;
                                idcasilla = Convert.ToInt32(c);
                            }
                            else
                            {
                                if (((angle_pos > 0) && (angle_girat < 90)) || ((angle_pos < 0) && (angle_girat < 3 * 90)))
                            
                                {
                                    double yupper = -Math.Tan(Math.PI / 6) * xclick + yorigen + Math.Tan(Math.PI / 6) * xorigen - ancho / Math.Sin(Math.PI / 3);
                                    double ydown = -Math.Tan(Math.PI / 6) * xclick + yorigen + Math.Tan(Math.PI / 6) * xorigen + ancho / Math.Sin(Math.PI / 3);

                                    if ((yclick > yupper) && (yclick < ydown))
                                    {
                                        if (angle_pos > 0)
                                            riera = "11";
                                        else
                                            riera = "14";
                                        string c = riera + piso;
                                        idcasilla = Convert.ToInt32(c);
                                    }
                                    else
                                        idcasilla = -1;
                                }
                                else
                                {
                                    double yupper = Math.Tan(Math.PI / 6) * xclick + yorigen - Math.Tan(Math.PI / 6) * xorigen + ancho / Math.Sin(Math.PI / 3);
                                    double ydown = Math.Tan(Math.PI / 6) * xclick + yorigen - Math.Tan(Math.PI / 6) * xorigen - ancho / Math.Sin(Math.PI / 3);

                                    if ((yclick < yupper) && (yclick > ydown))
                                    {
                                        if (angle_pos > 0)
                                            riera = "12";
                                        else
                                            riera = "15";
                                        string c = riera + piso;
                                        idcasilla = Convert.ToInt32(c);
                                    }
                                    else
                                        idcasilla = -1;
                                }

                            }


                        }

                    }
                }
                else
                    idcasilla = -1;

                bool encontrado = false;
                int n = 0;
                while ((n<this.casillas.DameCasilla(casilla).GetMovimientos(dice).Count) && (encontrado==false)&&(idcasilla!=-1))
                {
                    if (this.casillas.DameCasilla(casilla).GetMovimientos(dice)[n] == idcasilla)
                        encontrado = true;
                    else
                        n++;
                }
                if (encontrado == true)
                {
                    this.tableroClick = false;
                    this.casilla = idcasilla;
                    int x = this.casillas.DameCasilla(casilla).GetX();
                    int y = this.casillas.DameCasilla(casilla).GetY();
                    foreach (PictureBox u in ubicaciones) // Desaparecen las ubicaciones
                        u.Visible = false;
                    Bitmap bitmap = MakeNewImage(miJugador);
                    piezas[miJugador.GetRolNum()].Location = new Point(Convert.ToInt32(x - hostBox.Size.Width / 2), Convert.ToInt32(y - hostBox.Size.Height / 2));

                    //Enviamos el movimiento al servidor
                    string mensaje = "10/" + partida + "/" + this.casilla + "/" + miJugador.GetRol();
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    //Se muestra la pregunta
                    string categoria = this.casillas.DameCasilla(casilla).GetCategoria();
                    if (categoria == "Tira otra vez")
                    {
                        dadoClick = true;
                        MessageBox.Show("Tira otra vez!");
                    }
                    else
                    {
                        if (categoria == "Central")
                        {
                            Random randCat = new Random();
                            int cat = randCat.Next(1, 7);
                            switch (cat)
                            {
                                case 1:
                                    categoria = "Ciencia";
                                    break;
                                case 2:
                                    categoria = "Historia";
                                    break;
                                case 3:
                                    categoria = "Geografia";
                                    break;
                                case 4:
                                    categoria = "Tecnologia";
                                    break;
                                case 5:
                                    categoria = "Entretenimiento";
                                    break;
                                case 6:
                                    categoria = "Deportes";
                                    break;
                            }
                        }
                        FormPregunta formPrueba = new FormPregunta();
                        formPrueba.SetCategory(categoria);
                        formPrueba.ShowDialog();
                        int acierto = formPrueba.GetAcierto();                        
                        //acierto = 1 == pregunta acertada
                        if ((acierto == 1) && (casilla % 7 == 0) && (casilla < 42))
                        {
                            acierto = 2;
                            MessageBox.Show("Has ganado un quesito");

                            int res = miJugador.SetQuesitoCat(categoria);
                            Bitmap bitmap2 = MakeNewImage(miJugador);
                            piezas[miJugador.GetRolNum()].Image = (Image)bitmap2;

                            // Cambio del playersGridView
                            if (res == 0)
                                playersGridView.Rows[miJugador.GetRolNum()].Cells[1].Value = Convert.ToInt32(playersGridView.Rows[miJugador.GetRolNum()].Cells[1].Value) + 1;
                        }

                        // Se envía el resultado
                        mensaje = "11/" + partida + "/" + miJugador.GetRol() + "/" + acierto;
                        if (acierto == 2)
                            mensaje = mensaje + "/" + categoria;
                        msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);

                        if (acierto == 1)
                        {
                            dadoClick = true;
                            notificacionLbl.Text = "Puedes volver a tirar";
                        }
                        else
                        {
                            miTurno = false;
                            dadoClick = false;
                            
                            
                            notificacionLbl.Text = "Has perdido el turno";

                        }
                    }
                }              
            
            }

        }

        private void dadolbl_Click(object sender, EventArgs e)
        {

        }

        private void jug2Box_Click(object sender, EventArgs e)
        {

        }
    }
}
