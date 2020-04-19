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
    }
}
