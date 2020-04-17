using ArmaOps.Domain.Coordinates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain
{
    public class ForwardObserver : BattlefieldEntity
    {
        public ForwardObserver (string name, Cartesian location) 
            : base (name,location)
        {

        }
    }
}
