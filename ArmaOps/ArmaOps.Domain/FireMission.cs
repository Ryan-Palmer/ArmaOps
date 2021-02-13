using ArmaOps.Domain.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmaOps.Domain
{
    public class FireMission
    {
        const double EARTH_G = 9.80665; // Could pass this in constructor if necessary
        public Guid Id { get; }
        public DateTime Created { get; }
        public string Name { get; set; }

        //private List<IFireMissionLogEntry> _missionLog = new List<IFireMissionLogEntry>();
        //public IEnumerable<IFireMissionLogEntry> MissionLog => _missionLog.AsReadOnly();

        public FireMission(
            Guid id,
            string name,
            DateTime created)
        {
            Id = id;
            Name = name;
            Created = created;
        }

        BatterySolutionSet GetSolutionSet(Battery battery, Cartesian target)
        {
            var ballistics = new Ballistics(EARTH_G);
            var polarToTarget = target.ToPolar(battery.Location);
            var azToTarget = new Angle(polarToTarget.Azimuth);
            var solutions = new List<FireSolution>();
            foreach (var cv in battery.Weapon.ChargeVelocities)
            {
                var ballisticSolutions = ballistics?.GetSolutions(cv, polarToTarget.HDist, polarToTarget.VDist);
                if (ballisticSolutions != null)
                {
                    var directSolution = new Angle(ballisticSolutions.NegativeSolution);
                    var indirectSolution = new Angle(ballisticSolutions.PositiveSolution);

                    if (battery.Weapon.ElevationIsAllowed(directSolution))
                    {
                        solutions.Add(new FireSolution(cv, directSolution, SolutionType.Direct));
                    }

                    if (battery.Weapon.ElevationIsAllowed(indirectSolution))
                    {
                        solutions.Add(new FireSolution(cv, indirectSolution, SolutionType.Indirect));
                    }
                }
            }
            return new BatterySolutionSet(battery, target, azToTarget, solutions);
        }

        BatterySolutionSet GetSolutionSet(Battery battery, 
            ForwardObserver fo, Angle observedAzimuth, Angle observedElevation, double observedDistanceMetres)
        {
            var polarTarget = new Polar(fo.Location, observedAzimuth.Radians, observedElevation.Radians, observedDistanceMetres);
            return GetSolutionSet(battery, polarTarget.ToCartesian());
        }

        public IEnumerable<BatterySolutionSet> GetSolutionSets(IEnumerable<Battery> batteries, Cartesian target)
        {
            return batteries.Select(b => GetSolutionSet(b, target));
        }

        public IEnumerable<BatterySolutionSet> GetSolutionSets(IEnumerable<Battery> batteries, 
            ForwardObserver fo, Angle observedAzimuth, Angle observedElevation, double observedDistanceMetres)
        {
            return batteries.Select(b => GetSolutionSet(b, fo, observedAzimuth, observedElevation, observedDistanceMetres));
        }

        public BatterySolutionSet ApplyCorrection(BatterySolutionSet last, Cartesian delta)
        {
            return GetSolutionSet(last.Battery, last.Target.Add(delta));
        }

        public BatterySolutionSet ApplyCorrection(BatterySolutionSet last,
            ForwardObserver fo, Angle deltaAzimuth,
            Angle deltaElevation, double deltaDistanceMetres)
        {
            var polarTarget = last.Target.ToPolar(fo.Location);
            var newTargetPolar = polarTarget.Add(deltaAzimuth.Radians, deltaElevation.Radians, deltaDistanceMetres);
            var newTargetCartesian = newTargetPolar.ToCartesian();

            return GetSolutionSet(last.Battery, newTargetCartesian);
        }
    }
}
