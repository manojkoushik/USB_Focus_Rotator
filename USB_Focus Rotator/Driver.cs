//tabs=4
// --------------------------------------------------------------------------------
// ASCOM Rotator driver for USB_Focus
//
// Description:	Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam 
//				nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
//				erat, sed diam voluptua. At vero eos et accusam et justo duo 
//				dolores et ea rebum. Stet clita kasd gubergren, no sea takimata 
//				sanctus est Lorem ipsum dolor sit amet.
//
// Implements:	ASCOM Rotator interface version: <To be completed by driver developer>
// Author:		(MK) Manoj Koushik
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 16-Aug-2016	MK	6.0.0	Initial edit, created from ASCOM driver template
// --------------------------------------------------------------------------------
//


// This is used to define code in the template that is specific to one class implementation
// unused code canbe deleted and this definition removed.
#define Rotator

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;


using ASCOM;
using ASCOM.Astrometry;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;
using System.Globalization;
using System.Collections;
using System.Reflection;

namespace ASCOM.USB_Focus
{
    //
    // Your driver's DeviceID is ASCOM.USB_Focus.Rotator
    //
    // The Guid attribute sets the CLSID for ASCOM.USB_Focus.Rotator
    // The ClassInterface/None addribute prevents an empty interface called
    // _USB_Focus from being created and used as the [default] interface
    //
    // TODO Replace the not implemented exceptions with code to implement the function or
    // throw the appropriate ASCOM exception.
    //

