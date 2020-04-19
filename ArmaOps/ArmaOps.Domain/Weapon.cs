﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmaOps.Domain
{
    public class Weapon
    {
        public string Name { get; }
        public int MinElevation { get; }
        public int MaxElevation { get; }
        public IEnumerable<double> ChargeVelocities { get; }

        public Weapon (string name, int minElevation, int maxElevation, IEnumerable<double> chargeVelocities)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Must specify a name!");
            }
            if(minElevation < -6400 || minElevation > 6400)
            {
                throw new ArgumentOutOfRangeException("Minimum elevation must be ±6400");
            }
            if (maxElevation < -6400 || maxElevation > 6400)
            {
                throw new ArgumentOutOfRangeException("Maximum elevation must be ±6400");
            }
            if (minElevation > maxElevation)
            {
                throw new ArgumentException("Minimum elevation cannot be greater than maximum elevation!");
            }
            int count = 0;
            foreach(var cv in chargeVelocities)
            {
                ++count;
                if(cv < 0)
                {
                    throw new ArgumentOutOfRangeException("Velocity cannot be negative!");
                }
            }
            if(count == 0)
            {
                throw new ArgumentException("Must specify at least one velocity!");
            }

            Name = name;
            MinElevation = minElevation;
            MaxElevation = maxElevation;
            ChargeVelocities = chargeVelocities;
        }

        public Weapon EditName(string name)
        {
            return new Weapon(name, MinElevation, MaxElevation, ChargeVelocities);
        }
        public Weapon EditMinElevation(int minElevation)
        {
            return new Weapon(Name, minElevation, MaxElevation, ChargeVelocities);
        }
        public Weapon EditMaxElevation(int maxElevation)
        {
            return new Weapon(Name, MinElevation, maxElevation, ChargeVelocities);
        }
        public Weapon EditChargeVelocities(IEnumerable<double> chargeVelocities)
        {
            return new Weapon(Name, MinElevation, MaxElevation, chargeVelocities);
        }

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

        public override int GetHashCode()
        {
            return (Name, MinElevation, MaxElevation, ChargeVelocities).GetHashCode();
        }
    }
}
