CREATE TABLE IF NOT EXISTS log(
                    id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                    Fecha DATE NOT NULL,
                    Hora TIME NOT NULL,
                    mensaje TEXT NOT NULL
)