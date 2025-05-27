namespace Trivial
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.accederBox = new System.Windows.Forms.GroupBox();
            this.candadoBox = new System.Windows.Forms.PictureBox();
            this.Login = new System.Windows.Forms.Button();
            this.conexion = new System.Windows.Forms.Button();
            this.registroBox = new System.Windows.Forms.GroupBox();
            this.inicio = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.password2Box = new System.Windows.Forms.TextBox();
            this.mailBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.userBox = new System.Windows.Forms.TextBox();
            this.Registrarme = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.invitarButton = new System.Windows.Forms.Button();
            this.consultaBox = new System.Windows.Forms.GroupBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.jugMaxBtn = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.nombresBox = new System.Windows.Forms.TextBox();
            this.pregBtn = new System.Windows.Forms.Button();
            this.fechaBtn = new System.Windows.Forms.RadioButton();
            this.companyia = new System.Windows.Forms.RadioButton();
            this.ConQuienLbl = new System.Windows.Forms.RadioButton();
            this.consultasButton = new System.Windows.Forms.Button();
            this.listaconGridView = new System.Windows.Forms.DataGridView();
            this.labelConectados = new System.Windows.Forms.Label();
            this.invitadosGridView = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.regVisible = new System.Windows.Forms.Button();
            this.regLabel = new System.Windows.Forms.Label();
            this.eliminarLbl = new System.Windows.Forms.Label();
            this.eliminarCuenta = new System.Windows.Forms.Button();
            this.eliminarBox = new System.Windows.Forms.GroupBox();
            this.candadoEliminado = new System.Windows.Forms.PictureBox();
            this.volverLbl = new System.Windows.Forms.Label();
            this.contrasenyaEliminado = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.usuarioEliminado = new System.Windows.Forms.TextBox();
            this.eliminarBtn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.inv_lbl = new System.Windows.Forms.Label();
            this.accederBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.candadoBox)).BeginInit();
            this.registroBox.SuspendLayout();
            this.consultaBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listaconGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invitadosGridView)).BeginInit();
            this.eliminarBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.candadoEliminado)).BeginInit();
            this.SuspendLayout();
            // 
            // PasswordBox
            // 
            this.PasswordBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordBox.Location = new System.Drawing.Point(28, 153);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.Size = new System.Drawing.Size(128, 27);
            this.PasswordBox.TabIndex = 2;
            this.PasswordBox.TextChanged += new System.EventHandler(this.PasswordBox_TextChanged);
            // 
            // NameBox
            // 
            this.NameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameBox.Location = new System.Drawing.Point(28, 59);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(128, 27);
            this.NameBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(24, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(24, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Contraseña";
            // 
            // accederBox
            // 
            this.accederBox.BackColor = System.Drawing.Color.Transparent;
            this.accederBox.Controls.Add(this.candadoBox);
            this.accederBox.Controls.Add(this.PasswordBox);
            this.accederBox.Controls.Add(this.label2);
            this.accederBox.Controls.Add(this.Login);
            this.accederBox.Controls.Add(this.label1);
            this.accederBox.Controls.Add(this.NameBox);
            this.accederBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.accederBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accederBox.ForeColor = System.Drawing.Color.White;
            this.accederBox.Location = new System.Drawing.Point(1036, 83);
            this.accederBox.Name = "accederBox";
            this.accederBox.Size = new System.Drawing.Size(316, 310);
            this.accederBox.TabIndex = 6;
            this.accederBox.TabStop = false;
            this.accederBox.Text = " ";
            // 
            // candadoBox
            // 
            this.candadoBox.BackColor = System.Drawing.Color.White;
            this.candadoBox.Location = new System.Drawing.Point(177, 139);
            this.candadoBox.Name = "candadoBox";
            this.candadoBox.Size = new System.Drawing.Size(44, 43);
            this.candadoBox.TabIndex = 6;
            this.candadoBox.TabStop = false;
            this.candadoBox.Click += new System.EventHandler(this.candadoBox_Click);
            // 
            // Login
            // 
            this.Login.BackColor = System.Drawing.Color.DarkGreen;
            this.Login.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login.ForeColor = System.Drawing.Color.White;
            this.Login.Location = new System.Drawing.Point(82, 233);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(164, 65);
            this.Login.TabIndex = 0;
            this.Login.Text = "Iniciar sesión";
            this.Login.UseVisualStyleBackColor = false;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // conexion
            // 
            this.conexion.AutoSize = true;
            this.conexion.BackColor = System.Drawing.Color.DarkGreen;
            this.conexion.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conexion.ForeColor = System.Drawing.Color.White;
            this.conexion.Location = new System.Drawing.Point(227, 61);
            this.conexion.Name = "conexion";
            this.conexion.Size = new System.Drawing.Size(195, 45);
            this.conexion.TabIndex = 8;
            this.conexion.Text = "Conectar";
            this.conexion.UseVisualStyleBackColor = false;
            this.conexion.Click += new System.EventHandler(this.conexion_Click);
            // 
            // registroBox
            // 
            this.registroBox.BackColor = System.Drawing.Color.Transparent;
            this.registroBox.Controls.Add(this.inicio);
            this.registroBox.Controls.Add(this.label3);
            this.registroBox.Controls.Add(this.password2Box);
            this.registroBox.Controls.Add(this.mailBox);
            this.registroBox.Controls.Add(this.label4);
            this.registroBox.Controls.Add(this.userBox);
            this.registroBox.Controls.Add(this.Registrarme);
            this.registroBox.Controls.Add(this.label5);
            this.registroBox.ForeColor = System.Drawing.Color.White;
            this.registroBox.Location = new System.Drawing.Point(1020, 12);
            this.registroBox.Name = "registroBox";
            this.registroBox.Size = new System.Drawing.Size(347, 332);
            this.registroBox.TabIndex = 10;
            this.registroBox.TabStop = false;
            // 
            // inicio
            // 
            this.inicio.AutoSize = true;
            this.inicio.BackColor = System.Drawing.Color.Transparent;
            this.inicio.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inicio.ForeColor = System.Drawing.Color.White;
            this.inicio.Location = new System.Drawing.Point(230, 13);
            this.inicio.Name = "inicio";
            this.inicio.Size = new System.Drawing.Size(57, 23);
            this.inicio.TabIndex = 25;
            this.inicio.Text = "Atrás";
            this.inicio.Click += new System.EventHandler(this.inicio_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(36, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 23);
            this.label3.TabIndex = 17;
            this.label3.Text = "Correo";
            // 
            // password2Box
            // 
            this.password2Box.Location = new System.Drawing.Point(40, 122);
            this.password2Box.Name = "password2Box";
            this.password2Box.Size = new System.Drawing.Size(159, 22);
            this.password2Box.TabIndex = 12;
            // 
            // mailBox
            // 
            this.mailBox.Location = new System.Drawing.Point(40, 184);
            this.mailBox.Name = "mailBox";
            this.mailBox.Size = new System.Drawing.Size(159, 22);
            this.mailBox.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(36, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 23);
            this.label4.TabIndex = 16;
            this.label4.Text = "Contraseña";
            // 
            // userBox
            // 
            this.userBox.Location = new System.Drawing.Point(40, 63);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(159, 22);
            this.userBox.TabIndex = 13;
            // 
            // Registrarme
            // 
            this.Registrarme.BackColor = System.Drawing.Color.DarkGreen;
            this.Registrarme.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Registrarme.ForeColor = System.Drawing.Color.White;
            this.Registrarme.Location = new System.Drawing.Point(62, 241);
            this.Registrarme.Name = "Registrarme";
            this.Registrarme.Size = new System.Drawing.Size(195, 55);
            this.Registrarme.TabIndex = 14;
            this.Registrarme.Text = "Registrarse";
            this.Registrarme.UseVisualStyleBackColor = false;
            this.Registrarme.Click += new System.EventHandler(this.Registrarme_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(36, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 23);
            this.label5.TabIndex = 15;
            this.label5.Text = "Usuario";
            // 
            // invitarButton
            // 
            this.invitarButton.BackColor = System.Drawing.Color.DarkGreen;
            this.invitarButton.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invitarButton.ForeColor = System.Drawing.Color.White;
            this.invitarButton.Location = new System.Drawing.Point(810, 314);
            this.invitarButton.Name = "invitarButton";
            this.invitarButton.Size = new System.Drawing.Size(149, 43);
            this.invitarButton.TabIndex = 19;
            this.invitarButton.Text = "Invitar";
            this.invitarButton.UseVisualStyleBackColor = false;
            this.invitarButton.Click += new System.EventHandler(this.invitarButton_Click);
            // 
            // consultaBox
            // 
            this.consultaBox.BackColor = System.Drawing.Color.Transparent;
            this.consultaBox.Controls.Add(this.dateTimePicker);
            this.consultaBox.Controls.Add(this.jugMaxBtn);
            this.consultaBox.Controls.Add(this.label7);
            this.consultaBox.Controls.Add(this.nombresBox);
            this.consultaBox.Controls.Add(this.pregBtn);
            this.consultaBox.Controls.Add(this.fechaBtn);
            this.consultaBox.Controls.Add(this.companyia);
            this.consultaBox.Controls.Add(this.ConQuienLbl);
            this.consultaBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consultaBox.ForeColor = System.Drawing.Color.White;
            this.consultaBox.Location = new System.Drawing.Point(38, 331);
            this.consultaBox.Name = "consultaBox";
            this.consultaBox.Size = new System.Drawing.Size(726, 317);
            this.consultaBox.TabIndex = 11;
            this.consultaBox.TabStop = false;
            this.consultaBox.Text = "Consultas";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker.Location = new System.Drawing.Point(52, 215);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(383, 30);
            this.dateTimePicker.TabIndex = 28;
            // 
            // jugMaxBtn
            // 
            this.jugMaxBtn.AutoSize = true;
            this.jugMaxBtn.BackColor = System.Drawing.Color.Transparent;
            this.jugMaxBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jugMaxBtn.ForeColor = System.Drawing.Color.White;
            this.jugMaxBtn.Location = new System.Drawing.Point(31, 47);
            this.jugMaxBtn.Margin = new System.Windows.Forms.Padding(4);
            this.jugMaxBtn.Name = "jugMaxBtn";
            this.jugMaxBtn.Size = new System.Drawing.Size(266, 27);
            this.jugMaxBtn.TabIndex = 27;
            this.jugMaxBtn.TabStop = true;
            this.jugMaxBtn.Text = "JMV (Jugador Más Valioso)";
            this.jugMaxBtn.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(281, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(218, 23);
            this.label7.TabIndex = 19;
            this.label7.Text = "Separa los nombres con /";
            // 
            // nombresBox
            // 
            this.nombresBox.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombresBox.Location = new System.Drawing.Point(52, 131);
            this.nombresBox.Name = "nombresBox";
            this.nombresBox.Size = new System.Drawing.Size(223, 28);
            this.nombresBox.TabIndex = 18;
            // 
            // pregBtn
            // 
            this.pregBtn.BackColor = System.Drawing.Color.DarkGreen;
            this.pregBtn.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pregBtn.ForeColor = System.Drawing.Color.White;
            this.pregBtn.Location = new System.Drawing.Point(530, 252);
            this.pregBtn.Name = "pregBtn";
            this.pregBtn.Size = new System.Drawing.Size(174, 47);
            this.pregBtn.TabIndex = 17;
            this.pregBtn.Text = "Preguntar";
            this.pregBtn.UseVisualStyleBackColor = false;
            this.pregBtn.Click += new System.EventHandler(this.pregBtn_Click);
            // 
            // fechaBtn
            // 
            this.fechaBtn.AutoSize = true;
            this.fechaBtn.BackColor = System.Drawing.Color.Transparent;
            this.fechaBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fechaBtn.ForeColor = System.Drawing.Color.White;
            this.fechaBtn.Location = new System.Drawing.Point(31, 185);
            this.fechaBtn.Margin = new System.Windows.Forms.Padding(4);
            this.fechaBtn.Name = "fechaBtn";
            this.fechaBtn.Size = new System.Drawing.Size(252, 27);
            this.fechaBtn.TabIndex = 16;
            this.fechaBtn.TabStop = true;
            this.fechaBtn.Text = "Partidas jugadas en un día";
            this.fechaBtn.UseVisualStyleBackColor = false;
            // 
            // companyia
            // 
            this.companyia.AutoSize = true;
            this.companyia.BackColor = System.Drawing.Color.Transparent;
            this.companyia.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.companyia.ForeColor = System.Drawing.Color.White;
            this.companyia.Location = new System.Drawing.Point(31, 95);
            this.companyia.Margin = new System.Windows.Forms.Padding(4);
            this.companyia.Name = "companyia";
            this.companyia.Size = new System.Drawing.Size(317, 27);
            this.companyia.TabIndex = 15;
            this.companyia.TabStop = true;
            this.companyia.Text = "Partidas jugados con X jugador/es";
            this.companyia.UseVisualStyleBackColor = false;
            // 
            // ConQuienLbl
            // 
            this.ConQuienLbl.AutoSize = true;
            this.ConQuienLbl.BackColor = System.Drawing.Color.Transparent;
            this.ConQuienLbl.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConQuienLbl.ForeColor = System.Drawing.Color.White;
            this.ConQuienLbl.Location = new System.Drawing.Point(31, 265);
            this.ConQuienLbl.Margin = new System.Windows.Forms.Padding(4);
            this.ConQuienLbl.Name = "ConQuienLbl";
            this.ConQuienLbl.Size = new System.Drawing.Size(231, 27);
            this.ConQuienLbl.TabIndex = 14;
            this.ConQuienLbl.TabStop = true;
            this.ConQuienLbl.Text = "Contrincantes recientes";
            this.ConQuienLbl.UseVisualStyleBackColor = false;
            // 
            // consultasButton
            // 
            this.consultasButton.AutoSize = true;
            this.consultasButton.BackColor = System.Drawing.Color.DarkGreen;
            this.consultasButton.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consultasButton.ForeColor = System.Drawing.Color.White;
            this.consultasButton.Location = new System.Drawing.Point(227, 112);
            this.consultasButton.Name = "consultasButton";
            this.consultasButton.Size = new System.Drawing.Size(195, 45);
            this.consultasButton.TabIndex = 13;
            this.consultasButton.Text = "Consultar";
            this.consultasButton.UseVisualStyleBackColor = false;
            this.consultasButton.Click += new System.EventHandler(this.consultasButton_Click);
            // 
            // listaconGridView
            // 
            this.listaconGridView.AllowUserToAddRows = false;
            this.listaconGridView.AllowUserToDeleteRows = false;
            this.listaconGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.listaconGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.listaconGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.listaconGridView.GridColor = System.Drawing.SystemColors.Control;
            this.listaconGridView.Location = new System.Drawing.Point(464, 75);
            this.listaconGridView.Name = "listaconGridView";
            this.listaconGridView.RowHeadersWidth = 51;
            this.listaconGridView.RowTemplate.Height = 24;
            this.listaconGridView.Size = new System.Drawing.Size(230, 233);
            this.listaconGridView.TabIndex = 16;
            this.listaconGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ConectadosGridView_CellClick);
            // 
            // labelConectados
            // 
            this.labelConectados.AutoSize = true;
            this.labelConectados.BackColor = System.Drawing.Color.Transparent;
            this.labelConectados.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConectados.Location = new System.Drawing.Point(460, 53);
            this.labelConectados.Name = "labelConectados";
            this.labelConectados.Size = new System.Drawing.Size(167, 20);
            this.labelConectados.TabIndex = 17;
            this.labelConectados.Text = "Lista de Conectados";
            // 
            // invitadosGridView
            // 
            this.invitadosGridView.AllowUserToAddRows = false;
            this.invitadosGridView.AllowUserToDeleteRows = false;
            this.invitadosGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.invitadosGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.invitadosGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.invitadosGridView.GridColor = System.Drawing.SystemColors.Control;
            this.invitadosGridView.Location = new System.Drawing.Point(729, 75);
            this.invitadosGridView.Name = "invitadosGridView";
            this.invitadosGridView.RowHeadersWidth = 51;
            this.invitadosGridView.RowTemplate.Height = 24;
            this.invitadosGridView.Size = new System.Drawing.Size(230, 233);
            this.invitadosGridView.TabIndex = 21;
            this.invitadosGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.invitadosGridView_CellClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(725, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 20);
            this.label6.TabIndex = 22;
            this.label6.Text = "Lista de Invitados";
            // 
            // regVisible
            // 
            this.regVisible.AutoSize = true;
            this.regVisible.BackColor = System.Drawing.Color.DarkGreen;
            this.regVisible.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regVisible.ForeColor = System.Drawing.Color.White;
            this.regVisible.Location = new System.Drawing.Point(38, 59);
            this.regVisible.Name = "regVisible";
            this.regVisible.Size = new System.Drawing.Size(183, 45);
            this.regVisible.TabIndex = 23;
            this.regVisible.Text = "Regístrate";
            this.regVisible.UseVisualStyleBackColor = false;
            this.regVisible.Click += new System.EventHandler(this.regVisible_Click);
            // 
            // regLabel
            // 
            this.regLabel.AutoSize = true;
            this.regLabel.BackColor = System.Drawing.Color.Transparent;
            this.regLabel.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regLabel.ForeColor = System.Drawing.Color.White;
            this.regLabel.Location = new System.Drawing.Point(33, 31);
            this.regLabel.Name = "regLabel";
            this.regLabel.Size = new System.Drawing.Size(228, 25);
            this.regLabel.TabIndex = 24;
            this.regLabel.Text = "Si no tienes cuenta aún";
            // 
            // eliminarLbl
            // 
            this.eliminarLbl.AutoSize = true;
            this.eliminarLbl.BackColor = System.Drawing.Color.Transparent;
            this.eliminarLbl.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eliminarLbl.ForeColor = System.Drawing.Color.White;
            this.eliminarLbl.Location = new System.Drawing.Point(33, 130);
            this.eliminarLbl.Name = "eliminarLbl";
            this.eliminarLbl.Size = new System.Drawing.Size(142, 25);
            this.eliminarLbl.TabIndex = 25;
            this.eliminarLbl.Text = "Darse de baja";
            // 
            // eliminarCuenta
            // 
            this.eliminarCuenta.AutoSize = true;
            this.eliminarCuenta.BackColor = System.Drawing.Color.DarkGreen;
            this.eliminarCuenta.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eliminarCuenta.ForeColor = System.Drawing.Color.White;
            this.eliminarCuenta.Location = new System.Drawing.Point(38, 160);
            this.eliminarCuenta.Name = "eliminarCuenta";
            this.eliminarCuenta.Size = new System.Drawing.Size(218, 45);
            this.eliminarCuenta.TabIndex = 26;
            this.eliminarCuenta.Text = "Eliminar Cuenta";
            this.eliminarCuenta.UseVisualStyleBackColor = false;
            this.eliminarCuenta.Click += new System.EventHandler(this.eliminarCuenta_Click);
            // 
            // eliminarBox
            // 
            this.eliminarBox.BackColor = System.Drawing.Color.Transparent;
            this.eliminarBox.Controls.Add(this.candadoEliminado);
            this.eliminarBox.Controls.Add(this.volverLbl);
            this.eliminarBox.Controls.Add(this.contrasenyaEliminado);
            this.eliminarBox.Controls.Add(this.label9);
            this.eliminarBox.Controls.Add(this.usuarioEliminado);
            this.eliminarBox.Controls.Add(this.eliminarBtn);
            this.eliminarBox.Controls.Add(this.label10);
            this.eliminarBox.ForeColor = System.Drawing.Color.White;
            this.eliminarBox.Location = new System.Drawing.Point(1146, 410);
            this.eliminarBox.Name = "eliminarBox";
            this.eliminarBox.Size = new System.Drawing.Size(314, 273);
            this.eliminarBox.TabIndex = 27;
            this.eliminarBox.TabStop = false;
            // 
            // candadoEliminado
            // 
            this.candadoEliminado.BackColor = System.Drawing.Color.White;
            this.candadoEliminado.Location = new System.Drawing.Point(191, 122);
            this.candadoEliminado.Name = "candadoEliminado";
            this.candadoEliminado.Size = new System.Drawing.Size(44, 43);
            this.candadoEliminado.TabIndex = 26;
            this.candadoEliminado.TabStop = false;
            this.candadoEliminado.Click += new System.EventHandler(this.candadoEliminado_Click);
            // 
            // volverLbl
            // 
            this.volverLbl.AutoSize = true;
            this.volverLbl.BackColor = System.Drawing.Color.Transparent;
            this.volverLbl.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.volverLbl.ForeColor = System.Drawing.Color.White;
            this.volverLbl.Location = new System.Drawing.Point(240, 18);
            this.volverLbl.Name = "volverLbl";
            this.volverLbl.Size = new System.Drawing.Size(57, 23);
            this.volverLbl.TabIndex = 25;
            this.volverLbl.Text = "Atrás";
            this.volverLbl.Click += new System.EventHandler(this.volverLbl_Click);
            // 
            // contrasenyaEliminado
            // 
            this.contrasenyaEliminado.Location = new System.Drawing.Point(40, 130);
            this.contrasenyaEliminado.Name = "contrasenyaEliminado";
            this.contrasenyaEliminado.PasswordChar = '●';
            this.contrasenyaEliminado.Size = new System.Drawing.Size(128, 22);
            this.contrasenyaEliminado.TabIndex = 12;
            this.contrasenyaEliminado.UseSystemPasswordChar = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(36, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 23);
            this.label9.TabIndex = 16;
            this.label9.Text = "Contraseña";
            // 
            // usuarioEliminado
            // 
            this.usuarioEliminado.Location = new System.Drawing.Point(40, 66);
            this.usuarioEliminado.Name = "usuarioEliminado";
            this.usuarioEliminado.Size = new System.Drawing.Size(128, 22);
            this.usuarioEliminado.TabIndex = 13;
            // 
            // eliminarBtn
            // 
            this.eliminarBtn.BackColor = System.Drawing.Color.DarkGreen;
            this.eliminarBtn.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eliminarBtn.ForeColor = System.Drawing.Color.White;
            this.eliminarBtn.Location = new System.Drawing.Point(40, 188);
            this.eliminarBtn.Name = "eliminarBtn";
            this.eliminarBtn.Size = new System.Drawing.Size(195, 60);
            this.eliminarBtn.TabIndex = 14;
            this.eliminarBtn.Text = "Darse de baja";
            this.eliminarBtn.UseVisualStyleBackColor = false;
            this.eliminarBtn.Click += new System.EventHandler(this.eliminarBtn_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(36, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 23);
            this.label10.TabIndex = 15;
            this.label10.Text = "Usuario";
            // 
            // inv_lbl
            // 
            this.inv_lbl.AutoSize = true;
            this.inv_lbl.BackColor = System.Drawing.Color.Transparent;
            this.inv_lbl.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inv_lbl.ForeColor = System.Drawing.Color.White;
            this.inv_lbl.Location = new System.Drawing.Point(460, 12);
            this.inv_lbl.Name = "inv_lbl";
            this.inv_lbl.Size = new System.Drawing.Size(281, 23);
            this.inv_lbl.TabIndex = 41;
            this.inv_lbl.Text = "Pulsa sobre quién quieras invitar";
            // 
            // Acceso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1472, 727);
            this.Controls.Add(this.inv_lbl);
            this.Controls.Add(this.eliminarBox);
            this.Controls.Add(this.eliminarCuenta);
            this.Controls.Add(this.registroBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.accederBox);
            this.Controls.Add(this.eliminarLbl);
            this.Controls.Add(this.invitadosGridView);
            this.Controls.Add(this.invitarButton);
            this.Controls.Add(this.regVisible);
            this.Controls.Add(this.labelConectados);
            this.Controls.Add(this.regLabel);
            this.Controls.Add(this.consultasButton);
            this.Controls.Add(this.conexion);
            this.Controls.Add(this.listaconGridView);
            this.Controls.Add(this.consultaBox);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Acceso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acceder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Acceso_FormClosing);
            this.Load += new System.EventHandler(this.Acceso_Load);
            this.accederBox.ResumeLayout(false);
            this.accederBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.candadoBox)).EndInit();
            this.registroBox.ResumeLayout(false);
            this.registroBox.PerformLayout();
            this.consultaBox.ResumeLayout(false);
            this.consultaBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listaconGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invitadosGridView)).EndInit();
            this.eliminarBox.ResumeLayout(false);
            this.eliminarBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.candadoEliminado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox accederBox;
        private System.Windows.Forms.Button conexion;
        private System.Windows.Forms.GroupBox registroBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox password2Box;
        private System.Windows.Forms.TextBox mailBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox userBox;
        private System.Windows.Forms.Button Registrarme;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox consultaBox;
        private System.Windows.Forms.Button pregBtn;
        private System.Windows.Forms.RadioButton fechaBtn;
        private System.Windows.Forms.RadioButton companyia;
        private System.Windows.Forms.RadioButton ConQuienLbl;
        private System.Windows.Forms.PictureBox candadoBox;
        private System.Windows.Forms.Button consultasButton;
        private System.Windows.Forms.DataGridView listaconGridView;
        private System.Windows.Forms.Label labelConectados;
        private System.Windows.Forms.Button invitarButton;
        private System.Windows.Forms.DataGridView invitadosGridView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Button regVisible;
        private System.Windows.Forms.Label regLabel;
        private System.Windows.Forms.Label inicio;
        private System.Windows.Forms.Label eliminarLbl;
        private System.Windows.Forms.Button eliminarCuenta;
        private System.Windows.Forms.GroupBox eliminarBox;
        private System.Windows.Forms.Label volverLbl;
        private System.Windows.Forms.TextBox contrasenyaEliminado;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox usuarioEliminado;
        private System.Windows.Forms.Button eliminarBtn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox candadoEliminado;
        private System.Windows.Forms.Label inv_lbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox nombresBox;
        private System.Windows.Forms.RadioButton jugMaxBtn;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
    }
}

