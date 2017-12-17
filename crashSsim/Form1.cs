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
        
        int X , Y;
        int ligth = 0;
        Bitmap mainBitmap;
        Graphics g;
        Infrastructure[,] roads ;
        Thread mainloopT;
        int[] Times = { 4000, 2000, 6000 };
        TrafficLight tl ;
        List<Point> ChangeInfrastructureType = new List<Point>();
        List<Point> InfrastructureChanges = new List<Point>();
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();


            tl = new TrafficLight(new Light(new RectangleF(50, 10, 120, 40), 90), Times, ligth);
            mainloopT = new Thread(this.mainloop);


        }
        private void initializeRoads(int X,int Y)
        {
            g = this.CreateGraphics();
            roads = new Infrastructure[X, Y];
            this.X = X;
            this.Y = Y;
            mainBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    if (i == 1)
                        roads[i, j] = new CrossRoad(new Size(mainBitmap.Width / X, mainBitmap.Height / Y), new Point(mainBitmap.Width * i / X, mainBitmap.Height * j / Y), new Point(i, j), new Point(X - 1, Y - 1), Times, ligth);
                    else if (i == 0)
                        roads[i, j] = new LeftRightRoad(new Size(mainBitmap.Width / X, mainBitmap.Height / Y), new Point(mainBitmap.Width * i / X, mainBitmap.Height * j / Y), new Point(i, j), new Point(X - 1, Y - 1));
                    else
                        roads[i, j] = new UpDownRoad(new Size(mainBitmap.Width / X, mainBitmap.Height / Y), new Point(mainBitmap.Width * i / X, mainBitmap.Height * j / Y), new Point(i, j), new Point(X - 1, Y - 1));
                }
            }
        }
        private void mainloop()
        {
            mainBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics tempG = Graphics.FromImage(mainBitmap);

            while (Application.OpenForms.OfType<Form1>().Any()) {
                tempG.Clear(this.BackColor);

                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        roads[i, j].render(tempG);
                    }
                }
                g.DrawImage(mainBitmap, 0, 0);
                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        roads[i, j].calculate(ref roads);
                    }
                }
                foreach (Point p in ChangeInfrastructureType)
                {
                    int i = p.X, j = p.Y;
                    if (roads[i, j].GetType() == typeof(EmptyInfrastructure))
                        roads[i, j] = new CrossRoad(new Size(mainBitmap.Width / X, mainBitmap.Height / Y), new Point(mainBitmap.Width * i / X, mainBitmap.Height * j / Y), new Point(i, j), new Point(X - 1, Y - 1), Times, ligth);
                    else if (roads[i, j].GetType() == typeof(CrossRoad))
                        roads[i, j] = new LeftRightRoad(new Size(mainBitmap.Width / X, mainBitmap.Height / Y), new Point(mainBitmap.Width * i / X, mainBitmap.Height * j / Y), new Point(i, j), new Point(X - 1, Y - 1));
                    else if (roads[i, j].GetType() == typeof(LeftRightRoad))
                        roads[i, j] = new UpDownRoad(new Size(mainBitmap.Width / X, mainBitmap.Height / Y), new Point(mainBitmap.Width * i / X, mainBitmap.Height * j / Y), new Point(i, j), new Point(X - 1, Y - 1));
                    else
                        roads[i, j] = new EmptyInfrastructure(new Size(mainBitmap.Width / X, mainBitmap.Height / Y), new Point(mainBitmap.Width * i / X, mainBitmap.Height * j / Y), new Point(i, j), new Point(X - 1, Y - 1));
                }
                ChangeInfrastructureType.Clear();
            }
            tempG.Dispose();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            this.flowLayoutPanel1.Hide();
            this.Ok.Hide();
            this.flowLayoutPanel2.Hide();
            this.Width = (int)this.WindowWidth.Value;
            this.Height = (int)this.WindowHeight.Value;
            Times[0] = (int)this.TimeGreen.Value;
            Times[1] = (int)this.TimeOrange.Value;
            Times[2] = (int)this.TimeRed.Value;
            initializeRoads((int)this.GridWidth.Value, (int)this.GridHeight.Value);
            mainloopT.Start();
        }

        private void WindowWidth_ValueChanged(object sender, EventArgs e)
        {
            GridWidth.Maximum = (int)WindowWidth.Value / 100;
        }

        private void WindowHeight_ValueChanged(object sender, EventArgs e)
        {
            GridHeight.Maximum = (int)WindowHeight.Value / 100;
        }

        private void LoadDefault_Click(object sender, EventArgs e)
        {
            this.TimeGreen.Value = 4000;
            this.TimeOrange.Value = 2000;
            this.TimeRed.Value = 6000;
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

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                int i = (int)(e.X * this.GridWidth.Value / this.Width);
                int j = (int)(e.Y * this.GridHeight.Value / this.Height);
                ChangeInfrastructureType.Add(new Point(i, j));
            }else if(e.Button == MouseButtons.Left)
            {

            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }
    }
    

    
}