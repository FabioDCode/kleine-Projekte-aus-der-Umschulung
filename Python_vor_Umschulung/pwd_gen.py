#! /usr/bin/python3

import random
import string

s1 = list(string.ascii_lowercase)
s2 = list(string.ascii_uppercase)
s3 = list(string.digits)
s4 = list(string.punctuation)

lenght = input ('Wie lang soll dein Passwort sein? > ')

while True:

    try: 
        char_num = int (lenght)
        if char_num < 8:
            print ('mind 8 Zeichen lang')
            length = input ('Bitte erneut eingeben > ')
        else:
            break
    except:
        print('Nur Zahlen bitte')
        lenght= input ('Wie lang soll dein Passwort sein? > ')

random.shuffle(s1)
random.shuffle(s2)
random.shuffle(s3)
random.shuffle(s4)

part1 = round(char_num * (30/100))
part2 = round(char_num * (20/100))

ergebnis= []

for x in range (part1):
    ergebnis.append(s1[x])
    ergebnis.append(s2[x])

for x in range (part2):
    ergebnis.append(s3[x])
    ergebnis.append(s4[x])

random.shuffle(ergebnis)

pwd = ''.join(ergebnis)
print(pwd)