using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
namespace UnCrashSim
{
    public partial class Form1 : Form
    {
        int GridX , GridY;
        SizeF mapSize;
        Point pointOfSet=new Point(0,0), mainBitmapPos=new Point(200,0), centerPointOfSet;
        int ligth = 0;
        Bitmap mainBitmap;
        Graphics g;
        Thread mainloopT;
        int[] Times = { 4000, 2000, 6000 };
        TrafficLight tl ;
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            tl = new TrafficLight(new Light(new RectangleF(50, 10, 120, 40), 90), Times, ligth);
            mainloopT = new Thread(this.mainloop);
            mainloopT.Name = "Main Loop";
        }
        private void mainloop()
        {
            Bitmap tempBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            while (Application.OpenForms.OfType<Form1>().Any()) {
                tl.calculate();
                
                Graphics tempG = Graphics.FromImage(tempBitmap);
                tl.render(tempG);
                g.DrawImage(tempBitmap, 0, 0);
                tempG.Dispose();
            }
        }


        private void Ok_Click(object sender, EventArgs e)
        {
            this.flowLayoutPanel1.Hide();
            this.Ok.Hide();
            this.flowLayoutPanel2.Hide();
            Times[0] = (int)this.TimeGreen.Value;
            Times[1] = (int)this.TimeOrange.Value;
            Times[2] = (int)this.TimeRed.Value;
            mapSize = new Size((int)this.MapWidth.Value, (int)this.MapHeight.Value);
            Form2 sup = new Form2(Times, mapSize,new Size((int)this.GridWidth.Value, (int)this.GridHeight.Value),ligth);
            sup.Closed += (s, args) => this.Close();
            sup.Width = (int)this.WindowWidth.Value;
            sup.Height = (int)this.WindowHeight.Value;
            this.Hide();
            sup.Show();

            mainloopT.Abort();
        }
        private void MapWidth_ValueChanged(object sender, EventArgs e)
        {
            GridWidth.Maximum = (int)MapWidth.Value / 100;
            MapHeight.Value = MapWidth.Value;
        }

        private void MapHeight_ValueChanged(object sender, EventArgs e)
        {
            GridHeight.Maximum = (int)MapHeight.Value / 100;
            MapWidth.Value = MapHeight.Value;
        }
        private void LoadDefault_Click(object sender, EventArgs e)
        {
            this.TimeGreen.Value = 4000;
            this.TimeOrange.Value = 2000;
            this.TimeRed.Value = 6000;
            ligth = 0;
        }

        private void NextLight_Click(object sender, EventArgs e)
        {
            if (ligth < 2)
                ligth++;
            else
                ligth = 0;
            tl.light = ligth;
            Bitmap tempBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics tempG = Graphics.FromImage(tempBitmap);
            //tempG.Clear(Color.DarkGreen);


            tl.render(tempG);
            g.DrawImage(tempBitmap, 0, 0);
            tempG.Dispose();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (!mainloopT.IsAlive) {
                Bitmap tempBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                Graphics tempG = Graphics.FromImage(tempBitmap);
                //tempG.Clear(Color.DarkGreen);
                tl.render(tempG);
                g.DrawImage(tempBitmap, 0, 0);
                tempG.Dispose();
            }
        }
        private void PreView_Click(object sender, EventArgs e)
        {
            if (mainloopT.ThreadState ==ThreadState.Unstarted)
            {
                
                mainloopT.Start();
            }else if (mainloopT.ThreadState == ThreadState.Aborted)
            {
                mainloopT = new Thread(this.mainloop);
                mainloopT.Start();
            }
            else
            {
                mainloopT.Abort();
            }
        }

        private void TimeRed_ValueChanged(object sender, EventArgs e)
        {
            Times[2] = (int)TimeRed.Value;
            tl.update(Times,ligth);
        }

        private void TimeOrange_ValueChanged(object sender, EventArgs e)
        {
            Times[1] = (int)TimeOrange.Value;
            tl.update(Times, ligth);
        }

        private void TimeGreen_ValueChanged(object sender, EventArgs e)
        {
            Times[0] = (int)TimeGreen.Value;
            tl.update(Times, ligth);
        }

        private void GridWidth_ValueChanged(object sender, EventArgs e)
        {
            GridHeight.Value = GridWidth.Value;
        }

        private void GridHeight_ValueChanged(object sender, EventArgs e)
        {
            GridWidth.Value = GridHeight.Value;
        }
    }
}