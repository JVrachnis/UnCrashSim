namespace UnCrashSim
{
    partial class Form2
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
            this.LeftRight = new System.Windows.Forms.GroupBox();
            this.LoadDefaultLeftRight = new System.Windows.Forms.Button();
            this.NextLightLeftRight = new System.Windows.Forms.Button();
            this.GreenLeftRight = new System.Windows.Forms.NumericUpDown();
            this.OrangeLeftRight = new System.Windows.Forms.NumericUpDown();
            this.RedLeftRight = new System.Windows.Forms.NumericUpDown();
            this.UpDown = new System.Windows.Forms.GroupBox();
            this.LoadDefaultUpDown = new System.Windows.Forms.Button();
            this.NextLightUpDown = new System.Windows.Forms.Button();
            this.GreenUpDown = new System.Windows.Forms.NumericUpDown();
            this.OrangeUpDown = new System.Windows.Forms.NumericUpDown();
            this.RedUpDown = new System.Windows.Forms.NumericUpDown();
            this.Update = new System.Windows.Forms.Button();
            this.indexXInfrastructure = new System.Windows.Forms.NumericUpDown();
            this.indexYInfrastructure = new System.Windows.Forms.NumericUpDown();
            this.Auto = new System.Windows.Forms.Button();
            this.All = new System.Windows.Forms.Button();
            this.location = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LeftRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GreenLeftRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrangeLeftRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedLeftRight)).BeginInit();
            this.UpDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GreenUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrangeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.indexXInfrastructure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.indexYInfrastructure)).BeginInit();
            this.location.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftRight
            // 
            this.LeftRight.Controls.Add(this.LoadDefaultLeftRight);
            this.LeftRight.Controls.Add(this.NextLightLeftRight);
            this.LeftRight.Controls.Add(this.GreenLeftRight);
            this.LeftRight.Controls.Add(this.OrangeLeftRight);
            this.LeftRight.Controls.Add(this.RedLeftRight);
            this.LeftRight.Location = new System.Drawing.Point(5, 135);
            this.LeftRight.Name = "LeftRight";
            this.LeftRight.Size = new System.Drawing.Size(81, 146);
            this.LeftRight.TabIndex = 0;
            this.LeftRight.TabStop = false;
            this.LeftRight.Text = "Left Right";
            // 
            // LoadDefaultLeftRight
            // 
            this.LoadDefaultLeftRight.Location = new System.Drawing.Point(3, 123);
            this.LoadDefaultLeftRight.Name = "LoadDefaultLeftRight";
            this.LoadDefaultLeftRight.Size = new System.Drawing.Size(75, 23);
            this.LoadDefaultLeftRight.TabIndex = 4;
            this.LoadDefaultLeftRight.Text = "Set Def";
            this.LoadDefaultLeftRight.UseVisualStyleBackColor = true;
            this.LoadDefaultLeftRight.Click += new System.EventHandler(this.LoadDefaultLeftRight_Click);
            // 
            // NextLightLeftRight
            // 
            this.NextLightLeftRight.BackColor = System.Drawing.Color.Red;
            this.NextLightLeftRight.Location = new System.Drawing.Point(3, 94);
            this.NextLightLeftRight.Name = "NextLightLeftRight";
            this.NextLightLeftRight.Size = new System.Drawing.Size(75, 23);
            this.NextLightLeftRight.TabIndex = 3;
            this.NextLightLeftRight.Text = "Next Light";
            this.NextLightLeftRight.UseVisualStyleBackColor = false;
            this.NextLightLeftRight.Click += new System.EventHandler(this.NextLightLeftRight_Click);
            // 
            // GreenLeftRight
            // 
            this.GreenLeftRight.BackColor = System.Drawing.Color.Lime;
            this.GreenLeftRight.Location = new System.Drawing.Point(3, 68);
            this.GreenLeftRight.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.GreenLeftRight.Name = "GreenLeftRight";
            this.GreenLeftRight.Size = new System.Drawing.Size(75, 20);
            this.GreenLeftRight.TabIndex = 2;
            this.GreenLeftRight.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.GreenLeftRight.ValueChanged += new System.EventHandler(this.GreenLeftRight_ValueChanged);
            // 
            // OrangeLeftRight
            // 
            this.OrangeLeftRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.OrangeLeftRight.Location = new System.Drawing.Point(3, 42);
            this.OrangeLeftRight.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.OrangeLeftRight.Name = "OrangeLeftRight";
            this.OrangeLeftRight.Size = new System.Drawing.Size(75, 20);
            this.OrangeLeftRight.TabIndex = 1;
            this.OrangeLeftRight.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.OrangeLeftRight.ValueChanged += new System.EventHandler(this.OrangeLeftRight_ValueChanged);
            // 
            // RedLeftRight
            // 
            this.RedLeftRight.BackColor = System.Drawing.Color.Red;
            this.RedLeftRight.Location = new System.Drawing.Point(3, 16);
            this.RedLeftRight.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.RedLeftRight.Name = "RedLeftRight";
            this.RedLeftRight.Size = new System.Drawing.Size(75, 20);
            this.RedLeftRight.TabIndex = 0;
            this.RedLeftRight.Value = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.RedLeftRight.ValueChanged += new System.EventHandler(this.RedLeftRight_ValueChanged);
            // 
            // UpDown
            // 
            this.UpDown.Controls.Add(this.LoadDefaultUpDown);
            this.UpDown.Controls.Add(this.NextLightUpDown);
            this.UpDown.Controls.Add(this.GreenUpDown);
            this.UpDown.Controls.Add(this.OrangeUpDown);
            this.UpDown.Controls.Add(this.RedUpDown);
            this.UpDown.Location = new System.Drawing.Point(89, 135);
            this.UpDown.Name = "UpDown";
            this.UpDown.Size = new System.Drawing.Size(84, 146);
            this.UpDown.TabIndex = 1;
            this.UpDown.TabStop = false;
            this.UpDown.Text = "Up Down";
            // 
            // LoadDefaultUpDown
            // 
            this.LoadDefaultUpDown.Location = new System.Drawing.Point(6, 123);
            this.LoadDefaultUpDown.Name = "LoadDefaultUpDown";
            this.LoadDefaultUpDown.Size = new System.Drawing.Size(75, 23);
            this.LoadDefaultUpDown.TabIndex = 7;
            this.LoadDefaultUpDown.Text = "Set Def";
            this.LoadDefaultUpDown.UseVisualStyleBackColor = true;
            this.LoadDefaultUpDown.Click += new System.EventHandler(this.LoadDefaultUpDown_Click);
            // 
            // NextLightUpDown
            // 
            this.NextLightUpDown.BackColor = System.Drawing.Color.Lime;
            this.NextLightUpDown.Location = new System.Drawing.Point(6, 94);
            this.NextLightUpDown.Name = "NextLightUpDown";
            this.NextLightUpDown.Size = new System.Drawing.Size(75, 23);
            this.NextLightUpDown.TabIndex = 6;
            this.NextLightUpDown.Text = "Next Light";
            this.NextLightUpDown.UseVisualStyleBackColor = false;
            this.NextLightUpDown.Click += new System.EventHandler(this.NextLightUpDown_Click);
            // 
            // GreenUpDown
            // 
            this.GreenUpDown.BackColor = System.Drawing.Color.Lime;
            this.GreenUpDown.Location = new System.Drawing.Point(6, 68);
            this.GreenUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.GreenUpDown.Name = "GreenUpDown";
            this.GreenUpDown.Size = new System.Drawing.Size(75, 20);
            this.GreenUpDown.TabIndex = 5;
            this.GreenUpDown.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.GreenUpDown.ValueChanged += new System.EventHandler(this.GreenUpDown_ValueChanged);
            // 
            // OrangeUpDown
            // 
            this.OrangeUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.OrangeUpDown.Location = new System.Drawing.Point(6, 42);
            this.OrangeUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.OrangeUpDown.Name = "OrangeUpDown";
            this.OrangeUpDown.Size = new System.Drawing.Size(75, 20);
            this.OrangeUpDown.TabIndex = 4;
            this.OrangeUpDown.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.OrangeUpDown.ValueChanged += new System.EventHandler(this.OrangeUpDown_ValueChanged);
            // 
            // RedUpDown
            // 
            this.RedUpDown.BackColor = System.Drawing.Color.Red;
            this.RedUpDown.Location = new System.Drawing.Point(6, 16);
            this.RedUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.RedUpDown.Name = "RedUpDown";
            this.RedUpDown.Size = new System.Drawing.Size(75, 20);
            this.RedUpDown.TabIndex = 3;
            this.RedUpDown.Value = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.RedUpDown.ValueChanged += new System.EventHandler(this.RedUpDown_ValueChanged);
            // 
            // Update
            // 
            this.Update.Location = new System.Drawing.Point(50, 287);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(75, 23);
            this.Update.TabIndex = 2;
            this.Update.Text = "Update";
            this.Update.UseVisualStyleBackColor = true;
            this.Update.Click += new System.EventHandler(this.Update_Click);
            // 
            // indexXInfrastructure
            // 
            this.indexXInfrastructure.Location = new System.Drawing.Point(6, 16);
            this.indexXInfrastructure.Name = "indexXInfrastructure";
            this.indexXInfrastructure.ReadOnly = true;
            this.indexXInfrastructure.Size = new System.Drawing.Size(36, 20);
            this.indexXInfrastructure.TabIndex = 9;
            this.indexXInfrastructure.ValueChanged += new System.EventHandler(this.indexXInfrastructure_ValueChanged);
            // 
            // indexYInfrastructure
            // 
            this.indexYInfrastructure.Location = new System.Drawing.Point(48, 16);
            this.indexYInfrastructure.Name = "indexYInfrastructure";
            this.indexYInfrastructure.ReadOnly = true;
            this.indexYInfrastructure.Size = new System.Drawing.Size(33, 20);
            this.indexYInfrastructure.TabIndex = 10;
            this.indexYInfrastructure.ValueChanged += new System.EventHandler(this.indexYInfrastructure_ValueChanged);
            // 
            // Auto
            // 
            this.Auto.Location = new System.Drawing.Point(35, 12);
            this.Auto.Name = "Auto";
            this.Auto.Size = new System.Drawing.Size(99, 23);
            this.Auto.TabIndex = 11;
            this.Auto.Text = "Toggle Manual";
            this.Auto.UseVisualStyleBackColor = true;
            this.Auto.Click += new System.EventHandler(this.Auto_Click);
            // 
            // All
            // 
            this.All.Location = new System.Drawing.Point(35, 41);
            this.All.Name = "All";
            this.All.Size = new System.Drawing.Size(99, 23);
            this.All.TabIndex = 13;
            this.All.Text = "Toggle Specific";
            this.All.UseVisualStyleBackColor = true;
            this.All.Click += new System.EventHandler(this.All_Click);
            // 
            // location
            // 
            this.location.Controls.Add(this.label1);
            this.location.Controls.Add(this.indexXInfrastructure);
            this.location.Controls.Add(this.indexYInfrastructure);
            this.location.Location = new System.Drawing.Point(26, 70);
            this.location.Name = "location";
            this.location.Size = new System.Drawing.Size(122, 59);
            this.location.TabIndex = 15;
            this.location.TabStop = false;
            this.location.Text = "Location";
            this.location.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "help";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(173, 351);
            this.Controls.Add(this.location);
            this.Controls.Add(this.All);
            this.Controls.Add(this.Auto);
            this.Controls.Add(this.Update);
            this.Controls.Add(this.UpDown);
            this.Controls.Add(this.LeftRight);
            this.Name = "Form2";
            this.Text = "UnCrashSim";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseUp);
            this.LeftRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GreenLeftRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrangeLeftRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedLeftRight)).EndInit();
            this.UpDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GreenUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrangeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.indexXInfrastructure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.indexYInfrastructure)).EndInit();
            this.location.ResumeLayout(false);
            this.location.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox LeftRight;
        private System.Windows.Forms.Button LoadDefaultLeftRight;
        private System.Windows.Forms.Button NextLightLeftRight;
        private System.Windows.Forms.NumericUpDown GreenLeftRight;
        private System.Windows.Forms.NumericUpDown OrangeLeftRight;
        private System.Windows.Forms.NumericUpDown RedLeftRight;
        private System.Windows.Forms.GroupBox UpDown;
        private System.Windows.Forms.Button LoadDefaultUpDown;
        private System.Windows.Forms.Button NextLightUpDown;
        private System.Windows.Forms.NumericUpDown GreenUpDown;
        private System.Windows.Forms.NumericUpDown OrangeUpDown;
        private System.Windows.Forms.NumericUpDown RedUpDown;
        private System.Windows.Forms.Button Update;
        private System.Windows.Forms.NumericUpDown indexXInfrastructure;
        private System.Windows.Forms.NumericUpDown indexYInfrastructure;
        private System.Windows.Forms.Button Auto;
        private System.Windows.Forms.Button All;
        private System.Windows.Forms.GroupBox location;
        private System.Windows.Forms.Label label1;
    }
}