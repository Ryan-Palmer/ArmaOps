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
