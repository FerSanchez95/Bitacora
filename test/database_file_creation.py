import os, sqlite3

class file_creation:

    def __init__(self, name) -> None:
        self.name = name
    
    def creation_task(self):
        self.name = self.name + ".db"
        self.path = os.getcwd() + "/resources/databases/"
        self.path_to_return = self.path + self.name
        self.new_database = open(self.path_to_return, "w")
        self.new_database.close()

        return self.path_to_return
    #PROBAR SI FUNCIONA. 17/09/2023
    def first_init(self):
        self.database_path = self.creation_task()
        self.database_conn = sqlite3.connect(self.database_path)
        self.databse_cursor = self.database_conn.cursor()
        self.schema_path = os.getcwd() + "/resources/schemas/dbLogSchema.sql"
        with open(self.schema_path) as self.database_schema:
            self.database_cursor.executescript(self.database_schema.read().decode('utf-8'))
        self.database_conn.commit()
        self.database_conn.close()

        return self.database_path


    def __call__(self):
        self.first_init()
