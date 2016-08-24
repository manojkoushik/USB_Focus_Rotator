This is an ASCOM Rotator Driver for USB_Focus based motor and controller.

This is a quick hack so please bear with any bugs that might exist.

A few brief notes:
-  To avoid cable tangles, the rotator will only go from 0 to 180 or 0 to -180. PA is still specified, and dealt with, in the range 0 to 360. Internally it automatically converts the position angle to what the movement needs to be and will never cross 180. Instead opting to reverse directions and go through 0 when angles are greater than 180
- Because of this, the setup involves 4 steps:
    - Set you max steps to 65535 on first connection. 
    - Mark your 0 position with a piece of tape or something like that. Also set the rotator to be zero (pressing both in and out buttons at the same time)
    - From here rotate the rotator one full turn and note the step position in the test app. This is your max steps
    - Bring the rotator back to 1/2 your max steps and make this your new zero position (pressing both in and out buttons at the same time). Also disconnect and change the maxsteps in the settings for the driver. 

The rotator test app should now show that your PA is 180 degrees and step position is 0 steps. Now you can go to any angle without any cable tangles. 

There are options to set motor speed and half steps. In my case I don't need to use half steps since I have sufficient resolution without it. You can also change the rotation direction to be either clockwise or anticlockwise. In my case the motor shaft faces to the back of the scope and as a result have to use anticlockwise. But this will depend on the way the motor is mounted. 

Hope you like it. 