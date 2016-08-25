using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ASCOM.Utilities;
using ASCOM.USB_Focus;

namespace ASCOM.USB_Focus
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        public SetupDialogForm()
        {
            InitializeComponent();
            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here
            // Update the state variables with results from the dialogue
            Rotator.comPort = (string)comboBoxComPort.SelectedItem;
            Rotator.maxSteps = int.Parse(textBoxMaxSteps.Text);
            Rotator.isLogEnabled = chkLog.Checked;
            Rotator.halfSteps = chkHalfSteps.Checked;
            Rotator.reverse = chkReverse.Checked;
            Rotator.motorSpeed = int.Parse(comboMotorSpeed.Text);
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {
            chkLog.Checked = Rotator.isLogEnabled;
            chkHalfSteps.Checked = Rotator.halfSteps;
            chkReverse.Checked = Rotator.reverse;
            textBoxMaxSteps.Text = Rotator.maxSteps.ToString();
            comboMotorSpeed.SelectedItem = Rotator.motorSpeed.ToString();
            // set the list of com ports to those that are currently available
            comboBoxComPort.Items.Clear();
            comboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());      // use System.IO because it's static
            // select the current port if possible
            if (comboBoxComPort.Items.Contains(Rotator.comPort))
            {
                comboBoxComPort.SelectedItem = Rotator.comPort;
            }
        }
    }
}