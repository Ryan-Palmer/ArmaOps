using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain.Coordinates
{
    public interface ICoordinateManager
    {
        Polar ToPolar(ForwardObserver fO, Cartesian target);
        Cartesian ToCartesian(Polar pol);
    }

    public class CoordinateManager : ICoordinateManager
    {
        public Polar ToPolar(ForwardObserver fO, Cartesian target)
        {
            var diff = Cartesian.Subtract(target, fO.Location);
            var azimuth = Math.Atan2(diff.X, diff.Y);
            var elevation = 0.0;
            var dist = Math.Sqrt(Math.Pow(diff.X,2) + Math.Pow(diff.Y,2));
            return new Polar(fO, azimuth, elevation, dist);
        }

        public Cartesian ToCartesian(Polar pol)
        {
            var dx = pol.Distance * Math.Sin(pol.Azimuth);
            var dy = pol.Distance * Math.Cos(pol.Azimuth);
            var dz = 0.0;
            return new Cartesian(dx, dy, dz).Add(pol.FO.Location);
        }
    }
}