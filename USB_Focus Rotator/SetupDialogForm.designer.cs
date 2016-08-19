namespace ASCOM.USB_Focus
{
    partial class SetupDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMaxSteps = new System.Windows.Forms.TextBox();
            this.chkHalfSteps = new System.Windows.Forms.CheckBox();
            this.chkAntiClockwise = new System.Windows.Forms.CheckBox();
            this.comboMotorSpeed = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(204, 64);
            this.cmdOK.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(157, 57);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(552, 65);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(157, 56);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = global::ASCOM.USB_Focus.Properties.Resources.ASCOM;
            this.picASCOM.Location = new System.Drawing.Point(38, 33);
            this.picASCOM.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 56);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 3;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            this.picASCOM.DoubleClick += new System.EventHandler(this.BrowseToAscom);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 236);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 32);
            this.label2.TabIndex = 5;
            this.label2.Text = "Comm Port";
            // 
            // chkLog
            // 
            this.chkLog.AutoSize = true;
            this.chkLog.Location = new System.Drawing.Point(552, 229);
            this.chkLog.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(214, 36);
            this.chkLog.TabIndex = 6;
            this.chkLog.Text = "Log Enabled";
            this.chkLog.UseVisualStyleBackColor = true;
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(195, 229);
            this.comboBoxComPort.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(233, 39);
            this.comboBoxComPort.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 306);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 32);
            this.label3.TabIndex = 8;
            this.label3.Text = "MaxSteps";
            // 
            // textBoxMaxSteps
            // 
            this.textBoxMaxSteps.Location = new System.Drawing.Point(195, 300);
            this.textBoxMaxSteps.Name = "textBoxMaxSteps";
            this.textBoxMaxSteps.Size = new System.Drawing.Size(233, 38);
            this.textBoxMaxSteps.TabIndex = 9;
            // 
            // chkHalfSteps
            // 
            this.chkHalfSteps.AutoSize = true;
            this.chkHalfSteps.Location = new System.Drawing.Point(552, 301);
            this.chkHalfSteps.Name = "chkHalfSteps";
            this.chkHalfSteps.Size = new System.Drawing.Size(184, 36);
            this.chkHalfSteps.TabIndex = 10;
            this.chkHalfSteps.Text = "Half Steps";
            this.chkHalfSteps.UseVisualStyleBackColor = true;
            // 
            // chkAntiClockwise
            // 
            this.chkAntiClockwise.AutoSize = true;
            this.chkAntiClockwise.Checked = true;
            this.chkAntiClockwise.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAntiClockwise.Location = new System.Drawing.Point(552, 379);
            this.chkAntiClockwise.Name = "chkAntiClockwise";
            this.chkAntiClockwise.Size = new System.Drawing.Size(238, 36);
            this.chkAntiClockwise.TabIndex = 11;
            this.chkAntiClockwise.Text = "Anti Clockwise";
            this.chkAntiClockwise.UseVisualStyleBackColor = true;
            // 
            // comboMotorSpeed
            // 
            this.comboMotorSpeed.FormattingEnabled = true;
            this.comboMotorSpeed.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboMotorSpeed.Location = new System.Drawing.Point(195, 372);
            this.comboMotorSpeed.MaxDropDownItems = 7;
            this.comboMotorSpeed.Name = "comboMotorSpeed";
            this.comboMotorSpeed.Size = new System.Drawing.Size(121, 39);
            this.comboMotorSpeed.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 379);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 32);
            this.label1.TabIndex = 13;
            this.label1.Text = "Motor Speed";
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 455);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboMotorSpeed);
            this.Controls.Add(this.chkAntiClockwise);
            this.Controls.Add(this.chkHalfSteps);
            this.Controls.Add(this.textBoxMaxSteps);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxComPort);
            this.Controls.Add(this.chkLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "USB_Focus Rotator Setup";
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkLog;
        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMaxSteps;
        private System.Windows.Forms.CheckBox chkHalfSteps;
        private System.Windows.Forms.CheckBox chkAntiClockwise;
        private System.Windows.Forms.ComboBox comboMotorSpeed;
        private System.Windows.Forms.Label label1;
    }
}