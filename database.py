import psycopg2
from psycopg2 import Error


class Database:
    def __init__(self, db_name, username, password, host, port):
        self.db_name = db_name
        self.username = username
        self.password = password
        self.host = host
        self.port = port
        self.connection = None

    def connect(self):
        try:
            self.connection = psycopg2.connect(
                database=self.db_name,
                user=self.username,
                password=self.password,
                host=self.host,
                port=self.port
            )
            print("Создано подключение к базе данных PostgreSQL")
        except (Exception, Error) as error:
            print("Ошибка при подключении к базе данных PostgreSQL:", error)

    def execute_insert(self, query):
        cursor = self.connection.cursor()
        try:
            cursor.execute(query)
            self.connection.commit()
        except (Exception, Error) as error:
            self.connection.rollback()
            print("Ошибка при выполнении запроса:", error)
        cursor.close()

    def execute_select(self, query):
        result = ""
        cursor = self.connection.cursor()
        try:
            cursor.execute(query)
            self.connection.commit()
            result = cursor.fetchall()
            return result
        except (Exception, Error) as error:
            self.connection.rollback()
            print("Ошибка при выполнении запроса:", error)
        cursor.close()

    def close_connection(self):
        if self.connection:
            self.connection.close()
