import flask
from flask import request
import json
import random

app = flask.Flask(__name__)
app.config["DEBUG"] = True

# TODO dodaj jakas baze danych
DATABASE = []


@app.route('/api/register', methods=['POST', 'DELETE'])
def registration():
    try:
        js = request.json
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
            DATABASE.append(new_js)
            return json.dumps(new_js), 201

        if request.method == 'DELETE':
            for record in DATABASE:
                if record['id'] == js['id']:
                    DATABASE.remove(record)
                    return "OK", 200
            return "ID NOT FOUND", 404
    except:
        return "INTERNAL SERVER ERROR", 500


@app.route('/api/attendance', methods=['POST'])
def attend():
    #try:
    js = request.json

    for record in DATABASE:
        if record['id'] == js['id']:
            print('Student ' + str(js['student_id']) + ' reports presence!')
            # TODO dodanie obecności studenta do bazy danych
            return "OK", 201
    #except:
    #    return "INTERNAL SERVER ERROR", 500

@app.route('/api/getall', methods=['GET'])
def getall():
    return json.dumps(DATABASE), 200


if __name__ == '__main__':
    app.run()
