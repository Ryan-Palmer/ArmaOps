using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain
{
    public class Weapon
    {
        public string Name { get; }
        public int MinElevation { get; }
        public int MaxElevation { get; }
        public IEnumerable<int> ChargeVelocity { get; }
        public bool AllowDirectFire { get; }

        public Weapon (string name, int minElevation, int maxElevation, IEnumerable<int> chargeVelocity, bool allowDirectFire)
        {
            Name = name;
            MinElevation = minElevation;
            MaxElevation = maxElevation;
            ChargeVelocity = chargeVelocity;
            AllowDirectFire = allowDirectFire;
        }
    }
}
