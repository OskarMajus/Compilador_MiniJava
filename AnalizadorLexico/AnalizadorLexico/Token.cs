using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class Token
    {
        public Token(string nombre, string lexema, int linea, int columna) 
        {
            Nombre = nombre;
            Lexema = lexema;
            Linea = linea;
            Columna = columna;
        }

        public string Nombre { get; set; }

        public string Lexema { get; private set; }

        public int Linea { get; private set; }

        public int Columna { get;  private set; }
    }
}
