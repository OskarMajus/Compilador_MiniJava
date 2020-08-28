namespace AnalizadorLexico
{
    partial class Form1
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
            this.btnBuscar = new System.Windows.Forms.Button();
            this.rchtbxArchivo = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstbxErrores = new System.Windows.Forms.ListBox();
            this.btnEscanear = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lnombreArchivo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lrutaArchivo = new System.Windows.Forms.Label();
            this.dGV_Token_Lexema = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_Token_Lexema)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(35, 69);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(408, 39);
            this.btnBuscar.TabIndex = 0;
            this.btnBuscar.Text = "Cargar Archivo";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // rchtbxArchivo
            // 
            this.rchtbxArchivo.Location = new System.Drawing.Point(35, 270);
            this.rchtbxArchivo.Name = "rchtbxArchivo";
            this.rchtbxArchivo.Size = new System.Drawing.Size(408, 201);
            this.rchtbxArchivo.TabIndex = 1;
            this.rchtbxArchivo.Text = "";
            this.rchtbxArchivo.TextChanged += new System.EventHandler(this.rchtbxArchivo_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Archivo a examinar:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(32, 483);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Errores";
            // 
            // lstbxErrores
            // 
            this.lstbxErrores.FormattingEnabled = true;
            this.lstbxErrores.Location = new System.Drawing.Point(35, 515);
            this.lstbxErrores.Name = "lstbxErrores";
            this.lstbxErrores.Size = new System.Drawing.Size(408, 82);
            this.lstbxErrores.TabIndex = 9;
            // 
            // btnEscanear
            // 
            this.btnEscanear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEscanear.Location = new System.Drawing.Point(35, 114);
            this.btnEscanear.Name = "btnEscanear";
            this.btnEscanear.Size = new System.Drawing.Size(408, 39);
            this.btnEscanear.TabIndex = 10;
            this.btnEscanear.Text = "Escanear";
            this.btnEscanear.UseVisualStyleBackColor = true;
            this.btnEscanear.Click += new System.EventHandler(this.btnEscanear_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lnombreArchivo
            // 
            this.lnombreArchivo.AutoSize = true;
            this.lnombreArchivo.Location = new System.Drawing.Point(158, 180);
            this.lnombreArchivo.Name = "lnombreArchivo";
            this.lnombreArchivo.Size = new System.Drawing.Size(16, 13);
            this.lnombreArchivo.TabIndex = 12;
            this.lnombreArchivo.Text = "---";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Ruta Archivo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Nombre Archivo:";
            // 
            // lrutaArchivo
            // 
            this.lrutaArchivo.AutoSize = true;
            this.lrutaArchivo.Location = new System.Drawing.Point(158, 214);
            this.lrutaArchivo.Name = "lrutaArchivo";
            this.lrutaArchivo.Size = new System.Drawing.Size(16, 13);
            this.lrutaArchivo.TabIndex = 15;
            this.lrutaArchivo.Text = "---";
            // 
            // dGV_Token_Lexema
            // 
            this.dGV_Token_Lexema.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_Token_Lexema.Location = new System.Drawing.Point(514, 270);
            this.dGV_Token_Lexema.Name = "dGV_Token_Lexema";
            this.dGV_Token_Lexema.Size = new System.Drawing.Size(445, 327);
            this.dGV_Token_Lexema.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1001, 52);
            this.panel1.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(445, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Analizador Léxico";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1001, 609);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dGV_Token_Lexema);
            this.Controls.Add(this.lrutaArchivo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lnombreArchivo);
            this.Controls.Add(this.btnEscanear);
            this.Controls.Add(this.lstbxErrores);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rchtbxArchivo);
            this.Controls.Add(this.btnBuscar);
            this.Name = "Form1";
            this.Text = "Analizador Léxico";
            ((System.ComponentModel.ISupportInitialize)(this.dGV_Token_Lexema)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.RichTextBox rchtbxArchivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstbxErrores;
        private System.Windows.Forms.Button btnEscanear;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lnombreArchivo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lrutaArchivo;
        private System.Windows.Forms.DataGridView dGV_Token_Lexema;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
    }
}

