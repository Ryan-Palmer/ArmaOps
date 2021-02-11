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
        public double PositiveTimeOfFlight { get; }
        public double NegativeTimeOfFlight { get; }
        public double PositiveAngleOfImpact { get; }
        public double NegativeAngleOfImpact { get; }

        public BallisticsSolution (double posSolution, double negSolution, 
            double posTimeOfFlight, double negTimeOfFlight, double posAngleOfImpact, double negAngleOfImpact)
        {
            PositiveSolution = posSolution;
            NegativeSolution = negSolution;
            PositiveTimeOfFlight = posTimeOfFlight;
            NegativeTimeOfFlight = negTimeOfFlight;
            PositiveAngleOfImpact = posAngleOfImpact;
            NegativeAngleOfImpact = negAngleOfImpact;
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

        public override int GetHashCode() => (PositiveSolution, NegativeSolution).GetHashCode();
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

            var x2 = x*x;
            var v2 = v*v;
            var v4 = v2*v2;

            var viableSolution = v4 - Gravity * (Gravity * x2 + 2 * y * v2);
            if(viableSolution < 0)
            {
                return null;
            }
            var posSolution = Math.Atan2(Math.Pow(v, 2) + Math.Sqrt(viableSolution), Gravity * x);
            var negSolution = Math.Atan2(Math.Pow(v, 2) - Math.Sqrt(viableSolution), Gravity * x);

            var pvse = v * Math.Sin(posSolution);
            var pvce = v * Math.Cos(posSolution);
            var nvse = v * Math.Sin(negSolution);
            var nvce = v * Math.Cos(negSolution);
            var pvse2 = pvse * pvse;
            var nvse2 = nvse * nvse;
            var tgy = (2 * Gravity * y);

            var posTimeOfFlight = (pvse + Math.Sqrt(pvse2 + tgy)) / Gravity;
            var negTimeOfFlight = (nvse + Math.Sqrt(nvse2 + tgy)) / Gravity;

            var posAngleOfImpact = Math.Atan2(Math.Sqrt(pvse2 + tgy), pvce);
            var negAngleOfImpact = Math.Atan2(Math.Sqrt(nvse2 + tgy), nvce);

            return new BallisticsSolution(posSolution, negSolution, posTimeOfFlight, negTimeOfFlight, posAngleOfImpact, negAngleOfImpact);
        }
    }
}
