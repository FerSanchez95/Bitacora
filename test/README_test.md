# Notas de la bitacora

## Problema mayor: __init__ sería el main

No se siguio el tutorial donde describía como crear el indice de directorios  de archivos.

## **Decorators (decoradores)**

Un decorador es una referencia a una función en otra parte del cuerpo del código. Un decorador es una funcion de primera clase lo que le da la posibilidad de vincularse con una varible, ser utilizado como un argumento e inclusive ser retronada. A su vez utiliza las funciones como argumentos y devuelve la referencia de la posición de memoria en las cuales esas funciones estan alojadas.
Un decorador apunta a una función del mismo nombre dentro del código. Esto lo hace mediante el prefijo '*@*' y el nombre de la funcion a la que se apunta (¿Funciona como un puntero?). También evita que una función tenga que ser enlazada con una variable para ser llamada.
Los decoradores puede ser llamados desde otros archivos importandolos dentro del código.

    from decoradores import suma_decorador

Un decorador es una función envolvente que esta construida por un mínimo de 2 funciones, una función madre y por lo menos, una función hija.

    def decorador():
        def funcion_hija():
            pass
        return funcion_hija

En la última linea el *retrun* devuelve la función hija pero __NO__ la devuelve como una función en si misma funcional, lo que hace es devolver la referencia de la función hija. Imprimir esa devolución mostraría la posicion de memoria que ocupa dicha función.

Eg.1:

    def decorador(i):
        def decorador_hijo():
            if i > 5:
                return 'Ejecutando propiedad adicional'
            else:
                pass
        retrun decorador_hijo

    def propiedades(prop):
        prop = 6
        return prop
    
    variable = decorador(propiedades)

En este ejemplo (Eg.1) la función del decorador tiene que tener enlazada una variable que pueda ser llamada. Como se ve en el código la función '*propiedades*' es pasada como un argumento. En cambio, en el ejemplo de abajo (Eg.2) se puede ver como la varable esta ligada (mediante el prefijo *@*) a la función que la modifica por el puntero correspondiente a la misma.

Eg.2:

    def decorador(i):
        def decorador_hijo():
            if i > 5:
                return 'Ejecutando propiedad adicional'
            else:
                pass
        retrun decorador_hijo

    @decorador
    def propiedades(prop):
        prop = 6
        return prop

EL objetivo de un decorador es agregar funcionalidad al código sin tener que modificar sus funciones principales. Esta pensado para gregar caracteristicas a funciones ya exitentes sin alterarlas.

Recursos:

1. <https://codigofacilito.com/articulos/decoradores-python>
2. <https://realpython.com/primer-on-python-decorators/>

### __Debug mode__

Puedo correr el servidor en modo 'debug', esto hace que
el servidor se actualice con cada cambio de codigo.
Es un modo interactivo de ver los cambios a medida que
se va formantenado el código. Los cambios se ven reflejados
cuando se refresca la página en el navegador.
Para activar esta opción uso el comando:

    flask --app app.py run --debug

En este caso precisamente la instrucción es:

    flask --app main_test.py run --debug

### ¿Cómo funciona un ataque de inyección y cómo funciona la función '*escape()*' de flask?¿Es automática esta función?

La función '*escape()*' logra hacer que los campos que quedan abiertos a los usuarios finales no sean usados para iniciar ataques de inyección, ya sea por insyecciónde código, inyeeección SQL, inyección de comandos u otro tipo de ataque que se aproveche de esas vulnerabilidades.

### Flaskwebgui

Flaskweb gui es una librería a parte de Flask que esta hecha para poder ejecutar las webs apps como guis.
Solo funciona con el motor de renderizado de Chrome. Usar Chromium tambien sirve.

Primero se tiene que instalar la librería:

    pip install flaskwebgui

Para poder uarlo es necesario instalar o tener instalado Google chrome o Chromium.
En el caso de Chromium:
    sudo apt install chromium-browser

Una vez instalados estos dos elementos se debe importar la librería en código principal.

    from flaskwebgui import FlaskUI

Agregar al final del código lo que va a ligar la app, que ya esta ligada a flask, y decirle que la ejecute en una gui.

    FlaskUI(app=app, server="Flask").run()

Se pueden agregar opciones como la altura y el ancho de la pantalla como el navegador(siempre que este basado en Chrome).

    FlaskUI(app=app,
            server="Flask",
            browser_path="/usr/bin/chromium",
            widht=640,
            hieght=480)

## Método POST

Para obtener un dato mediante el método POST uso la instrucción 'request.form['X']' (X= Nombre de la etiqueta html que contiene el dato requerido).

Eg.3:
        data = request.form['name']
        print(data)

