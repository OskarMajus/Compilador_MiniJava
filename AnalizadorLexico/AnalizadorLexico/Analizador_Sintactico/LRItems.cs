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
    public class LR0Items
    {
        public int Produccion { get; set; }
        public int Posición { get; set; }

        public bool Iguales(LR0Items item)
        {
            return (Produccion == item.Produccion) && (Posición == item.Posición);
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
