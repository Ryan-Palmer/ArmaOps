using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain
{
    public class Angle
    {
        const int FULL_CIRCLE_NATO = 6400;
        const int HALF_CIRCLE_NATO = FULL_CIRCLE_NATO / 2;
        const double NATOMilsToRadians = Math.PI / HALF_CIRCLE_NATO;
        const double RadiansToNATOMils = HALF_CIRCLE_NATO / Math.PI;
        const double MilsToRadians = 0.001;
        const double RadiansToMils = 1000.0;

        public double Radians { get; }
        public double NATOMils => Math.Round(Radians * RadiansToNATOMils);
        public double Mils => Math.Round(Radians * RadiansToMils);
        public Angle(int milliRadians, bool nato)
        {
            if(nato)
            {
                Radians = Validate(milliRadians) * NATOMilsToRadians;
            }
            else
            {
                Radians = Validate((double)milliRadians * MilsToRadians);
            }
        }

        public Angle(double radians)
        {
            Radians = Validate(radians);
        }

        public Angle Add(Angle b)
        {
            var result = Radians + b.Radians;
            return new Angle(result);
        }

        public Angle Sub(Angle b)
        {
            var result = Radians - b.Radians;
            return new Angle(result);
        }

        static int Validate(int NATOmilliRadians)
        {
            while (NATOmilliRadians >= FULL_CIRCLE_NATO)
            {
                NATOmilliRadians -= FULL_CIRCLE_NATO;
            }
            while (NATOmilliRadians < 0)
            {
                NATOmilliRadians += FULL_CIRCLE_NATO;
            }
            return NATOmilliRadians;
        }

        static double Validate(double radians)
        {
            while (radians >= 2 * Math.PI)
            {
                radians -= 2 * Math.PI;
            }
            while (radians < 0)
            {
                radians += 2 * Math.PI;
            }
            return radians;
        }
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is Angle other)
                {
                    return
                        Radians.Equals(other.Radians);
                }
            }
            return false;
        }

        public override string ToString()
        {
            return $"Angle:{Radians}";
        }

        public override int GetHashCode() => HashCode.Combine(Radians);
    }
}
