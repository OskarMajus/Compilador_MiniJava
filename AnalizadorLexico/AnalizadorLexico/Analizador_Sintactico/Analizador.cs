using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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

        List<HashSet<int>> m_lr0States;
        List<HashSet<int>> m_lr0Kernels;
        List<HashSet<int>> m_lalrStates;
        Dictionary<int, HashSet<int>> m_lookAheads;

        List<int[]> m_lrGotos;
        List<int[]> m_gotoPrecedence;

        Gramatica m_grammar;
        List<Produccion> m_productions;
        List<int> m_terminals;
        List<int> m_nonterminals;

        List<Dictionary<int, List<LALRPropagacion>>> m_lalrPropogations;

        TablaDeAnalisis m_parseTable;

        List<int> m_productionPrecedence;
        List<Derivacion> m_productionDerivation;


        public HashSet<int>[] FirstSets
        {
            get
            {
                return m_firstSets;
            }
        }

        public List<LR0Item> LR0Items
        {
            get
            {
                return m_lr0Items;
            }
        }

        public List<LR1Item> LR1Items
        {
            get
            {
                return m_lr1Items;
            }
        }


        public List<HashSet<int>> LR0States
        {
            get
            {
                return m_lr0States;
            }
        }

        public List<HashSet<int>> LR0Kernels
        {
            get
            {
                return m_lr0Kernels;
            }
        }

        public List<HashSet<int>> LALRStates
        {
            get
            {
                return m_lalrStates;
            }
        }


        public Dictionary<int, HashSet<int>> LookAheads
        {
            get
            {
                return m_lookAheads;
            }
        }

        public List<int[]> LRGotos
        {
            get
            {
                return m_lrGotos;
            }
        }

        public List<int[]> GotoPrecedence
        {
            get
            {
                return m_gotoPrecedence;
            }
        }

        public List<Produccion> Productions
        {
            get
            {
                return m_productions;
            }
        }

        public List<int> Terminals
        {
            get
            {
                return m_terminals;
            }
        }

        public List<int> NonTerminals
        {
            get
            {
                return m_nonterminals;
            }
        }

        public List<Dictionary<int, List<LALRPropagacion>>> LALRPropogations
        {
            get
            {
                return m_lalrPropogations;
            }
        }

        public List<int> ProductionPrecedence
        {
            get
            {
                return m_productionPrecedence;
            }
        }

        public List<Derivacion> ProductionDerivation
        {
            get
            {
                return m_productionDerivation;
            }
        }

        public Gramatica Grammar
        {
            get
            {
                return m_grammar;
            }
        }

        public TablaDeAnalisis ParseTable
        {
            get
            {
                return m_parseTable;
            }
        }

        /// <summary>
		/// Agrega una propagación a la Tabla de Propagación
		/// </summary>
		void AddPropogation(int nLR0SourceState, int nLR0SourceItem, int nLR0TargetState, int nLR0TargetItem)
        {
            while (m_lalrPropogations.Count <= nLR0SourceState)
            {
                m_lalrPropogations.Add(new Dictionary<int, List<LALRPropagacion>>());
            }

            Dictionary<int, List<LALRPropagacion>> propogationsForState = m_lalrPropogations[nLR0SourceState];
            List<LALRPropagacion> propogationList = null;
            if (!propogationsForState.TryGetValue(nLR0SourceItem, out propogationList))
            {
                propogationList = new List<LALRPropagacion>();
                propogationsForState[nLR0SourceItem] = propogationList;
            }

            propogationList.Add(new LALRPropagacion { LR0TargetState = nLR0TargetState, LR0TargetItem = nLR0TargetItem });
        }

        /// <summary>
		/// Obtiene el ID de un elemento LR0
		/// </summary>
		int GetLR0ItemID(LR0Item item)
        {
            int nItemID = 0;
            foreach (LR0Item oItem in m_lr0Items)
            {
                if (oItem.Equals(item))
                {
                    return nItemID;
                }
                nItemID++;
            }
            m_lr0Items.Add(item);
            return nItemID;
        }


        /// <summary>
        /// Obtiene el ID del elemento LR1
        /// </summary>
        int GetLR1ItemID(LR1Item item)
        {
            int nItemID = 0;
            foreach (LR1Item oItem in m_lr1Items)
            {
                if (oItem.Equals(item))
                {
                    return nItemID;
                }
                nItemID++;
            }
            m_lr1Items.Add(item);
            return nItemID;
        }

        /// <summary>
		/// Obtiene el ID de un Estado LR0
		/// </summary>
		int GetLR0StateID(HashSet<int> state, ref bool bAdded)
        {
            int nStateID = 0;
            foreach (HashSet<int> oState in m_lr0States)
            {
                if (oState.SetEquals(state))
                {
                    return nStateID;
                }
                nStateID++;
            }
            m_lr0States.Add(state);
            bAdded = true;
            return nStateID;
        }

        /// <summary>
        /// Se hace la clausura 
        /// Toma un conjunto de elementos LR0 y produce todos los elementos LR0 a los que se puede acceder mediante sustitución
        /// </summary>
        HashSet<int> LR0Closure(HashSet<int> i)
        {
            HashSet<int> closed = new HashSet<int>();
            List<int> open = new List<int>();

            foreach (int itemCopy in i)
            {
                open.Add(itemCopy);
            }

            while (open.Count > 0)
            {
                int nItem = open[0];
                open.RemoveAt(0);
                LR0Item item = m_lr0Items[nItem];
                closed.Add(nItem);

                int nProduction = 0;
                foreach (Produccion production in m_productions)
                {
                    if ((item.Posicion < m_productions[item.Produccion].Right.Length) && (production.Left == m_productions[item.Produccion].Right[item.Posicion]))
                    {
                        LR0Item newItem = new LR0Item { Produccion = nProduction, Posicion = 0 };
                        int nNewItemID = GetLR0ItemID(newItem);
                        if (!open.Contains(nNewItemID) && !closed.Contains(nNewItemID))
                        {
                            open.Add(nNewItemID);

                        }
                    }
                    nProduction++;
                }
            }

            return closed;
        }


        /// <summary>
        /// Se calcula la clausara para LR1
		/// Toma un conjunto de elementos LR1 (LR0 con Lookaheads) y produce todos esos elementos LR1
		/// </summary>
		HashSet<int> LR1Closure(HashSet<int> i)
        {
            HashSet<int> closed = new HashSet<int>();
            List<int> open = new List<int>();

            foreach (int itemCopy in i)
            {
                open.Add(itemCopy);
            }

            while (open.Count > 0)
            {
                int nLR1Item = open[0];
                open.RemoveAt(0);
                LR1Item lr1Item = m_lr1Items[nLR1Item];
                LR0Item lr0Item = m_lr0Items[lr1Item.LR0ItemID];
                closed.Add(nLR1Item);

                if (lr0Item.Posicion < m_productions[lr0Item.Produccion].Right.Length)
                {
                    int nToken = m_productions[lr0Item.Produccion].Right[lr0Item.Posicion];
                    if (m_nonterminals.Contains(nToken))
                    {
                        List<int> argFirst = new List<int>();
                        for (int nIdx = lr0Item.Posicion + 1; nIdx < m_productions[lr0Item.Produccion].Right.Length; nIdx++)
                        {
                            argFirst.Add(m_productions[lr0Item.Produccion].Right[nIdx]);
                        }
                        HashSet<int> first = First(argFirst, lr1Item.LookAhead);
                        int nProduction = 0;
                        foreach (Produccion production in m_productions)
                        {
                            if (production.Left == nToken)
                            {
                                foreach (int nTokenFirst in first)
                                {
                                    LR0Item newLR0Item = new LR0Item { Produccion = nProduction, Posicion = 0 };
                                    int nNewLR0ItemID = GetLR0ItemID(newLR0Item);
                                    LR1Item newLR1Item = new LR1Item { LR0ItemID = nNewLR0ItemID, LookAhead = nTokenFirst };
                                    int nNewLR1ItemID = GetLR1ItemID(newLR1Item);
                                    if (!open.Contains(nNewLR1ItemID) && !closed.Contains(nNewLR1ItemID))
                                    {
                                        open.Add(nNewLR1ItemID);
                                    }
                                }
                            }
                            nProduction++;
                        }
                    }
                }
            }

            return closed;
        }


        /// <summary>
		/// Toma un estado LR0 y un tokenID, produce el siguiente estado dado el token y las producciones de la grámatica
		/// </summary>
		int GotoLR0(int nState, int nTokenID, ref bool bAdded, ref int nPrecedence)
        {
            HashSet<int> gotoLR0 = new HashSet<int>();
            HashSet<int> state = m_lr0States[nState];
            foreach (int nItem in state)
            {
                LR0Item item = m_lr0Items[nItem];
                if (item.Posicion < m_productions[item.Produccion].Right.Length && (m_productions[item.Produccion].Right[item.Posicion] == nTokenID))
                {
                    LR0Item newItem = new LR0Item { Produccion = item.Produccion, Posicion = item.Posicion + 1 };
                    int nNewItemID = GetLR0ItemID(newItem);
                    gotoLR0.Add(nNewItemID);
                    int nProductionPrecedence = m_productionPrecedence[item.Produccion];
                    if (nPrecedence < nProductionPrecedence)
                    {
                        nPrecedence = nProductionPrecedence;
                    }
                }
            }
            if (gotoLR0.Count == 0)
            {
                return -1;
            }
            else
            {
                return GetLR0StateID(LR0Closure(gotoLR0), ref bAdded);
            }
        }

        /// <summary>
        /// Genera todos los elementos LR0
        /// </summary>
        void GenerateLR0Items()
        {
            HashSet<int> startState = new HashSet<int>();
            startState.Add(GetLR0ItemID(new LR0Item { Produccion = 0, Posicion = 0 }));

            bool bIgnore = false;
            List<int> open = new List<int>();
            open.Add(GetLR0StateID(LR0Closure(startState), ref bIgnore));

            while (open.Count > 0)
            {
                int nState = open[0];
                open.RemoveAt(0);
                while (m_lrGotos.Count <= nState)
                {
                    m_lrGotos.Add(new int[m_grammar.Tokens.Length]);
                    m_gotoPrecedence.Add(new int[m_grammar.Tokens.Length]);
                }

                for (int nToken = 0; nToken < m_grammar.Tokens.Length; nToken++)
                {
                    bool bAdded = false;
                    int nPrecedence = int.MinValue;
                    int nGoto = GotoLR0(nState, nToken, ref bAdded, ref nPrecedence);

                    m_lrGotos[nState][nToken] = nGoto;
                    m_gotoPrecedence[nState][nToken] = nPrecedence;

                    if (bAdded)
                    {
                        open.Add(nGoto);
                    }
                }
            }
        }


        /// <summary>
        /// Calcula el conjunto de los primeros terminales para cada token en la grámatica
        /// </summary>
        void ComputeFirstSets()
        {
            int nCountTokens = m_nonterminals.Count + m_terminals.Count;
            m_firstSets = new HashSet<int>[nCountTokens];
            for (int nIdx = 0; nIdx < nCountTokens; nIdx++)
            {
                m_firstSets[nIdx] = new HashSet<int>();
                if (m_terminals.Contains(nIdx))
                {
                    m_firstSets[nIdx].Add(nIdx);
                }
            }

            foreach (Produccion production in m_productions)
            {
                if (production.Right.Length == 0)
                {
                    m_firstSets[production.Left].Add(-1);
                }
            }

            bool bDidSomething;
            do
            {
                bDidSomething = false;
                foreach (Produccion production in m_productions)
                {
                    foreach (int nToken in production.Right)
                    {
                        bool bLookAhead = false;
                        foreach (int nTokenFirst in m_firstSets[nToken])
                        {
                            if (nTokenFirst == -1)
                            {
                                bLookAhead = true;
                            }
                            else if (m_firstSets[production.Left].Add(nTokenFirst))
                            {
                                bDidSomething = true;
                            }
                        }

                        if (!bLookAhead)
                        {
                            break;
                        }
                    }
                }
            }
            while (bDidSomething);
        }

        /// <summary>
		/// devuelve el conjunto de terminales que se pueden ver dada una lista arbitraria de tokens
		/// </summary>
		HashSet<int> First(List<int> tokens, int nTerminal)
        {
            HashSet<int> first = new HashSet<int>();
            foreach (int nToken in tokens)
            {
                bool bLookAhead = false;
                foreach (int nTokenFirst in m_firstSets[nToken])
                {
                    if (nTokenFirst == -1)
                    {
                        bLookAhead = true;
                    }
                    else
                    {
                        first.Add(nTokenFirst);
                    }
                }

                if (!bLookAhead)
                {
                    return first;
                }
            }

            first.Add(nTerminal);
            return first;
        }

        /// <summary>
        /// Inicializa la tabla de propagación y el estado inicial de la tabla LALR
        /// </summary>
        void InitLALRTables()
        {
            int nLR0State = 0;
            foreach (HashSet<int> lr0State in m_lr0States)
            {
                m_lalrStates.Add(new HashSet<int>());
            }
            foreach (HashSet<int> lr0Kernel in m_lr0Kernels)
            {
                HashSet<int> J = new HashSet<int>();
                foreach (int jLR0ItemID in lr0Kernel)
                {
                    LR1Item lr1Item = new LR1Item { LR0ItemID = jLR0ItemID, LookAhead = -1 };
                    int nLR1ItemID = GetLR1ItemID(lr1Item);
                    J.Add(nLR1ItemID);
                }
                HashSet<int> JPrime = LR1Closure(J);
                foreach (int jpLR1ItemID in JPrime)
                {
                    LR1Item lr1Item = m_lr1Items[jpLR1ItemID];
                    LR0Item lr0Item = m_lr0Items[lr1Item.LR0ItemID];

                    if ((lr1Item.LookAhead != -1) || (nLR0State == 0))
                    {
                        m_lalrStates[nLR0State].Add(jpLR1ItemID);
                    }

                    if (lr0Item.Posicion < m_productions[lr0Item.Produccion].Right.Length)
                    {
                        int nToken = m_productions[lr0Item.Produccion].Right[lr0Item.Posicion];
                        LR0Item lr0Successor = new LR0Item { Produccion = lr0Item.Produccion, Posicion = lr0Item.Posicion + 1 };
                        int nLR0Successor = GetLR0ItemID(lr0Successor);
                        int nSuccessorState = m_lrGotos[nLR0State][nToken];
                        if (lr1Item.LookAhead == -1)
                        {
                            AddPropogation(nLR0State, lr1Item.LR0ItemID, nSuccessorState, nLR0Successor);
                        }
                        else
                        {
                            LR1Item lalrItem = new LR1Item { LR0ItemID = nLR0Successor, LookAhead = lr1Item.LookAhead };
                            int nLALRItemID = GetLR1ItemID(lalrItem);
                            m_lalrStates[nSuccessorState].Add(nLALRItemID);
                        }
                    }
                }

                nLR0State++;
            }
        }



        /// <summary>
        /// Calcula los Lookaheads
		/// Calcula los estados en la tabla LALR
		/// </summary>
		void CalculateLookAheads()
        {
            bool bChanged;
            do
            {
                bChanged = false;
                int nState = 0;
                foreach (Dictionary<int, List<LALRPropagacion>> statePropogations in m_lalrPropogations)
                {
                    bool bStateChanged = false;
                    foreach (int nLR1Item in m_lalrStates[nState])
                    {
                        LR1Item lr1Item = m_lr1Items[nLR1Item];

                        if (statePropogations.ContainsKey(lr1Item.LR0ItemID))
                        {
                            foreach (LALRPropagacion lalrPropogation in statePropogations[lr1Item.LR0ItemID])
                            {
                                int nGoto = lalrPropogation.LR0TargetState;
                                LR1Item item = new LR1Item { LR0ItemID = lalrPropogation.LR0TargetItem, LookAhead = lr1Item.LookAhead };
                                if (m_lalrStates[nGoto].Add(GetLR1ItemID(item)))
                                {
                                    bChanged = true;
                                    bStateChanged = true;
                                }
                            }
                        }
                    }

                    if (bStateChanged)
                    {
                        m_lalrStates[nState] = LR1Closure(m_lalrStates[nState]);
                    }
                    nState++;
                }
            }
            while (bChanged);
        }

        /// <summary>
        /// Inicializa los tokens para la gramática
        /// </summary>
        void InitSymbols()
        {
            for (int nSymbol = 0; nSymbol < m_grammar.Tokens.Length; nSymbol++)
            {
                bool bTerminal = true;
                foreach (Produccion production in m_productions)
                {
                    if (production.Left == nSymbol)
                    {
                        bTerminal = false;
                        break;
                    }
                }

                if (bTerminal)
                {
                    m_terminals.Add(nSymbol);
                }
                else
                {
                    m_nonterminals.Add(nSymbol);
                }
            }
        }

        /// <summary>
        /// Convierte un estado LR0 en un kernel(nucleo) LR0 que consta solo de los elementos LR0 que inician en el estado 
        /// </summary>
        public void ConvertLR0ItemsToKernels()
        {
            foreach (HashSet<int> lr0State in m_lr0States)
            {
                HashSet<int> lr0Kernel = new HashSet<int>();
                foreach (int nLR0Item in lr0State)
                {
                    LR0Item item = m_lr0Items[nLR0Item];
                    if (item.Posicion != 0)
                    {
                        lr0Kernel.Add(nLR0Item);
                    }
                    else if (m_productions[item.Produccion].Left == 0)
                    {
                        lr0Kernel.Add(nLR0Item);
                    }
                }
                m_lr0Kernels.Add(lr0Kernel);
            }
        }


        /// <summary>
		/// Función Auxiliar que devuelve verdadero si la lista de acciones contiene una acción
		/// </summary>
		bool ListContainsAction(List<Accion> list, Accion action)
        {
            foreach (Accion listAction in list)
            {
                if (listAction.Equals(action))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Genera la tabla de análisis dados los estados LALR y la gramática
        /// </summary>
        void GenerarTablaDeAnalisis()
        {
            m_parseTable = new TablaDeAnalisis();
            m_parseTable.Actions = new Accion[m_lalrStates.Count, m_grammar.Tokens.Length + 1];
            for (int nStateID = 0; nStateID < m_lalrStates.Count; nStateID++)
            {
                HashSet<int> lalrState = m_lalrStates[nStateID];

                for (int nToken = -1; nToken < m_grammar.Tokens.Length; nToken++)
                {
                    List<Accion> acciones = new List<Accion>();
                    string sToken = "$";
                    if (nToken >= 0)
                    {
                        sToken = m_grammar.Tokens[nToken];

                        if (m_lrGotos[nStateID][nToken] >= 0)
                        {
                            acciones.Add(new Accion { ActionType = ActionType.Shift, ActionParameter = m_lrGotos[nStateID][nToken] });
                        }
                    }

                    foreach (int nLR1ItemID in lalrState)
                    {
                        LR1Item lr1Item = m_lr1Items[nLR1ItemID];
                        LR0Item lr0Item = m_lr0Items[lr1Item.LR0ItemID];

                        if ((lr0Item.Posicion == m_productions[lr0Item.Produccion].Right.Length) && lr1Item.LookAhead == nToken)
                        {
                            Accion accion = new Accion { ActionType = ActionType.Reduce, ActionParameter = lr0Item.Produccion };
                            if (!ListContainsAction(acciones, accion))
                            {
                                acciones.Add(accion);
                            }
                        }
                    }

                    int nMaxPrecedence = int.MinValue;
                    List<Accion> importantActions = new List<Accion>();
                    foreach (Accion action in acciones)
                    {
                        int nActionPrecedence = int.MinValue;
                        if (action.ActionType == ActionType.Shift)
                        {
                            nActionPrecedence = m_gotoPrecedence[nStateID][nToken]; //nToken will never be -1
                        }
                        else if (action.ActionType == ActionType.Reduce)
                        {
                            nActionPrecedence = m_productionPrecedence[action.ActionParameter];
                        }

                        if (nActionPrecedence > nMaxPrecedence)
                        {
                            nMaxPrecedence = nActionPrecedence;
                            importantActions.Clear();
                            importantActions.Add(action);
                        }
                        else if (nActionPrecedence == nMaxPrecedence)
                        {
                            importantActions.Add(action);
                        }
                    }

                    if (importantActions.Count == 1)
                    {
                        m_parseTable.Actions[nStateID, nToken + 1] = importantActions[0];
                    }
                    else if (importantActions.Count > 1)
                    {
                        Accion shiftAction = null;
                        List<Accion> reduceActions = new List<Accion>();
                        foreach (Accion action in importantActions)
                        {
                            if (action.ActionType == ActionType.Reduce)
                            {
                                reduceActions.Add(action);
                            }
                            else
                            {
                                shiftAction = action;
                            }
                        }

                        Derivacion derv = m_grammar.grupoPrecedencias[-nMaxPrecedence].Derivacion;
                        if (derv == Derivacion.MasAIzquierda && reduceActions.Count == 1)
                        {
                            m_parseTable.Actions[nStateID, nToken + 1] = reduceActions[0];
                        }
                        else if (derv == Derivacion.MasADerecha && shiftAction != null)
                        {
                            m_parseTable.Actions[nStateID, nToken + 1] = shiftAction;
                        }
                        else
                        {
                            if (derv == Derivacion.Ninguno && reduceActions.Count == 1)
                            {
                                MessageBox.Show("Error, Conflicto en la gramática desplazamiento-reducción");
                                //Console.WriteLine("Error, shift-reduce conflict in grammar");
                            }
                            else
                            {
                                MessageBox.Show("Error, conflicto de reducción-reduccion en la gramatica");
                                //Console.WriteLine("Error, reduce-reduce conflict in grammar");
                            }
                            m_parseTable.Actions[nStateID, nToken + 1] = new Accion { ActionType = ActionType.Error, ActionParameter = nToken };
                        }
                    }
                    else
                    {
                        m_parseTable.Actions[nStateID, nToken + 1] = new Accion { ActionType = ActionType.Error, ActionParameter = nToken };
                    }
                }
            }
        }

        /// <summary>
        /// función Auxiliar
        /// </summary>
        void PopulateProductions()
        {
            int nPrecedence = 0;
            foreach (GruposPrecedencia oGroup in m_grammar.grupoPrecedencias)
            {
                foreach (Produccion oProduction in oGroup.Producciones)
                {
                    m_productions.Add(oProduction);
                    m_productionPrecedence.Add(nPrecedence);
                    m_productionDerivation.Add(oGroup.Derivacion);
                }
                nPrecedence--;
            }
        }

        /// <summary>
		/// constructor, construct parser table
		/// </summary>
		public Analizador(Gramatica gramatica)
        {
            m_lrGotos = new List<int[]>();
            m_gotoPrecedence = new List<int[]>();
            m_lr0Items = new List<LR0Item>();
            m_lr1Items = new List<LR1Item>();
            m_lr0States = new List<HashSet<int>>();
            m_lr0Kernels = new List<HashSet<int>>();
            m_lalrStates = new List<HashSet<int>>();
            m_terminals = new List<int>();
            m_nonterminals = new List<int>();
            m_lalrPropogations = new List<Dictionary<int, List<LALRPropagacion>>>();
            m_grammar = gramatica;
            m_productions = new List<Produccion>();
            m_productionDerivation = new List<Derivacion>();
            m_productionPrecedence = new List<int>();

            PopulateProductions();
            InitSymbols();
            GenerateLR0Items();
            ComputeFirstSets();
            ConvertLR0ItemsToKernels();
            InitLALRTables();
            CalculateLookAheads();
            GenerarTablaDeAnalisis();
        }





    }
}
