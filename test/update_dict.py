from datetime import datetime

d={}
x=True
y=1
while x == True:
    for k in range(y):
        ahora = datetime.now()
        fechaHora = ahora.strftime("%d/%m/%y-l%H:%M:%S")
        mensaje =input(f'\nIndique el mensaje: ')
        d.update({fechaHora: mensaje})
        print(d)
    x = False
