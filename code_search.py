# Prueba declaración 'match', junto con selección de longitud ======

def code_generator(binary_list):
    
    if len(binary_list) == 0:
        list_item = "0"
    else:
        list_item = "1"
    
    return list_item

def mapping(datalist):
    
    code_mapping = list(map(code_generator, datalist))
    search_code = ''.join(code_mapping)
    #print(search_code) #for debug

    return search_code

def code_selector(param_list):
    #print('code_selector accionado') # for debug
    returning_list = [l for l in param_list if l != ""] #comprensión de listas.
    match mapping(param_list):
        case "0000": # -0/ESP-
            #FUNCIONA
            query_code = 'SELECT * FROM log' 

        case "0001": #-1-
            #FUNCIONA
            query_code = 'SELECT * FROM log WHERE Fecha >= ?' #Parámetros: Param_F_i.

        case "0010": #-2-
            #FUNCIONA
            query_code = 'SELECT * FROM log WHERE  Fecha <= ?' #Parámetros: Param_F_f.

        case "0011": #-3-
            #FUNCIONA
            query_code = 'SELECT * FROM log WHERE Fecha BETWEEN ? AND ?' #Parámetros: Param_F_i, Param_F_f.

        case "0100": #-4- 
            #FUNCIONA
            query_code = 'SELECT * FROM log WHERE Hora >= ?' #Parámetros: Param_H_i

        case "0101" | "0110": #-5-6-
            query_code = 'SELECT * FROM log WHERE Hora >= ? AND Fecha = ?' #Parámetros: Param_F_i, Param_F_f, Param_H_i, Param_H_f.
      
        #case "0110": #-6-
        #    query_code = 'No es posible computar la combinación'

        case "0111": #-7-
            #FUNCIONA
            query_code = 'SELECT * FROM log WHERE Hora >= ? AND Fecha BETWEEN ? AND ? ' #Parámetros: Param_F_i, Param_F_f, Param_H_i.

        case "1000": #-8- 
            #FUNCIONA
            query_code = 'SELECT * FROM log WHERE Hora <= ?' #Parámetros: Param_H_f.

        case "1001" | "1010": #-9-10-
            #FUNCIONA
            query_code = 'SELECT * FROM log WHERE Hora <= ? AND Fecha = ?'#Parámetros: Param_F_i, Param_H_f

        #case "1010": #-10-
        #    query_code = 'SELECT * FROM log WHERE Hora <= ? AND Fecha = ?'

        case "1011": #-11-
            #FUNCIONA
            query_code = 'SELECT * FROM log WHERE Fecha BETWEEN ? AND ? AND Hora <= ?' #Parámetros: Param_F_i, Param_F_f, Param_H_f. 

        case "1100": #-12-
            #FUNCIONA
            query_code = 'SELECT * FROM log WHERE Hora BETWEEN ? AND ?' #Parámetros: Param_H_i, Param_H_f.

        case "1101" | "1110": #-13-14-
            #FUNCIONA. Incluso ingresando las dos fechas.
            query_code = 'SELECT * FROM log WHERE Fecha = ? OR Fecha = ? AND Hora BETWEEN ? AND ?' #Parámetros: Param_F_i, Param_F_f, Param_H_i, Param_H_f.

        case "1111": #-15-
            #FUNCIONA
            query_code = 'SELECT * FROM log WHERE Hora BETWEEN ? AND ? AND Fecha BETWEEN ? AND ?' #Parámetros: Param_F_i, Param_F_f, Param_H_i, Para_H_f.

        case _:
            query_code = 'No se encuantra la sentencia para dicha combinación'
            
    #print(returning_list) #for debug
    #print(query_code) #for debug
    return returning_list, query_code


# En caso de que quiera ejecutar es script por si mismo utilizo la siguiente sentencia.
#if __name__ == '__main__':
    # La lista que ingresa lo hace con un orden único. Lista_de_prámetros = [hora_final, hora_inicial, fecha_final, fecha_inicial]
 #   param_list = ["", "", "2023/05/26", ""]
  #  code_selector()
            

# Prueba de seleccion por longitud del string. ============
# Funciona.

# Lista ejemplo.
#param_list = ["", "04:31", "", "2023/05/30"]
#
#def code_generator(list):
#    if len(list) == 0:
#        val = "0"
#    else:
#        val = "1"
#
#    return val
#
#def mapping():
#    code_mapping = list(map(code_generator, param_list))
#    search_code = ''.join(code_mapping)
#    print(type(search_code))
#    print(search_code)
#
#
#if __name__ == '__main__':
#    mapping()

# Prueba de armado de código ==============================

#code_list = ["0", "1", "0", "1"]
#code_mapping = list(map(str, code_list))
#search_code = ''.join(code_list)
#print(type(search_code))
#print(search_code)

# =========================================================