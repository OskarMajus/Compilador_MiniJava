using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico.Analizador_Sintactico
{
    /// <summary>
    /// Describe cómo resolver ambigüedades dentro de un grupo de precedencia
    /// </summary>
    public enum Derivacion 
    {
        Ninguno,
        MasAIzquierda,
        MasADerecha
    };

    /// <summary>
    /// Las producciones de la gramática
    /// </summary>
    public class Producciones
    {
        public int Left { get; set; }
        public int [] Right { get; set; }
    }

    /// <summary>
    /// Una Colección de producciones con un precedente particular
    /// </summary>
    public class GruposPrecedencia
    {
        public Derivacion Derivacion { get; set; }
        public Producciones[] Producciones { get; set; }
    }

    /// <summary>
    /// Toda la gramatica necesaria para hacer el analizador
    /// </summary>
    public class Gramatica
    {
        public string[] Tokens { get; set; }

        public GruposPrecedencia[] grupoPrecedencias { get; set; }
    }
}
