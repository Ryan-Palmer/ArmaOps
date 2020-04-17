using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace ArmaOps.Domain
{
    public class BallisticsSolution
    {
        public double PositiveSolution { get; }
        public double NegativeSolution { get; }

        public BallisticsSolution (double posSolution, double negSolution)
        {
            PositiveSolution = posSolution;
            NegativeSolution = negSolution;
        }

        public override string ToString()
        {
            return $"Pos:{PositiveSolution} Neg:{NegativeSolution}";
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as BallisticsSolution;
                if (other != null)
                {
                    return
                        other.PositiveSolution == this.PositiveSolution
                        && other.NegativeSolution == this.NegativeSolution;
                }
            }
            return false;
        }
        public override int GetHashCode()
        {
            return (PositiveSolution, NegativeSolution).GetHashCode();
        }
    }
 
    public class Ballistics
    {
        public double Gravity { get; }
        public Ballistics (double gravity)
        {
            Gravity = gravity;
        }

        double ViableSolution(double v, double x, double y)
        {
            return Math.Pow(v, 4) - Gravity * (Gravity * Math.Pow(x, 2) + 2 * y * Math.Pow(v, 2));
        }

        public BallisticsSolution? GetSolutions(double v, double x, double y)
        {
            if (   v >   1000 
                || v <      0
                || x > 100000
                || x <      0 
                || y >   1000
                || y <  -1000)
            {
                throw new ArgumentOutOfRangeException();
            }

            var viable = ViableSolution(v, x, y);
            if(viable < 0)
            {
                return null;
            }
            var posSolution = Math.Atan2(Math.Pow(v, 2) + Math.Sqrt(viable), Gravity * x);
            var negSolution = Math.Atan2(Math.Pow(v, 2) - Math.Sqrt(viable), Gravity * x);
            return new BallisticsSolution(posSolution, negSolution);
        }
    }
}
