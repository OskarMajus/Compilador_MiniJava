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
			Un identificador es una secuencia de letras, dígitos y signo dólar. Puede comenzar con cualquiera excepto un número.
			
		Espacios en blanco
			El espacio en blanco (es decir, espacios, tabuladores y saltos de línea) sirve para separar tokens, pero por lo demás debe ser ignorado. Palabras clave y los identificadores deben estar separados por espacios en blanco, o por una señal de que no es ni una palabra ni un identificador.
				if ( 23 this se escanea como cuatro tokens, al igual que if(23this
				
		Comentarios
			Un comentario de una sola línea se inicia con // y se extiende hasta el final de la línea. Comentarios de varias líneas comienzan con /* y terminan con la primera */ posterior. Cualquier símbolo se permite en un comentario, excepto el de secuencia */ que pone fin al comentario actual. Los comentarios de varias líneas no se anidan.
			
			Si un archivo incluye un comentario sin terminar, el escáner debe informar de un error.
			
		Constantes
			Las constantes booleanas son: true o false.
			Una constante entera puede ser expresada en decimal (base 10) o en hexadecimal (Base 16).
				* Un entero decimal es una secuencia de dígitos decimales (0-9).
				* Un entero hexadecimal debe comenzar con 0X o 0x (es el carácter cero) y es seguida por una secuencia de dígitos hexadecimales. Los dígitos hexadecimales incluyen los dígitos hexadecimales y las letras a a la f (ya sea en minúsculas o mayúsculas).
				* Ejemplos de enteros válidos: 8,012,0X0,0x12aE.

			Una constante double es una secuencia de dígitos, un punto, seguido de una secuencia de dígitos, o nada. Así, .12 no es una constante de tipo double, pero 12. y 0.12 lo son. Una constante doble puede tener parte exponencial, por ejemplo, 12.2E+2. Para la constante doble en la notación científica el punto decimal es requerido, el signo del exponente es opcional (si no está especificado, + es asumido), y el E puede ser en mayúscula y minúscula. Entonces, .12E+2 es inválido, pero 12.E+2 es válido. Ceros al inicio de la mantisa y el exponente son permitidos.

			Una constante string o cadena de caracteres es una secuencia de caracteres encerrada por comillas dobles “”. Los strings pueden contener cualquier carácter excepto una línea nueva, doble comilla o carácter nulo. Una constante string debe comenzar y finalizar en una misma línea, y no puede partirse en líneas múltiples. Ejemplos:

				"Está es una cadena de caracteres que no tiene su doble comilla
				Esta no es parte de la cadena de arriba // Todos deben ser ids
			
		Operadores y caracteres de puntuación:
			+ - * / % < <= > >= = == != && || ! ; , . [ ] ( ) { } [] () {}
			Note que [, ], y [] son tres tokens diferentes y que para el operador [], al igual que los otros dos operadores de dos caracteres, no debe haber ningún espacio en blanco entre los dos caracteres.
	
	Manejo de errores
		Los errores que el escaner puede reportar son:
			a. Cadenas de caracteres no válidas, debe mostrar un mensaje de carácter no reconocido y el carácter en cuestión. Debe continuar con el análisis con el siguiente carácter.
			b. Strings sin terminar (sin comilla doble de cierre), debe reportarlos como cadena sin terminar y continuar el análisis en la línea siguiente.
			c. Identificadores de longitud no permitida y caracteres inválidos. Si un identificador es más grande que el máximo aceptado, muestre un mensaje de error truncando el identificador con los primeros 31 caracteres (descartando el resto), y continuar el escaneo. Si la cadena contiene caracteres no válidos (por ejemplo, un carácter nulo), debe reportar que la cadena tiene el carácter nulo que no es válido. En cualquier caso, debe continuar el análisis con el carácter siguiente a esa cadena. El fin de cadena será la nueva línea o la comilla doble de cierre.
			d. Si un comentario se queda abierto cuando se encuentre el final del archivo, reportar el error con el mensaje “EOF en comentario". Su escáner no debería analizar este último comentario y su contenido. De igual forma para las cadenas, si el final del archivo se encuentra antes de unas comillas de cierre, reportarlo como error “EOF en una cadena”.
			e. Si se escanea un “*/” fuera de un comentario, reportarlo como fin de comentario sin emparejar, en lugar de reconocer los tokens “*” y “/”.