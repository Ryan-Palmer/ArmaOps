using ArmaOps.Domain.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmaOps.Domain
{
    public class BatterySolutionSet
    {
        const double EARTH_G = 9.80665; // Could pass this in constructor if necessary
        public Cartesian Target { get; }
        public Battery Battery { get; }
        public Mils AzimuthToTarget { get; }
        public IEnumerable<FireSolution> FireSolutions { get; }

        public BatterySolutionSet(
            Battery battery,
            Cartesian target,
            Mils azToTarget,
            IEnumerable<FireSolution> fireSolutions)
        {
            Battery = battery;
            Target = target;
            AzimuthToTarget = azToTarget;
            FireSolutions = fireSolutions;
        }

        //public BatterySolutionSet ApplyCorrection(Cartesian delta)
        //{
        //    return new FireMission(Target.Add(delta)).GetSolutionSet(Battery);
        //}

        //public BatterySolutionSet ApplyCorrection(
        //    ForwardObserver fo, Mils deltaAzimuth,
        //    Mils deltaElevation, double deltaDistanceMetres)
        //{
        //    var polarTarget = Target.ToPolar(fo.Location);
        //    var newTargetPolar = polarTarget.Add(deltaAzimuth.Radians, deltaElevation.Radians, deltaDistanceMetres);
        //    var newTargetCartesian = newTargetPolar.ToCartesian();

        //    return new FireMission(newTargetCartesian).GetSolutionSet(Battery);
        //}

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
                        && AzimuthToTarget.Equals(other.AzimuthToTarget)
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
            return $"Battery:{Battery} \nTarget:{Target} \nAzToTarget:{AzimuthToTarget} \nFireSolutions:\n{builder}";
        }

        public override int GetHashCode()
        {
            return (Target, Battery, AzimuthToTarget, FireSolutions).GetHashCode();
        }
    }
}
