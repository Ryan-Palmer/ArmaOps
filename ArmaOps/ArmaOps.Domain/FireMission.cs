using ArmaOps.Domain.Coordinates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain
{
    public abstract class FireMission
    {
        public string Name { get; }
        public Cartesian Target { get; }

        public FireMission(string name, Cartesian target)
        {
            Name = name;
            Target = target;
        }

        public FireMission(Cartesian target)
        {
            Name = Guid.NewGuid().ToString();
            Target = target;
        }
    }

    public class PolarFireMission : FireMission
    {
        public PolarFireMission(
            ForwardObserver fo, Mils observedAzimuth,
            Mils observedElevation, int observedDistanceMetres)
            : base(fo.Name, GetCartesianTarget(fo, observedAzimuth, observedElevation, observedDistanceMetres))
        {
            
        }

        public PolarFireMission(
            string name, ForwardObserver fo, Mils observedAzimuth,
            Mils observedElevation, int observedDistanceMetres)
            : base(name, GetCartesianTarget(fo, observedAzimuth, observedElevation, observedDistanceMetres))
        {

        }

        static Cartesian GetCartesianTarget (ForwardObserver fo, Mils observedAzimuth,
            Mils observedElevation, int observedDistanceMetres)
        {
            return new Polar(fo.Location,
                observedAzimuth.Radians,
                observedElevation.Radians,
                observedDistanceMetres).ToCartesian();
        }


    }

    public class GridFireMission : FireMission
    {
        public GridFireMission(Cartesian target) : base(target)
        {
            
        }
        public GridFireMission(string name, Cartesian target) : base(name, target)
        {

        }
    }
}
