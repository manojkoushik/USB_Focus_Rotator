This is an ASCOM Rotator Driver for USB_Focus based motor and controller.

This is a quick hack so please bear with any bugs that might exist.

Based on Ascom spec. Rotator shaft will move from 0* to 359.99*. 

To avoid cable tangle position it such that they exit at 180*. That way they will only ever rotate 180* in either direction.

If specified angle is >360*, the rotator will move as if it's specified from 0. That is, 370* will be treated as 10*.

A few brief notes:
    - Set your max steps to 65535 (or some similarly high number) on first connection. .
    - Mark your 0 position with a piece of tape or something like that. Align it as close to Sky PA 0* as possible (this just makes things simpler).
    - Also set the rotator to be zero (pressing both in and out buttons at the same time).
    - From here rotate the rotator one full turn and note the step position in the test app. This is your max steps.
    - Go back to 0* and change the config to enter the new max steps. 

You are all set.

There are options to set motor speed, reverse direction and half steps. 
Reverse needs to be set in case the rotation of the shaft is not aligned with the Sky PA rotation. 
You will need to redo the zero position set if you change this setting.

Hope you like it. 