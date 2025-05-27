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
    public partial class FormConsultas : Form
    {
        int totalRowHeight;
        public FormConsultas()
        {
            InitializeComponent();
        }
        // Escribe la pregunta
       

        //Muestra informacion
        public void SetDataGrid (int codigo, string mensaje)
        {
            string[] jugadores = mensaje.Split('/');
            if (codigo == 3) //lista contrincantes
            {
                if (mensaje == "0")
                {
                    infoLabel.Visible = true;
                    infoLabel.Text = "No has jugado con nadie... Todavía";
                    respGridView.Visible = false;
                }
                else
                {
                    infoLabel.Visible = false;
                    respGridView.Visible = true;
                    respGridView.ColumnCount = 1;
                    respGridView.RowCount = jugadores.Length;
                    respGridView.ColumnHeadersVisible = true;
                    respGridView.Columns[0].HeaderText = "Jugadores";
                    respGridView.RowHeadersVisible = false;
                    respGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    respGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    respGridView.SelectAll();
                    respGridView.BackgroundColor = Color.White;

                    int totalRowHeight = respGridView.ColumnHeadersHeight;
                    respGridView.RowsDefaultCellStyle.BackColor = Color.White;
                    for (int i = 0; i < jugadores.Length; i++)
                    {
                        respGridView.Rows[i].Cells[0].Value = jugadores[i];
                        totalRowHeight += respGridView.Rows[i].Height;
                    }
                    respGridView.Height = totalRowHeight;
                    respGridView.Height = respGridView.Height + 5;
                }
            }
            else if (codigo == 4) //Resultados con otros jugadores
            {
                if (mensaje == "0")
                {
                    infoLabel.Visible = true;
                    infoLabel.Text = "Ninguna coincidencia";
                    respGridView.Visible = false;
                }
                else
                {
                    infoLabel.Visible = false;
                    respGridView.Visible = true;
                    respGridView.ColumnCount = 3;                    
                    respGridView.ColumnHeadersVisible = true;
                    respGridView.Columns[0].HeaderText = "Contrincante";
                    respGridView.Columns[1].HeaderText = "Número de partida";
                    respGridView.Columns[2].HeaderText = "Ganador";
                    respGridView.RowHeadersVisible = false;
                    respGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    respGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    respGridView.SelectAll();
                    respGridView.BackgroundColor = Color.White;

                    int totalRowHeight = respGridView.ColumnHeadersHeight;
                    respGridView.RowsDefaultCellStyle.BackColor = Color.White;
                    string[] trozos = mensaje.Split('*');
                    respGridView.RowCount = trozos.Length;
                    int i = 0;
                    while(i<trozos.Length)
                    {
                        string[] info = trozos[i].Split(',');
                        respGridView.Rows[i].Cells[0].Value = info[0];
                        respGridView.Rows[i].Cells[1].Value = info[1];
                        respGridView.Rows[i].Cells[2].Value = info[2];
                        totalRowHeight += respGridView.Rows[i].Height;
                        i++;
                    }
                    respGridView.Height = totalRowHeight;
                    respGridView.Height = respGridView.Height + 5;
                }
            }
            else if (codigo == 5) //jugador más valioso 
            {
                respGridView.Visible = false;
                infoLabel.Visible = true;
                if (mensaje == "-2")
                {
                    infoLabel.Text = "No existe tal jugador";
                }
                else
                {
                    infoLabel.Text = "El jugador con la puntuación más alta es: " + mensaje.Split('*')[0] + "\nLleva acumulados "+ mensaje.Split('*')[1] + " puntos";
                }
            }
            else 
            {
                if (mensaje == "0")
                {
                    infoLabel.Text = "Ninguna coincidencia";
                    respGridView.Visible = false;
                }
                else
                {
                    respGridView.Visible = true;
                    respGridView.ColumnCount = 2;

                    respGridView.ColumnHeadersVisible = true;
                    respGridView.Columns[0].HeaderText = "Número de partida";
                    respGridView.Columns[1].HeaderText = "Duración";
                    respGridView.RowHeadersVisible = false;
                    respGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    respGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    respGridView.SelectAll();
                    respGridView.BackgroundColor = Color.White;

                    int totalRowHeight = respGridView.ColumnHeadersHeight;
                    respGridView.RowsDefaultCellStyle.BackColor = Color.White;

                    totalRowHeight = respGridView.ColumnHeadersHeight;
                    respGridView.RowsDefaultCellStyle.BackColor = Color.White;
                    string[] trozos = mensaje.Split('*');
                    respGridView.RowCount = trozos.Length;
                    int i = 0;
                    while (i < trozos.Length)
                    {
                        string[] info = trozos[i].Split(',');
                        respGridView.Rows[i].Cells[0].Value = info[0];
                        respGridView.Rows[i].Cells[1].Value = info[1] + " s";
                        totalRowHeight += respGridView.Rows[i].Height;
                        i++;
                    }
                    respGridView.Height = totalRowHeight;
                    respGridView.Height = respGridView.Height + 5;

                }
            }
                
        }
        public void SetPregunta (string text)
        {
            askLbl.Text = text;
        }

        private void FormConsultas_Load(object sender, EventArgs e)
        {

        }
    }
}
