using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico.Analizador_Sintactico
{
    /// <summary>
    /// Elemento Necesario para construir analizador LR0
    /// </summary>
    public class LR0Item
    {
        public int Produccion { get; set; }
        public int Posicion { get; set; }

        public bool EsIgualA(LR0Item item)
        {
            return (Produccion == item.Produccion) && (Posicion == item.Posicion);
        }
    }

    /// <summary>
    /// Un elemento necesario para construir un analizador LR1
    /// </summary>
    public class LR1Item
    {
        public int LR0ItemID { get; set; }
        public int LookAhead { get; set; }

        public bool EsIgualA(LR1Item item)
        {
            return (LR0ItemID == item.LR0ItemID) && (LookAhead == item.LookAhead);
        }
    };
}
