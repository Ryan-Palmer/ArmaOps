using ArmaOps.Domain.Coordinates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain
{
    // For Gun X to hit Target Y, it can use Z ballistic solutions
    public class FireSolution
    {
        public Cartesian Target { get; }
        public Battery Battery { get; }

        public IEnumerable<BallisticsSolution> BallisticsSolutions { get; }

        public FireSolution (
            Cartesian target,
            Battery battery,
            IEnumerable<BallisticsSolution> ballistics)
        {
            Target = target;
            Battery = battery;
            BallisticsSolutions = ballistics;
        }
    }
}
