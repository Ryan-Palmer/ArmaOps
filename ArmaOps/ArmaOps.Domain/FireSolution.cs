using ArmaOps.Domain.Coordinates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain
{
    public enum SolutionType { Direct, Indirect }
    public class FireSolution
    {
        public double ChargeVelocity { get; }
        public Mils Elevation { get; }
        public SolutionType SolutionType { get; }

        public FireSolution(
            double chargeVelocity,
            Mils elevation,
            SolutionType solutionType)
        {
            ChargeVelocity = chargeVelocity;
            Elevation = elevation;
            SolutionType = solutionType;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as FireSolution;
                if (other != null)
                {
                    return
                        ChargeVelocity.Equals(other.ChargeVelocity)
                        && Elevation.Equals(other.Elevation)
                        && SolutionType.Equals(other.SolutionType);
                }
            }
            return false;
        }
        public override string ToString()
        {
            return $"ChargeVelocity:{ChargeVelocity} Elevation:{Elevation} SolutionType:{SolutionType}";
        }

        public override int GetHashCode() => (ChargeVelocity, Elevation, SolutionType).GetHashCode();
    }
}
