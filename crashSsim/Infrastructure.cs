using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace UnCrashSim
{
    public abstract class Infrastructure
    {
        //abstract public int Area();
        class EntryPoint
        {
            Point pos;
            int direction;
            public EntryPoint()
            {
            }
            public EntryPoint(Point pos, int direction)
            {
                this.pos = pos;
                this.direction = direction;
            }
        }
        //float sizeOfset = 1;
        //TrafficLight tLUpDown, tLLeftRigth;
        protected SolidBrush b = new SolidBrush(Color.DarkGray);
        public List<Car> cars = new List<Car>();
        protected List<Car> carsToRemove = new List<Car>();
        protected Size size = new Size(300, 300);
        public Size Size { get { return size; } }
        protected Point pos = new Point(0, 0);
        public bool autoMode = true;
        public Point Pos { get { return pos; } }
        protected Dictionary<int, Point> entryPoints = new Dictionary<int, Point>();
        public Dictionary<int, Point> EntryPoints{get{return entryPoints;} }
        protected Dictionary<int, PointF> movement = new Dictionary<int, PointF>();
        protected Dictionary<int, PointF> Movement { get { return movement; } }
        protected Point posToList = new Point(0, 0);
        protected Point maxPosToList = new Point(0, 0);
        public abstract void calculate(ref Infrastructure[,] roads);
        public abstract Graphics render(Graphics g);

        protected void carOutOfBorders(ref Infrastructure[,] roads, Car c)
        {
            if (c.Pos.X >= pos.X + size.Width)
            {
                if (posToList.X < maxPosToList.X )if( roads[posToList.X + 1, posToList.Y].EntryPoints.Keys.Contains((int)c.Rotation)) roads[posToList.X + 1, posToList.Y].cars.Add(c);
                carsToRemove.Add(c);
            }
            if (c.Pos.Y >= pos.Y + size.Height )
            {
                if (posToList.Y < maxPosToList.Y)if(roads[posToList.X, posToList.Y + 1].EntryPoints.Keys.Contains((int)c.Rotation)) roads[posToList.X, posToList.Y + 1].cars.Add(c);
                carsToRemove.Add(c);
            }
            if (c.Pos.X <= pos.X)
            {
                if (0 < posToList.X) if (roads[posToList.X - 1, posToList.Y].EntryPoints.Keys.Contains((int)c.Rotation)) roads[posToList.X - 1, posToList.Y].cars.Add(c);
                carsToRemove.Add(c);
            }
            if (c.Pos.Y <= pos.Y)
            {
                if (0 < posToList.Y) if (roads[posToList.X, posToList.Y - 1].EntryPoints.Keys.Contains((int)c.Rotation)) roads[posToList.X, posToList.Y - 1].cars.Add(c);
                carsToRemove.Add(c);
            }
        }
    }
    public class EmptyInfrastructure : Infrastructure
    {
        public EmptyInfrastructure(Size size, Point pos, Point posToList, Point maxPosToList)
        {
            this.maxPosToList = maxPosToList;
            this.posToList = posToList;
            this.size = size;
            this.pos = pos;
        }
        public override void calculate(ref Infrastructure[,] roads)
        {
        }
        public override Graphics render(Graphics g)
        {
            return g;
        }
    }
    public class UpDownRoad : Infrastructure
    {
        private void initialize()
        {
            //movement.Add(0, new PointF(-1, 0));
            //movement.Add(180, new PointF(1, 0));
            movement.Add(90, new PointF(0, -1));
            movement.Add(-90, new PointF(0, 1));
            //entryPoints.Add(0, new Point(pos.X + size.Width * 7 / 8, pos.Y + size.Height * 4 / 8));
            //entryPoints.Add(180, new Point(pos.X + size.Width * 1 / 8, pos.Y + size.Height * 5 / 8));
            entryPoints.Add(90, new Point(pos.X + size.Width * 4 / 8, pos.Y + size.Height * 7 / 8));
            entryPoints.Add(-90, new Point(pos.X + size.Width * 3 / 8, pos.Y + size.Height * 1 / 8));
            initializeCars(ref cars);
        }
        public UpDownRoad(Size size, Point pos, Point posToList, Point maxPosToList)
        {
            this.maxPosToList = maxPosToList;
            this.posToList = posToList;
            this.size = size;
            this.pos = pos;
            initialize();
        }
        
        private void carCalculations(ref Infrastructure[,] roads)
        {
            foreach (Car c in cars)
            {

                if (posToList.Y != 0 && c.Rotation == 90)
                    c.calculate(cars.Concat(roads[posToList.X, posToList.Y - 1].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                else if (posToList.Y != maxPosToList.Y && c.Rotation == -90)
                    c.calculate(cars.Concat(roads[posToList.X, posToList.Y + 1].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                else if (posToList.X != maxPosToList.X && c.Rotation == 180)
                    c.calculate(cars.Concat(roads[posToList.X + 1, posToList.Y].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                else if (posToList.X != 0 && c.Rotation == 0)
                    c.calculate(cars.Concat(roads[posToList.X - 1, posToList.Y].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                else if (c.Rotation == 90 || c.Rotation == -90)
                    c.calculate(cars, new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                else if (c.Rotation == 180 || c.Rotation == 0)
                    c.calculate(cars, new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                carOutOfBorders(ref roads, c);

            }
            foreach (Car c in carsToRemove)
            {

                cars.Remove(c);
            }
            carsToRemove.Clear();
            SpawnNewCars(ref cars);
        }
        public override void calculate(ref Infrastructure[,] roads)
        {
            carCalculations(ref roads);
        }
        private void SpawnNewCars(ref List<Car> cars)
        {/*
            if (posToList.X == 0)
            {
                SpawnCar(ref cars, 180);
            }
            if (posToList.X == maxPosToList.X)
            {
                SpawnCar(ref cars, 0);
            }*/
            if (posToList.Y == 0)
            {
                SpawnCar(ref cars, -90);
            }
            if (posToList.Y == maxPosToList.Y)
            {
                SpawnCar(ref cars, 90);
            }
        }
        private void initializeCars(ref List<Car> cars)
        {
            foreach (int i in entryPoints.Keys)
            {
                SpawnCar(ref cars, i);
            }
        }
        private void SpawnCar(ref List<Car> cars, int direction)
        {
            if (PossitionClear(ref cars, direction))
            {
                cars.Add(new Car(new Size(size.Width / 8, size.Height / 8), entryPoints[direction], movement[direction], direction));
            }
        }
        private bool PossitionClear(ref List<Car> cars, int direction)
        {/*
            if (direction == 0)
            {
                return !cars.Exists(c => (c.Pos.X >= entryPoints[direction].X - c.size.Width) && (c.Rotation == direction));
            }
            else if (direction == 180)
            {
                return !cars.Exists(c => (c.Pos.X <= entryPoints[direction].X + c.size.Width) && (c.Rotation == direction));
            }*/
            if (direction == 90)
            {
                return !cars.Exists(c => (c.Pos.Y >= entryPoints[direction].Y - c.size.Width) && (c.Rotation == direction));
            }
            else if (direction == -90)
            {
                return !cars.Exists(c => (c.Pos.Y <= entryPoints[direction].Y + c.size.Width) && (c.Rotation == direction));
            }
            return true;
        }
        public override Graphics render(Graphics g)
        {
            //Point[] p = { new Point(pos.X + 3 * size.Width / 8, pos.Y), new Point(pos.X + 5 * size.Width / 8, pos.Y), new Point(pos.X + 5 * size.Width / 8, pos.Y + 3 * size.Height / 8), new Point(pos.X + 1 * size.Width, pos.Y + 3 * size.Height / 8), new Point(pos.X + 1 * size.Width, pos.Y + 5 * size.Height / 8), new Point(pos.X + 5 * size.Width / 8, pos.Y + 5 * size.Height / 8), new Point(pos.X + 5 * size.Width / 8, pos.Y + 1 * size.Height), new Point(pos.X + 3 * size.Width / 8, pos.Y + 1 * size.Height), new Point(pos.X + 3 * size.Width / 8, pos.Y + 5 * size.Height / 8), new Point(pos.X, pos.Y + 5 * size.Height / 8), new Point(pos.X, pos.Y + 3 * size.Height / 8), new Point(pos.X + 3 * size.Width / 8, pos.Y + 3 * size.Height / 8) };
            Point[] p = {new Point(pos.X + 3 * size.Width / 8, pos.Y), new Point(pos.X + 5 * size.Width / 8, pos.Y), new Point(pos.X + 5 * size.Width / 8, pos.Y + 1 * size.Height), new Point(pos.X + 3 * size.Width / 8, pos.Y + 1 * size.Height)};
            g.FillPolygon(b, p);
            foreach (Car c in cars)
            {
                c.render(g);
            }
            return g;
        }
    }
    public class LeftRightRoad : Infrastructure
    {
        private void initialize()
        {
            movement.Add(0, new PointF(-1, 0));
            movement.Add(180, new PointF(1, 0));
            //movement.Add(90, new PointF(0, -1));
            //movement.Add(-90, new PointF(0, 1));
            entryPoints.Add(0, new Point(pos.X + size.Width * 7 / 8, pos.Y + size.Height * 4 / 8));
            entryPoints.Add(180, new Point(pos.X + size.Width * 1 / 8, pos.Y + size.Height * 5 / 8));
            //entryPoints.Add(90, new Point(pos.X + size.Width * 4 / 8, pos.Y + size.Height * 7 / 8));
            //entryPoints.Add(-90, new Point(pos.X + size.Width * 3 / 8, pos.Y + size.Height * 1 / 8));
            initializeCars(ref cars);
        }
        public LeftRightRoad(Size size, Point pos, Point posToList, Point maxPosToList)
        {
            this.maxPosToList = maxPosToList;
            this.posToList = posToList;
            this.size = size;
            this.pos = pos;
            initialize();
        }

        private void carCalculations(ref Infrastructure[,] roads)
        {
            foreach (Car c in cars)
            {

                if (posToList.Y != 0 && c.Rotation == 90)
                    c.calculate(cars.Concat(roads[posToList.X, posToList.Y - 1].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                else if (posToList.Y != maxPosToList.Y && c.Rotation == -90)
                    c.calculate(cars.Concat(roads[posToList.X, posToList.Y + 1].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                else if (posToList.X != maxPosToList.X && c.Rotation == 180)
                    c.calculate(cars.Concat(roads[posToList.X + 1, posToList.Y].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                else if (posToList.X != 0 && c.Rotation == 0)
                    c.calculate(cars.Concat(roads[posToList.X - 1, posToList.Y].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                else if (c.Rotation == 90 || c.Rotation == -90)
                    c.calculate(cars, new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                else if (c.Rotation == 180 || c.Rotation == 0)
                    c.calculate(cars, new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                carOutOfBorders(ref roads, c);

            }
            foreach (Car c in carsToRemove)
            {

                cars.Remove(c);
            }
            carsToRemove.Clear();
            SpawnNewCars(ref cars);
        }
        public override void calculate(ref Infrastructure[,] roads)
        {
            carCalculations(ref roads);
        }
        private void SpawnNewCars(ref List<Car> cars)
        {
            if (posToList.X == 0)
            {
                SpawnCar(ref cars, 180);
            }
            if (posToList.X == maxPosToList.X)
            {
                SpawnCar(ref cars, 0);
            }/*
            if (posToList.Y == 0)
            {
                SpawnCar(ref cars, -90);
            }
            if (posToList.Y == maxPosToList.Y)
            {
                SpawnCar(ref cars, 90);
            }*/
        }
        private void initializeCars(ref List<Car> cars)
        {
            foreach (int i in entryPoints.Keys)
            {
                SpawnCar(ref cars, i);
            }
        }
        private void SpawnCar(ref List<Car> cars, int direction)
        {
            if (PossitionClear(ref cars, direction))
            {
                cars.Add(new Car(new Size(size.Width / 8, size.Height / 8), entryPoints[direction], movement[direction], direction));
            }
        }
        private bool PossitionClear(ref List<Car> cars, int direction)
        {
            if (direction == 0)
            {
                return !cars.Exists(c => (c.Pos.X >= entryPoints[direction].X - c.size.Width) && (c.Rotation == direction));
            }
            else if (direction == 180)
            {
                return !cars.Exists(c => (c.Pos.X <= entryPoints[direction].X + c.size.Width) && (c.Rotation == direction));
            }
            /*
            if (direction == 90)
            {
                return !cars.Exists(c => (c.Pos.Y >= entryPoints[direction].Y - c.size.Width) && (c.Rotation == direction));
            }
            else if (direction == -90)
            {
                return !cars.Exists(c => (c.Pos.Y <= entryPoints[direction].Y + c.size.Width) && (c.Rotation == direction));
            }*/
            return true;
        }
        public override Graphics render(Graphics g)
        {
            //Point[] p = { new Point(pos.X + 3 * size.Width / 8, pos.Y), new Point(pos.X + 5 * size.Width / 8, pos.Y), new Point(pos.X + 5 * size.Width / 8, pos.Y + 3 * size.Height / 8), new Point(pos.X + 1 * size.Width, pos.Y + 3 * size.Height / 8), new Point(pos.X + 1 * size.Width, pos.Y + 5 * size.Height / 8), new Point(pos.X + 5 * size.Width / 8, pos.Y + 5 * size.Height / 8), new Point(pos.X + 5 * size.Width / 8, pos.Y + 1 * size.Height), new Point(pos.X + 3 * size.Width / 8, pos.Y + 1 * size.Height), new Point(pos.X + 3 * size.Width / 8, pos.Y + 5 * size.Height / 8), new Point(pos.X, pos.Y + 5 * size.Height / 8), new Point(pos.X, pos.Y + 3 * size.Height / 8), new Point(pos.X + 3 * size.Width / 8, pos.Y + 3 * size.Height / 8) };
            Point[] p = { new Point(pos.X , pos.Y + 3 * size.Height / 8), new Point(pos.X , pos.Y + 5 * size.Height / 8), new Point(pos.X + size.Width, pos.Y  + 5 * size.Height / 8), new Point(pos.X+size.Width , pos.Y + 3 * size.Height / 8) };
            g.FillPolygon(b, p);
            foreach (Car c in cars)
            {
                c.render(g);
            }
            return g;
        }
    }

    public class CrossRoad: Infrastructure
    {
        TrafficLight tLUpDown, tLLeftRigth;
        private void addTrafficLights(int[] upDownTimes, int upDownLight, int[] leftRightTimes, int leftRightLight)
        {
            int upDownOffSet = 0, leftRightOffSet=0;
            if (leftRightLight ==1 && upDownLight==2)
            {
                upDownOffSet = leftRightTimes[0];
            }
            if (upDownLight == 1 && leftRightLight == 2)
            {
                leftRightOffSet = upDownTimes[0];
            }
            tLUpDown = new TrafficLight(new Light(new RectangleF(pos.X + size.Width * 5 / 8 + size.Width / 30, pos.Y + size.Height * 5 / 8 + size.Width / 30, size.Width / 6, size.Height / 30), 90), upDownTimes.ToArray<int>(), upDownLight,upDownOffSet);
            tLUpDown.addTrafficLight(new Light(new RectangleF(pos.X + size.Width * 3 / 8, pos.Y + size.Height * 5 / 8 + size.Width / 30, size.Width / 6, size.Height / 30), 90));
            tLUpDown.addTrafficLight(new Light(new RectangleF(pos.X + size.Width * 3 / 8 - size.Width / 30, pos.Y + size.Height * 3 / 8 - size.Width / 30, size.Width / 6, size.Height / 30), -90));
            tLUpDown.addTrafficLight(new Light(new RectangleF(pos.X + size.Width * 5 / 8, pos.Y + size.Height * 3 / 8 - size.Width / 30, size.Width / 6, size.Height / 30), -90));


            tLLeftRigth = new TrafficLight(new Light(new RectangleF(pos.X + size.Width * 5 / 8 + size.Width / 30, pos.Y + size.Height * 5 / 8, size.Width / 6, size.Height / 30), 0), leftRightTimes.ToArray<int>(), leftRightLight, leftRightOffSet);
            tLLeftRigth.addTrafficLight(new Light(new RectangleF(pos.X + size.Width * 5 / 8 + size.Width / 30, pos.Y + size.Height * 3 / 8 - size.Width / 30, size.Width / 6, size.Height / 30), 0));
            tLLeftRigth.addTrafficLight(new Light(new RectangleF(pos.X + size.Width * 3 / 8 - size.Width / 30, pos.Y + size.Height * 3 / 8, size.Width / 6, size.Height / 30), 180));
            tLLeftRigth.addTrafficLight(new Light(new RectangleF(pos.X + size.Width * 3 / 8 - size.Width / 30, pos.Y + size.Height * 5 / 8 + size.Width / 30, size.Width / 6, size.Height / 30), 180));
            
        }
        private void initialize(int[] upDownTimes, int upDownLight, int[] leftRightTimes, int leftRightLight) {
            movement.Add(0, new PointF(-1, 0));
            movement.Add(180, new PointF(1, 0));
            movement.Add(90, new PointF(0, -1));
            movement.Add(-90, new PointF(0, 1));
            entryPoints.Add(0, new Point(pos.X + size.Width * 7 / 8, pos.Y + size.Height * 4 / 8));
            entryPoints.Add(180, new Point(pos.X + size.Width * 1 / 8, pos.Y + size.Height * 5 / 8));
            entryPoints.Add(90, new Point(pos.X + size.Width * 4 / 8, pos.Y + size.Height * 7 / 8));
            entryPoints.Add(-90, new Point(pos.X + size.Width * 3 / 8, pos.Y + size.Height * 1 / 8));
            
            addTrafficLights(upDownTimes, upDownLight, leftRightTimes, leftRightLight);
            SpawnCar(ref cars, 180);

            SpawnCar(ref cars, 0);

            SpawnCar(ref cars, -90);

            SpawnCar(ref cars, 90);
        }
        public CrossRoad(Size size, Point pos, Point posToList, Point maxPosToList, int[] upDownTimes, int upDownLight, int[] leftRightTimes, int leftRightLight,bool autoMode)
        {
            this.maxPosToList = maxPosToList;
            this.posToList = posToList;
            this.size = size;
            this.pos = pos;
            this.autoMode = autoMode;
            initialize(upDownTimes, upDownLight, leftRightTimes, leftRightLight);

        }
        public void UpdateTraficLight(int[] upDownTimes, int upDownLight, int[] leftRightTimes, int leftRightLight)
        {
            int upDownOffSet = 0, leftRightOffSet = 0;
            if (leftRightLight == 1 && upDownLight == 2)
            {
                upDownOffSet = leftRightTimes[0];
            }
            if (upDownLight == 1 && leftRightLight == 2)
            {
                leftRightOffSet = upDownTimes[0];
            }
            tLUpDown.update(upDownTimes.ToArray<int>(), upDownLight, upDownOffSet);
            tLLeftRigth.update(leftRightTimes.ToArray<int>(), leftRightLight, leftRightOffSet);
        }
        private void carCalculations(ref Infrastructure[,] roads)
        {
            foreach (Car c in cars)
            {

                if (posToList.Y != 0 && c.Rotation == 90)
                    c.calculate(tLUpDown, cars.Concat(roads[posToList.X, posToList.Y - 1].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                else if (posToList.Y != maxPosToList.Y && c.Rotation == -90)
                    c.calculate(tLUpDown, cars.Concat(roads[posToList.X, posToList.Y + 1].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                else if (posToList.X != maxPosToList.X && c.Rotation == 180)
                    c.calculate(tLLeftRigth, cars.Concat(roads[posToList.X + 1, posToList.Y].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));
               
                else if (posToList.X != 0 && c.Rotation == 0)
                    c.calculate(tLLeftRigth, cars.Concat(roads[posToList.X - 1, posToList.Y].cars).ToList(), new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                else if (c.Rotation == 90 || c.Rotation == -90)
                    c.calculate(tLUpDown, cars, new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));
                        
                else if (c.Rotation == 180 || c.Rotation == 0)
                    c.calculate(tLLeftRigth, cars, new Point(pos.X + size.Width / 2, pos.Y + size.Height / 2));

                carOutOfBorders(ref roads, c);

            }
            foreach (Car c in carsToRemove)
            {
                cars.Remove(c);
            }
            carsToRemove.Clear();
            SpawnNewCars(ref cars);
        }
        public override void calculate(ref Infrastructure[,] roads)
        {
            if (autoMode)
            {
                tLLeftRigth.calculate();

                tLUpDown.calculate();
            }
            carCalculations(ref roads);
        }
        public void NextTLLeftRigth()
        {
            tLLeftRigth.Next();
           
        }
        public void NextTLUpDown()
        {
            tLUpDown.Next();
        }
        private void SpawnNewCars(ref List<Car> cars)
        {
            if (posToList.X == 0)
            {
                SpawnCar(ref cars, 180);
            }
            if (posToList.X == maxPosToList.X)
            {
                SpawnCar(ref cars, 0);
            }
            if (posToList.Y == 0)
            {
                SpawnCar(ref cars, -90);
            }
            if (posToList.Y == maxPosToList.Y)
            {
                SpawnCar(ref cars, 90);
            }
        }
        private void SpawnCar(ref List<Car> cars,int direction)
        {
            if (PossitionClear(ref cars, direction))
            {
                cars.Add(new Car(new Size(size.Width / 8, size.Height / 8), entryPoints[direction], movement[direction], direction));
            }
        }
        private bool PossitionClear(ref List<Car> cars, int direction)
        {
            if (direction == 0)
            {
                return !cars.Exists(c => (c.Pos.X >= entryPoints[direction].X - c.size.Width) && (c.Rotation == direction));
            }
            else if (direction == 180)
            {
                return !cars.Exists(c => (c.Pos.X <= entryPoints[direction].X + c.size.Width) && (c.Rotation == direction));
            }
            else if (direction == 90)
            {
                return !cars.Exists(c => (c.Pos.Y >= entryPoints[direction].Y - c.size.Width) && (c.Rotation == direction));
            }
            else if (direction == -90)
            {
                return !cars.Exists(c => (c.Pos.Y <= entryPoints[direction].Y + c.size.Width) && (c.Rotation == direction));
            }
            return true;
        }
        public override Graphics render(Graphics g)
        {
            Point[] p = { new Point(pos.X + 3 * size.Width / 8, pos.Y), new Point(pos.X + 5 * size.Width / 8, pos.Y), new Point(pos.X + 5 * size.Width / 8, pos.Y + 3 * size.Height / 8), new Point(pos.X + 1 * size.Width, pos.Y + 3 * size.Height / 8), new Point(pos.X + 1 * size.Width, pos.Y + 5 * size.Height / 8), new Point(pos.X + 5 * size.Width / 8, pos.Y + 5 * size.Height / 8), new Point(pos.X + 5 * size.Width / 8, pos.Y + 1 * size.Height), new Point(pos.X + 3 * size.Width / 8, pos.Y + 1 * size.Height), new Point(pos.X + 3 * size.Width / 8, pos.Y + 5 * size.Height / 8), new Point(pos.X, pos.Y + 5 * size.Height / 8), new Point(pos.X, pos.Y + 3 * size.Height / 8), new Point(pos.X + 3 * size.Width / 8, pos.Y + 3 * size.Height / 8) };
            g.FillPolygon(b, p);

            foreach (Car c in cars)
            {
                c.render(g);
            }
            tLLeftRigth.render(g);
            tLUpDown.render(g);
            return g;
        }
    }
}
