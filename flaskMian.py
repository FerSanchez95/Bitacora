from flask import Flask, render_template, redirect, url_for, request, g, flash
#from flaskwebgui import FlaskUI
from tkinter import filedialog
from datetime import datetime
import sqlite3, json, click
import os
from code_search_test import code_selector as cs

app = Flask(__name__)
app.secret_key = '54654*adqweqdasdsd_ad32432?#'
#ui = FlaskUI(app)
transferList = []
# Funciones fuera de Flask ======================

# Inicialización de base de datos.
# Voy a usar la plantilla por defecto de la documentación de Flask.
def get_db():
    db = getattr(g, "_database", None)
    if 'db' not in g:
        db = g.db = sqlite3.connect('test_data.db') # dbLog.db
        g.db.row_factory = sqlite3.Row
    return db

# Función de cierre
@app.teardown_appcontext
def close_connection(exception):
    db = getattr(g, '_database', None)
    if db is not None:
        db.close()

# Primera inicialización de la db.
# open.resource() carga el archivo SQL con los comandos de creación de tablas.
def init_db():
    with app.app_context():
        db = get_db()
        with app.open_resource('dbLogSchema.sql') as f:
            db.executescript(f.read().decode('utf8'))
        db.commit()
    return db

# Comprobador de existencia. se fija si la base de datos existe, si lo hace no utiliza 'init_db'
def exist_db():
    ruta = os.getcwd() + "/test_data.db" # /dbLog.db
    if not os.path.isfile(ruta):
        db = init_db()
        flash('Creando DB: dbLog.db')
        print('se activó init_db')
        return db
    else: 
        db = get_db()
        flash('Cargando DB: dbLog.db')
        print('se activo get_db')
        return db
              
# Formato de mensaje y guardado en DB.
def custom_list(message):
    transfer_db = exist_db()
    time_now = datetime.now()
    dateKey = time_now.strftime("%Y-%m-%d") #%d-%m-%Y 
    timeKey = time_now.strftime("%H:%M:%S")
    cursor_db = transfer_db.cursor()
    dataLog = f"INSERT INTO Log(Fecha, Hora, mensaje) VALUES(?,?,?)" # (id, Fecha, Hora, mensaje)
    cursor_db.execute(dataLog, [dateKey, timeKey, message])
    transfer_db.commit()    
    comment = dateKey + " ~ " + timeKey + " >> " + message
    transferList.append(comment)
    transfer_db.close()

    return reversed(transferList) 

#================================================
# Funciones de Flask ============================
#   -INDEX-
@app.route("/", methods=['POST', 'GET']) #¿Qué son los decoradores?
def index():

    if request.method == 'POST':
        input_message = request.form["input_ta"] 
        lista_salida = custom_list(input_message)
        list_out = str(f"\n\r".join(lista_salida))
        print(lista_salida)
        return render_template('index.html', item_list = list_out)
    else:
        error = 'mensaje d error 1'
        flash(error)
        return render_template('index.html')

#   -SEARCH-
@app.route("/search", methods=['POST', 'GET'])
def search():
    if request.method == 'POST':
        db = exist_db()
        cursor_db = db.cursor()
        Param_F_i = request.form['fecha_desde']
        Param_F_f = request.form['fecha_hasta']
        Param_H_i = request.form['hora_desde']
        Param_H_f = request.form['hora_hasta']
        #data_list = [Param_F_i, Param_F_f, Param_H_i, Param_H_f]
        data_list = [Param_H_f, Param_H_i, Param_F_f, Param_F_i]

        #print(f'\n_ Los parámetros obtenidos son:\n {Param_F_i}, {Param_F_f}, {Param_H_i}, {Param_H_f}\n') #for debug
        
        # busqueda en BD  normal =============================
        #sql_command = 'SELECT * FROM log WHERE Fecha BETWEEN ? AND ? AND Hora >= ?' # Funciona.
        #select_from_db = cursor_db.execute(sql_command, [Param_F_i, Param_F_f, Param_H_i])
        
        # Busqueda en BD utilizando el módulo de busqueda code_search_tes.py ===
        returned_list, sql_query = cs(data_list) # Uso el generador de código del módulo. Devuelve la lista de parámetros y la consulta.
        select_from_db = cursor_db.execute(sql_query, returned_list) # Ejecuto la sentencia SQL
        info_db = select_from_db.fetchall() 

        #print(f'\n_ La lista devuelta desde el generador de código es:\n {returned_list}') #for debug
        #print(f'\n_ La sentencia devuelta desde el generador de código es:\n {sql_query}\n') #for debug
        #print(info_db) #for debug
        db.close()
        return render_template('search_results.html', items = info_db)
    else:
        return render_template('search.html', data=None)
    
#   -OPEN-
@app.route("/open", methods=['GET'])
def open():
    filename = filedialog.askopenfilename()
    print(filename)
    # 'filename' devuelve la ruta del archivo seleccionado.
    # Puedo abrir ese archivo con sql e intentar imprimirlo 
    # en la 'textarea' que muestra los logs anteriores.
    # Importar la librería 'datetime' y guardarla en el 
    # comentario añadido en el JSON.
    # Guardar todos lo comentado en el textarea de salida.
    # Puedo generar un único archivo DB para usar siempre
    # en el mismo directorio.

    return redirect(url_for('index'))
#   -SAVE-
@app.route("/save", methods=['GET'])
def save():
    pass
# ================================================

if __name__ == '__main__':
    app.run(debug="True", port="5000")
   # FlaskUI(
    #    app=app,
    #    server="flask",
    #    browser_path="/usr/bin/chromium",
    #    width=640,
    #    height=480
    #).run()
    #ui.run()