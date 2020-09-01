import flask
from flask import request
import json
import random
import sys
import db_handler
from datetime import datetime


app = flask.Flask(__name__)
app.config["DEBUG"] = True

DATABASE = []
ADMIN_KEY = "qwerty"
db_handle = db_handler.db_handler()


@app.route('/api/register', methods=['POST', 'DELETE'])
def registration():
    try:
        js = request.json
        if js['admin_key'] != ADMIN_KEY:
            return json.dumps({'response': "WRONG KEY"}), 404

        new_js = {}

        if request.method == 'POST':
            new_id = random.randint(0, 65534)
            while True:
                for record in DATABASE:
                    if record['id'] == new_id:
                        new_id = random.randint(0, 65534)
                        continue
                break
            new_js['id'] = new_id
            new_js['room'] = js['room']
            new_js['position'] = js['position']
            new_js['response'] = 'OK'
            DATABASE.append(new_js)
            return json.dumps(new_js), 201

        if request.method == 'DELETE':
            for record in DATABASE:
                if record['id'] == js['id']:
                    DATABASE.remove(record)
                    return json.dumps({'response': "OK"}), 200
            return json.dumps({'response': "ID NOT FOUND"}), 404
    except:
        return json.dumps({'response': "INTERNAL SERVER ERROR"}), 500


@app.route('/api/attendance', methods=['POST'])
def attend():
    try:
        js = request.json

        for record in DATABASE:
            if record['id'] == js['id']:
                print('Student ' + str(js['student_id']) + ' reports presence!')
                """now = datetime.now()
                dt_string = now.strftime("%Y-%m-%d %H:%M:%S")
                db_handle.execute_modify(
                    "declare @var1 int "
                    "set @var1 = (select id "
                    "from Sale "
                    "where nazwa='" + record['room'] + "')"
                    "declare @var2 int "
                    "set @var2 = (select id " 
                    "from Zajecia "
                    "where (" + dt_string + " between rozpoczecie and zakonczenie) "
                    "and (id_sali=@var1))"
                    "insert into Obecnosci values (" + js['student_id'] + ", @var2, 1, 0))"
                )"""
                return json.dumps({'response': "OK"}), 201
        return json.dumps({'response': "ID NOT FOUND"}), 404
    except:
        return json.dumps({'response': "INTERNAL SERVER ERROR"}), 500


@app.route('/api/info', methods=['GET'])
def getinfo():
    try:
        js = request.json

        for record in DATABASE:
            if record['id'] == js['id']:
                """now = datetime.now()
                dt_string = now.strftime("%Y-%m-%d %H:%M:%S")
                result = db_handle.execute_query(
                    "declare @var1 int "
                    "set @var1 = (select id "
                    "from Sale "
                    "where nazwa='" + record['room'] + "')"
                    "select pro.imie, pro.nazwisko, prz.nazwa "
                    "from Zajecia as z, Grupy as g, Przedmioty as prz, Prowadzacy as pro "
                    "where (" + dt_string + " between z.rozpoczecie and z.zakonczenie) and (z.id_sali=@var1) "
                    "and (g.id=z.id_grupy) and (prz.id=g.id_przedmiotu) and (pro.id=g.id_prowadzacego)"
                )"""
                # TODO sprawdz format wyniku query
                return json.dumps({'response': "OK"}), 201
        return json.dumps({'response': "ID NOT FOUND"}), 404
    except:
        return json.dumps({'response': "INTERNAL SERVER ERROR"}), 500


@app.route('/api/getall', methods=['GET'])
def getall():
    return json.dumps(DATABASE), 200


if __name__ == '__main__':
    if len(sys.argv) > 1:
        ADMIN_KEY = sys.argv[1]
    app.run()
