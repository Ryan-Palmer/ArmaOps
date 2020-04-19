using ArmaOps.Domain.Coordinates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain
{
    public class Battery : BattlefieldEntity
    {
        public Weapon Weapon { get; }
        public Battery(string name, Cartesian location, Weapon weapon)
            : base(name, location)
        {
            Weapon = weapon;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as Battery;
                if (other != null)
                {
                    return
                        base.Equals(other)
                        && Weapon.Equals(other.Weapon);
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (base.GetHashCode(), Weapon.GetHashCode()).GetHashCode();
        }
    }
}