Un diccionario es utilizado par apasar los datos desde el área de texto de entrada al de salida.
Se actualiza de manera sencilla el diccionario de traspaso usando las clave 'dict.update({key: value})'.

    >>>d={}
    >>>d.update({new_key: new_value})
    >>>print(d)

    Output
    >>> {new_key: new_value}

## DATABASE (Funcionando)

En la documentación de flask sobre base de datos '<https://flask.palletsprojects.com/en/2.3.x/tutorial/database/>' dice que hay que crear un archivo llamado 'db.py' que contenga el ejemplo básico que se muestra en dicha página. Pero además de eso, para que flask lo use tiene que estar registrado dentro del archivo '__init__.py' que se encuentra en el mismo directorio.
En el siguiente enlace: '<https://flask.palletsprojects.com/en/2.3.x/patterns/sqlite3/>', al final muestra las importación de la función 'init_db' desde un archivo 'x' llamado 'yourapplication'.

    >>> from yourapplication import init_db
    >>> init_db()

Sabiendo esto puedo hacer el esquema SQL y el archivo 'db.py' y importandolos directamente en el 'main'. En el caso que no resulte voy a pasar a realizar los pasos descriptos en el primer enlace.

Todo estaba mal porque no segui bien el tutorial de creación. Pero ahora agregue todo  el código de del archivo 'db' al 'flaskMain' para ver si funciona.

Usando la manera en como esta descrito en el segundo enlace funciono.

