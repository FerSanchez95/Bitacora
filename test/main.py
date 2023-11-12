from flask import Flask, render_template
from flaskwebgui import FlaskUI

app = Flask(__name__)
#ui = FlaskUI(app)

@app.route("/") #¿Qué son los decoradores?
def index():
    #'Names' no es un nombre que le pueda pasar al motor de plantillas jinja2.
    data={
            "titulo": "Inicio",
            "t_formulario": "Ingrese sus datos",
            "param1": "Nombre",
            "param2": "Apellido"
    }
    
    return render_template('new_index.html', data = data)

if __name__ == '__main__':
    #app()
    FlaskUI(
        app=app,
        server="flask",
        browser_path="/usr/bin/chromium",
        width=640,
        height=480
    ).run()
    #ui.run()