using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain.Coordinates
{
    public class Cartesian
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public Cartesian (double x, double y, double z)
        {
            //if (x < 0 || y < 0 || z < 0)
            //{
            //    throw new ArgumentOutOfRangeException("Co-ordinates must be greater than zero");
            //}

            X = x;
            Y = y;
            Z = z;
        }
    }
}
