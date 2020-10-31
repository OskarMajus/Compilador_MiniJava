using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico.Analizador_Sintactico
{
    /// <summary>
    /// El tipo de accíón que realizara el analizador
    /// </summary>
    public enum ActionType
    {
        Reduce,
        Shift,
        Error
    };

    /// <summary>
    /// Una entrada de tabla de análisis
    /// </summary>    
    public class Accion
    {
        public ActionType ActionType { get; set; }
        public int ActionParameter { get; set;}

        public bool EsIgualA(Accion accion)
        {
            return (ActionType == accion.ActionType) && (ActionParameter == accion.ActionParameter);
        }
    }

    /// <summary>
    /// Dirige al analizador sobre que acción realizar 
    /// en un estado dado en una entrada particular
    /// </summary>
    public class TablaDeAnalisis
    {
        public Accion [,] Actions { get; set; }
    }
}
