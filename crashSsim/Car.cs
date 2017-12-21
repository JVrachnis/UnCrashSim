using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace UnCrashSim
{
    public class Car
    {
        class Choice
        {
            public PointF speed;
            public double direction;
            public Choice(PointF speed, double direction)
            {
                this.speed = speed;
                this.direction = direction;
            }
        }
        //float sizeOfset=1;
        bool canTurn = true;
        //private Choice[] choices = { new Choice(new PointF(-1, 0), 0), new Choice(new PointF(1, 0), 180), new Choice(new PointF(0, -1), 90), new Choice(new PointF(0, 1), -90) };
        static Random rnd = new Random();
        SolidBrush b = new SolidBrush(Color.LightGray);
        private PointF pos;
        public PointF Pos { get { return pos; } }
        public PointF[] p;
        public Size size;
        private PointF speed;
        private PointF acceleration, deceleration;
        private double direction, directionSpeed;
        public double Rotation { get { return direction; } }
        public Car(Size size, Point pos, PointF speed, double direction)
        {
            this.size = size;
            this.pos = pos;
            this.speed = speed;
            this.acceleration = new PointF(speed.X / 20, speed.Y / 20);
            this.direction = direction;
            this.deceleration = new PointF(speed.X / 2, speed.Y / 2);
            PointF[] tmp = { new PointF(pos.X - 5 * size.Width / 9, pos.Y - 10 * size.Height / 35), new PointF(pos.X - 5 * size.Width / 9, pos.Y - 23 * size.Height / 35), new PointF(pos.X - 3 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X - 2 * size.Width / 9, pos.Y - 35 * size.Height / 35), new PointF(pos.X + 2 * size.Width / 9, pos.Y - 35 * size.Height / 35), new PointF(pos.X + 3 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X + 4 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X + 4 * size.Width / 9, pos.Y - 10 * size.Height / 35) };
            this.p = tmp;
        }
        public Car(Size size, Point pos, PointF speed, PointF acceleration, int direction, int directionSpeed)
        {

            this.size = size;
            this.pos = pos;
            this.speed = speed;
            this.acceleration = acceleration;
            this.direction = direction;
            this.directionSpeed = directionSpeed;
            PointF[] tmp = { new PointF(pos.X - 5 * size.Width / 9, pos.Y - 10 * size.Height / 35), new PointF(pos.X - 5 * size.Width / 9, pos.Y - 23 * size.Height / 35), new PointF(pos.X - 3 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X - 2 * size.Width / 9, pos.Y - 35 * size.Height / 35), new PointF(pos.X + 2 * size.Width / 9, pos.Y - 35 * size.Height / 35), new PointF(pos.X + 3 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X + 4 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X + 4 * size.Width / 9, pos.Y - 10 * size.Height / 35) };
            this.p = tmp;
        }
        private bool CarCollision(ref List<Car> cars)
        {
            //List <Car> c = cars.FindAll(x => x != this && this.direction == x.direction);
            //if (c != null)
            //    return (c.direction == 0 && c.pos.X < pos.X + size.Width) || (c.direction == 180 && c.pos.X > pos.X - size.Width) || (c.direction == 90 && c.pos.Y < pos.Y + size.Width) || (c.direction == -90 && c.pos.Y > pos.Y - size.Width);
            foreach (Car c in cars.FindAll(c => c != this && this.direction == c.direction))//(c => c != this && Math.Pow(this.pos.X-this.pos.X,2)+ Math.Pow(this.pos.Y - this.pos.Y, 2)< Math.Pow(this.size.Width, 2))
            {
                if ((pos.X - c.pos.X >= 0 && direction == 0 && pos.X - c.pos.X <= size.Width * 1.1) || (c.pos.X - pos.X >= 0 && direction == 180 && c.pos.X - pos.X < size.Width * 1.1) || (pos.Y - c.pos.Y > 0 && direction == 90 && pos.Y - c.pos.Y < size.Width * 1.1) || (c.pos.Y - pos.Y > 0 && direction == -90 && c.pos.Y - pos.Y < +size.Width * 1.1))
                {
                    //pos.X -= speed.X * size.Width / 10;
                    //pos.Y -= speed.Y * size.Width / 10;
                    if ((pos.X - c.pos.X >= 0 && direction == 0 && pos.X - c.pos.X <= size.Width) || (c.pos.X - pos.X >= 0 && direction == 180 && c.pos.X - pos.X < size.Width) || (pos.Y - c.pos.Y > 0 && direction == 90 && pos.Y - c.pos.Y < size.Width) || (c.pos.Y - pos.Y > 0 && direction == -90 && c.pos.Y - pos.Y < +size.Width)) {
                        speed.X = 0;
                        speed.Y = 0;

                    }
                    else
                    {
                       speed.X = c.speed.X;
                       speed.Y = c.speed.Y;
                    }
                    return true;
                }
            }
            return false;
        }
        private bool inFrontOfLight(ref TrafficLight tL)
        {
            Light l = tL.lights.Find(x => x.direction == direction);
            if (l != null)
                return (l.direction == 0 && l.rect.X <= pos.X - size.Width) || (l.direction == 180 && l.rect.X >= pos.X + size.Width) || (l.direction == 90 && l.rect.Y <= pos.Y - size.Width) || (l.direction == -90 && l.rect.Y >= pos.Y + size.Width);
            else return false;
        }
        public void calculate(List<Car> cars, Point roadCenter)
        {
            if (!CarCollision(ref cars))
            {
                if (Math.Abs(speed.X) <= size.Width / 8 && Math.Abs(speed.Y) <= size.Width / 8)
                {
                    speed.X += acceleration.X;
                    speed.Y += acceleration.Y;
                }
                else
                {
                    speed.X -= deceleration.X;
                    speed.Y -= deceleration.Y;
                }
            }
            pos.X += speed.X * size.Width / 10;
            pos.Y += speed.Y * size.Width / 10;
        }
        public void calculate(TrafficLight tL, List<Car> cars, Point roadCenter)
        {

            if (!CarCollision(ref cars))
            {

                if (inFrontOfLight(ref tL))
                {
                    canTurn = true;
                    if (tL.light == 0)
                    {
                        if (Math.Abs(speed.X) <= size.Width / 8)
                            speed.X += acceleration.X;
                        if (Math.Abs(speed.Y) <= size.Width / 8)
                            speed.Y += acceleration.Y;
                    }
                    else if (tL.light == 1)
                    {
                        if (Math.Abs(speed.X) <= size.Width / 4 && Math.Abs(speed.Y) <= size.Width / 4)
                        {
                            if (Math.Abs(speed.X) <= size.Width / 4)
                                speed.X += acceleration.X;
                            if (Math.Abs(speed.Y) <= size.Width / 4)
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
                    if (Math.Abs(speed.X) <= size.Width / 8 && Math.Abs(speed.Y) <= size.Width / 8)
                    {
                        speed.X += acceleration.X;
                        speed.Y += acceleration.Y;
                    } else
                    {
                        speed.X -= deceleration.X;
                        speed.Y -= deceleration.Y;
                    }
                }
            }
            pos.X += speed.X * size.Width / 10;
            pos.Y += speed.Y * size.Width / 10;

        }
        public Graphics render(Graphics g)
        {
            PointF[] tmp = { new PointF(pos.X - 5 * size.Width / 9, pos.Y - 10 * size.Height / 35), new PointF(pos.X - 5 * size.Width / 9, pos.Y - 23 * size.Height / 35), new PointF(pos.X - 3 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X - 2 * size.Width / 9, pos.Y - 35 * size.Height / 35), new PointF(pos.X + 2 * size.Width / 9, pos.Y - 35 * size.Height / 35), new PointF(pos.X + 3 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X + 4 * size.Width / 9, pos.Y - 25 * size.Height / 35), new PointF(pos.X + 4 * size.Width / 9, pos.Y - 10 * size.Height / 35) };
            this.p = tmp;
            g.TranslateTransform(pos.X, pos.Y);
            g.RotateTransform((float)direction);
            if (direction == -90 || direction == 180)
            {
                g.ScaleTransform(1, -1);
            }
            
            g.TranslateTransform(-pos.X, -pos.Y);
            g.FillEllipse(new SolidBrush(Color.Black), pos.X - 4 * size.Width / 9, pos.Y - 20 * size.Height / 35, 2 * size.Width / 9, 15 * size.Height / 35);
            g.FillEllipse(new SolidBrush(Color.Black), pos.X + size.Width / 9, pos.Y - 20 * size.Height / 35, 2 * size.Width / 9, 15 * size.Height / 35);
            g.FillPolygon(b, p);
            g.ResetTransform();
            return g;
        }
    }
}
