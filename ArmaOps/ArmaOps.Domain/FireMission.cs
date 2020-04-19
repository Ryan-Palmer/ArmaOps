using ArmaOps.Domain.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmaOps.Domain
{
    public class FireMission
    {
        public string Name { get; }
        public Cartesian Target { get; }

        public FireMission(Cartesian target)
        {
            Name = Guid.NewGuid().ToString();
            Target = target;
        }

        public FireMission(string name, Cartesian target)
        {
            Name = name;
            Target = target;
        }

        public FireMission(
            ForwardObserver fo, Mils observedAzimuth,
            Mils observedElevation, int observedDistanceMetres)
        {
            Name = $"Fire Mission for {fo.Name}";
            Target = GetCartesianTarget(fo, observedAzimuth, observedElevation, observedDistanceMetres);
        }

        public FireMission(
            string name, ForwardObserver fo, Mils observedAzimuth,
            Mils observedElevation, int observedDistanceMetres)
        {
            Name = name;
            Target = GetCartesianTarget(fo, observedAzimuth, observedElevation, observedDistanceMetres);
        }

        Cartesian GetCartesianTarget(ForwardObserver fo, Mils observedAzimuth,
            Mils observedElevation, int observedDistanceMetres)
        {
            return new Polar(fo.Location,
                observedAzimuth.Radians,
                observedElevation.Radians,
                observedDistanceMetres).ToCartesian();
        }

        public IEnumerable<FireSolution> GetSolutions(Battery battery)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FireSolution> GetSolutions(IEnumerable<Battery> batteries)
        {
            return
                batteries
                .Select(b => GetSolutions(b))
                .SelectMany(s => s);
        }

        public FireMission ApplyCorrection(/* Some correction params, maybe multiple overrides?*/)
        {
            throw new NotImplementedException();
        }
    }
}
