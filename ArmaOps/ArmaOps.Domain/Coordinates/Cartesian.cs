using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain.Coordinates
{
    public class Cartesian
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public Cartesian (double x, double y, double z)
        {
            if (    x >  Math.Sqrt(double.MaxValue) 
                ||  x < -Math.Sqrt(double.MaxValue) 
                ||  y >  Math.Sqrt(double.MaxValue)
                ||  y < -Math.Sqrt(double.MaxValue)
                ||  z >  Math.Sqrt(double.MaxValue)
                ||  z < -Math.Sqrt(double.MaxValue))
            {
                throw new ArgumentOutOfRangeException("Co-ordinates out of range!");
            }

            X = x;
            Y = y;
            Z = z;
        }

        public Polar ToPolar(Cartesian origin)
        {
            var diff = Subtract(origin);
            var azimuth = Math.Atan2(diff.X, diff.Y);
            var elevation = 0.0d;
            var dist = Math.Sqrt(Math.Pow(diff.X, 2.0d) + Math.Pow(diff.Y, 2.0d));
            return new Polar(origin, azimuth, elevation, dist);
        }

        public static Cartesian Subtract(Cartesian a, Cartesian b)
        {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;
            var dz = a.Z - b.Z;
            return new Cartesian(dx, dy, dz);
        }

        public Cartesian Subtract(Cartesian b)
        {
            return Subtract(this, b);
        }

        public static Cartesian Add(Cartesian a, Cartesian b)
        {
            var dx = a.X + b.X;
            var dy = a.Y + b.Y;
            var dz = a.Z + b.Z;
            return new Cartesian(dx, dy, dz);
        }

        public Cartesian Add(Cartesian b)
        {
            return Add(this, b);
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as Cartesian;
                if (other != null)
                {
                    return
                        other.X == this.X
                        && other.Y == this.Y
                        && other.Z == this.Z;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (X, Y, Z).GetHashCode();
        }

        public override string ToString()
        {
            return $"X:{X} Y:{Y} Z:{Z}";
        }
    }
}
