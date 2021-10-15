using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    public class Punto
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Punto(double x, double y)
        {
            X = x;
            Y = y;

        }
        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }
    }
}
