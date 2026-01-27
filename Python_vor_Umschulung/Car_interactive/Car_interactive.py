#! /usr/bin/python3

import keyboard
import time
import Car_list

class car():

    def __init__(self, mark, model, colr):
        self.mark           = Car_list.car_mark
        self.model          = Car_list.car_model
        self.colr           = colr
        self.engine_status  = 'aus'
    
    def __str__(self):
        return f'{self.mark}, {self.model} in der Farbe {self.colr}'
    
    def toggle_engine(self):
        if self.engine_status == 'aus':
            self.engine_status = 'an'
            print('Motor an')
        else:
            self.engine_status =  'aus'
            print('Motor aus')
    
    def turn_left(self):
       print('du lenkst nach links.')

    def turn_right(self):
       print('du lenkst nach rechts.')

    def run(self):
        speed = 0  # Anfangsgeschwindigkeit
        print("Möchtest du beschleunigen (w) oder abbremsen (s). Drücke 'esc' um zu beenden: ")

        while True:
            if keyboard.is_pressed('esc'):
                print("Programm beendet.")
                break
              
            if keyboard.is_pressed('w'): 
                if speed < 360:
                    speed += 20
                    print(f"Geschwindigkeit: {speed} km/h")                    
                else:
                    print("Maximale Geschwindigkeit erreicht (360 km/h).")

            elif keyboard.is_pressed('s'):
                if speed > 0:
                    speed -= 20
                    print(f"Geschwindigkeit: {speed} km/h")                    
                else:
                    print("Das Fahrzeug ist bereits gestoppt.")
                
            elif keyboard.is_pressed('d'):
                self.turn_right()

            elif keyboard.is_pressed('a'):
                self.turn_left()

            if not (keyboard.is_pressed('w') or keyboard.is_pressed('s') or keyboard.is_pressed('a') or keyboard.is_pressed('d')):
                if speed > 0:
                    speed -= 5
                    print(f'Geschwindigkeit: {speed} km/h')

            time.sleep(0.3)        
    