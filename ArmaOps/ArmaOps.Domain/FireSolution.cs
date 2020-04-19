using ArmaOps.Domain.Coordinates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain
{
    public class FireSolution
    {
        public double ChargeVelocity { get; }
        public Mils DirectSolution { get; }
        public Mils IndirectSolution { get; }

        public FireSolution(
            double chargeVelocity,
            Mils directSolution,
            Mils indirectSolution)
        {
            ChargeVelocity = chargeVelocity;
            DirectSolution = directSolution;
            IndirectSolution = indirectSolution;
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
                        && DirectSolution.Equals(other.DirectSolution)
                        && IndirectSolution.Equals(other.IndirectSolution);
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (ChargeVelocity, DirectSolution, IndirectSolution).GetHashCode();
        }
    }
}
