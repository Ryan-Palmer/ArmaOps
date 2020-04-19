using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain
{
    public class Mils
    {
        const double MultiplicandMilsToRadians = Math.PI / 3200.0;
        const double MultiplicandRadiansToMils = 3200.0 / Math.PI;

        public int Value { get; }
        public double Radians => MultiplicandMilsToRadians * Value;

        public Mils (int milliRadians)
        {
            Value = milliRadians;
        }

        public Mils(double radians)
        {
            Value = (int)Math.Round(radians * MultiplicandRadiansToMils);
        }
    }
}
