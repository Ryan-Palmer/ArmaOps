using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain.Coordinates
{
    public interface ICoordinateManager
    {
        Polar ToPolar(Cartesian cartesian);
        Cartesian ToCartesian(Polar polar);
    }

    public class CoordinateManager : ICoordinateManager
    {
        public Cartesian ToCartesian(Polar polar)
        {
            throw new NotImplementedException();
        }

        public Polar ToPolar(Cartesian cartesian)
        {
            throw new NotImplementedException();
        }
    }
}
