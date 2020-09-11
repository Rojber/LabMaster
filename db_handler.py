import pyodbc


class db_handler(object):
    def __init__(self, conn_string):
        self.connection_string = conn_string

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
        affected_rows = cursor.rowcount
        conn.commit()
        cursor.cancel()
        conn.close()
        if affected_rows > 0:
            return True
        return False
