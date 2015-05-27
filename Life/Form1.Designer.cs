namespace Life
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblInterval = new System.Windows.Forms.Label();
            this.lblFillFactor = new System.Windows.Forms.Label();
            this.lblScaleFactor = new System.Windows.Forms.Label();
            this.nudInterval = new System.Windows.Forms.NumericUpDown();
            this.nudFillFactor = new System.Windows.Forms.NumericUpDown();
            this.nudScaleFactor = new System.Windows.Forms.NumericUpDown();
            this.startStopBtn = new System.Windows.Forms.Button();
            this.BtnGen = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.cbMovingNodes = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFillFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbMovingNodes);
            this.panel1.Controls.Add(this.lblInterval);
            this.panel1.Controls.Add(this.lblFillFactor);
            this.panel1.Controls.Add(this.lblScaleFactor);
            this.panel1.Controls.Add(this.nudInterval);
            this.panel1.Controls.Add(this.nudFillFactor);
            this.panel1.Controls.Add(this.nudScaleFactor);
            this.panel1.Controls.Add(this.startStopBtn);
            this.panel1.Controls.Add(this.BtnGen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(101, 247);
            this.panel1.TabIndex = 3;
            // 
            // lblInterval
            // 
            this.lblInterval.Location = new System.Drawing.Point(12, 165);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(75, 23);
            this.lblInterval.TabIndex = 7;
            this.lblInterval.Text = "Interval";
            this.lblInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFillFactor
            // 
            this.lblFillFactor.Location = new System.Drawing.Point(12, 116);
            this.lblFillFactor.Name = "lblFillFactor";
            this.lblFillFactor.Size = new System.Drawing.Size(75, 23);
            this.lblFillFactor.TabIndex = 7;
            this.lblFillFactor.Text = "Fill Factor";
            this.lblFillFactor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScaleFactor
            // 
            this.lblScaleFactor.Location = new System.Drawing.Point(12, 67);
            this.lblScaleFactor.Name = "lblScaleFactor";
            this.lblScaleFactor.Size = new System.Drawing.Size(75, 23);
            this.lblScaleFactor.TabIndex = 7;
            this.lblScaleFactor.Text = "Scale Factor";
            this.lblScaleFactor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudInterval
            // 
            this.nudInterval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudInterval.Location = new System.Drawing.Point(12, 191);
            this.nudInterval.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudInterval.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudInterval.Name = "nudInterval";
            this.nudInterval.Size = new System.Drawing.Size(75, 20);
            this.nudInterval.TabIndex = 6;
            this.nudInterval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudInterval.ValueChanged += new System.EventHandler(this.nudInterval_ValueChanged);
            // 
            // nudFillFactor
            // 
            this.nudFillFactor.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudFillFactor.Location = new System.Drawing.Point(12, 142);
            this.nudFillFactor.Name = "nudFillFactor";
            this.nudFillFactor.Size = new System.Drawing.Size(75, 20);
            this.nudFillFactor.TabIndex = 6;
            this.nudFillFactor.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // nudScaleFactor
            // 
            this.nudScaleFactor.Location = new System.Drawing.Point(12, 93);
            this.nudScaleFactor.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudScaleFactor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudScaleFactor.Name = "nudScaleFactor";
            this.nudScaleFactor.Size = new System.Drawing.Size(75, 20);
            this.nudScaleFactor.TabIndex = 5;
            this.nudScaleFactor.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // startStopBtn
            // 
            this.startStopBtn.Location = new System.Drawing.Point(12, 41);
            this.startStopBtn.Name = "startStopBtn";
            this.startStopBtn.Size = new System.Drawing.Size(75, 23);
            this.startStopBtn.TabIndex = 4;
            this.startStopBtn.Text = "Start/Stop";
            this.startStopBtn.UseVisualStyleBackColor = true;
            this.startStopBtn.Click += new System.EventHandler(this.startStopBtn_Click);
            // 
            // BtnGen
            // 
            this.BtnGen.Location = new System.Drawing.Point(12, 12);
            this.BtnGen.Name = "BtnGen";
            this.BtnGen.Size = new System.Drawing.Size(75, 23);
            this.BtnGen.TabIndex = 3;
            this.BtnGen.Text = "Generate";
            this.BtnGen.UseVisualStyleBackColor = true;
            this.BtnGen.Click += new System.EventHandler(this.BtnGen_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(107, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(396, 247);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // tmr
            // 
            this.tmr.Interval = 1000;
            this.tmr.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbMovingNodes
            // 
            this.cbMovingNodes.Location = new System.Drawing.Point(12, 218);
            this.cbMovingNodes.Name = "cbMovingNodes";
            this.cbMovingNodes.Size = new System.Drawing.Size(75, 24);
            this.cbMovingNodes.TabIndex = 8;
            this.cbMovingNodes.Text = "Moving";
            this.cbMovingNodes.UseVisualStyleBackColor = true;
            this.cbMovingNodes.CheckedChanged += new System.EventHandler(this.cbMovingNodes_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 247);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(200, 150);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFillFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nudScaleFactor;
        private System.Windows.Forms.Button startStopBtn;
        private System.Windows.Forms.Button BtnGen;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.NumericUpDown nudFillFactor;
        private System.Windows.Forms.Label lblScaleFactor;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.Label lblFillFactor;
        private System.Windows.Forms.NumericUpDown nudInterval;
        private System.Windows.Forms.CheckBox cbMovingNodes;

    }
}

