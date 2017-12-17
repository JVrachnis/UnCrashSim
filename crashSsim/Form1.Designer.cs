namespace UnCrashSim
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
            this.Ok = new System.Windows.Forms.Button();
            this.NextLight = new System.Windows.Forms.Button();
            this.LoadDefault = new System.Windows.Forms.Button();
            this.TimeRed = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.TimeOrange = new System.Windows.Forms.NumericUpDown();
            this.TimeGreen = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GridWidth = new System.Windows.Forms.NumericUpDown();
            this.GridHeight = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.WindowHeight = new System.Windows.Forms.NumericUpDown();
            this.WindowWidth = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.TimeRed)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeOrange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeGreen)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridHeight)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WindowHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindowWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(141, 157);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(90, 23);
            this.Ok.TabIndex = 0;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // NextLight
            // 
            this.NextLight.Location = new System.Drawing.Point(3, 3);
            this.NextLight.Name = "NextLight";
            this.NextLight.Size = new System.Drawing.Size(90, 23);
            this.NextLight.TabIndex = 2;
            this.NextLight.Text = "Next Light";
            this.NextLight.UseVisualStyleBackColor = true;
            this.NextLight.Click += new System.EventHandler(this.NextLight_Click);
            // 
            // LoadDefault
            // 
            this.LoadDefault.Location = new System.Drawing.Point(3, 110);
            this.LoadDefault.Name = "LoadDefault";
            this.LoadDefault.Size = new System.Drawing.Size(90, 23);
            this.LoadDefault.TabIndex = 3;
            this.LoadDefault.Text = "Load Default";
            this.LoadDefault.UseVisualStyleBackColor = true;
            this.LoadDefault.Click += new System.EventHandler(this.LoadDefault_Click);
            // 
            // TimeRed
            // 
            this.TimeRed.Location = new System.Drawing.Point(3, 32);
            this.TimeRed.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.TimeRed.Name = "TimeRed";
            this.TimeRed.Size = new System.Drawing.Size(90, 20);
            this.TimeRed.TabIndex = 10;
            this.TimeRed.Value = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.NextLight);
            this.flowLayoutPanel1.Controls.Add(this.TimeRed);
            this.flowLayoutPanel1.Controls.Add(this.TimeOrange);
            this.flowLayoutPanel1.Controls.Add(this.TimeGreen);
            this.flowLayoutPanel1.Controls.Add(this.LoadDefault);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(65, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(101, 139);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // TimeOrange
            // 
            this.TimeOrange.Location = new System.Drawing.Point(3, 58);
            this.TimeOrange.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.TimeOrange.Name = "TimeOrange";
            this.TimeOrange.Size = new System.Drawing.Size(90, 20);
            this.TimeOrange.TabIndex = 11;
            this.TimeOrange.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // TimeGreen
            // 
            this.TimeGreen.Location = new System.Drawing.Point(3, 84);
            this.TimeGreen.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.TimeGreen.Name = "TimeGreen";
            this.TimeGreen.Size = new System.Drawing.Size(90, 20);
            this.TimeGreen.TabIndex = 12;
            this.TimeGreen.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.groupBox1);
            this.flowLayoutPanel2.Controls.Add(this.groupBox2);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(172, 12);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(209, 139);
            this.flowLayoutPanel2.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.GridWidth);
            this.groupBox1.Controls.Add(this.GridHeight);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(95, 136);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grid Size";
            // 
            // GridWidth
            // 
            this.GridWidth.Location = new System.Drawing.Point(5, 19);
            this.GridWidth.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GridWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GridWidth.Name = "GridWidth";
            this.GridWidth.Size = new System.Drawing.Size(90, 20);
            this.GridWidth.TabIndex = 12;
            this.GridWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // GridHeight
            // 
            this.GridHeight.Location = new System.Drawing.Point(5, 45);
            this.GridHeight.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GridHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GridHeight.Name = "GridHeight";
            this.GridHeight.Size = new System.Drawing.Size(90, 20);
            this.GridHeight.TabIndex = 11;
            this.GridHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.WindowHeight);
            this.groupBox2.Controls.Add(this.WindowWidth);
            this.groupBox2.Location = new System.Drawing.Point(104, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(95, 136);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Window Size";
            // 
            // WindowHeight
            // 
            this.WindowHeight.Location = new System.Drawing.Point(6, 45);
            this.WindowHeight.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.WindowHeight.Name = "WindowHeight";
            this.WindowHeight.Size = new System.Drawing.Size(90, 20);
            this.WindowHeight.TabIndex = 14;
            this.WindowHeight.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.WindowHeight.ValueChanged += new System.EventHandler(this.WindowHeight_ValueChanged);
            // 
            // WindowWidth
            // 
            this.WindowWidth.Location = new System.Drawing.Point(6, 19);
            this.WindowWidth.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.WindowWidth.Name = "WindowWidth";
            this.WindowWidth.Size = new System.Drawing.Size(90, 20);
            this.WindowWidth.TabIndex = 13;
            this.WindowWidth.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.WindowWidth.ValueChanged += new System.EventHandler(this.WindowWidth_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(101, 107);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(393, 188);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "CrashSim";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.TimeRed)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TimeOrange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeGreen)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridHeight)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WindowHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindowWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button NextLight;
        private System.Windows.Forms.Button LoadDefault;
        private System.Windows.Forms.NumericUpDown TimeRed;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.NumericUpDown TimeOrange;
        private System.Windows.Forms.NumericUpDown TimeGreen;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown GridWidth;
        private System.Windows.Forms.NumericUpDown GridHeight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown WindowHeight;
        private System.Windows.Forms.NumericUpDown WindowWidth;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