Para comprobar que el archivo DB existe puedo utilizar la librería OS. Una funcion que puedo usar para copletar la ruta es: 'os.getcwd() para terminar de completar la ruta hasta el DB.

    import os
    ruta = os.getcwd() + "/" + nombreArchivo
    if not ruta.isFile():
        init_db()
        falsh(f'Creando DB: {nombreArchivo})
    else: 
        get_db()
        flash(f'Cargando DB: {nombreArchivo})

Por motivos de prueba nombreArchivo será el nombre que esta ahora como base de datos. Pero cuando termine el login las bases de datos van a aestar separadas por el nombre de ususario asi cada uno tien una propia.

En la Función custom_list, ademas de preparar las variables para la impresión en la página se guardan dichas variables en la base de datos.

Para la base de datos se siguió el procedimiento que se muestra en el tutorial de Flask. Para evitar fallos se copió el codigo del tutorial. La base de datos se crea y se abre correctamente.

## Busqueda por fecha y hora (Funcionando)

Para realizar la busqueda por fecha y por hora estoy construyendo una sentencia en SQL. Para poder obtener resultados de busqueda dentro de un rango epecificado se crea una tabla de verdad con los cuatro parámetros de entrada, según estos devuelvan un valor o no y dependiendo de la combinación, se le asignará una condición de busqueda.
En la tabla de la base de datos que se muestra a continuación, 'Fecha' y 'Hora' son las columnas donde se guardan los datos por lo cuales se realiza el filtrado.

    log(
        id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
        Fecha DATE NOT NULL,
        Hora TIME NOT NULL,
        mensaje TEXT NOT NULL
    )

|  Hora Final (H-f) | Hora Inicio (H-i) | Fecha Final (F-f) | Fecha Inicio (F-i) | Condición |
| --- | --- | --- | --- | --- |
| 0 | 0 | 0 | 0 | *ESP* |
| 0 | 0 | 0 | 1 | *1* |
| 0 | 0 | 1 | 0 | *2* |
| 0 | 0 | 1 | 1 | *3* |
| 0 | 1 | 0 | 0 | *4* |
| 0 | 1 | 0 | 1 | *5* |
| 0 | 1 | 1 | 0 | *6* |
| 0 | 1 | 1 | 1 | *7* |
| 1 | 0 | 0 | 0 | *8* |
| 1 | 0 | 0 | 1 | *9* |
| 1 | 0 | 1 | 0 | *10* |
| 1 | 0 | 1 | 1 | *11* |
| 1 | 1 | 0 | 0 | *12* |
| 1 | 1 | 0 | 1 | *13* |
| 1 | 1 | 1 | 0 | *14* |
| 1 | 1 | 1 | 1 | *15* |

<sub> 1 = valor ingrasado; 0= Valor vacío; Fecha Inicio -> Param_F_i;Fecha Final -> Param_F_f; Hora Inicio -> Param_H_i; Hora Final => Param_H_f.</sub>

### Lista de condiciones

0. (ESP). Si no se ingresa ningún valor y ser eraliza la busqueda el resultado será la impresión de toda la BD.

    SELECT fechaHora, mensaje FROM log

1. Si se ingresa una fecha de inicio, se filtrará la busqueda para todos los datos __a partir__ de esa fecha.

    SELECT Fecha, Hora, mensaje FROM log WHERE Fecha >= *Param_F_i*

2. Si se ingresa una fecha de final, se filtrará la busqueda para todos los datos __hasta__ esa fecha.

    SELECT Fecha, Hora, mensaje FROM log WHERE  Fecha <= *Param_F_i*

3. Si se ingresan dos fechas, se filtrará la busqueda a partir de la primer entrada del primer día hasta la la última entrada del segundo día.

    SELECT Fecha, Hora, mensaje FROM log WHERE Fecha BETWEEN  *Param_F_i* AND *Param_F_f*

4. Si se ingresa solo una hora, se filtrará la busqueda para todos los datos desde esa hora especifica **sin importar la fecha.**

    SELECT Fecha, Hora, mensaje FROM log WHERE Hora >= *Param_H_i*

5. Si se ingresa una fecha y una hora, se filtrará la busqueda para todos los datos durante ese mismo día y __a partir de la hora ingresada__, si es una hora de inicio. En el caso en que la hora ingresada sea una hora de final se filtrarán los datos en la misma fecha pero con la diferencia que será __hasta la hora ingresada__.

   . SELECT Fecha, Hora, mensaje FROM log WHERE Fecha = *Param_F_i* OR Fecha = *Param_F_f* AND Hora >= *Param_H_i* OR Hora <= *Param_H_f*

6. No es posible esta combinación.

7. Si se ingresan dos fechas (inicio y final) y una hora de inicio, se filtrará la busqueda para los datos entre las fechas ingresadas a partir de la hora ingresada.

    SELECT Fecha, Hora, mensaje FROM log WHERE Fecha BETWEEN *Param_F_i* AND *Param_F_f* AND Hora >= *Param_H_i*

8. Si se ingresa solo una hora, se filtrará la busqueda para todos los datos hasta esa hora especifica __sin importar la fecha.__

    SELECT Fecha, Hora, mensaje FROM log WHERE Hora <=*Param_H_f*

9. Si se ingresa la fecha de inicio y la hora de final,  se filtrarán los datos desde la primera entrada de la fecha ingresada hasta la hora de final.

    SELECT Fecha, Hora, mensaje FROM log WHERE Fecha = *Param_F_i* AND Hora <= *Param_H_f*

10. Idem. punto 5.

11. Si se ingresan dos fechas (inicio y final) y una hora de final, se filtrará la busqueda para los datos entre las fechas ingresadas desde la primer entrada hasta la hora de final de cada día.

    SELECT Fecha, Hora, mensaje FROM log WHERE Fecha BETWEEN *Param_F_i* AND *Param_F_f* AND Hora <= *Param_H_f*

12. Si se ingresan solo dos horas (inicio y final), se filtrarán los datos del último día entre las horas ingresadas.

    SELECT __*__ FROM log WHERE Hora BETWEEN *Param_H_i* AND *Param_H_f*

13. Si se ingresan solo dos horas (inicio y final) y una fecha, se filtrarán los datos del día ingresado entre las horas ingresadas.

    SELECT __*__ FROM log WHERE Fecha = *Param_F_i* OR Fecha = *Param_F_f* AND Hora BETWEEN *Param_H_i* AND *Param_H_f*

14. Idem. punto 13.

15. Si se ingresan dos fechas y dos horas, se filtrarán los datos de la busqueda entre las fechas ingresadas, entre las horas ingresadas.

    SELECT __*__ FROM log WHERE Fecha BETWEEN *Param_F_i* AND *Param_F_f* AND Hora BETWEEN *Param_H_i* AND *Param_H_f*

Las sentencias que tienen fechas y horas no funcionan como había pensado más arriba. Si yo le paso dos fechas y una hora como en la condción N°7 la DB va a devolver todos los datos a partir de la hora ingresada en el plazo de días ingresados.
Cambio en las llamadas a los elementos de la tabla (Fecha, Hora, mensaje) dentro de las sentencias por el signo '*' para que abarque todos los elementos dentro de la tabla.

Los parámetros de las diferentes sentencias tienen que estar en la misma posición en la que ingresan a la función de selección. Deben respetar un orden único para poder ser devueltos y utilizados de manera correcta. El orden de la lista es el siguinete: Lista_de_prámetros = [fecha_inicial, fecha_final, hora_inicial, hora_final]

¿El problema estará en la instancia vinculada a la función fetchall?
Voy a probar vincula fetchall a 'items' directamente en el retrun.
No se puede porque antes del 'retrun' se cierrra la db.
Se prueba la función 'fetchmany' en reemplazo de 'fetchall'. Funciona pero hayq ue especificar con el parámetro 'size' la cantida de indices a imprimir.
El problema era que las fechas se ingresaban al revés entonces la consulta no devolvía nada.
Se vuelve a reemplazar 'fetchmany' por 'fetchall'. Este último funciona e imprime todas las entradas en la db.

Al momento de mostrar los resultados de la busqueda, la tabla sigue sin mostrar las fechas. Supongo que debe ser un problema de el tipo de dato sobre el cual esta contruida esa columna de la db. El tipo de dato es 'DATETIME' y tal vez debeía ser solo 'DATE'.
Tengo que encontrar la manera de cambiar el tipo de dato sin tener que empezar otra db.

Cambie el tipo de dato de la comlumna 'Fecha' de DATETIME a DATE usando el software DB Browser for SQLite. Además el problema de que no se mostraba la fecha era un error de tipeo en el archivo HTML correspondiente a la página de resultados de busqueda. Al momento de designar los items correspondientes a la cada celda en la cual el valor de la db se vería impreso quedó mal escrito el nombre correspondiente para la fecha. En vez de ser: {{ item.fecha }} estaba escrito {{ item.decha }}.
Ahora si se muestran las fechas en la página de resultados de busqueda.

Para probar las sentencias Escritas mas arriba voy a usar DB Browser junto con los datos de la db de prueba.

Se modifican las sentencias 4 y 8. La sentencia cuatro buscará todos los horarios desde la hora ingresada en adelante y la sentencia 8 buscara todos los horarios desde las 00 hasta el horario indicado.

Para el refactoriazdo reordenar los parámetros correctamente.

Las semtemcias 5 y 6 quedan juntas, al igual que las 9 y 10 y las 13 y 14.

Las pruebas en de las sentencias en DB Browser fueron correctas. Cada sentencia realiza su acción correspondiente. Al momento de probarlas en el programa también funcionan. El archivo de acción de busqueda también funciona como era esperado.

## Devolucion de sentencia según los datos ingresados

Hacer que las sentencias de busqueda sean ejecutadas de manera correcta necesita que sean elegidas por los argumentos que se pasan desde el Frontend. Es decir, si se ingresan una fecha inicial y una hora inicial se debería ejecutar la sentencia SQL correspondiente al uso de esas dos variables, en este caso la sentencia es la número 5. Basado en un código binario, en cual está basada la tabla de verdad, se realizará la busqueda de la sentencia. La función va a retornar la sentencia SQL y la lista de parámetros a ser ejecutados.
Para lograr esto se creá una funcion que toma los cuatro valores que ingresan desde el front como una lista. Esta lista pasa por una función que devuelve otra lista conformada por un código binario con una correspondencia de '1' si el valor de la lista tiene un string y '0' si el valor llega vacío o como un None.

A continuacón se ve el código de prueba:

    Pram_List = [Param_1, Param_2, Param_3, Param_4]

    def transform(list_item):
        if list_item == None:
            return 0
        else:
            return 1

    Code_mapping = list(map(funcion, Param_List))
    Search_code = ''.join(Code_mapping)
    print(Search_code)

'print(Search_code)' va a retornar una lista con los valores del código de busqueda.

Los parámetros devueltos por el 'request.form[]' son strings, por lo tanto, si se devuelven vacíos no se puede usar la comparación actual del condicional 'if' de la función 'transform'. Por lo tanto, sabiendo que las respuestas por parte del 'request.form[]'serán strings puedo utilizar la longitud del string para el condicional. Entonces el código quedará de la siguiente manera:

    def transform(list_item):

        if len(list_item) == 0:
            val = "0"
        else:
            val = "1"

        return val

Una vez que tengo el código de busqueda, uso la función 'match-case' que esta disponible a partir de la versión 3.10 de python.La declaración 'match' permite parasar una función o un clase y evaluar casos dependiendo de lo que devuelva la función o la clase.
Ese es su funcionamiento básico, si bien tiene otras opciones más avanzadas voy a seguir con este comportamiento ya que es, en mi opinion es el adecuado para complementar el código. Para más informacion de la sintáxis de la declaración 'match': <https://peps.python.org/pep-0636/>

El siguiente ejemplo corresponde al boceto que iría en el código de pruebas:

    match transform():
        case "Códgo de busqueda 1":
            return "Sentencia 1"
        case "Codigo de busqueda 2":
            return "Sentencia 1"
        case "Código  de busqueda n":
            return "Sentencia n"
        case _:
            return "No se encuantra la sentencia para dicha combinación"

(Copiar lo que esta escrito en el cuaderno con fecha del 10 de Junio hasta el 17 de Junio inclusive)

## Función de apertura

Para abrir un Archivo DB voy a utilizar losc omandos básicos de Tk. En este caso, a modo de prueba, utilizo la función 'filedialog.askopenfilename' para buscar y abrir el archivo correspondiente.
Primero importo la librería Tkinter y el método filedialog:

    from tkinter import filedialog 

Luego utilizo ese método junto con una de sus funciones uniondola a una instancia:

    open_file = filedialog.askopenfilename()

Esto va a abrir un cuadro de busqueda donde se va mostrar una lista de directorios desde el cual se puede navegar hasta llegar al archivo correspondiente. Como resultado esto devolvera la ruta del archivo seleccionado.
## Operador morsa __:=__