//*** CHECK THIS ProgID ***
var X = new ActiveXObject("ASCOM.USB_Focus_Tester.Rotator");
WScript.Echo("This is " + X.Name + ")");
// You may want to uncomment this...
// X.Connected = true;
X.SetupDialog();
