using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
namespace UnCrashSim
{
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
        int[] Times = { 4000, 2000, 6000 };
        public int[] SetTimes { set {Times= value; } }
        public List<Light> lights = new List<Light>();
        Point pos = new Point(30, 30);
        Size Size = new Size(5, 40);
        Stopwatch stopWatch = new Stopwatch();
        int ofSet;
        public TrafficLight(Light rect, int[] Times, int light = 0,int ofSet=0)
        {
            this.light = light;
            this.lights.Add(rect);
            this.Times = Times;
            this.ofSet = ofSet;
        }
        public void addTrafficLight(Light light)
        {
            this.lights.Add(light);
        }
        public void calculate()
        {
            if (!stopWatch.IsRunning) stopWatch.Start();
            if (stopWatch.ElapsedMilliseconds+ ofSet > Times[light])
            {
                if (light < 2)
                {
                    Interlocked.Increment(ref light);
                }
                else light = 0;
                ofSet = 0;
                stopWatch.Restart();
            }
        }
        public Graphics render(Graphics g)
        {
            foreach (Light l in lights)
            {
                RectangleF rect = l.rect;
                g.TranslateTransform(rect.X, rect.Y);
                g.RotateTransform((float)l.direction);
                g.TranslateTransform(-(rect.X), -(rect.Y));
                g.FillRectangle(colorb, rect);
                if (light == 2)
                {
                    g.FillEllipse(colorr, new RectangleF(rect.X, rect.Y, rect.Width/3, rect.Height));
                }
                else if (light == 1)
                {
                    g.FillEllipse(coloro, new RectangleF(rect.X + rect.Width / 3, rect.Y , rect.Width / 3, rect.Height ));
                }
                else if (light == 0)
                {
                    g.FillEllipse(colorg, new RectangleF(rect.X + rect.Width * 2 / 3, rect.Y , rect.Width / 3, rect.Height));
                }
                g.ResetTransform();
            }
            return g;
        }
    }
}
