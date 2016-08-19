using System;
using System.Windows.Forms;
using System.Threading;

namespace ASCOM.USB_Focus
{
    public partial class Form1 : Form
    {

        private ASCOM.DriverAccess.Rotator driver;
        private Thread updateTextThread;

        public Form1()
        {
            InitializeComponent();
            SetUIState();
            updateTextThread = new Thread(new ThreadStart(updateCurrentPASteps));
            updateTextThread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsConnected)
                driver.Connected = false;

            Properties.Settings.Default.Save();
        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DriverId = ASCOM.DriverAccess.Rotator.Choose(Properties.Settings.Default.DriverId);
            SetUIState();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (IsConnected)
            {
                driver.Connected = false;
            }
            else
            {
                driver = new ASCOM.DriverAccess.Rotator(Properties.Settings.Default.DriverId);
                driver.Connected = true;
            }
            SetUIState();
        }

        private void SetUIState()
        {
            buttonConnect.Enabled = !string.IsNullOrEmpty(Properties.Settings.Default.DriverId);
            buttonChoose.Enabled = !IsConnected;
            buttonConnect.Text = IsConnected ? "Disconnect" : "Connect";
        }

        private bool IsConnected
        {
            get
            {
                return ((this.driver != null) && (driver.Connected == true));
            }
        }

        private void updateCurrentPASteps()
        {
            string textPA;
            string textSteps;
            while (true)
            {
                if (IsConnected) {
                    if (driver.IsMoving) {
                        textPA = "Moving";
                        textSteps = "Moving";
                    }
                    else
                    {
                        textPA = driver.Position.ToString();
                        textSteps = driver.Action("PAToSteps",textPA);
                    }
                }
                else {
                    textPA = "d/c";
                    textSteps = "d/c";
                }
                updateCurrentPAText(textPA);
                updateCurrentStepsText(textSteps);
                Thread.Sleep(1000);
            }
        }

        private void updateCurrentPAText(string textPA)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(updateCurrentPAText), new object[] { textPA });
                return;
            }
            currentPA.Text = textPA;
        }

        private void updateCurrentStepsText(string textSteps)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(updateCurrentStepsText), new object[] { textSteps });
                return;
            }
            currentSteps.Text = textSteps;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float positionAngle = float.Parse(goToPA.Text);
            driver.Move(positionAngle);
        }

        private float calcNewPA(float oldPA, float offset)
        {
            float newPA = oldPA + offset;
            if (newPA > 360)
                newPA -= 360;

            if (newPA < 0)
                newPA += 360;

            return newPA;
        }

        private void but1c_Click(object sender, EventArgs e)
        {
            float currentPos;
            if (IsConnected)
                if (!driver.IsMoving)
                {
                    currentPos = driver.TargetPosition;
                    driver.Move(calcNewPA(currentPos,1));
                }
        }

        private void but5c_Click(object sender, EventArgs e)
        {
            float currentPos;
            if (IsConnected)
                if (!driver.IsMoving)
                {
                    currentPos = driver.TargetPosition;
                    driver.Move(calcNewPA(currentPos, 5));
                }
        }

        private void but10c_Click(object sender, EventArgs e)
        {
            float currentPos;
            if (IsConnected)
                if (!driver.IsMoving)
                {
                    currentPos = driver.TargetPosition;
                    driver.Move(calcNewPA(currentPos, 10));
                }
        }

        private void but15c_Click(object sender, EventArgs e)
        {
            float currentPos;
            if (IsConnected)
                if (!driver.IsMoving)
                {
                    currentPos = driver.TargetPosition;
                    driver.Move(calcNewPA(currentPos, 15));
                }
        }

        private void but15ac_Click(object sender, EventArgs e)
        {
            float currentPos;
            if (IsConnected)
                if (!driver.IsMoving)
                {
                    currentPos = driver.TargetPosition;
                    driver.Move(calcNewPA(currentPos, -15));
                }
        }

        private void but10ac_Click(object sender, EventArgs e)
        {
            float currentPos;
            if (IsConnected)
                if (!driver.IsMoving)
                {
                    currentPos = driver.TargetPosition;
                    driver.Move(calcNewPA(currentPos, -10));
                }
        }

        private void but5ac_Click(object sender, EventArgs e)
        {
            float currentPos;
            if (IsConnected)
                if (!driver.IsMoving)
                {
                    currentPos = driver.TargetPosition;
                    driver.Move(calcNewPA(currentPos, -5));
                }
        }

        private void but1ac_Click(object sender, EventArgs e)
        {
            float currentPos;
            if (IsConnected)
                if (!driver.IsMoving)
                {
                    currentPos = driver.TargetPosition;
                    driver.Move(calcNewPA(currentPos, -1));
                }
        }

    }
}
