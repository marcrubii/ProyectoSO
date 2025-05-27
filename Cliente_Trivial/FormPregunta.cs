using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trivial
{
    public partial class FormPregunta : Form
    {
        // 1 ---> Respuesta correcta, 0 ---> Respuesta incorrecta
        string categoria;
        ListaPreguntas geografia = new ListaPreguntas(@".\\geografia.txt");
        ListaPreguntas historia = new ListaPreguntas(@".\\historia.txt");
        ListaPreguntas ciencia = new ListaPreguntas(@".\\ciencia.txt");
        ListaPreguntas deportes = new ListaPreguntas(@".\\deportes.txt");
        ListaPreguntas entretenimiento = new ListaPreguntas(@".\\cultura.txt");
        ListaPreguntas tecnologia = new ListaPreguntas(@".\\tecnologia.txt");
        Pregunta pregunta;
        Random number = new Random();
        string enunciado;
        string[] opciones;
        int correcta;
        int acierto;
        int segundos;
        
        public FormPregunta()
        {
            InitializeComponent();
 
        }

        //Pasar la cateogria de preguntas al formulario
        public void SetCategory(string categoria)
        {
            this.categoria = categoria;
        }
        //Función para enviar si el cliente ha acertado
        public int GetAcierto()
        {
            return this.acierto;
        }

        private void Prueba_Load(object sender, EventArgs e)
        {
            //Seleccionamos pregunta aleatoria y cambiamos el color del fondo a la cateogria actual
            if (categoria=="Ciencia")
            {
                Bitmap fondo = new Bitmap(Application.StartupPath + @"\science.png");
                this.BackgroundImage = (Image)fondo;
                this.BackgroundImageLayout = ImageLayout.Center;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.BackgroundImageLayout = ImageLayout.Stretch;
                this.MaximizeBox = false;
                this.ControlBox = false;
                int num = number.Next(1, ciencia.DameLongitud());
                pregunta = ciencia.DamePregunta(num);
                cat_label.Text = "CIENCIA";
            }
            else if (categoria == "Historia")
            {
                Bitmap fondo = new Bitmap(Application.StartupPath + @"\historia.png");
                this.BackgroundImage = (Image)fondo;
                this.BackgroundImageLayout = ImageLayout.Center;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.BackgroundImageLayout = ImageLayout.Stretch;
                this.MaximizeBox = false;
                this.ControlBox = false;
                int num = number.Next(1, historia.DameLongitud());
                pregunta = historia.DamePregunta(num);
                cat_label.Text = "HISTORIA";
            }
            else if (categoria == "Geografia")
            {
                Bitmap fondo = new Bitmap(Application.StartupPath + @"\geografia.png");
                this.BackgroundImage = (Image)fondo;
                this.BackgroundImageLayout = ImageLayout.Center;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.BackgroundImageLayout = ImageLayout.Stretch;
                this.MaximizeBox = false;
                this.ControlBox = false;
                int num = number.Next(1, geografia.DameLongitud());
                pregunta = geografia.DamePregunta(num);
                cat_label.Text = "GEOGRAFÍA";
            }
            else if (categoria == "Deportes")
            {
                Bitmap fondo = new Bitmap(Application.StartupPath + @"\deportes.png");
                this.BackgroundImage = (Image)fondo;
                this.BackgroundImageLayout = ImageLayout.Center;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.BackgroundImageLayout = ImageLayout.Stretch;
                this.MaximizeBox = false;
                this.ControlBox = false;
                int num = number.Next(1, deportes.DameLongitud());
                pregunta = deportes.DamePregunta(num);
                cat_label.Text = "DEPORTES";
            }
            else if (categoria == "Entretenimiento")
            {
                Bitmap fondo = new Bitmap(Application.StartupPath + @"\entreten.png");
                this.BackgroundImage = (Image)fondo;
                this.BackgroundImageLayout = ImageLayout.Center;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.BackgroundImageLayout = ImageLayout.Stretch;
                this.MaximizeBox = false;
                this.ControlBox = false;
                int num = number.Next(1, entretenimiento.DameLongitud());
                pregunta = entretenimiento.DamePregunta(num);
                cat_label.Text = "ENTRETENIMIENTO";
            }
            if (categoria == "Tecnologia")
            {
                Bitmap fondo = new Bitmap(Application.StartupPath + @"\tecno.png");
                this.BackgroundImage = (Image)fondo;
                this.BackgroundImageLayout = ImageLayout.Center;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.BackgroundImageLayout = ImageLayout.Stretch;
                this.MaximizeBox = false;
                this.ControlBox = false;
                int num = number.Next(1, tecnologia.DameLongitud());
                pregunta = tecnologia.DamePregunta(num);
                cat_label.Text = "TECNOLOGÍA";
            }

            //Nos quedamos con las variables de la pregunta
            enunciado = pregunta.GetEnunciado();
            opciones=pregunta.GetOpciones();
            correcta = pregunta.GetCorrecta();

            //Escribimos el enunciado y las posibles opciones de las preguntas
            pregunta_lbl.Text = enunciado;
            opcion0.Text = opciones[0];
            opcion1.Text = opciones[1];
            opcion2.Text = opciones[2];
            opcion3.Text = opciones[3];

            //Mostrar e iniciar temporizador
            timer1.Start();
            segundos = 20;
            timer_label.Text=segundos.ToString();

            acierto = 0;
        }

        private void Enviar_Click(object sender, EventArgs e)
        {
            if (opcion0.Checked)
            {
                if (correcta == 0)
                {
                    acierto = 1;
                }
            }
            else if (opcion1.Checked)
            {
                if (correcta == 1)
                {
                    acierto=1;
                }     
            }
            else if (opcion2.Checked)
            {
                if(correcta == 2)
                {
                    acierto = 1;
                }
            }
            else if (opcion3.Checked)
            {
                if(correcta==3)
                {
                    acierto = 1;
                }
            }
            if (acierto == 1)
                MessageBox.Show("Muy bien! Respuesta correcta");
            else
                MessageBox.Show("Ohhhh... Respuesta incorrecta");
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            segundos = segundos - 1;
            if(segundos>0)
            {
                timer_label.Text = segundos.ToString();              
            }
            else
            {
                timer1.Stop();
                this.Close();
            }
        }


    }
}
