using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmaOps.Domain
{
    public class Weapon
    {
        public string Name { get; }
        public Mils MinElevation { get; }
        public Mils MaxElevation { get; }
        public IEnumerable<double> ChargeVelocities { get; }

        public Weapon(string name, Mils minElevation, Mils maxElevation, IEnumerable<double> chargeVelocities)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Must specify a name!");
            }
            if (minElevation.Value > maxElevation.Value)
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

        public bool ElevationIsAllowed(Mils elevation) => (elevation.Value >= MinElevation.Value && elevation.Value <= MaxElevation.Value);

        public Weapon EditName(string name) => new Weapon(name, MinElevation, MaxElevation, ChargeVelocities);

        public Weapon EditMinElevation(Mils minElevation) => new Weapon(Name, minElevation, MaxElevation, ChargeVelocities);

        public Weapon EditMaxElevation(Mils maxElevation) => new Weapon(Name, MinElevation, maxElevation, ChargeVelocities);

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
