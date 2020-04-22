﻿using ArmaOps.Domain.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmaOps.Domain
{
    public class BatterySolutionSet
    {
        public Cartesian Target { get; }
        public Battery Battery { get; }
        public IEnumerable<FireSolution> FireSolutions { get; }

        public BatterySolutionSet(
            Battery battery, 
            Cartesian target,
            IEnumerable<FireSolution> fireSolutions)
        {
            Battery = battery;
            Target = target;
            FireSolutions = fireSolutions;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as BatterySolutionSet;
                if (other != null)
                {
                    return
                        Target.Equals(other.Target)
                        && Battery.Equals(other.Battery)
                        && FireSolutions.SequenceEqual(other.FireSolutions);
                }
            }
            return false;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach(var solution in FireSolutions)
            {
                builder.Append($"{solution}\n");
            }
            return $"Battery:{Battery} \nTarget:{Target} \nFireSolutions:\n{builder}";
        }

        public override int GetHashCode()
        {
            return (Target, Battery, FireSolutions).GetHashCode();
        }
    }
}
