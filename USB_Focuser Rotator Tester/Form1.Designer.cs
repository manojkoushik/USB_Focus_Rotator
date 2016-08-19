namespace ASCOM.USB_Focus
{
    partial class Form1
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
            this.buttonChoose = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelDriverId = new System.Windows.Forms.Label();
            this.goToPA = new System.Windows.Forms.TextBox();
            this.currentPA = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.but1c = new System.Windows.Forms.Button();
            this.but5c = new System.Windows.Forms.Button();
            this.but10c = new System.Windows.Forms.Button();
            this.but15c = new System.Windows.Forms.Button();
            this.but15ac = new System.Windows.Forms.Button();
            this.but10ac = new System.Windows.Forms.Button();
            this.but5ac = new System.Windows.Forms.Button();
            this.but1ac = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.currentSteps = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonChoose
            // 
            this.buttonChoose.Location = new System.Drawing.Point(32, 16);
            this.buttonChoose.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(192, 55);
            this.buttonChoose.TabIndex = 0;
            this.buttonChoose.Text = "Choose";
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(591, 16);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(192, 55);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // labelDriverId
            // 
            this.labelDriverId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.USB_Focus.Properties.Settings.Default, "DriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.labelDriverId.Location = new System.Drawing.Point(32, 95);
            this.labelDriverId.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.labelDriverId.Name = "labelDriverId";
            this.labelDriverId.Size = new System.Drawing.Size(751, 47);
            this.labelDriverId.TabIndex = 2;
            this.labelDriverId.Text = global::ASCOM.USB_Focus.Properties.Settings.Default.DriverId;
            this.labelDriverId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // goToPA
            // 
            this.goToPA.Location = new System.Drawing.Point(498, 273);
            this.goToPA.Name = "goToPA";
            this.goToPA.Size = new System.Drawing.Size(150, 38);
            this.goToPA.TabIndex = 4;
            // 
            // currentPA
            // 
            this.currentPA.BackColor = System.Drawing.Color.Silver;
            this.currentPA.Enabled = false;
            this.currentPA.Location = new System.Drawing.Point(498, 168);
            this.currentPA.Name = "currentPA";
            this.currentPA.Size = new System.Drawing.Size(150, 38);
            this.currentPA.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(363, 32);
            this.label1.TabIndex = 7;
            this.label1.Text = "Current Position Angle (PA)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(222, 262);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 58);
            this.button1.TabIndex = 8;
            this.button1.Text = "Go To PA";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // but1c
            // 
            this.but1c.Location = new System.Drawing.Point(63, 343);
            this.but1c.Name = "but1c";
            this.but1c.Size = new System.Drawing.Size(133, 46);
            this.but1c.TabIndex = 9;
            this.but1c.Text = "+1°";
            this.but1c.UseVisualStyleBackColor = true;
            this.but1c.Click += new System.EventHandler(this.but1c_Click);
            // 
            // but5c
            // 
            this.but5c.Location = new System.Drawing.Point(63, 435);
            this.but5c.Name = "but5c";
            this.but5c.Size = new System.Drawing.Size(133, 46);
            this.but5c.TabIndex = 10;
            this.but5c.Text = "+5°";
            this.but5c.UseVisualStyleBackColor = true;
            this.but5c.Click += new System.EventHandler(this.but5c_Click);
            // 
            // but10c
            // 
            this.but10c.Location = new System.Drawing.Point(222, 343);
            this.but10c.Name = "but10c";
            this.but10c.Size = new System.Drawing.Size(135, 46);
            this.but10c.TabIndex = 11;
            this.but10c.Text = "+10°";
            this.but10c.UseVisualStyleBackColor = true;
            this.but10c.Click += new System.EventHandler(this.but10c_Click);
            // 
            // but15c
            // 
            this.but15c.Location = new System.Drawing.Point(222, 435);
            this.but15c.Name = "but15c";
            this.but15c.Size = new System.Drawing.Size(135, 46);
            this.but15c.TabIndex = 12;
            this.but15c.Text = "+15°";
            this.but15c.UseVisualStyleBackColor = true;
            this.but15c.Click += new System.EventHandler(this.but15c_Click);
            // 
            // but15ac
            // 
            this.but15ac.Location = new System.Drawing.Point(453, 435);
            this.but15ac.Name = "but15ac";
            this.but15ac.Size = new System.Drawing.Size(134, 46);
            this.but15ac.TabIndex = 13;
            this.but15ac.Text = "-15°";
            this.but15ac.UseVisualStyleBackColor = true;
            this.but15ac.Click += new System.EventHandler(this.but15ac_Click);
            // 
            // but10ac
            // 
            this.but10ac.Location = new System.Drawing.Point(453, 343);
            this.but10ac.Name = "but10ac";
            this.but10ac.Size = new System.Drawing.Size(134, 46);
            this.but10ac.TabIndex = 14;
            this.but10ac.Text = "-10°";
            this.but10ac.UseVisualStyleBackColor = true;
            this.but10ac.Click += new System.EventHandler(this.but10ac_Click);
            // 
            // but5ac
            // 
            this.but5ac.Location = new System.Drawing.Point(631, 435);
            this.but5ac.Name = "but5ac";
            this.but5ac.Size = new System.Drawing.Size(118, 46);
            this.but5ac.TabIndex = 15;
            this.but5ac.Text = "-5°";
            this.but5ac.UseVisualStyleBackColor = true;
            this.but5ac.Click += new System.EventHandler(this.but5ac_Click);
            // 
            // but1ac
            // 
            this.but1ac.Location = new System.Drawing.Point(631, 343);
            this.but1ac.Name = "but1ac";
            this.but1ac.Size = new System.Drawing.Size(118, 46);
            this.but1ac.TabIndex = 16;
            this.but1ac.Text = "-1°";
            this.but1ac.UseVisualStyleBackColor = true;
            this.but1ac.Click += new System.EventHandler(this.but1ac_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(285, 32);
            this.label2.TabIndex = 17;
            this.label2.Text = "Current Step Position";
            // 
            // currentSteps
            // 
            this.currentSteps.BackColor = System.Drawing.Color.Silver;
            this.currentSteps.Enabled = false;
            this.currentSteps.Location = new System.Drawing.Point(498, 218);
            this.currentSteps.Name = "currentSteps";
            this.currentSteps.Size = new System.Drawing.Size(150, 38);
            this.currentSteps.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 527);
            this.Controls.Add(this.currentSteps);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.but1ac);
            this.Controls.Add(this.but5ac);
            this.Controls.Add(this.but10ac);
            this.Controls.Add(this.but15ac);
            this.Controls.Add(this.but15c);
            this.Controls.Add(this.but10c);
            this.Controls.Add(this.but5c);
            this.Controls.Add(this.but1c);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currentPA);
            this.Controls.Add(this.goToPA);
            this.Controls.Add(this.labelDriverId);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonChoose);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "Form1";
            this.Text = "USB_Focus Rotator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelDriverId;
        private System.Windows.Forms.TextBox goToPA;
        private System.Windows.Forms.TextBox currentPA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button but1c;
        private System.Windows.Forms.Button but5c;
        private System.Windows.Forms.Button but10c;
        private System.Windows.Forms.Button but15c;
        private System.Windows.Forms.Button but15ac;
        private System.Windows.Forms.Button but10ac;
        private System.Windows.Forms.Button but5ac;
        private System.Windows.Forms.Button but1ac;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox currentSteps;
    }
}

