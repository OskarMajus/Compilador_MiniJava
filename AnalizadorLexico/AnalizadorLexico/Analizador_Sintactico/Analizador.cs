using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico.Analizador_Sintactico
{
    public class LALRPropagacion
    {
        //Estado Objetivo
        public int LR0TargetState { get; set; }

        public int LR0TargetItem { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class Analizador
    {
        HashSet<int>[] m_firstSets;
        List<LR0Item> m_lr0Items;
        List<LR1Item> m_lr1Items;
    }
}
