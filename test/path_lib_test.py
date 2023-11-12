from pathlib import Path
import pathlib
from PyQt6 import QtWidgets, uic, QFileSystemModel
import sys

class UI(QtWidgets.QMainWindow):
    def __init__(self, parent=None):
        super(UI, self).__init__(parent)
        uic.loadUi('Anotador.ui',self)
        self.celda1 = QtWidgets.QMainWindow()# inicio el tipo de ventana.
      #  self.ui_celda1 = Ui_Explorador() # identifico la UI con la clase que contiene la conf. de la ventana.
      #  self.ui_celda1.setupUi(self.celda1)#Cargo el setup de la ui.
        self.modelo = QFileSystemModel() #Cargo el modelo del explorador de arcchivos.
        self.modelo.setRootPath('/home/fernando/Programas/Anotador/tests/') #Indico que directorio tiene que ser mostrado en el modelo.
        self.ui_celda1.Explorador.setModel(self.modelo)
        self.ui_celda1.Explorador.setRootIndex(self.modelo.index('/home/fernando/Programas/Anotador/tests/'))
        self.show()
def main():
    UserInt = QtWidgets.QApplication(sys.argv)
    app = UI()
    UserInt.exec()
if __name__ == '__main__':
    main()
