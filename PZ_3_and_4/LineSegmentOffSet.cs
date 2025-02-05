﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ_3_and_4
{
    public class LineSegmentOffSet : LineSegment
    {
        public LineSegmentOffSet(Point point1, Point point2) : base(point1, point2)
        {
            this.SavePoints();
        }
        public LineSegmentOffSet(double x1, double y1, double x2, double y2)
            : base(x1, y1, x2, y2)
        {
            this.SavePoints();
        }

        protected Point savedPoint1;
        protected Point savedPoint2;

        public override Point Point1
        {
            get
            {
                SavePoints();
                Offset();
                return this.point1;
            }
            set
            {
                SavePoints();
                this.point1 = value;
                Offset();
            }
        }
        public override Point Point2
        {
            get
            {
                SavePoints();
                Offset();
                return this.point2;
            }
            set
            {
                SavePoints();
                this.point2 = value;
                Offset();
            }
        }
        protected virtual void SavePoints()
        {
            this.savedPoint1 = this.point1;
            this.savedPoint2 = this.point2;
        }

        protected void Offset()
        {
            Point newPoint1 = this.point1;
            Point newPoint2 = this.point2;

            if (savedPoint1.GetCoord() != point1.GetCoord())
            {
                double f = Math.Atan((newPoint1.CoordY - this.savedPoint2.CoordY) /
                    (newPoint1.CoordX - this.savedPoint2.CoordX));
                double length = GetLength(this.savedPoint1, this.point2);
                newPoint2.CoordX = length * Math.Cos(f) + newPoint1.CoordX;
                newPoint2.CoordY = length * Math.Sin(f) + newPoint1.CoordY;
            }
            else if (savedPoint2.GetCoord() != point2.GetCoord())
            {
                double f = Math.Atan((newPoint2.CoordY - this.savedPoint1.CoordY) /
                    (newPoint2.CoordX - this.savedPoint1.CoordX));
                double length = GetLength(point1, savedPoint2);
                newPoint1.CoordX = length * Math.Cos(f) + newPoint2.CoordX;
                newPoint1.CoordY = length * Math.Sin(f) + newPoint2.CoordY;
            }
            point1 = newPoint1;
            point2 = newPoint2;
            SavePoints();
        }

        public double GetLength() =>
            this.GetLength(this.point1, this.point2);

        protected double GetLength(Point point1, Point point2) =>
            Math.Sqrt(Math.Pow(point2.CoordX - point1.CoordX, 2) +
                Math.Pow(point2.CoordY - point1.CoordY, 2));

        public override string ToString()
        {
            SavePoints();
            Offset();
            return "Point1: " + this.point1.ToString() +
                " Point2: " + this.point2.ToString();
        }
        public override void Show()
        {
            Console.WriteLine(Point1);
            Console.WriteLine(Point2);
            Console.WriteLine(GetLength());
        }
    }
}
