﻿
namespace Trivial
{
    partial class FormJuego
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.playersGridView = new System.Windows.Forms.DataGridView();
            this.dado = new System.Windows.Forms.PictureBox();
            this.dadolbl = new System.Windows.Forms.Label();
            this.username_lbl = new System.Windows.Forms.Label();
            this.partida_lbl = new System.Windows.Forms.Label();
            this.ChatTxt = new System.Windows.Forms.TextBox();
            this.ChatBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ChatBox = new System.Windows.Forms.RichTextBox();
            this.tableroBox = new System.Windows.Forms.PictureBox();
            this.ubi1Box = new System.Windows.Forms.PictureBox();
            this.ubi2Box = new System.Windows.Forms.PictureBox();
            this.ubi3Box = new System.Windows.Forms.PictureBox();
            this.ubi4Box = new System.Windows.Forms.PictureBox();
            this.ubi5Box = new System.Windows.Forms.PictureBox();
            this.ubi6Box = new System.Windows.Forms.PictureBox();
            this.ubi7Box = new System.Windows.Forms.PictureBox();
            this.hostBox = new System.Windows.Forms.PictureBox();
            this.jug2Box = new System.Windows.Forms.PictureBox();
            this.jug3Box = new System.Windows.Forms.PictureBox();
            this.jug4Box = new System.Windows.Forms.PictureBox();
            this.notLbl = new System.Windows.Forms.Label();
            this.notificacionLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.playersGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableroBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi1Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi2Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi3Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi4Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi5Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi6Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi7Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hostBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jug2Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jug3Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jug4Box)).BeginInit();
            this.SuspendLayout();
            // 
            // playersGridView
            // 
            this.playersGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.playersGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.playersGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.playersGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.playersGridView.Location = new System.Drawing.Point(45, 148);
            this.playersGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.playersGridView.Name = "playersGridView";
            this.playersGridView.ReadOnly = true;
            this.playersGridView.RowHeadersWidth = 51;
            this.playersGridView.RowTemplate.Height = 24;
            this.playersGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.playersGridView.Size = new System.Drawing.Size(223, 188);
            this.playersGridView.TabIndex = 0;
            // 
            // dado
            // 
            this.dado.Location = new System.Drawing.Point(1349, 15);
            this.dado.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dado.Name = "dado";
            this.dado.Size = new System.Drawing.Size(114, 121);
            this.dado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dado.TabIndex = 13;
            this.dado.TabStop = false;
            this.dado.Click += new System.EventHandler(this.dado_Click_1);
            // 
            // dadolbl
            // 
            this.dadolbl.AutoSize = true;
            this.dadolbl.BackColor = System.Drawing.Color.Transparent;
            this.dadolbl.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dadolbl.ForeColor = System.Drawing.Color.White;
            this.dadolbl.Location = new System.Drawing.Point(1344, 140);
            this.dadolbl.MaximumSize = new System.Drawing.Size(135, 0);
            this.dadolbl.Name = "dadolbl";
            this.dadolbl.Size = new System.Drawing.Size(128, 74);
            this.dadolbl.TabIndex = 15;
            this.dadolbl.Text = "Roll the dice!";
            this.dadolbl.Click += new System.EventHandler(this.dadolbl_Click);
            // 
            // username_lbl
            // 
            this.username_lbl.AutoSize = true;
            this.username_lbl.BackColor = System.Drawing.Color.Transparent;
            this.username_lbl.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_lbl.ForeColor = System.Drawing.Color.White;
            this.username_lbl.Location = new System.Drawing.Point(42, 36);
            this.username_lbl.Name = "username_lbl";
            this.username_lbl.Size = new System.Drawing.Size(140, 37);
            this.username_lbl.TabIndex = 16;
            this.username_lbl.Text = "Usuario:";
            // 
            // partida_lbl
            // 
            this.partida_lbl.AutoSize = true;
            this.partida_lbl.BackColor = System.Drawing.Color.Transparent;
            this.partida_lbl.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.partida_lbl.ForeColor = System.Drawing.Color.White;
            this.partida_lbl.Location = new System.Drawing.Point(42, 84);
            this.partida_lbl.Name = "partida_lbl";
            this.partida_lbl.Size = new System.Drawing.Size(133, 37);
            this.partida_lbl.TabIndex = 17;
            this.partida_lbl.Text = "Partida:";
            // 
            // ChatTxt
            // 
            this.ChatTxt.Location = new System.Drawing.Point(1217, 1072);
            this.ChatTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChatTxt.Name = "ChatTxt";
            this.ChatTxt.Size = new System.Drawing.Size(184, 26);
            this.ChatTxt.TabIndex = 19;
            // 
            // ChatBtn
            // 
            this.ChatBtn.BackColor = System.Drawing.Color.White;
            this.ChatBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ChatBtn.Location = new System.Drawing.Point(1408, 1072);
            this.ChatBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChatBtn.Name = "ChatBtn";
            this.ChatBtn.Size = new System.Drawing.Size(79, 31);
            this.ChatBtn.TabIndex = 20;
            this.ChatBtn.Text = "Enviar";
            this.ChatBtn.UseVisualStyleBackColor = false;
            this.ChatBtn.Click += new System.EventHandler(this.ChatBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1213, 838);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 32);
            this.label1.TabIndex = 22;
            this.label1.Text = "CHAT";
            // 
            // ChatBox
            // 
            this.ChatBox.Location = new System.Drawing.Point(1217, 869);
            this.ChatBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.ReadOnly = true;
            this.ChatBox.Size = new System.Drawing.Size(270, 195);
            this.ChatBox.TabIndex = 23;
            this.ChatBox.Text = "";
            // 
            // tableroBox
            // 
            this.tableroBox.BackColor = System.Drawing.Color.Transparent;
            this.tableroBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tableroBox.Location = new System.Drawing.Point(90, 28);
            this.tableroBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableroBox.Name = "tableroBox";
            this.tableroBox.Size = new System.Drawing.Size(1353, 689);
            this.tableroBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tableroBox.TabIndex = 24;
            this.tableroBox.TabStop = false;
            this.tableroBox.Click += new System.EventHandler(this.tableroBox_Click);
            // 
            // ubi1Box
            // 
            this.ubi1Box.Location = new System.Drawing.Point(1310, 148);
            this.ubi1Box.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ubi1Box.Name = "ubi1Box";
            this.ubi1Box.Size = new System.Drawing.Size(45, 50);
            this.ubi1Box.TabIndex = 25;
            this.ubi1Box.TabStop = false;
            // 
            // ubi2Box
            // 
            this.ubi2Box.Location = new System.Drawing.Point(1310, 205);
            this.ubi2Box.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ubi2Box.Name = "ubi2Box";
            this.ubi2Box.Size = new System.Drawing.Size(45, 50);
            this.ubi2Box.TabIndex = 26;
            this.ubi2Box.TabStop = false;
            // 
            // ubi3Box
            // 
            this.ubi3Box.Location = new System.Drawing.Point(1310, 262);
            this.ubi3Box.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ubi3Box.Name = "ubi3Box";
            this.ubi3Box.Size = new System.Drawing.Size(45, 50);
            this.ubi3Box.TabIndex = 27;
            this.ubi3Box.TabStop = false;
            // 
            // ubi4Box
            // 
            this.ubi4Box.Location = new System.Drawing.Point(1310, 320);
            this.ubi4Box.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ubi4Box.Name = "ubi4Box";
            this.ubi4Box.Size = new System.Drawing.Size(45, 50);
            this.ubi4Box.TabIndex = 28;
            this.ubi4Box.TabStop = false;
            // 
            // ubi5Box
            // 
            this.ubi5Box.Location = new System.Drawing.Point(1310, 378);
            this.ubi5Box.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ubi5Box.Name = "ubi5Box";
            this.ubi5Box.Size = new System.Drawing.Size(45, 50);
            this.ubi5Box.TabIndex = 29;
            this.ubi5Box.TabStop = false;
            // 
            // ubi6Box
            // 
            this.ubi6Box.Location = new System.Drawing.Point(1310, 435);
            this.ubi6Box.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ubi6Box.Name = "ubi6Box";
            this.ubi6Box.Size = new System.Drawing.Size(45, 50);
            this.ubi6Box.TabIndex = 30;
            this.ubi6Box.TabStop = false;
            // 
            // ubi7Box
            // 
            this.ubi7Box.Location = new System.Drawing.Point(1310, 492);
            this.ubi7Box.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ubi7Box.Name = "ubi7Box";
            this.ubi7Box.Size = new System.Drawing.Size(45, 50);
            this.ubi7Box.TabIndex = 31;
            this.ubi7Box.TabStop = false;
            // 
            // hostBox
            // 
            this.hostBox.Location = new System.Drawing.Point(1207, 148);
            this.hostBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hostBox.Name = "hostBox";
            this.hostBox.Size = new System.Drawing.Size(45, 50);
            this.hostBox.TabIndex = 32;
            this.hostBox.TabStop = false;
            // 
            // jug2Box
            // 
            this.jug2Box.Location = new System.Drawing.Point(1207, 205);
            this.jug2Box.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.jug2Box.Name = "jug2Box";
            this.jug2Box.Size = new System.Drawing.Size(45, 50);
            this.jug2Box.TabIndex = 33;
            this.jug2Box.TabStop = false;
            this.jug2Box.Click += new System.EventHandler(this.jug2Box_Click);
            // 
            // jug3Box
            // 
            this.jug3Box.Location = new System.Drawing.Point(1207, 262);
            this.jug3Box.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.jug3Box.Name = "jug3Box";
            this.jug3Box.Size = new System.Drawing.Size(45, 50);
            this.jug3Box.TabIndex = 34;
            this.jug3Box.TabStop = false;
            // 
            // jug4Box
            // 
            this.jug4Box.Location = new System.Drawing.Point(1207, 320);
            this.jug4Box.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.jug4Box.Name = "jug4Box";
            this.jug4Box.Size = new System.Drawing.Size(45, 50);
            this.jug4Box.TabIndex = 35;
            this.jug4Box.TabStop = false;
            // 
            // notLbl
            // 
            this.notLbl.AutoSize = true;
            this.notLbl.BackColor = System.Drawing.Color.Transparent;
            this.notLbl.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notLbl.ForeColor = System.Drawing.Color.White;
            this.notLbl.Location = new System.Drawing.Point(51, 888);
            this.notLbl.Name = "notLbl";
            this.notLbl.Size = new System.Drawing.Size(117, 37);
            this.notLbl.TabIndex = 37;
            this.notLbl.Text = "Turnos";
            // 
            // notificacionLbl
            // 
            this.notificacionLbl.AutoSize = true;
            this.notificacionLbl.BackColor = System.Drawing.Color.Transparent;
            this.notificacionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notificacionLbl.ForeColor = System.Drawing.Color.White;
            this.notificacionLbl.Location = new System.Drawing.Point(52, 940);
            this.notificacionLbl.MaximumSize = new System.Drawing.Size(281, 0);
            this.notificacionLbl.Name = "notificacionLbl";
            this.notificacionLbl.Size = new System.Drawing.Size(69, 29);
            this.notificacionLbl.TabIndex = 38;
            this.notificacionLbl.Text = "texto";
            // 
            // FormJuego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1520, 1050);
            this.Controls.Add(this.notificacionLbl);
            this.Controls.Add(this.notLbl);
            this.Controls.Add(this.jug4Box);
            this.Controls.Add(this.jug3Box);
            this.Controls.Add(this.jug2Box);
            this.Controls.Add(this.hostBox);
            this.Controls.Add(this.ubi7Box);
            this.Controls.Add(this.ubi6Box);
            this.Controls.Add(this.ubi5Box);
            this.Controls.Add(this.ubi4Box);
            this.Controls.Add(this.ubi3Box);
            this.Controls.Add(this.ubi2Box);
            this.Controls.Add(this.ubi1Box);
            this.Controls.Add(this.ChatBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChatBtn);
            this.Controls.Add(this.ChatTxt);
            this.Controls.Add(this.partida_lbl);
            this.Controls.Add(this.username_lbl);
            this.Controls.Add(this.dadolbl);
            this.Controls.Add(this.dado);
            this.Controls.Add(this.playersGridView);
            this.Controls.Add(this.tableroBox);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormJuego";
            this.Text = "Tablero";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tablero_FormClosing);
            this.Load += new System.EventHandler(this.Tablero_Load);
            ((System.ComponentModel.ISupportInitialize)(this.playersGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableroBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi1Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi2Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi3Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi4Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi5Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi6Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubi7Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hostBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jug2Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jug3Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jug4Box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView playersGridView;
        private System.Windows.Forms.PictureBox dado;
        private System.Windows.Forms.Label dadolbl;
        private System.Windows.Forms.Label username_lbl;
        private System.Windows.Forms.Label partida_lbl;
        private System.Windows.Forms.TextBox ChatTxt;
        private System.Windows.Forms.Button ChatBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox ChatBox;
        protected System.Windows.Forms.PictureBox tableroBox;
        private System.Windows.Forms.PictureBox ubi1Box;
        private System.Windows.Forms.PictureBox ubi2Box;
        private System.Windows.Forms.PictureBox ubi3Box;
        private System.Windows.Forms.PictureBox ubi4Box;
        private System.Windows.Forms.PictureBox ubi5Box;
        private System.Windows.Forms.PictureBox ubi6Box;
        private System.Windows.Forms.PictureBox ubi7Box;
        private System.Windows.Forms.PictureBox hostBox;
        private System.Windows.Forms.PictureBox jug2Box;
        private System.Windows.Forms.PictureBox jug3Box;
        private System.Windows.Forms.PictureBox jug4Box;
        private System.Windows.Forms.Label notLbl;
        private System.Windows.Forms.Label notificacionLbl;
    }
}