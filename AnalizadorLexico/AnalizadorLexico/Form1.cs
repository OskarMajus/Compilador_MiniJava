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
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                OpenFileDialog ventana = new OpenFileDialog();
                string nombreArchivo = "";
                string rutaArchivo = "";
                string linea = "";

                if (ventana.ShowDialog() == DialogResult.OK)
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

                //MessageBox.Show("El archivo tiene: " + cantidadLineas + " lineas");

                btnEscanear.Visible = true;
                lstbxErrores.Items.Clear();
                
                Cursor.Current = Cursors.Default;

            }
            catch (Exception)
            {

                
            }
        }

        private void btnEscanear_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Scanner Scan = new Scanner(rchtbxArchivo.Text);

            Scan.Lector(cantidadLineas);
            
            //Recorre la cola de errores y los despliega
            foreach (string error in Scan.GetErrors)
            {
                lstbxErrores.Items.Add(error);
            }

            //Crea un DataTable para poder luego desplegar los Tokens
            DataTable dtTokens = new DataTable("Tokens");
            //Crea las columnas de la tabla
            DataColumn dcTokens = new DataColumn("Tokens");
            DataColumn dcLexema = new DataColumn("Lexemas");
            DataColumn dcFila = new DataColumn("Fila");
            DataColumn dcColumna = new DataColumn("Columna");
            //Agrega las columnas a la tabla
            dtTokens.Columns.Add(dcTokens);
            dtTokens.Columns.Add(dcLexema);
            dtTokens.Columns.Add(dcFila);
            dtTokens.Columns.Add(dcColumna);
            //recorre la cola de tokens y los despliega
            foreach (Token token in Scan.GetTokens)
            {
                //Crea el dataRow para agregar a la tabla
                DataRow row = dtTokens.NewRow();
                row["Tokens"] = token.Nombre;
                row["Lexemas"] = token.Lexema;
                row["Fila"] = token.Linea;
                row["Columna"] = token.Columna;
                dtTokens.Rows.Add(row);
            }

            dGV_Token_Lexema.DataSource = dtTokens;

            Cursor.Current = Cursors.Default;
        }
    }
}
