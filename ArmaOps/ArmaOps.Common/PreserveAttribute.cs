using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Common
{
    public class PreserveAttribute : Attribute
    {
        public bool AllMembers;
        public bool Conditional;
    }
}
