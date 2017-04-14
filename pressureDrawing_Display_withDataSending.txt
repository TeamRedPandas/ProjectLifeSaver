#!/usr/bin/env python
from sense_hat import SenseHat, ACTION_PRESSED, ACTION_HELD, ACTION_RELEASED
from time import sleep

sense = SenseHat()


import socket
import sys


def pushed_middle(event):
    global x
    if event.action != ACTION_RELEASED:
        pusshed()

TCP_IP = '10.10.4.82'
TCP_PORT = 15258
BUFFER_SIZE = 1024

r = [255, 0, 0]
g = [0, 255, 0]


def low():
    sense.set_pixel(0,3, r)
    sleep(0.05)
    sense.set_pixel(1,3, r)
    sleep(0.05)
    sense.set_pixel(2,2, r)
    sleep(0.05)
    sense.set_pixel(3,3, r)
    sleep(0.05)
    sense.set_pixel(4,3, r)
    sleep(0.05)
    sense.set_pixel(5,4, r)
    sleep(0.05)
    sense.set_pixel(6,3, r)
    sleep(0.05)
    sense.set_pixel(7,3, r)
    sleep(0.05)
    sense.clear()

def med():
    sense.set_pixel(0,3, g)
    sleep(0.05)
    sense.set_pixel(1,3, g)
    sleep(0.05)
    sense.set_pixel(1,2, g)
    sleep(0.05)
    sense.set_pixel(2,1, g)
    sleep(0.05)
    sense.set_pixel(3,2, g)
    sleep(0.05)
    sense.set_pixel(3,3, g)
    sleep(0.05)
    sense.set_pixel(4,3, g)
    sleep(0.05)
    sense.set_pixel(4,4, g)
    sleep(0.05)
    sense.set_pixel(5,5, g)
    sleep(0.05)
    sense.set_pixel(6,4, g)
    sleep(0.05)
    sense.set_pixel(6,3, g)
    sleep(0.05)
    sense.set_pixel(7,3, g)
    sleep(0.05)
    sense.clear()




def high():
    sense.set_pixel(0,3, r)
    sleep(0.05)
    sense.set_pixel(1,3, r)
    sleep(0.05)
    sense.set_pixel(1,2, r)
    sleep(0.05)
    sense.set_pixel(1,1, r)
    sleep(0.05)
    sense.set_pixel(2,0, r)
    sleep(0.05)
    sense.set_pixel(3,1, r)
    sleep(0.05)
    sense.set_pixel(3,2, r)
    sleep(0.05)
    sense.set_pixel(3,3, r)
    sleep(0.05)
    sense.set_pixel(4,3, r)
    sleep(0.05)
    sense.set_pixel(4,4, r)
    sleep(0.05)
    sense.set_pixel(4,5, r)
    sleep(0.05)
    sense.set_pixel(5,6, r)
    sleep(0.05)
    sense.set_pixel(6,5, r)
    sleep(0.05)
    sense.set_pixel(6,4, r)
    sleep(0.05)
    sense.set_pixel(6,3, r)
    sleep(0.05)
    sense.set_pixel(7,3, r)
    sleep(0.05)
    sense.clear()

def pusshed():
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    s.connect((TCP_IP, TCP_PORT))
    s.send(bytes("Test", "utf-8"))
    data = s.recv(BUFFER_SIZE)
    s.close()

def main():
    global temp
    global p 
    while True:
        p = sense.get_pressure()
        temp = sense.get_temperature_from_pressure()
        sense.stick.direction_middle = pushed_middle
        
        if p > 1002 and p < 1004:
            low()
        elif p > 1004 and p < 1006:
            med()
        elif p > 1006 and p < 1020:
            high()
        else:
            sense.clear()

main()
    
        



    

