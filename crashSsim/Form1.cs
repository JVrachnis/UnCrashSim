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
using System.Collections.Concurrent;
using System.Diagnostics;
namespace UnCrashSim
{
    public partial class Form1 : Form
    {
        const int X =10, Y = 10;
        Bitmap mainBitmap;
        Graphics g;
        Road[,] roads = new Road[X,Y];
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            mainBitmap = new Bitmap(this.ClientSize.Width,this.ClientSize.Height);
            for (int i =0;i<X;i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    roads[i, j] = new Road(new Size(mainBitmap.Width / X, mainBitmap.Height / Y),new Point(mainBitmap.Width * i / X, mainBitmap.Height * j / Y), new Point(i, j), new Point(X-1, Y-1));
                }
            }

            
            Thread mainloop = new Thread(this.mainloop);
            mainloop.Start();
        }
        private void mainloop()
        {
            Graphics tempG = Graphics.FromImage(mainBitmap);
            while (Application.OpenForms.OfType<Form1>().Any()) {
                
                tempG.Clear(Color.Green);
                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        roads[i, j].render(tempG);
                    }
                }
                // r.render(tempG);
                g.DrawImage(mainBitmap, 0, 0);
                //r.calculate();
                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        roads[i, j].calculate(ref roads);
                    }
                }
                
                
            }
            tempG.Dispose();
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }
    }
    public class Road
    {
        TrafficLight tLUpDown,tLLeftRigth;
        //Road[,] roads;
        SolidBrush b = new SolidBrush(Color.DarkGray);
        public List<Car> cars = new List<Car>();
        List<Car> carsToRemove = new List<Car>();
        Size size = new Size(300, 300);
        Point pos = new Point(0, 0);
        Point posToList = new Point(0, 0);
        Point maxPosToList = new Point(0, 0);
        public Road(Size size, Point pos, Point posToList, Point maxPosToList)
        {
            this.maxPosToList = maxPosToList;
            //this.roads = roads;
            this.posToList = posToList;
            this.size = size;
            this.pos= pos ;
            tLUpDown = new TrafficLight(new Light(new RectangleF( pos.X + size.Width * 5 / 8, pos.Y + size.Height * 5 / 8 + size.Width / 30, size.Width / 30, size.Height / 6),0),0);
            tLUpDown.addTrafficLight(new Light(new RectangleF(pos.X + size.Width * 3 / 8 - size.Width / 30, pos.Y + size.Height * 5 / 8+ size.Width / 30, size.Width / 30, size.Height / 6),0));
            tLUpDown.addTrafficLight(new Light(new RectangleF(pos.X + size.Width * 3 / 8 , pos.Y + size.Height * 3 / 8 - size.Width / 30, size.Width / 30, size.Height / 6),180));
            tLUpDown.addTrafficLight(new Light(new RectangleF(pos.X + size.Width * 5 / 8 + size.Width / 30, pos.Y + size.Height * 3 / 8- size.Width / 30, size.Width / 30, size.Height / 6), 180));


            tLLeftRigth = new TrafficLight(new Light(new RectangleF(pos.X + size.Width * 5 / 8 + size.Width / 30, pos.Y + size.Height * 5 / 8+ size.Width / 30, size.Width / 30, size.Height / 6), -90),2);
            tLLeftRigth.addTrafficLight(new Light(new RectangleF(pos.X + size.Width * 5 / 8 + size.Width / 30, pos.Y + size.Height * 3 / 8, size.Width / 30, size.Height / 6), -90));
            tLLeftRigth.addTrafficLight(new Light(new RectangleF(pos.X + size.Width * 3 / 8 - size.Width / 30, pos.Y + size.Height * 3 / 8 - size.Width / 30, size.Width / 30, size.Height / 6), 90));
            tLLeftRigth.addTrafficLight(new Light(new RectangleF(pos.X + size.Width * 3 / 8 - size.Width / 30, pos.Y + size.Height * 5 / 8, size.Width / 30, size.Height / 6), 90));
            cars.Add(new Car(new Size(size.Width/ 8,  size.Height / 8),  new Point(pos.X+ size.Width*7/8, pos.Y+size.Height *4/8), new PointF(-1, 0),0));
            cars.Add(new Car(new Size(size.Width / 8, size.Height / 8), new Point(pos.X + size.Width*1/8, pos.Y + size.Height *4/8), new PointF(1, 0), 180));
            cars.Add(new Car(new Size(size.Width / 8, size.Height / 8), new Point(pos.X + size.Width * 4 / 8, pos.Y + size.Height * 7 / 8), new PointF(0, -1), 90));
            cars.Add(new Car(new Size(size.Width / 8, size.Height / 8), new Point(pos.X + size.Width * 4 / 8, pos.Y + size.Height * 1 / 8), new PointF(0, 1), -90));
        }
        public void calculate(ref Road[,] roads)
        {
            tLUpDown.calculate();
            tLLeftRigth.calculate();
            foreach (Car c in cars)
            {
                if (posToList.X != maxPosToList.X&& posToList.Y != maxPosToList.Y && posToList.X !=0&& posToList.Y!=0)
                {
                    if (c.Rotation == 90)
                    {

                        c.calculate(tLUpDown, cars.Concat(roads[posToList.X, posToList.Y + 1].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                    }
                    else if (c.Rotation == -90)
                    {

                        c.calculate(tLUpDown, cars.Concat(roads[posToList.X, posToList.Y - 1].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                    }
                    else if (c.Rotation == 180)
                    {

                        c.calculate(tLLeftRigth, cars.Concat(roads[posToList.X + 1, posToList.Y].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                    }
                    else if (c.Rotation == 0)
                    {

                        c.calculate(tLLeftRigth, cars.Concat(roads[posToList.X - 1, posToList.Y].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                    }
                }else
                {
                    if (c.Rotation == 90|| c.Rotation == -90)
                    {

                        c.calculate(tLUpDown, cars, new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                    }
                    else if (c.Rotation == 180|| c.Rotation == 0)
                    {

                        c.calculate(tLLeftRigth, cars,new Point(pos.X+size.Width/2,pos.Y+size.Height / 2));

                    }
                }
                if (c.Pos.X >= pos.X + size.Width) {
                    if (posToList.X<maxPosToList.X ) roads[posToList.X+1, posToList.Y].cars.Add(c);
                    carsToRemove.Add(c);
                }
                if (c.Pos.Y >= pos.Y+ size.Height) {
                    if (posToList.Y < maxPosToList.Y) roads[posToList.X, posToList.Y+1].cars.Add(c);
                    carsToRemove.Add(c);
                }
                if (c.Pos.X <= pos.X) {
                    if (0 < posToList.X) roads[posToList.X-1, posToList.Y].cars.Add(c);
                    carsToRemove.Add(c);
                }
                if (c.Pos.Y <= pos.Y) {
                    if (0 < posToList.Y) roads[posToList.X, posToList.Y - 1].cars.Add(c);
                    carsToRemove.Add(c);
                }
                    
            }
            foreach (Car c in carsToRemove)
            {
                cars.Remove(c);
            }
            carsToRemove.Clear();
            if (!cars.Any())
            {
                if (posToList.X == 0)
                {
                    cars.Add(new Car(new Size(size.Width / 8, size.Height / 8), new Point(pos.X + size.Width * 1 / 8, pos.Y + size.Height * 4 / 8), new PointF(1, 0), 180));
                }
                if (posToList.X == maxPosToList.X)
                {
                    cars.Add(new Car(new Size(size.Width / 8, size.Height / 8), new Point(pos.X + size.Width * 7 / 8, pos.Y + size.Height * 4 / 8), new PointF(-1, 0), 0));
                }
                if (posToList.Y == 0)
                {
                    cars.Add(new Car(new Size(size.Width / 8, size.Height / 8), new Point(pos.X + size.Width * 4 / 8, pos.Y + size.Height * 1 / 8), new PointF(0, 1), -90));
                }
                if (posToList.Y == maxPosToList.Y)
                {
                    cars.Add(new Car(new Size(size.Width / 8, size.Height / 8), new Point(pos.X + size.Width * 4 / 8, pos.Y + size.Height * 7 / 8), new PointF(0, -1), 90));
                }
            }
        }
        public Graphics render(Graphics g)
        {
            Point[] p = { new Point(pos.X + 3* size.Width / 8, pos.Y) ,new Point(pos.X + 5 * size.Width / 8, pos.Y) , new Point(pos.X + 5 * size.Width / 8, pos.Y + 3 * size.Height / 8), new Point(pos.X + 1 * size.Width, pos.Y + 3 * size.Height / 8), new Point(pos.X + 1 * size.Width, pos.Y + 5 * size.Height / 8), new Point(pos.X + 5 * size.Width / 8, pos.Y + 5 * size.Height / 8), new Point(pos.X + 5 * size.Width / 8, pos.Y + 1 * size.Height), new Point(pos.X + 3 * size.Width / 8, pos.Y + 1 * size.Height), new Point(pos.X + 3 * size.Width / 8, pos.Y + 5 * size.Height / 8), new Point(pos.X, pos.Y + 5 * size.Height / 8), new Point(pos.X, pos.Y +3 * size.Height / 8), new Point(pos.X + 3 * size.Width / 8, pos.Y + 3 * size.Height / 8) };
            g.FillPolygon(b, p);
            
            foreach(Car c in cars)
            {
                c.render(g);
            }
            tLLeftRigth.render(g);
            tLUpDown.render(g);
            return g;
        }
    }
    public class Light
    {
        public RectangleF rect;
        public int direction;
        public Light(RectangleF rect, int direction)
        {
            this.rect = rect;
            this.direction = direction;
        }
    }
    public class TrafficLight
    {
        SolidBrush colorb = new SolidBrush(Color.Black);
        SolidBrush colorr = new SolidBrush(Color.Red);
        SolidBrush colorg = new SolidBrush(Color.Green);
        SolidBrush coloro = new SolidBrush(Color.Orange);
        public int light = 0;//0=green,1=oragne,2=red
        int[] Times = {2000,4000,6000};
        public List<Light> lights= new List<Light>() ;
        Point pos =new Point(30,30);
        Size Size =new Size(5,40);
        Stopwatch stopWatch = new Stopwatch();
        public TrafficLight(Light rect, int light = 0)
        {
            this.light = light;
            this.lights.Add(rect);
        }
        public void addTrafficLight(Light light)
        {
            this.lights.Add(light);
        }
        public void calculate()
        {
            if (!stopWatch.IsRunning) stopWatch.Start();
            if (stopWatch.ElapsedMilliseconds > Times[light])
            {
                if (light < 2)
                {
                    Interlocked.Increment(ref light);
                }
                else light = 0;
                stopWatch.Restart();
            }
        }
        public Graphics render(Graphics g)
        {
            foreach (Light l in lights) {
                RectangleF rect = l.rect;
                g.TranslateTransform(rect.X, rect.Y);
                g.RotateTransform((float)l.direction);
                g.TranslateTransform(-(rect.X), -(rect.Y));
                g.FillRectangle(colorb, rect);
                if (light == 2)
                {
                    g.FillEllipse(colorr, new RectangleF(rect.X, rect.Y, rect.Width, rect.Height / 3));
                }
                else if (light == 1)
                {
                    g.FillEllipse(coloro, new RectangleF(rect.X, rect.Y + rect.Height / 3, rect.Width, rect.Height / 3));
                }
                else if (light == 0)
                {
                    g.FillEllipse(colorg, new RectangleF(rect.X, rect.Y + rect.Height * 2 / 3, rect.Width, rect.Height / 3));
                }
                g.ResetTransform();
            }
            return g;
        }
    }
    public class Car
    {
        class Choice {
            public PointF speed;
            public double rotation;
            public Choice(PointF speed,double rotation)
            {
                this.speed = speed;
                this.rotation = rotation;
            }
        }
        private Choice[] choices = { new Choice(new PointF(-1,0),0), new Choice(new PointF(1, 0), 180),new Choice(new PointF(0, -1), 90),new Choice(new PointF(0, 1), -90) };
        static Random rnd = new Random();
        SolidBrush b = new SolidBrush(Color.LightGray);
        private PointF pos;
        public PointF Pos { get { return pos; } }
        public PointF[] p;
        private Size size;
        private PointF speed;
        private PointF acceleration, deceleration;
        private double rotation, rotationSpeed;
        public double Rotation { get { return rotation; } }
        public Car(Size size, Point pos, PointF speed, double rotation)
        {
            this.size = size;
            this.pos = pos;
            this.speed = speed;
            this.acceleration = new PointF(speed.X / 80, speed.Y / 80);
            this.rotation = rotation;
            this.deceleration = new PointF(speed.X / 2, speed.Y / 2);
            PointF[] tmp = { new PointF(pos.X - 5 * size.Width / 9, pos.Y - 10 * size.Height / 35), new PointF(pos.X - 5 * size.Width / 9, pos.Y - 23 * size.Height / 35), new PointF(pos.X - 3 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X - 2 * size.Width / 9, pos.Y - 35 * size.Height / 35), new PointF(pos.X + 2 * size.Width / 9, pos.Y - 35 * size.Height / 35), new PointF(pos.X + 3 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X + 4 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X + 4 * size.Width / 9, pos.Y - 10 * size.Height / 35) };
            this.p = tmp;
        }
        public Car(Size size, Point pos, PointF speed, PointF acceleration, int rotation, int rotationSpeed)
        {

            this.size = size;
            this.pos = pos;
            this.speed = speed;
            this.acceleration = acceleration;
            this.rotation = rotation;
            this.rotationSpeed = rotationSpeed;
            PointF[] tmp = { new PointF(pos.X - 5 * size.Width / 9, pos.Y - 10 * size.Height / 35), new PointF(pos.X - 5 * size.Width / 9, pos.Y - 23 * size.Height / 35), new PointF(pos.X - 3 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X - 2 * size.Width / 9, pos.Y - 35 * size.Height / 35), new PointF(pos.X + 2 * size.Width / 9, pos.Y - 35 * size.Height / 35), new PointF(pos.X + 3 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X + 4 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X + 4 * size.Width / 9, pos.Y - 10 * size.Height / 35) };
            this.p = tmp;
        }
        public void calculate(TrafficLight tL, List<Car> cars,Point roadCenter)
        {
            bool infrondOfLight = false;
            Light t;
            if (rotation == 90) t = tL.lights.Find(x => x.direction == 0);
            else if (rotation == -90) t = tL.lights.Find(x => x.direction == 180);
            else if (rotation == 0) t = tL.lights.Find(x => x.direction == -90);
            else t = tL.lights.Find(x => x.direction == 90);
            if (t != null)
            {
                if (t.direction == 0 && t.rect.Y <= pos.Y)
                {
                    infrondOfLight = true;
                }
                else if (t.direction == 180 && t.rect.Y >= pos.Y)
                {
                    infrondOfLight = true;
                }
                else if (t.direction == 90 && t.rect.X >= pos.X)
                {
                    infrondOfLight = true;
                }
                else if (t.direction == -90 && t.rect.X <= pos.X)
                {
                    infrondOfLight = true;
                }
            }
            else
            {
            }/*
            if (Math.Pow(roadCenter.X - this.pos.X, 2) + Math.Pow(roadCenter.Y - this.pos.Y, 2) < Math.Pow(size.Width, 2))
            {
                int i  = rnd.Next(0,3);
                
                rotation = choices[i].rotation;
                speed.X = (Math.Abs(speed.X)+ Math.Abs(speed.Y)) * choices[i].speed.X;
                speed.Y = (Math.Abs(speed.Y)+ Math.Abs(speed.X)) * choices[i].speed.Y;
                deceleration.X = (Math.Abs(deceleration.X)+ Math.Abs(deceleration.Y)) * choices[i].speed.X;
                deceleration.Y = (Math.Abs(deceleration.Y)+ Math.Abs(deceleration.X)) * choices[i].speed.Y;
                acceleration.X = (Math.Abs(acceleration.X)+ Math.Abs(acceleration.Y)) * choices[i].speed.X;
                acceleration.Y = (Math.Abs(acceleration.Y)+ Math.Abs(acceleration.X)) * choices[i].speed.Y;
                pos.X = roadCenter.X+ size.Width * choices[i].speed.X;
                pos.Y = roadCenter.Y+ size.Width * choices[i].speed.Y;
            }*/
            bool carClose = false;
            foreach (Car c in cars)
            {
                if (c != this && this.rotation == c.rotation && Math.Pow(c.Pos.X - this.pos.X, 2) + Math.Pow(c.Pos.Y - this.pos.Y, 2) <= Math.Pow(this.size.Width, 2)) carClose = true;
            }
            if (carClose)//cars.Exists(x => Math.Pow(x.Pos.X - this.pos.X, 2) + Math.Pow(x.Pos.Y - this.pos.Y, 2) <= Math.Pow(this.size.Width, 2))
            {
                if (Math.Abs(speed.X) >= Math.Abs(deceleration.X))
                    speed.X -= deceleration.X;
                else speed.X = 0;
                if (Math.Abs(speed.Y) >= Math.Abs(deceleration.Y))
                    speed.Y -= deceleration.Y;
                else speed.Y = 0;
            }
            else
            {

                if (infrondOfLight)
                {
                    if (tL.light == 0)
                    {
                        if (Math.Abs(speed.X) <= size.Width / 8)
                            speed.X += acceleration.X;
                        if (Math.Abs(speed.Y) <= size.Width / 8)
                            speed.Y += acceleration.Y;
                    }
                    else if (tL.light < 1)
                    {
                        if (Math.Abs(speed.X) <= size.Width / 4 && Math.Abs(speed.Y) <= size.Width / 4)
                        {
                            speed.X += acceleration.X;
                            speed.Y += acceleration.Y;
                        }
                    }
                    else if (tL.light == 2)
                    {
                        if (Math.Abs(speed.X) >= Math.Abs(deceleration.X))
                            speed.X -= deceleration.X;
                        else speed.X = 0;
                        if (Math.Abs(speed.Y) >= Math.Abs(deceleration.Y))
                            speed.Y -= deceleration.Y;
                        else speed.Y = 0;
                    }

                }
                else
                {
                    if (Math.Abs(speed.X) <= size.Width / 2 && Math.Abs(speed.Y) <= size.Width / 2)
                    {
                        speed.X += acceleration.X;
                        speed.Y += acceleration.Y;
                    }
                }
            }
            pos.X += speed.X;
            pos.Y += speed.Y;

        }
        public Graphics render(Graphics g)
        {
            PointF[] tmp = { new PointF(pos.X - 5 * size.Width / 9, pos.Y - 10 * size.Height / 35), new PointF(pos.X - 5 * size.Width / 9, pos.Y - 23 * size.Height / 35), new PointF(pos.X - 3 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X - 2 * size.Width / 9, pos.Y - 35 * size.Height / 35), new PointF(pos.X + 2 * size.Width / 9, pos.Y - 35 * size.Height / 35), new PointF(pos.X + 3 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X + 4 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X + 4 * size.Width / 9, pos.Y - 10 * size.Height / 35) };
            this.p = tmp;
            g.TranslateTransform(pos.X, pos.Y);
            g.RotateTransform((float)rotation);
            g.TranslateTransform(-pos.X, -pos.Y);
            g.FillEllipse(new SolidBrush(Color.Black), pos.X - 4 * size.Width / 9, pos.Y - 20 * size.Height / 35, 2 * size.Width / 9, 15 * size.Height / 35);
            g.FillEllipse(new SolidBrush(Color.Black), pos.X + size.Width / 9, pos.Y - 20 * size.Height / 35, 2 * size.Width / 9, 15 * size.Height / 35);
            g.FillPolygon(b, p);
            g.ResetTransform();
            return g;
        }
    }
}