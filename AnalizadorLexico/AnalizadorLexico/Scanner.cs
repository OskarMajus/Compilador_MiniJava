using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class Scanner
    {
        //Texto a escanear
        string text = string.Empty;

        //Lista de tokens
        protected List<Token> Tokens = new List<Token>();
        //Lista de errores
        protected List<string> errores = new List<string>();

        public Scanner(string texto) => text = texto;

        //Properties get & set
        public List<Token> GetTokens
        {
            get { return Tokens; }
        }

        public List<string> GetErrors
        {
            get { return errores; }
        }
        

        public string Lector(int fila) {
            //Valor final de cadena
            char finalCadena = '#';

            //EXPRESIONES REGULARES
            string AllowedChars = @"([a-zA-Z]|\$)"; // Ids
            string AllowedNumbers = @"(\d)"; // Numers
            string AllowedSpace = @"(\s|#)"; //space, tab, next line, etc.
            string AllowedOperators = @"(\[\]|\[|\]|\(\)|\(|\)|\{\}|\{|\}|\+|\-|\*|\/|\%|\<\=|\>\=|\<|\>|\=\=|\=|\!\=|\!|\&\&|\|\||;|,|\.)"; //Operadores, parentesis, corchetes y llaves

            //variables para crear el token
            string palabra = ""; //palabra Lexema
            string nameTokenID = "ID"; //nombre del token para cualquier ID
            string nameTokenInt = "int"; //nombre del token para integer
            string nameTokenDouble = "double"; //nombre del token para double
            string nameTokenReservadas = "reservada"; //nombre del token para palabras reservadas
            string nameTokenComment = "comentario"; //nombre del token para comentarios
            string nameTokenOperador = "operador";
            string nameTokenString = "string";
            string nameTokenBool = "bool";
            int columna = 0; //Fila esta siendo recorrida en el ciclo

            //PALABRAS RESERVADAS
            string[] reservadas = { "void", "boolean", "const", "this", "int", "string", "interface", "extends",
                "double", "class", "null", "implements", "for", "while", "if", "else", "return", "break", "new", "system",
                "out", "println" };
            string[] booleanos = { "true", "false" };

            //Es el estado para el AFD
            int stage = 0;

            //Crea las lineas del parrafo para leerlo
            string[] lineasParrafo = text.Split('\n');
            //Recorre cada linea llamando al contador fila y al texto linea
            int i = 0;string linea;
            
            //Este int indica el EOF abierto 1 = string, 2= comentario
            int EOF = 0;
            foreach (string line in lineasParrafo)
            //for (int i = 0; i < fila; i++)
            {
                //string linea = lineasParrafo[i];
                linea = line + "#";
                //Inicializa la columna para poder iniciar en el char 0
                columna = 0;
                for (int j = 0; j < linea.Length;j++)
                {
                    char letra = linea[j];
                    switch (stage)
                    {                        
                        case 0: // Acepta operadores o inicia IDs o inicia numeros o es error
                            {
                                palabra = letra+"";
                                //Inicia buscando letras para IDs
                                if (Regex.IsMatch(letra + "", AllowedChars))
                                {
                                    stage = 1;
                                }
                                //Sino algun numero
                                else if (Regex.IsMatch(letra + "", AllowedNumbers))
                                {
                                    stage = 2;
                                }
                                //Revisa por comentarios
                                else if (letra == '/')
                                {
                                    stage = 7;
                                }
                                //Sino revisa si es operador y acepta la cadena
                                else if (Regex.IsMatch(letra + "", AllowedOperators))
                                {
                                    stage = 0;
                                    //reinicia la palabra al caracter actual y lo guarda ya que es un operador 
                                    palabra = letra + "";
                                    saveToken(nameTokenOperador, palabra, i, j);
                                }
                                //Sino revisa si es STRING
                                else if (letra == '"')
                                {
                                    stage = 6;
                                }
                                //Sino se come los espacios
                                else if (Regex.IsMatch(letra + "", AllowedSpace))
                                {
                                    stage = 0;
                                }
                                //Si no es nada de esto es un error
                                else
                                {
                                    stage = 0;
                                    errores.Add(errorMessage(i, j, palabra));
                                }
                                break;
                            }
                        case 1: // Acepta IDs u Operador
                            {
                                //Busca letras, numeros o $ para IDs
                                if ((Regex.IsMatch(letra + "", AllowedChars)) || (Regex.IsMatch(letra + "", AllowedNumbers)))
                                {
                                    palabra += letra;
                                    stage = 1;
                                }
                                //Sino acepta la cadena que traia e inicia la siguiente
                                else if (Regex.IsMatch(letra + "", AllowedOperators)){
                                    
                                    //Revisa si es una palabra reservada
                                    if (reservadas.Contains(palabra))
                                    {
                                        //Crea el token que guardara la palabra que se traia COMO RESERVADA
                                        saveToken(nameTokenReservadas, palabra, i, j);
                                    }
                                    else if (booleanos.Contains(palabra)) {
                                        //Crea el token que guardara la palabra como bool (True|False)
                                        saveToken(nameTokenBool, palabra, i, j);
                                    }
                                    else
                                    {
                                        //Crea el token que guardara la palabra que se traia 
                                        saveToken(nameTokenID, palabra, i, j);
                                    }
                                    //reinicia la palabra al caracter actual y lo guarda ya que es un operador
                                    if (letra == '/') {
                                        stage = 7;
                                    }
                                    else
                                    {
                                        stage = 0;
                                        palabra = letra + "";
                                        saveToken(nameTokenOperador, palabra, i, j);
                                    }
                                }
                                //Si viene espacio acepta la cadena que traia e inicia la siguiente
                                else if (Regex.IsMatch(letra + "", AllowedSpace))
                                {
                                    stage = 0;
                                    //Revisa si es una palabra reservada
                                    if (reservadas.Contains(palabra))
                                    {
                                        //Crea el token que guardara la palabra que se traia COMO RESERVADA
                                        saveToken(nameTokenReservadas, palabra, i, j);
                                    }
                                    else if (booleanos.Contains(palabra))
                                    {
                                        //Crea el token que guardara la palabra como bool (True|False)
                                        saveToken(nameTokenBool, palabra, i, j);
                                    }
                                    else
                                    {
                                        //Crea el token que guardara la palabra que se traia 
                                        saveToken(nameTokenID, palabra, i, j);
                                    }
                                }
                                //Si no es nada de esto es un error
                                else
                                {
                                    stage = 0;
                                    errores.Add(errorMessage(i, j, letra + ""));
                                    //Revisa si es una palabra reservada
                                    if (reservadas.Contains(palabra))
                                    {
                                        //Crea el token que guardara la palabra que se traia COMO RESERVADA
                                        saveToken(nameTokenReservadas, palabra, i, j);
                                    }
                                    else if (booleanos.Contains(palabra))
                                    {
                                        //Crea el token que guardara la palabra como bool (True|False)
                                        saveToken(nameTokenBool, palabra, i, j);
                                    }
                                    else
                                    {
                                        //Crea el token que guardara la palabra que se traia 
                                        saveToken(nameTokenID, palabra, i, j);
                                    }
                                }
                                break;
                            }
                        case 2: // Acepta Numeros u Operador
                            {
                                //Revisa si es HEX
                                if (palabra.Contains("0") && (letra == 'x' || letra == 'X'))
                                {
                                    palabra += letra;
                                    stage = 2;
                                }
                                //Revisa los caracteres A, B, C, D, E, F del HEX
                                else if ((Regex.IsMatch(palabra+letra, @"(0x|0X)(\d|[a-fA-F])+")) && (Regex.IsMatch(letra + "", @"(\d|[a-fA-F])")) )
                                {
                                    palabra += letra;
                                    stage = 2;
                                }
                                //Busca numeros
                                else if (Regex.IsMatch(letra + "", AllowedNumbers))
                                {
                                    palabra += letra;
                                    stage = 2;
                                }
                                //Si viene punto busca ser Double
                                else if (letra == '.')
                                {
                                    if (Regex.IsMatch(palabra, @"(0x|0X)(\d|[a-fA-F])+"))
                                    {
                                        stage = 0;
                                        //Crea el token que guardara el numero que se traia 
                                        saveToken(nameTokenInt, palabra, i, j);
                                        //reinicia la palabra al caracter actual y lo guarda ya que es un operador 
                                        palabra = letra + "";
                                        saveToken(nameTokenOperador, palabra, i, j);
                                    }
                                    else
                                    {
                                        stage = 3;
                                        palabra += letra;
                                    }
                                }
                                //Si viene una letra acepta la cadena e inicia la siguiente
                                else if (Regex.IsMatch(letra + "", AllowedChars))
                                {
                                    stage = 1;
                                    //Revisa que si es HEX no este "vacio"
                                    if (palabra.ToLower().Equals("0x"))
                                    {
                                        errores.Add(errorMessage(i, j, palabra + "--- NO ES VALIDO EL INT HEX SIN UN VALOR CORRESPONDIENTE"));

                                    }
                                    else
                                    {
                                        //Crea el token que guardara el numero que se traia 
                                        saveToken(nameTokenInt, palabra, i, j);
                                    }
                                    //reinicia la palabra al caracter actual y lo guarda ya que es un operador 
                                    palabra = letra + "";
                                }
                                //Si viene un operador acepta la cadena que traia e inicia la siguiente
                                else if (Regex.IsMatch(letra + "", AllowedOperators))
                                {
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenInt, palabra, i, j);
                                    //reinicia la palabra al caracter actual y lo guarda ya que es un operador
                                    if (letra == '/')
                                    {
                                        stage = 7;
                                    }
                                    else
                                    {
                                        stage = 0;
                                        saveToken(nameTokenOperador, letra+"", i, j);
                                    }                                                                    
                                }
                                //Si es espacio acepta y se va a la siguiente letra
                                else if (Regex.IsMatch(letra + "", AllowedSpace))
                                {
                                    stage = 0;
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenInt, palabra, i, j);
                                }
                                //Si no es nada de esto es un error
                                else
                                {
                                    stage = 0;
                                    errores.Add(errorMessage(i, j, letra + ""));
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenInt, palabra, i, j);
                                }
                                break;
                            }
                        case 3: //Busca numeros DOUBLE
                            {                                
                                if (Regex.IsMatch(letra + "", AllowedNumbers))
                                {
                                    palabra += letra;
                                    stage = 3;
                                }
                                //Si viene una E o e es un exponente
                                else if (letra == 'E' || letra == 'e') 
                                {
                                    stage = 4;
                                    palabra += letra;
                                }
                                //Si viene una letra acepta la cadena e inicia la siguiente
                                else if (Regex.IsMatch(letra + "", AllowedChars))
                                {
                                    stage = 1;
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenDouble, palabra, i, j);
                                    //reinicia la palabra al caracter actual y lo guarda ya que es un operador 
                                    palabra = letra + "";
                                }
                                //Si viene un operador acepta la cadena que traia e inicia la siguiente
                                else if (Regex.IsMatch(letra + "", AllowedOperators))
                                {
                                    //reinicia la palabra al caracter actual y lo guarda ya que es un operador
                                    if (letra == '/')
                                    {
                                        stage = 7;
                                    }
                                    else
                                    {
                                        stage = 0;
                                        palabra = letra + "";
                                        saveToken(nameTokenOperador, palabra, i, j);
                                    }
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenDouble, palabra, i, j);
                                }
                                //Si es espacio acepta y se va a la siguiente letra
                                else if (Regex.IsMatch(letra + "", AllowedSpace))
                                {
                                    stage = 0;
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenDouble, palabra, i, j);
                                }
                                //Si no es nada de esto es un error
                                else
                                {
                                    stage = 0;
                                    errores.Add(errorMessage(i, j, letra + ""));
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenDouble, palabra, i, j);
                                }
                                break;
                            }
                        case 4: //Busca exponente E,e
                            {
                                //Busca numeros
                                if (Regex.IsMatch(letra + "", AllowedNumbers))
                                {
                                    palabra += letra;
                                    stage = 4;
                                }
                                //Si viene una E o e es un exponente
                                else if (letra == '-' || letra == '+')
                                {
                                    stage = 5;
                                    palabra += letra;
                                }
                                //Si viene una letra acepta la cadena e inicia la siguiente
                                else if (Regex.IsMatch(letra + "", AllowedChars))
                                {
                                    stage = 1;
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenDouble, palabra, i, j);
                                    //reinicia la palabra al caracter actual y lo guarda ya que es un operador 
                                    palabra = letra + "";
                                }
                                //Si viene un operador acepta la cadena que traia e inicia la siguiente
                                else if (Regex.IsMatch(letra + "", AllowedOperators))
                                {
                                    //reinicia la palabra al caracter actual y lo guarda ya que es un operador
                                    if (letra == '/')
                                    {
                                        stage = 7;
                                    }
                                    else
                                    {
                                        stage = 0;
                                        palabra = letra + "";
                                        saveToken(nameTokenOperador, palabra, i, j);
                                    }
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenDouble, palabra, i, j);
                                }
                                //Si es espacio acepta y se va a la siguiente letra
                                else if (Regex.IsMatch(letra + "", AllowedSpace))
                                {
                                    stage = 0;
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenDouble, palabra, i, j);
                                }
                                //Si no es nada de esto es un error
                                else
                                {
                                    stage = 0;
                                    errores.Add(errorMessage(i, j, letra + ""));
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenDouble, palabra, i, j);
                                }
                                break;
                            }
                        case 5: //Busca el valor del exponente
                            {
                                //Busca numeros
                                if (Regex.IsMatch(letra + "", AllowedNumbers))
                                {
                                    palabra += letra;
                                    stage = 5;
                                }
                                //Si viene una letra acepta la cadena e inicia la siguiente
                                else if (Regex.IsMatch(letra + "", AllowedChars))
                                {
                                    stage = 1;
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenDouble, palabra, i, j);
                                    //reinicia la palabra al caracter actual y lo guarda ya que es un operador 
                                    palabra = letra + "";
                                }
                                //Si viene un operador acepta la cadena que traia e inicia la siguiente
                                else if (Regex.IsMatch(letra + "", AllowedOperators))
                                {
                                    //reinicia la palabra al caracter actual y lo guarda ya que es un operador
                                    if (letra == '/')
                                    {
                                        stage = 7;
                                    }
                                    else
                                    {
                                        stage = 0;
                                        palabra = letra + "";
                                        saveToken(nameTokenOperador, palabra, i, j);
                                    }
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenDouble, palabra, i, j);
                                }
                                //Si es espacio acepta y se va a la siguiente letra
                                else if (Regex.IsMatch(letra + "", AllowedSpace))
                                {
                                    stage = 0;
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenDouble, palabra, i, j);
                                }
                                //Si no es nada de esto es un error
                                else
                                {
                                    stage = 0;
                                    errores.Add(errorMessage(i, j, letra + ""));
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenDouble, palabra, i, j);
                                }
                                break;
                            }
                        case 6: //Busca STRING
                            {
                                //Se come cualquier valor y agrega al string
                                if (letra == finalCadena)
                                {
                                    stage = 0;
                                    errores.Add(errorMessage(i, j, palabra + "--- SE ESPERABA UN \""));
                                    EOF = 1;
                                }
                                else if (letra == '"')
                                {
                                    EOF = 0;
                                    stage = 0;
                                    palabra += letra;
                                    //Crea el token tipo STRING
                                    saveToken(nameTokenString, palabra, i, j);
                                }
                                else
                                {
                                    stage = 6;
                                    palabra += letra;
                                }
                                break;
                            }
                        case 7:
                            {
                                //revisa si es un comentario simple 
                                if (letra == '/')
                                {
                                    stage = 8;
                                    palabra += letra;
                                }
                                //revisa si es un comentario complejo
                                else if (letra == '*')
                                {
                                    stage = 9;
                                    palabra += letra;
                                    EOF = 2;
                                }
                                //revisa si es una letra de ID
                                else if (Regex.IsMatch(letra + "", AllowedChars))
                                {
                                    stage = 1;
                                    //Crea el token que guardara el operador 
                                    saveToken(nameTokenOperador, palabra, i, j);
                                    //reinicia la palabra al caracter actual y lo guarda ya que es un operador 
                                    palabra = letra + "";
                                }
                                //Si viene un operador distinto de "/" acepta el operador e inicia la siguiente
                                else if (Regex.IsMatch(letra + "", AllowedOperators))
                                {
                                    stage = 0;
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenOperador, palabra, i, j);
                                    //reinicia la palabra al caracter actual y lo guarda ya que es un operador 
                                    palabra = letra + "";
                                    saveToken(nameTokenOperador, palabra, i, j);
                                }
                                //Si es espacio acepta y se va a la siguiente letra
                                else if (Regex.IsMatch(letra + "", AllowedSpace))
                                {
                                    stage = 0;
                                    //Crea el token que guardara como operador 
                                    saveToken(nameTokenOperador, palabra, i, j);
                                }
                                //Si no es nada de esto es un error
                                else
                                {
                                    stage = 0;
                                    errores.Add(errorMessage(i, j, letra + ""));
                                    //Crea el token que guardara el numero que se traia 
                                    saveToken(nameTokenOperador, palabra, i, j);
                                }
                                break;
                            }
                        case 8: // Acepta comentarios simples
                            {
                                //Se come cualquier valor y agrega al string
                                if (letra == finalCadena)
                                {
                                    stage = 0;
                                    saveToken(nameTokenComment, palabra, i, j);
                                }
                                else
                                {
                                    stage = 8;
                                    palabra += letra;
                                }
                                break;
                            }
                        case 9:
                            {
                                //Se come cualquier valor y agrega al string
                                if (letra == '*')
                                {
                                    stage = 10;
                                    palabra += letra;
                                }
                                else
                                {
                                    stage = 9;
                                    palabra += letra;
                                }
                                
                                break;
                            }
                        case 10:
                            {
                                if (letra == '/')
                                {
                                    EOF = 0;
                                    stage = 0;
                                    palabra += letra;
                                    saveToken(nameTokenComment, palabra, i, j);
                                }
                                else {
                                    stage = 9;
                                    palabra += letra;
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    //Aumenta el contador de la columna en la fila
                    columna = j;
                }
                i++;
            }
            switch (EOF)
            {
                case 1:
                    errores.Add(errorMessage(fila, columna, " --- EOF EN UNA CADENA")); break;
                case 2:
                    errores.Add(errorMessage(fila, columna, " --- EOF EN UN COMENTARIO")); break;
                default:
                    break;
            }

            string salida = "Numero de filas: " + fila.ToString() + " y de la ultima fila columna: " + columna.ToString();
            return salida;
        }

        protected string errorMessage(int linea, int columna, string palabra) {
            //se le agrega el +1 en linea y columna ya que el contador normal inicia en 0 y no se entiende
            return "Fallo en la linea " + (linea+1).ToString() + " columna " + (columna+1).ToString() + " Caracter invalido: " + palabra;
        }

        protected bool saveToken(string nombre, string lexema, int linea, int columna) {
            try
            {
                Token token = new Token(nombre, lexema, linea+1, columna+1);
                Tokens.Add(token);
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }

        
    }
}
