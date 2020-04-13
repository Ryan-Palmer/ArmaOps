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
            //if (x < 0 || y < 0 || z < 0)
            //{
            //    throw new ArgumentOutOfRangeException("Co-ordinates must be greater than zero");
            //}

            X = x;
            Y = y;
            Z = z;
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
            var dx = X - b.X;
            var dy = Y - b.Y;
            var dz = Z - b.Z;
            return new Cartesian(dx, dy, dz);
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
            var dx = X + b.X;
            var dy = Y + b.Y;
            var dz = Z + b.Z;
            return new Cartesian(dx, dy, dz);
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
            (X, Y, Z).GetHashCode();
            //unchecked
            //{
            //    int hash = 17; // multiply 2 primes by the hash code of internal values
            //    hash = hash * 23 + X.GetHashCode();
            //    hash = hash * 23 + Y.GetHashCode();
            //    hash = hash * 23 + Z.GetHashCode();
            //    return hash;
            //}
        }
    }
}
