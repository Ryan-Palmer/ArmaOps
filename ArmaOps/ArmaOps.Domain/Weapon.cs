using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmaOps.Domain
{
    public class Weapon
    {
        public string Name { get; }
        public Angle MinElevation { get; }
        public Angle MaxElevation { get; }
        public IEnumerable<double> ChargeVelocities { get; }

        public Weapon(string name, Angle minElevation, Angle maxElevation, IEnumerable<double> chargeVelocities)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Must specify a name!");
            }
            if (minElevation.Radians > maxElevation.Radians)
            {
                throw new ArgumentException("Minimum elevation cannot be greater than maximum elevation!");
            }
            int count = 0;
            foreach (var cv in chargeVelocities)
            {
                ++count;
                if (cv < 0)
                {
                    throw new ArgumentOutOfRangeException("Velocity cannot be negative!");
                }
            }
            if (count == 0)
            {
                throw new ArgumentException("Must specify at least one velocity!");
            }

            Name = name;
            MinElevation = minElevation;
            MaxElevation = maxElevation;
            ChargeVelocities = chargeVelocities;
        }

        public bool ElevationIsAllowed(Angle elevation) => (elevation.Radians >= MinElevation.Radians && elevation.Radians <= MaxElevation.Radians);

        public Weapon EditName(string name) => new Weapon(name, MinElevation, MaxElevation, ChargeVelocities);

        public Weapon EditMinElevation(Angle minElevation) => new Weapon(Name, minElevation, MaxElevation, ChargeVelocities);

        public Weapon EditMaxElevation(Angle maxElevation) => new Weapon(Name, MinElevation, maxElevation, ChargeVelocities);

        public Weapon EditChargeVelocities(IEnumerable<double> chargeVelocities) => new Weapon(Name, MinElevation, MaxElevation, chargeVelocities);

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as Weapon;
                if (other != null)
                {
                    return
                        other.Name.Equals(this.Name)
                        && other.MinElevation.Equals(this.MinElevation)
                        && other.MaxElevation.Equals(this.MaxElevation)
                        && other.ChargeVelocities.SequenceEqual(this.ChargeVelocities);
                }
            }
            return false;
        }

        public override int GetHashCode() => (Name, MinElevation, MaxElevation, ChargeVelocities).GetHashCode();
    }
}
