#! /usr/bin/python3

import keyboard
import time
from Car_interactive import car

#Usereingabe
auto_usr = input('Welches Auto möchtest du?(z.B. Ford, Mustang, Blau)> ')

#Eingabe wird aufgeteilt
auto_data = auto_usr.split(',')

#Auto wird "erstellt"
auto_usr = car(auto_data[0], auto_data[1], auto_data[2])

print(auto_usr, ',geile Karre du Mongo!')

#Motor wird gestartet
engine_input = input('Motor starten? (an/aus)> ')
if engine_input.lower() == 'an':
    auto_usr.toggle_engine()
else:
    print('Motor bleibt aus.')

#User kann Gas geben und Bremsen
auto_usr.run()