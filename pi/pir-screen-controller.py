import time, os, subprocess
import RPi.GPIO as GPIO
os.environ['DISPLAY'] = ":0"

# Motion sensor (PIR) is wired to GPIO pin 4
PirSensorPin=4

# Wait time in seconds
DISPLAY_ON_TIME=12

GPIO.setwarnings(False)
GPIO.setmode(GPIO.BCM)
GPIO.setup(PirSensorPin,GPIO.IN)

def SetDisplay(DisplayOn):
    if DisplayOn==True:
        subprocess.call('XAUTHORITY=~pi/.Xauthority DISPLAY=:0 xset dpms force on && xset -dpms && xset s off', shell=True)
    else:
        subprocess.call('XAUTHORITY=~pi/.Xauthority DISPLAY=:0 xset dpms force off && xset s off', shell=True)
    time.sleep(1)

LastMotionDetected=time.time()
DisplayOn=True
i=0
while True:
    time.sleep(0.1)
    i=i+1
    if i==10:
        i=0
        if DisplayOn==True:
            print("Time-Out: "+str(DISPLAY_ON_TIME-round(time.time()-LastMotionDetected)))

    if GPIO.input(PirSensorPin)==1:
        # Save the time of last motion detection, and turn on the display
        LastMotionDetected=time.time()
        if DisplayOn==False:
            DisplayOn=True
            SetDisplay(True)

    elif DisplayOn==True:
        if time.time()-LastMotionDetected>DISPLAY_ON_TIME:
            # Turn the display off when no motion has been detected for the set amount of time
            DisplayOn=False
            SetDisplay(False)

# Clean up on exit
GPIO.cleanup()
sys.exit()
