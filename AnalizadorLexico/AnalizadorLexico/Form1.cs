using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AnalizadorLexico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void rchtbxArchivo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ventana = new OpenFileDialog();
            string nombreArchivo = "";
            string rutaArchivo = "";

            if (ventana.ShowDialog()== DialogResult.OK)
            {
                rutaArchivo = ventana.FileName;
                nombreArchivo = ventana.SafeFileName;
            }

            Cursor.Current = Cursors.WaitCursor;
            try 
            {
                lnombreArchivo.Text = nombreArchivo;
                lrutaArchivo.Text = rutaArchivo;
                StreamReader sr = new StreamReader(rutaArchivo);
                rchtbxArchivo.Text = sr.ReadToEnd(); //lee el archivo completo, no linea por linea
                sr.Close();
                
            }
            catch 
            {
            }
            finally 
            {
                Cursor.Current = Cursors.Default;
                btnBuscar.Visible = false;
                btnEscanear.Visible = true;

            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
