using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        public Form1()
        {
            InitializeComponent();
            IDPartida.Enabled = true; // Siempre habilitado
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9030);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");
            }
            catch (SocketException)
            {
                MessageBox.Show("No se pudo conectar al servidor");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string mensaje = "0/";
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            if (ConsultaMarc.Checked)
            {
                if (string.IsNullOrWhiteSpace(IDJugador.Text))
                {
                    MessageBox.Show("Por favor, introduce el ID del Jugador.");
                    return;
                }
                mensaje = "1/" + IDJugador.Text;
            }
            else if (ConsultaAlvaro.Checked)
            {
                if (string.IsNullOrWhiteSpace(IDPartida.Text))
                {
                    MessageBox.Show("Por favor, introduce el ID de la Partida.");
                    return;
                }
                mensaje = "2/" + IDPartida.Text;
            }
            else if (ConsultaVictorias.Checked)  // Consulta de Victorias por Jugador (Función 3)
            {
                if (string.IsNullOrWhiteSpace(IDJugador.Text))
                {
                    MessageBox.Show("Por favor, introduce el ID del Jugador.");
                    return;
                }
                mensaje = "3/" + IDJugador.Text;
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una consulta.");
                return;
            }

            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            byte[] msg2 = new byte[256];
            int bytesRecibidos = server.Receive(msg2);
            string respuesta = Encoding.ASCII.GetString(msg2, 0, bytesRecibidos);

            MessageBox.Show(respuesta);
        }

        private void ConsultaAlvaro_CheckedChanged(object sender, EventArgs e)
        {
            if (ConsultaAlvaro.Checked)
            {
                IDPartida.Enabled = true;
                IDJugador.Enabled = false;
                IDJugador.Text = "";
            }
            else
            {
                IDJugador.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
