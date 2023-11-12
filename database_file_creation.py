'''
Works fine. Last test: 14/10/2023
'''

import os
import sqlite3

class FileCreation:
    '''
    This class creates and load the schema for the used database files. A name
    variable has to be pass from the main program.

    Functions:
    ----------
        __init__:
            Initializae the name, path and path to the SQL schema. The three variables 
            are strings, completed to create the needed name and paths.
        
        __call__:
            This function is used to run the method 'first_init' when the class is instanced
            and used. 
    
    Methods:
    --------
        creation_task:
            Creates a file wihtin the default directory given by the paht. 
            using 'open()' in the write mode (w) to create the file and '.close'
            to close it. The name of the file and the path is defined in the 
            '__ini__' method.
        
        first_init:
            This method loads the SQL schema for the new database file. Making use
            of the 'creation_task' method to crete a file whit the correct 
            extension and opening it like a database instance with sqlite3. Then open
            the schema using 'open()' in a 'wiht' instance and the '.executescript()'
            function to load it in the file.
    '''

    def __init__(self, name):
        self.name = name + ".db"
        self.path = os.getcwd() + "/resources/databases/" + self.name
        self.schema_path = os.getcwd() + "/resources/schemas/dbLogSchema.sql"

    def creation_task(self):
        '''
        Creates a file with the name that is passed by the user.
        '''
        new_database = open(self.path, "w", encoding="utf-8")
        new_database.close()

    def first_init(self):
        '''
        First initialization of the file. Creates the file using
        the method 'creation_task' and then load the SQL schema
        in it.
        '''

        self.creation_task()
        database_conn = sqlite3.connect(self.path)
        database_cursor = database_conn.cursor()

        with open(self.schema_path, "r", encoding="utf-8") as database_schema:
            database_cursor.executescript(database_schema.read())

        database_conn.commit()
        database_conn.close()

        return self.path


    def __call__(self):
        self.first_init()
