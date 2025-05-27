namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.IDJugador = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ConsultaAlvaro = new System.Windows.Forms.RadioButton();
            this.ConsultaVictorias = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.IDPartida = new System.Windows.Forms.TextBox();
            this.ConsultaMarc = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID del jugador";
            // 
            // IDJugador
            // 
            this.IDJugador.Location = new System.Drawing.Point(243, 62);
            this.IDJugador.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.IDJugador.Name = "IDJugador";
            this.IDJugador.Size = new System.Drawing.Size(244, 26);
            this.IDJugador.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(38, 62);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 48);
            this.button1.TabIndex = 4;
            this.button1.Text = "conectar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(189, 242);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 5;
            this.button2.Text = "Enviar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.ConsultaAlvaro);
            this.groupBox1.Controls.Add(this.ConsultaVictorias);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.IDPartida);
            this.groupBox1.Controls.Add(this.ConsultaMarc);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.IDJugador);
            this.groupBox1.Location = new System.Drawing.Point(34, 120);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(814, 351);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peticion";
            // 
            // ConsultaAlvaro
            // 
            this.ConsultaAlvaro.AutoSize = true;
            this.ConsultaAlvaro.Location = new System.Drawing.Point(174, 140);
            this.ConsultaAlvaro.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ConsultaAlvaro.Name = "ConsultaAlvaro";
            this.ConsultaAlvaro.Size = new System.Drawing.Size(406, 24);
            this.ConsultaAlvaro.TabIndex = 7;
            this.ConsultaAlvaro.TabStop = true;
            this.ConsultaAlvaro.Text = "Listado de jugadores que participaron en una partida";
            this.ConsultaAlvaro.UseVisualStyleBackColor = true;
            // 
            // ConsultaVictorias
            // 
            this.ConsultaVictorias.AutoSize = true;
            this.ConsultaVictorias.Location = new System.Drawing.Point(174, 183);
            this.ConsultaVictorias.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ConsultaVictorias.Name = "ConsultaVictorias";
            this.ConsultaVictorias.Size = new System.Drawing.Size(353, 24);
            this.ConsultaVictorias.TabIndex = 7;
            this.ConsultaVictorias.TabStop = true;
            this.ConsultaVictorias.Text = "Consulta la cantidad total de partidas gandas";
            this.ConsultaVictorias.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(530, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "ID de la partida";
            // 
            // IDPartida
            // 
            this.IDPartida.Location = new System.Drawing.Point(664, 62);
            this.IDPartida.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.IDPartida.Name = "IDPartida";
            this.IDPartida.Size = new System.Drawing.Size(91, 26);
            this.IDPartida.TabIndex = 9;
            // 
            // ConsultaMarc
            // 
            this.ConsultaMarc.AutoSize = true;
            this.ConsultaMarc.Location = new System.Drawing.Point(174, 105);
            this.ConsultaMarc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ConsultaMarc.Name = "ConsultaMarc";
            this.ConsultaMarc.Size = new System.Drawing.Size(425, 24);
            this.ConsultaMarc.TabIndex = 8;
            this.ConsultaMarc.TabStop = true;
            this.ConsultaMarc.Text = "Listado de jugadores con los que he jugado una partida";
            this.ConsultaMarc.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(334, 59);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(206, 51);
            this.button3.TabIndex = 10;
            this.button3.Text = "desconectar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 865);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IDJugador;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton ConsultaAlvaro;
        private System.Windows.Forms.RadioButton ConsultaMarc;
        private System.Windows.Forms.RadioButton ConsultaVictorias;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IDPartida;
        private System.Windows.Forms.Button button3;
    }
}