    /// <summary>
    /// ASCOM Rotator Driver for USB_Focus.
    /// </summary>
    [Guid("7838a180-ee67-4a38-bcdb-65542a594e88")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Rotator : IRotatorV2
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.USB_Focus.Rotator";
        // TODO Change the descriptive string for your driver then remove this line
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "ASCOM Rotator Driver for USB_Focus.";

        private Serial serialPort;


        internal static bool isLogEnabled = false;
        internal static string comPort; // Variables to hold the currrent device configuration
        internal static int maxSteps; // Variables to hold the currrent device configuration
        internal static bool halfSteps = false;
        internal static bool reverse = false;
        internal static int motorSpeed;

        internal static string comPortDefault = "COM1";
        internal static string maxStepsDefault = "8000";
        internal static int motorSpeedDefault = 4;

        internal static string comPortProfileName = "COM Port"; // Constants used for Profile persistence
        internal static string maxStepsProfileName = "Max Steps"; // Constants used for Profile persistence
        internal static string isLogEnabledProfileName = "Log Enabled"; // Constants used for Profile persistence
        internal static string halfStepsProfileName = "Half Steps";
        internal static string reverseProfileName = "Reverse";
        internal static string motorSpeedProfileName = "Motor Speed";

        private bool checkingStatus = false; // flag pour réaliser un status du déplacement 
        private bool bMoving = false;       // Si true déplacement en cours
        private int lastStepPosition = 0;              // Store last known position (used to feed client app with last position when waiting thread to release comm port)
        private int PID = 0;                    //identifiant de log Position
        private int CID = 0;                    //identifiant de log de commandes

        Thread move_thread;                     // Thread to manage MOVE command (ACK) to get non-blocking operations

        /// <summary>
        /// Private variable to hold the connected state
        /// </summary>
        private bool connectedState;

        /// <summary>
        /// Private variable to hold an ASCOM Utilities object
        /// </summary>
        private Util utilities;

        /// <summary>
        /// Private variable to hold an ASCOM AstroUtilities object to provide the Range method
        /// </summary>
        private AstroUtils astroUtilities;

        /// <summary>
        /// Variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>

        /// <summary>
        /// Initializes a new instance of the <see cref="USB_Focus"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public Rotator()
        {
            ReadProfile(); // Read device configuration from the ASCOM Profile store

            if(isLogEnabled)
                Logger.Write("Rotator: Starting initialisation");

            connectedState = false; // Initialise connected to false
            utilities = new Util(); //Initialise util object
            astroUtilities = new AstroUtils(); // Initialise astro utilities object
            move_thread = new Thread(new ThreadStart(moveAck));
            move_thread.Start();
        }


        //
        // PUBLIC COM INTERFACE IRotatorV2 IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected
            if (IsConnected)
                System.Windows.Forms.MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm())
            {
                var result = F.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        public ArrayList SupportedActions
        {
            get
            {
                return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            //LogMessage("", "Action {0}, parameters {1} not implemented", actionName, actionParameters);
            //throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
            //Get the method information using the method info class
            MethodInfo mi = this.GetType().GetMethod(actionName);

            //Invoke the method
            // (null- no parameter for the method call
            // or you can pass the array of parameters...)
            return (string)(mi.Invoke(this, actionParameters.Split(',')));
        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            // Call CommandString and return as soon as it finishes
            this.CommandString(command, raw);
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            string ret = CommandString(command, raw);
            // TODO decode the return string and return true or false
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBool");
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");

            if (!this.Connected)
                throw new ASCOM.NotConnectedException();

            serialPort.ClearBuffers();
            serialPort.Transmit(command);

            serialPort.ReceiveTimeout = 1;  // Timeout 3 sec
                                            // move_thread thread handles longer Move commands
            String st = "";

            if (!(command.StartsWith("I") || command.StartsWith("O")))//Case not a Move command
            {
                st = serialPort.ReceiveTerminated("\n\r");   // Wait for terminated character
            }

            return st;
        }

        public void moveAck()
        {
            String st = "";
            int timeoutMove = 0;

            try
            {
                while (true)
                {
                    Thread.Sleep(100);  //polling bMoving each xxxms

                    if (bMoving)    // if focuser moves
                    {
                        st = "";

                        if (timeoutMove++ > 60)
                        {
                            // aprés X cycles de lecture du Receive on vérifie si le focuser s'est arrété et si la fonction peut être quittée

                            timeoutMove = 0;

                            // on vérifie si le : checkingStatus
                            checkingStatus = true;          // on force le get position
                            int pos1 = this.stepPosition;   // get position
                            Thread.Sleep(100);          // petite pause
                            int pos2 = this.stepPosition;   // get position
                            checkingStatus = false;         // on force le get position

                            if (pos1 == pos2)
                            {
                                // le moteur ne tourne plus..
                                bMoving = false;

                                if (isLogEnabled)
                                    Logger.Write("[MoveThread timeout] !!! Move timeout - motor stopped !!!");
                            }
                            else
                            {
                                // le moteur tourne...
                                if (isLogEnabled)
                                    Logger.Write("[MoveThread timeout] !!! motor still moving...");
                            }

                        }
                        else
                        {
                            // si on a pas atteint le timeout
                            // on écoute le port com
                            try
                            {
                                st = serialPort.Receive();   // blocking read function up to timeout
                                if (isLogEnabled)
                                    Logger.Write("[MoveThread] Moving... (timeout=" + timeoutMove.ToString() + ")");
                            }
                            catch (Exception ex)
                            {
                                if (isLogEnabled)
                                    Logger.Write("[MoveThread] serialPort Receive timed out (" + timeoutMove + ")");
                            }


                            if (st.Contains("*"))   // if focuser sends ACK command
                            {
                                bMoving = false;    // Move ended notified to client app
                                timeoutMove = 0;    // timeout de reception initialisé à 0 aprés chaque fin de lecture..
                                if (isLogEnabled)
                                    Logger.Write("[MoveThread] Move ended.");
                            }
                        }
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                if (isLogEnabled)
                    Logger.Write("[MoveThread error] System.Threading.ThreadAbortException");
                System.Threading.Thread.ResetAbort();
            }
        }

        public void Dispose()
        {
            utilities.Dispose();
            utilities = null;
            astroUtilities.Dispose();
            astroUtilities = null;
        }

        /// Returns true if there is a valid connection to the driver hardware
        private bool IsConnected
        {
            get
            {
                // TODO check that the driver hardware connection exists and is connected to the hardware
                if (serialPort == null)
                    return false;
                return serialPort.Connected;
            }
        }

        /// Use this function to throw an exception if we aren't connected to the hardware
        private void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                if (isLogEnabled)
                    Logger.Write("[Serial port] connection error :" + message);

                throw new ASCOM.NotConnectedException(message);
            }
        }

        public bool Connected
        {
            get
            {
                return IsConnected;
            }
            set
            {

                if (value == IsConnected)
                    return;

                if (value)
                {
                    //Connect

                    try
                    {
                        serialPort = new Serial();
                        serialPort.PortName = comPort;
                        serialPort.Speed = SerialSpeed.ps9600;
                        serialPort.Connected = true;
                    }
                    catch (Exception ex)
                    {
                        if (isLogEnabled)
                            Logger.Write("[Serial port] connection error :" + ex.ToString());
                        throw new ASCOM.NotConnectedException("Serial port connection error:", ex);
                    }

                    // Get current position
                    try
                    {
                        if (isLogEnabled)
                            Logger.Write("\r\n---------------------------------------------------------------------------------------INIT\r\n");
                        lastStepPosition = this.stepPosition;                          // Get current position from Focuser
                    }
                    catch (Exception ex)
                    {
                        if (isLogEnabled)
                            Logger.Write("[Serial port] connection error :" + ex.ToString());
                        throw new ASCOM.NotConnectedException("Serial port connection error:", ex);
                    }

                    // Set HalfSteps or FullSteps
                    try
                    {
                        if (halfSteps)
                        {
                            if (isLogEnabled)
                                Logger.Write("\r\n---------------------------------------------------------------------------------------SMSTPD\r\n");
                            CommandString(string.Format("SMSTPD"), true);
                        }
                        else
                        {
                            if (isLogEnabled)
                                Logger.Write("\r\n---------------------------------------------------------------------------------------SMSTPF\r\n");
                            CommandString(string.Format("SMSTPF"), true);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (isLogEnabled)
                            Logger.Write("Half Step Setting Failed :" + ex.ToString());
                    }

                    // Set reverse mode
                    try
                    {
                        if (reverse)
                        {
                            if (isLogEnabled)
                                Logger.Write("\r\n---------------------------------------------------------------------------------------SMROTT\r\n");
                            CommandString(string.Format("SMROTH"), true);
                        }
                        else
                        {
                            if (isLogEnabled)
                                Logger.Write("\r\n---------------------------------------------------------------------------------------SMROTH\r\n");
                            CommandString(string.Format("SMROTT"), true);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (isLogEnabled)
                            Logger.Write("Half Step Setting Failed :" + ex.ToString());
                    }

                    // Set Motor Speed
                    try
                    {
                        if (isLogEnabled)
                            Logger.Write("\r\n---------------------------------------------------------------------------------------" + string.Format("SMO{0:000}",motorSpeed) + "\r\n");
                        CommandString(string.Format("SMO{0:000}",motorSpeed), true);
                    }
                    catch (Exception ex)
                    {
                        if (isLogEnabled)
                            Logger.Write("Motor Speed Setting Failed :" + ex.ToString());
                    }

                    // Set Max Steps
                    try
                    {
                        if (isLogEnabled)
                            Logger.Write("\r\n---------------------------------------------------------------------------------------" + string.Format("M{0:00000}", maxSteps) + "\r\n");
                        CommandString(string.Format("M{0:00000}", maxSteps), true);
                    }
                    catch (Exception ex)
                    {
                        if (isLogEnabled)
                            Logger.Write("Max Steps Setting Failed :" + ex.ToString());
                    }
                }
                else
                {
                    if (serialPort != null && serialPort.Connected)
                    {
                        serialPort.Connected = false;
                    }
                }
            }
        }

        public string Description
        {
            // TODO customise this device description
            get
            {
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                // TODO customise this driver description
                string driverInfo = "Information about the driver itself. Version: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                LogMessage("InterfaceVersion Get", "2");
                return Convert.ToInt16("2");
            }
        }

        public string Name
        {
            get
            {
                string name = "Short driver name - please customise";
                return name;
            }
        }

        private int PAToSteps(float Position)
        {
            int stepPosition = (int)(Position * maxSteps / 360);
            if (isLogEnabled)
                Logger.Write("Position Angle:" + Position.ToString() + " Step Position:" + stepPosition.ToString()); // Move to this position
            return stepPosition;
        }

        public string PAToStepsPublic(string Position)
        {
            int stepPosition = (int)(float.Parse(Position) * maxSteps / 360);
            if (isLogEnabled)
                Logger.Write("Position Angle:" + Position.ToString() + " Step Position:" + stepPosition.ToString()); // Move to this position
            return stepPosition.ToString();
        }
        private float StepsToPA(int steps)
        {
            double positionAngle = steps * 360.0 / maxSteps;

            if (isLogEnabled)
                Logger.Write("Step Position:" + steps.ToString() + " PositionAngle:" + positionAngle.ToString()); // Move to this position

            return (float)positionAngle;
        }

        #endregion

        #region IRotator Implementation

        private float rotatorPosition = 0; // Absolute position angle of the rotator 

        public bool CanReverse
        {
            get
            {
                return true;
            }
        }

        public void Halt()
        {
            //FQUITx
            if (this.Connected)
            {
                string msgLog = "";
                CID++;

                if (bMoving)
                {
                    msgLog += "\r\nCID" + CID + "---------------------------------------------------------------------------------------\r\n";
                    msgLog += "CID" + CID + " command :" + string.Format("FQUITx");

                    bMoving = false;                                    // stop move thread
                    CommandString(string.Format("FQUITx"), false);      // send FQUITx cmd to focuser
                }
                else
                {
                    return;
                }

                if (isLogEnabled)
                    Logger.Write(msgLog);
            }
            else
            {
                if (isLogEnabled)
                    Logger.Write("[Halt] connection error.");

                throw new ASCOM.NotConnectedException("Halt");
            }
        }

        public bool IsMoving
        {
            get
            {
                if (isLogEnabled)
                    Logger.Write("[Get IsMoving] :" + this.bMoving.ToString());

                return this.bMoving;
            }
        }

        public void Move(float Position)
        {
            float targetPosition = Position + rotatorPosition;

            MoveAbsolute(targetPosition);
        }

        public void MoveAbsolute(float Position)
        {
            string msgLog = "";
            CID++;

            if (Position == rotatorPosition)
                return;

            if (Position >= 360.00)
                Position -= 360;

            if (Position < 0)
                Position += 360;

            // No MOVE while moving
            if (bMoving)
                throw new ASCOM.InvalidOperationException("Focuser is moving, try again");

            rotatorPosition = Position;
            int targetPosition = PAToSteps(Position);
            int delta = targetPosition - this.lastStepPosition;



            bMoving = true;

            if (delta >= 0)
            {
                // Debug
                //System.Windows.Forms.MessageBox.Show("target position :" + targetPosition + ", current position :" + this.lastStepPosition + ", move OUT:" + delta);
                msgLog += "\r\nCID" + CID + "---------------------------------------------------------------------------------------\r\n";
                msgLog += "CID" + CID + " command :" + string.Format("O{0:00000}", delta);
                CommandString(string.Format("O{0:00000}", delta), true);
            }
            else if (delta < 0)
            {
                // Debug
                //System.Windows.Forms.MessageBox.Show("target position :" + targetPosition + ", current position :" + this.lastStepPosition + ", move IN:" + -delta);
                msgLog += "\r\nCID" + CID + "---------------------------------------------------------------------------------------\r\n";
                msgLog += "CID" + CID + " command :" + string.Format("I{0:00000}", -delta);
                CommandString(string.Format("I{0:00000}", -delta), true);
            }

            if (isLogEnabled)
                Logger.Write(msgLog);
        }

        public float Position
        {
            get
            {
                return StepsToPA(stepPosition);
            }
        }

        private int stepPosition
        {
            get
            {
                int j = 0;
                string ret = "";
                int indexP = 0;
                int indexEnd = 0;
                int newPos = 0;

                string msgLog = "";

                PID++;

                try
                {
                    // si checkingStatus est FAUX on fait comme d'hab
                    if (!checkingStatus)
                    {
                        if (bMoving)
                            return lastStepPosition;
                    }
                    // sinon on rentre en forçant le passage..

                    msgLog += "\r\nPID" + PID + "---------------------------------------------------------------------------------------\r\n";

                    while (j <= 5)
                    {

                        // position command is FPOSRO, returns P=vwxyzLFCR
                        try
                        {
                            ret = CommandString("FPOSRO", true);
                        }
                        catch
                        {
                            // ERROR 0 : Command with no answer Exception catched
                            msgLog += "\r\nPID" + PID + " ER0, retry\r\n";
                        }

                        msgLog += "PID" + PID + " GetPosition #try" + j + " Pos:" + ret + "##, P:" + ret.IndexOf("P=") + "\\r\\n" + ret.IndexOf("\n\r") + "length:" + ret.Length + " ";

                        if (ret.IndexOf("P=") != -1 && ret.IndexOf("\n\r") != -1 && ret.IndexOf("P=") < ret.IndexOf("\n\r") && !ret.Contains("*"))
                        {
                            indexP = ret.IndexOf("P=");
                            indexEnd = ret.IndexOf("\n\r");
                            ret = ret.Substring(indexP + 2, indexEnd - indexP - 2);

                            if (ret.Length.Equals(5))    // check if position value is 5 digits length..
                            {
                                newPos = int.Parse(ret);

                                msgLog += "PID" + PID + " good format";

                                if (newPos >= 0 && newPos <= maxSteps)
                                {
                                    lastStepPosition = newPos;
                                    msgLog += "PID" + PID + ", good value :" + lastStepPosition;
                                    break;
                                }
                            }
                        }

                        if (ret.Contains("*"))   // if focuser sends ACK command
                            bMoving = false;    // Move ended notified to client app

                        // ERROR 1 : wrong format received
                        msgLog += "PID" + PID + ", ER1, retry\r\n";
                        j++;
                    }

                    if (j >= 10)
                    {
                        //msgLog+="PID"+PID+", !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  EXCEPTION - Position values wrong format. UNBALE TO UNDERSTAND HARDWARE !" + ret;
                        throw new ASCOM.InvalidValueException();
                    }

                    if (isLogEnabled)
                        Logger.Write(msgLog);

                    return lastStepPosition;
                }
                catch (Exception ex)
                {
                    // ERROR 2 : EXCEPTION - Position value wrong format after " + j + " retry. Last message received from hardware :
                    if (isLogEnabled)
                        Logger.Write("PID" + PID + ", ER2 " + ret + "##, P:" + ret.IndexOf("P=") + "\\r\\n" + ret.IndexOf("\n\r") + "length:" + ret.Length + "\r\n" + ex.ToString());

                    throw new ASCOM.InvalidValueException("PID" + PID + " ER2," + ret + "##, P:" + ret.IndexOf("P=") + "\\r\\n" + ret.IndexOf("\n\r") + "length:" + ret.Length, ex);
                }
            }
        }

        public bool Reverse
        {
            get
            {
                return reverse;
            }
            set
            {
                reverse = value;
            }
        }

        public float StepSize
        {
            get
            {
                throw new ASCOM.PropertyNotImplementedException("StepSize", false);
            }
        }

        public float TargetPosition
        {
            get
            {
                return rotatorPosition;
            }
        }

        #endregion

        #region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

        #region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "Rotator";
                if (bRegister)
                {
                    P.Register(driverID, driverDescription);
                }
                else
                {
                    P.Unregister(driverID);
                }
            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

        #endregion

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Rotator";
                comPort = driverProfile.GetValue(driverID, comPortProfileName, string.Empty, comPortDefault);
                maxSteps = int.Parse(driverProfile.GetValue(driverID, maxStepsProfileName, string.Empty, maxStepsDefault));
                isLogEnabled = Convert.ToBoolean(driverProfile.GetValue(driverID, isLogEnabledProfileName, string.Empty, isLogEnabled.ToString()));
                halfSteps = Convert.ToBoolean(driverProfile.GetValue(driverID, halfStepsProfileName, string.Empty, halfSteps.ToString()));
                reverse = Convert.ToBoolean(driverProfile.GetValue(driverID, reverseProfileName, string.Empty, reverse.ToString()));
                motorSpeed = int.Parse(driverProfile.GetValue(driverID, motorSpeedProfileName, string.Empty, motorSpeedDefault.ToString()));
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Rotator";
                driverProfile.WriteValue(driverID, comPortProfileName, comPort.ToString());
                driverProfile.WriteValue(driverID, maxStepsProfileName, maxSteps.ToString());
                driverProfile.WriteValue(driverID, isLogEnabledProfileName, isLogEnabled.ToString());
                driverProfile.WriteValue(driverID, halfStepsProfileName, halfSteps.ToString());
                driverProfile.WriteValue(driverID, reverseProfileName, reverse.ToString());
                driverProfile.WriteValue(driverID, motorSpeedProfileName, motorSpeed.ToString());
            }
        }

        /// <summary>
        /// Log helper function that takes formatted strings and arguments
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        internal static void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
        }
        #endregion
    }

    public static class Logger
    {
        static readonly TextWriter tw;

        private static readonly object _syncObject = new object();

        static Logger()
        {
            string specificFolder = "";     // User application data folder
            string logFilePath = "";        // Log file path
            string folder = "c:\\";
            specificFolder = Path.Combine(folder, "USB_Focus");

            // Check if folder exists and if not, create it
            if (!Directory.Exists(specificFolder))
                Directory.CreateDirectory(specificFolder);

            // Create final log file path
            logFilePath = Path.Combine(specificFolder, "USB_Focus_log_" + DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH-mm-ss") + ".txt");

            tw = TextWriter.Synchronized(File.AppendText(logFilePath));
        }

        public static void Write(string logMessage)
        {
            try
            {
                Log(logMessage, tw);
            }
            catch (IOException e)
            {
                tw.Close();
            }
        }

        public static void Log(string logMessage, TextWriter w)
        {
            // only one thread can own this lock, so other threads
            // entering this method will wait here until lock is
            // available.
            lock (_syncObject)
            {
                w.WriteLine("{0} : {1}", DateTime.Now.ToLocalTime(), logMessage);

                // Update the underlying file.
                w.Flush();
            }
        }
    }
}
