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

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as Mils;
                if (other != null)
                {
                    return
                        Value.Equals(other.Value);
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
