"""'os' module is used to get the path to the databases files"""
import os
import sqlite3
from tkinter import filedialog
from datetime import datetime
from flaskwebgui import FlaskUI
from flask import Flask, render_template, redirect, url_for, request, g
from code_search import code_selector
from database_file_creation import FileCreation


app = Flask(__name__)
app.secret_key = '54654*adqweqdasdsd_ad32432?#'
ui = FlaskUI(app) # Para ejecutar la app como una GUI.

class DatabaseFile:
    '''This class is only used to define and store the variables for the code.'''
    #File Name
    Name = ""
    #Path to the file
    Path = ""
    #List of comments, this lists will be loaded in de database.
    Comment_list = []


# Funciones fuera de Flask ======================

def get_db(path_to_db):
    '''
    Load of the database based on the path stored in DatabaseFile class.
    '''
    db = getattr(g, "_database", None)
    if 'db' not in g:
        db = g.db = sqlite3.connect(path_to_db)
        g.db.row_factory = sqlite3.Row
    return db

@app.teardown_appcontext
def close_connection(exception):
    '''
    A close function used by Flask.
    '''
    db = getattr(g, '_database', None)
    if db is not None:
        db.close()

def exist_db():
    '''
    Make a comprobation if the file already exist.
    '''

    if not os.path.isfile(DatabaseFile.Path):
        return None

    existent_db = get_db(DatabaseFile.Path)
    return existent_db

def custom_list(message):
    '''
    This function is utilized to format the outcoming message in the web interface,
    load the message, date and time in the database.
    '''

    loaded_db = exist_db()

    if loaded_db is None:
        empty_list = []
        return empty_list

    time_now = datetime.now()
    date_key = time_now.strftime("%Y-%m-%d") #%d-%m-%Y
    time_key = time_now.strftime("%H:%M:%S")
    cursor_db = loaded_db.cursor()
    data_log = "INSERT INTO Log(Fecha, Hora, mensaje) VALUES(?,?,?)" # (id, Fecha, Hora, mensaje)
    cursor_db.execute(data_log, [date_key, time_key, message])
    loaded_db.commit()
    comment = date_key + " ~ " + time_key + " >> " + message
    DatabaseFile.Comment_list.append(comment)
    loaded_db.close()
    return reversed(DatabaseFile.Comment_list)
     #La función 'reversed' se usa para que en la página se muestre la última entrada primero.

#================================================

# Funciones de Flask ============================
#   -INDEX-
@app.route("/", methods=['POST', 'GET'])
def index():
    '''
    In the index function is were the message is passed for process and load into the database.
    When 'POST' the program takes the input message and pass it to another function to process it.
    '''

    if request.method == 'POST':
        input_message = request.form["input_ta"]
        lista_salida = custom_list(input_message)
        list_out = str("\n\r".join(lista_salida))
        print(lista_salida)
        return render_template('index.html', item_list = list_out )

    return render_template('index.html')

#   -SEARCH-
@app.route("/search", methods=['POST', 'GET'])
def search():
    '''
    This function obtains four parameters from the frontend needed 
    for the search and pass this parameters to a module that returns
    the query and the parameters used.
    '''

    if request.method == 'POST':
        db = exist_db()
        cursor_db = db.cursor()
        param_f_i = request.form['fecha_desde']
        param_f_f = request.form['fecha_hasta']
        param_h_i = request.form['hora_desde']
        param_h_f = request.form['hora_hasta']
        data_list = [param_h_f, param_h_i, param_f_f, param_f_i]
        # Uso el generador de código del módulo. Devuelve la lista de parámetros y la consulta.
        returned_list, sql_query = code_selector(data_list)
        select_from_db = cursor_db.execute(sql_query, returned_list) # Ejecuto la sentencia SQL
        info_db = select_from_db.fetchall()
        db.close()
        return render_template('search_results.html', items = info_db)
    return render_template('search.html', data=None)

#   -OPEN-
@app.route("/open", methods=['POST', 'GET'])
def open_resources():
    '''
    The function obtains the path to the database and stores it in the class attribute. 
    '''
    temp_file_instance = filedialog.askopenfilename()
    DatabaseFile.Path = temp_file_instance
    return redirect(url_for('index'))

#   -CREATE-
@app.route("/create", methods=['GET', 'POST'])
def create():
    '''
    The function will create a new database file with the appropiate scheme for use.
    this is made in a app context.
    '''
    if request.method == 'POST':
        new_file_name = request.form['new_filename']
        creation_instance = FileCreation(new_file_name)
        creation_instance()
        message = "archivo creado"
        print(message)

    return redirect(url_for('index'))
# ================================================

if __name__ == '__main__':
    #app.run()
    FlaskUI(
        app = app,
        server = "flask",
        browser_path = "/usr/bin/chromium",
        width = 550,
        height = 920
    ).run()
   # ui.run()
