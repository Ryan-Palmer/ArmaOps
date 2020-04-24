using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain
{
    public class Mils
    {
        const int FULL_CIRCLE = 6400;
        const int HALF_CIRCLE = FULL_CIRCLE / 2;
        const double MultiplicandMilsToRadians = Math.PI / HALF_CIRCLE;
        const double MultiplicandRadiansToMils = HALF_CIRCLE / Math.PI;

        public int Value { get; }
        public double Radians => MultiplicandMilsToRadians * Value;

        public int Unsigned
        {
            get
            {
                if (Value < 0)
                {
                    return Value + FULL_CIRCLE;
                }
                return Value;
            }
        }

        public Mils (int milliRadians)
        {
            Value = Validate(milliRadians);
        }

        public Mils(double radians)
        {
            var milliRadians = (int)Math.Round(radians * MultiplicandRadiansToMils);
            Value = Validate(milliRadians);
        }

        public Mils Add(Mils b)
        {
            var result = Value + b.Value;
            return new Mils(result);
        }

        public Mils Sub(Mils b)
        {
            var result = Value - b.Value;
            return new Mils(result);
        }

        static int Validate(int milliRadians)
        {
            while (milliRadians > HALF_CIRCLE)
            {
                milliRadians -= FULL_CIRCLE;
            }
            while (milliRadians < -HALF_CIRCLE)
            {
                milliRadians += FULL_CIRCLE;
            }
            return milliRadians;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is Mils other)
                {
                    return
                        Value.Equals(other.Value);
                }
            }
            return false;
        }

        public override string ToString()
        {
            return $"Mils:{Value}";
        }

        public override int GetHashCode() => HashCode.Combine(Value);
    }
}
