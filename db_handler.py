import pyodbc


class db_handler(object):
    def __init__(self):
        self.connection_string = 'Driver={SQL Server};Server=.\KONRADKSQL;Database=TIP;Trusted_Connection=yes;'

    def execute_query(self, query):
        conn = pyodbc.connect(self.connection_string)
        cursor = conn.cursor()
        cursor.execute(query)
        data = cursor.fetchall()
        cursor.cancel()
        conn.close()
        return data

    def execute_modify(self, query):
        conn = pyodbc.connect(self.connection_string)
        cursor = conn.cursor()
        cursor.execute(query)
        conn.commit()
        cursor.cancel()
        conn.close()
