from flask import Flask, render_template, redirect, url_for, request, g
#from flaskwebgui import FlaskUI
from tkinter import filedialog
from datetime import datetime
import sqlite3, json, click
import os
from code_search_test import code_selector as cs

app = Flask(__name__)
app.secret_key = '54654*adqweqdasdsd_ad32432?#'
#ui = FlaskUI(app) # Para ejecutar la app como una GUI.
transferList = []


# Funciones fuera de Flask ======================

# Inicialización de base de datos.
# Voy a usar la plantilla por defecto de la documentación de Flask.
def get_db(path_to_db):
    db = getattr(g, "_database", None)
    if 'db' not in g:
        db = g.db = sqlite3.connect(path_to_db) # dbLog.db; 'test_data.db'
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
        #Cargo el esquema de la base de datos.
        with app.open_resource('dbLogSchema.sql') as f: 
            db.executescript(f.read().decode('utf8'))
        db.commit()
    return db

# Comprobador de existencia. se fija si la base de datos existe, si lo hace no utiliza 'init_db'
def exist_db(filename_path):
    #ruta = os.getcwd() + "/test_data.db" # /dbLog.db

    if not os.path.isfile(filename_path):
        recently_created_db = init_db()
        print('se activó init_db')
        return recently_created_db  
    else: 
        already_existent_db = get_db(filename_path)
        print('se activo get_db')
       # print(f'En exist_db se utiliza la ruta: {db_filename}')
        return already_existent_db
              
# Formato de mensaje y guardado en DB.
def custom_list(message):

    print(filename)
    transfer_db = exist_db(filename)
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

    return reversed(transferList) #La función 'reversed' se usa para que en la página se muestre la última entrada primero.

#================================================
# Funciones de Flask ============================
#   -INDEX-
@app.route("/", methods=['POST', 'GET']) 
def index():

    if request.method == 'POST':
        input_message = request.form["input_ta"] 
        lista_salida = custom_list(input_message)
        list_out = str(f"\n\r".join(lista_salida))
        print(lista_salida)
        return render_template('index.html', item_list = list_out)
    else:
        error = 'mensaje d error 1'
        return render_template('index.html')

#   -SEARCH-
@app.route("/search", methods=['POST', 'GET'])
def search():
    if request.method == 'POST':
        db = exist_db(filename)
        cursor_db = db.cursor()
        Param_F_i = request.form['fecha_desde']
        Param_F_f = request.form['fecha_hasta']
        Param_H_i = request.form['hora_desde']
        Param_H_f = request.form['hora_hasta']
        data_list = [Param_H_f, Param_H_i, Param_F_f, Param_F_i]

        #print(f'\n_ Los parámetros obtenidos son:\n {Param_F_i}, {Param_F_f}, {Param_H_i}, {Param_H_f}\n') #for debug        
        # Busqueda en BD utilizando el módulo de busqueda code_search_tes.py ===
        returned_list, sql_query = cs(data_list) # Uso el generador de código del módulo. Devuelve la lista de parámetros y la consulta.
        select_from_db = cursor_db.execute(sql_query, returned_list) # Ejecuto la sentencia SQL
        info_db = select_from_db.fetchall() 

        print(f'\n_ La lista devuelta desde el generador de código es:\n {returned_list}') #for debug
        print(f'\n_ La sentencia devuelta desde el generador de código es:\n {sql_query}\n') #for debug
        print(info_db) #for debug
        db.close()
        return render_template('search_results.html', items = info_db)
    else:
        return render_template('search.html', data=None)
    
#   -OPEN-
@app.route("/open", methods=['POST', 'GET'])
def open():

    global filename
    filename = filedialog.askopenfilename()
    print(filename)
    return redirect(url_for('index'))

#   -SAVE-
@app.route("/save", methods=['GET'])
def save():
    filedialog.asksaveasfilename()
    return redirect(url_for('index'))
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
