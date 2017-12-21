using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace UnCrashSim
{
    
    public partial class Form2 : Form
    {
        

        
        int light;
        int UpDownlight=0;
        int LeftRightlight=2;
        int[] Times = { 4000, 2000, 6000 };
        int[] UpDownTimes = { 4000, 2000, 6000 };
        int[] LeftRightTimes = { 4000, 2000, 6000 };
        int[] DefTimes = { 4000, 2000, 6000 };
        SizeF mapSize;
        Size gridSize;
        Point focusedInfrastructure = new Point(0, 0);
        bool mouseDown = false, AutoMode = true, selectedAll=true;
        int GridX, GridY;
        Point pointOfSet = new Point(0, 0), mainBitmapPos = new Point(200, 0), centerPointOfSet;
        Bitmap mainBitmap;
        Graphics g;
        Infrastructure[,] roads;
        Thread mainloopT;
        List<UpdateEvent> UpdateEvents = new List<UpdateEvent>();
        //List<Point> upDateInfrastructure = new List<Point>();
        delegate void Action(Point pos);
        class UpdateEvent
        {
            Point pos;
            Action action;
            bool All = false;
            Size gridSize;
            public UpdateEvent(Point pos, Action action)
            {
                this.pos = pos;
                this.action = action;
                All = false;
            }
            public UpdateEvent(Action action, Size gridSize)
            {
                this.action = action;
                this.gridSize = gridSize;
                All = true;
            }
            public void Calc()
            {
                if (All)
                {
                    for (int i = 0; i < gridSize.Height; i++)
                    {
                        for (int j = 0; j < gridSize.Width; j++)
                        {
                            action(new Point(i,j));
                        }
                    }
                }
                else
                {
                    action(pos);
                }
            }
        }
        void ChangeInfrastructureType(Point p)
        {
            int i = p.X, j = p.Y;
            if (roads[i, j].GetType() == typeof(EmptyInfrastructure))
                roads[i, j] = new CrossRoad(roads[i, j].Size, roads[i, j].Pos, new Point(i, j), new Point(GridX - 1, GridY - 1), UpDownTimes, UpDownlight, LeftRightTimes, LeftRightlight, AutoMode);
            else if (roads[i, j].GetType() == typeof(CrossRoad))
                roads[i, j] = new LeftRightRoad(roads[i, j].Size, roads[i, j].Pos, new Point(i, j), new Point(GridX - 1, GridY - 1));
            else if (roads[i, j].GetType() == typeof(LeftRightRoad))
                roads[i, j] = new UpDownRoad(roads[i, j].Size, roads[i, j].Pos, new Point(i, j), new Point(GridX - 1, GridY - 1));
            else
                roads[i, j] = new EmptyInfrastructure(roads[i, j].Size, roads[i, j].Pos, new Point(i, j), new Point(GridX - 1, GridY - 1));
        }
        void upDateInfrastructure(Point p)
        {
            int i = p.X, j = p.Y;
            if (roads[i, j].GetType() == typeof(CrossRoad))
                ((CrossRoad)roads[i, j]).UpdateTraficLight(UpDownTimes, UpDownlight, LeftRightTimes, LeftRightlight);
        }
        void ToggleAuto(Point p)
        {
            int i = p.X, j = p.Y;
            roads[i, j].autoMode = AutoMode;
            if (roads[i, j].GetType() == typeof(CrossRoad))
                ((CrossRoad)roads[i, j]).UpdateTraficLight(UpDownTimes, UpDownlight, LeftRightTimes, LeftRightlight);
        }
        void NextTLLeftRigth(Point p)
        {
            int i = p.X, j = p.Y;
            if (roads[i, j].GetType() == typeof(CrossRoad))
                ((CrossRoad)roads[i, j]).NextTLLeftRigth();
        }
        void NextTLUpDown(Point p)
        {
            int i = p.X, j = p.Y;
            if (roads[i, j].GetType() == typeof(CrossRoad))
                ((CrossRoad)roads[i, j]).NextTLUpDown();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            initializeRoads(gridSize.Width, gridSize.Height);
            g = this.CreateGraphics();
            mainloopT.Start();
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mouseDown = true;
                centerPointOfSet = new Point( e.X+ pointOfSet.X, e.Y + pointOfSet.Y);
            }
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Point tempPoint = new Point(centerPointOfSet.X - e.X, centerPointOfSet.Y - e.Y);
                if (tempPoint.X > 0 &&  tempPoint.X < mapSize.Width - this.ClientSize.Width + 200 )
                {
                    pointOfSet.X = tempPoint.X;
                }
                if (tempPoint.Y > 0 && tempPoint.Y < mapSize.Height - this.ClientSize.Height)
                {
                    pointOfSet.Y = tempPoint.Y;
                }
            }
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mouseDown = false;
            }
        }

        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            int i = (int)((e.X - mainBitmapPos.X + pointOfSet.X) * (float)this.gridSize.Width / (mapSize.Width));
            int j = (int)((e.Y - mainBitmapPos.Y + pointOfSet.Y) * (float)this.gridSize.Height / (mapSize.Height));
            if (j<gridSize.Height&&i<gridSize.Width && e.X > mainBitmapPos.X)
            {
                if (e.Button == MouseButtons.Left )
                {
                    UpdateEvents.Add(new UpdateEvent( new Point(i, j), ChangeInfrastructureType));
                }
                else if (e.Button == MouseButtons.Right)
                {
                    indexXInfrastructure.Value = i;
                    indexYInfrastructure.Value = j;
                }
            }
        }

        private void LoadDefaultLeftRight_Click(object sender, EventArgs e)
        {
            RedLeftRight.Value = DefTimes[2];
            OrangeLeftRight.Value = DefTimes[1];
            GreenLeftRight.Value = DefTimes[0];
            NextLightLeftRight.BackColor = RedLeftRight.BackColor;
            LeftRightlight = 2;
        }

        private void LoadDefaultUpDown_Click(object sender, EventArgs e)
        {
            RedUpDown.Value = DefTimes[2];
            OrangeUpDown.Value = DefTimes[1];
            GreenUpDown.Value = DefTimes[0];
            NextLightUpDown.BackColor = GreenLeftRight.BackColor;
            UpDownlight = 0;
        }
        private void Update_Click(object sender, EventArgs e)
        {
            if (selectedAll)
            {
                UpdateEvents.Add(new UpdateEvent(upDateInfrastructure, gridSize));
            }
            else
            {
                UpdateEvents.Add(new UpdateEvent(new Point((int)indexXInfrastructure.Value, (int)indexYInfrastructure.Value), upDateInfrastructure));
            }
        }

        private void NextLightLeftRight_Click(object sender, EventArgs e)
        {
            if (LeftRightlight==2)
            {
                NextLightLeftRight.BackColor = GreenLeftRight.BackColor;
                LeftRightlight=0;
            }
            else if (LeftRightlight == 0)
            {
                NextLightLeftRight.BackColor = OrangeLeftRight.BackColor;
                LeftRightlight++;
            }
            else if (LeftRightlight ==1)
            {
                NextLightLeftRight.BackColor = RedLeftRight.BackColor;
                LeftRightlight++;
            }
            if (!AutoMode)
            {
                if (selectedAll)
                {
                    UpdateEvents.Add(new UpdateEvent(NextTLLeftRigth, gridSize));
                }
                else
                {
                    UpdateEvents.Add(new UpdateEvent(new Point((int)indexXInfrastructure.Value, (int)indexYInfrastructure.Value), NextTLLeftRigth));
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void RedLeftRight_ValueChanged(object sender, EventArgs e)
        {
            LeftRightTimes[2] = (int)RedLeftRight.Value;
        }

        private void OrangeLeftRight_ValueChanged(object sender, EventArgs e)
        {
            LeftRightTimes[1] = (int)OrangeLeftRight.Value;
        }

        private void GreenLeftRight_ValueChanged(object sender, EventArgs e)
        {
            LeftRightTimes[0] = (int)GreenLeftRight.Value;
        }

        private void RedUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpDownTimes[2] = (int)RedUpDown.Value;
        }

        private void OrangeUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpDownTimes[1] = (int)OrangeUpDown.Value;
        }

        private void GreenUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpDownTimes[0] = (int)GreenUpDown.Value;
        }
        private void Specific_Click(object sender, EventArgs e)
        {
            selectedAll = false;
            location.Visible = true;

        }

        private void Auto_Click(object sender, EventArgs e)
        {
            AutoMode = !AutoMode;
            if (AutoMode)
            {
                Auto.Text = "Toggle Manual";
            }
            else
            {
                Auto.Text = "Toggle Auto";
            }
            UpdateEvents.Add(new UpdateEvent(ToggleAuto, gridSize));
            
        }

        private void indexXInfrastructure_ValueChanged(object sender, EventArgs e)
        {
            if (!AutoMode) {
                UpdateEvents.Add(new UpdateEvent(new Point((int)indexXInfrastructure.Value, (int)indexYInfrastructure.Value), upDateInfrastructure));
            }
        }

        private void indexYInfrastructure_ValueChanged(object sender, EventArgs e)
        {
            if (!AutoMode)
            {
                UpdateEvents.Add(new UpdateEvent(new Point((int)indexXInfrastructure.Value, (int)indexYInfrastructure.Value), upDateInfrastructure));
            }
        }

        private void All_Click(object sender, EventArgs e)
        {
            selectedAll = !selectedAll;
            location.Visible = !selectedAll;
            if (!selectedAll)
            {
                All.Text = "Toggle All";
            }
            else
            {
                All.Text = "Toggle Specific";
                UpdateEvents.Add(new UpdateEvent(upDateInfrastructure, gridSize));
            }
        }

        private void NextLightUpDown_Click(object sender, EventArgs e)
        {
            if (UpDownlight==2)
            {
                NextLightUpDown.BackColor = GreenUpDown.BackColor;
                UpDownlight = 0;
            }
            else if (UpDownlight == 0)
            {
                NextLightUpDown.BackColor = OrangeUpDown.BackColor;
                UpDownlight++;
            }
            else if (UpDownlight == 1)
            {
                NextLightUpDown.BackColor = RedUpDown.BackColor;
                UpDownlight++;
            }
            if (!AutoMode)
            {
                if (selectedAll)
                {
                    UpdateEvents.Add(new UpdateEvent(NextTLUpDown, gridSize));
                }
                else
                {
                    UpdateEvents.Add(new UpdateEvent(new Point((int)indexXInfrastructure.Value, (int)indexYInfrastructure.Value), NextTLUpDown));
                }
            }
        }

        public Form2(int[] Times, SizeF mapSize, Size gridSize,int light)
        { 
            InitializeComponent();
            mainloopT = new Thread(this.mainloop);
            this.Closed += (s, args) => mainloopT.Abort();
            this.Times = Times;
            this.mapSize = mapSize;
            this.gridSize = gridSize;
            this.light = light;
            indexXInfrastructure.Maximum = gridSize.Width-1;
            indexYInfrastructure.Maximum = gridSize.Height-1;
        }
        private void initializeRoads(int GridX, int GridY)
        {
            roads = new Infrastructure[GridX, GridY];
            this.GridX = GridX;
            this.GridY = GridY;
            mainBitmap = new Bitmap((int)mapSize.Width, (int)mapSize.Height);
            for (int i = 0; i < GridX; i++)
            {
                for (int j = 0; j < GridY; j++)
                {
                    if (i == 1)
                        roads[i, j] = new CrossRoad(new Size(mainBitmap.Width / GridX, mainBitmap.Height / GridY), new Point(mainBitmap.Width * i / GridX, mainBitmap.Height * j / GridY), new Point(i, j), new Point(GridX - 1, GridY - 1), UpDownTimes, UpDownlight, LeftRightTimes, LeftRightlight, AutoMode);
                    else if (i == 0)
                        roads[i, j] = new LeftRightRoad(new Size(mainBitmap.Width / GridX, mainBitmap.Height / GridY), new Point(mainBitmap.Width * i / GridX, mainBitmap.Height * j / GridY), new Point(i, j), new Point(GridX - 1, GridY - 1));
                    else
                        roads[i, j] = new UpDownRoad(new Size(mainBitmap.Width / GridX, mainBitmap.Height / GridY), new Point(mainBitmap.Width * i / GridX, mainBitmap.Height * j / GridY), new Point(i, j), new Point(GridX - 1, GridY - 1));
                }
            }
        }
        private void mainloop()
        {
            mainBitmap = new Bitmap((int)mapSize.Width,(int)mapSize.Height);
            Graphics tempG = Graphics.FromImage(mainBitmap);
            while (Application.OpenForms.OfType<Form2>().Any())
            {
                tempG.Clear(this.BackColor);
                for (int i = 0; i < GridX; i++)
                {
                    for (int j = 0; j < GridY; j++)
                    {
                        roads[i, j].render(tempG);
                    }
                }
                Rectangle rect = new Rectangle(pointOfSet, new Size(this.ClientSize.Width - mainBitmapPos.X, this.ClientSize.Height));
                g.DrawImage(mainBitmap, mainBitmapPos.X, mainBitmapPos.Y, rect, GraphicsUnit.Pixel);
                for (int i = 0; i < GridX; i++)
                {
                    for (int j = 0; j < GridY; j++)
                    {
                        roads[i, j].calculate(ref roads);
                    }
                }
                foreach (UpdateEvent e in UpdateEvents)
                {
                    e.Calc();
                }
                UpdateEvents.Clear();
            }
            tempG.Dispose();
        }
    }
}