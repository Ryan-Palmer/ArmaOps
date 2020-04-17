using ArmaOps.Domain.Coordinates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain
{
    public interface IBattlefieldEntity
    {
        string Name { get; }
        Cartesian Location { get; }
    }

    public class BattlefieldEntity : IBattlefieldEntity
    {
        public string Name { get; }
        public Cartesian Location { get; }

        public BattlefieldEntity(string name, Cartesian location)
        {
            Name = name;
            Location = location;
        }
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as BattlefieldEntity;
                if (other != null)
                {
                    return
                        other.Name.Equals(this.Name)
                        && other.Location.Equals(this.Location);
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Name, Location).GetHashCode();
        }
    }
}
