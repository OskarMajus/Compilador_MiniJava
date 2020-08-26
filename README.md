# Compilador_MiniJava
Repositorio para Curso de Compiladores

En esta ocasion se prepara la entrega de la fase 1 "Compilador mini-java"
Creado en colaboracion de Oskar Majus y Juan Chavez

Entendimiento del proyecto:

PROYECTO MINI-JAVA

	Case Sensitive 
	if es una palabra clave pero IF es un identificador
	binky y Binky son dos identificadores distinto

	Tipos de Tokens:
		Palabras reservadas
			void int double boolean string class const interface null this extends implements for while if else return break New System out println
			
		Identificadores
			Un identificador es una secuencia de letras, d�gitos y signo d�lar. Puede comenzar con cualquiera excepto un n�mero.
			
		Espacios en blanco
			El espacio en blanco (es decir, espacios, tabuladores y saltos de l�nea) sirve para separar tokens, pero por lo dem�s debe ser ignorado. Palabras clave y los identificadores deben estar separados por espacios en blanco, o por una se�al de que no es ni una palabra ni un identificador.
				if ( 23 this se escanea como cuatro tokens, al igual que if(23this
				
		Comentarios
			Un comentario de una sola l�nea se inicia con // y se extiende hasta el final de la l�nea. Comentarios de varias l�neas comienzan con /* y terminan con la primera */ posterior. Cualquier s�mbolo se permite en un comentario, excepto el de secuencia */ que pone fin al comentario actual. Los comentarios de varias l�neas no se anidan.
			
			Si un archivo incluye un comentario sin terminar, el esc�ner debe informar de un error.
			
		Constantes
			Las constantes booleanas son: true o false.
			Una constante entera puede ser expresada en decimal (base 10) o en hexadecimal (Base 16).
				* Un entero decimal es una secuencia de d�gitos decimales (0-9).
				* Un entero hexadecimal debe comenzar con 0X o 0x (es el car�cter cero) y es seguida por una secuencia de d�gitos hexadecimales. Los d�gitos hexadecimales incluyen los d�gitos hexadecimales y las letras a a la f (ya sea en min�sculas o may�sculas).
				* Ejemplos de enteros v�lidos: 8,012,0X0,0x12aE.

			Una constante double es una secuencia de d�gitos, un punto, seguido de una secuencia de d�gitos, o nada. As�, .12 no es una constante de tipo double, pero 12. y 0.12 lo son. Una constante doble puede tener parte exponencial, por ejemplo, 12.2E+2. Para la constante doble en la notaci�n cient�fica el punto decimal es requerido, el signo del exponente es opcional (si no est� especificado, + es asumido), y el E puede ser en may�scula y min�scula. Entonces, .12E+2 es inv�lido, pero 12.E+2 es v�lido. Ceros al inicio de la mantisa y el exponente son permitidos.

			Una constante string o cadena de caracteres es una secuencia de caracteres encerrada por comillas dobles ��. Los strings pueden contener cualquier car�cter excepto una l�nea nueva, doble comilla o car�cter nulo. Una constante string debe comenzar y finalizar en una misma l�nea, y no puede partirse en l�neas m�ltiples. Ejemplos:

				"Est� es una cadena de caracteres que no tiene su doble comilla
				Esta no es parte de la cadena de arriba // Todos deben ser ids
			
		Operadores y caracteres de puntuaci�n:
			+ - * / % < <= > >= = == != && || ! ; , . [ ] ( ) { } [] () {}
			Note que [, ], y [] son tres tokens diferentes y que para el operador [], al igual que los otros dos operadores de dos caracteres, no debe haber ning�n espacio en blanco entre los dos caracteres.
	
	Manejo de errores
		Los errores que el escaner puede reportar son:
			a. Cadenas de caracteres no v�lidas, debe mostrar un mensaje de car�cter no reconocido y el car�cter en cuesti�n. Debe continuar con el an�lisis con el siguiente car�cter.
			b. Strings sin terminar (sin comilla doble de cierre), debe reportarlos como cadena sin terminar y continuar el an�lisis en la l�nea siguiente.
			c. Identificadores de longitud no permitida y caracteres inv�lidos. Si un identificador es m�s grande que el m�ximo aceptado, muestre un mensaje de error truncando el identificador con los primeros 31 caracteres (descartando el resto), y continuar el escaneo. Si la cadena contiene caracteres no v�lidos (por ejemplo, un car�cter nulo), debe reportar que la cadena tiene el car�cter nulo que no es v�lido. En cualquier caso, debe continuar el an�lisis con el car�cter siguiente a esa cadena. El fin de cadena ser� la nueva l�nea o la comilla doble de cierre.
			d. Si un comentario se queda abierto cuando se encuentre el final del archivo, reportar el error con el mensaje �EOF en comentario". Su esc�ner no deber�a analizar este �ltimo comentario y su contenido. De igual forma para las cadenas, si el final del archivo se encuentra antes de unas comillas de cierre, reportarlo como error �EOF en una cadena�.
			e. Si se escanea un �*/� fuera de un comentario, reportarlo como fin de comentario sin emparejar, en lugar de reconocer los tokens �*� y �/�.