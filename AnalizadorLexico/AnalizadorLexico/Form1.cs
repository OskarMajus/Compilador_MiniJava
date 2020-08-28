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
using System.Text.RegularExpressions;


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

        int cantidadLineas = 0;

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            OpenFileDialog ventana = new OpenFileDialog();
            string nombreArchivo = "";
            string rutaArchivo = "";
            string linea = "";


            if (ventana.ShowDialog()== DialogResult.OK)
            {
                rutaArchivo = ventana.FileName;
                nombreArchivo = ventana.SafeFileName;
            }                   
            
            lnombreArchivo.Text = nombreArchivo;
            lrutaArchivo.Text = rutaArchivo;
            StreamReader lectorArchivo = new StreamReader(rutaArchivo);
            StringBuilder builder = new StringBuilder();

          

            //linea = lectorArchivo.ReadLine();

            while ((linea = lectorArchivo.ReadLine()) != null)//while (lectorArchivo.Peek()>-1) 
            {
                //linea = lectorArchivo.ReadLine();
                builder.AppendLine(linea);
               
                cantidadLineas++;
            }                    

            lectorArchivo.Close();
            rchtbxArchivo.Text = builder.ToString();

            MessageBox.Show("El archivo tiene: " + cantidadLineas + " lineas");




            Cursor.Current = Cursors.Default;
          

        }

        private void btnEscanear_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Scanner Scan = new Scanner(rchtbxArchivo.Text);

            Scan.Lector(cantidadLineas);
            

            foreach (string error in Scan.errores)
            {
                lstbxErrores.Items.Add(error);
            }

            

            Cursor.Current = Cursors.Default;
        }
    }
}
