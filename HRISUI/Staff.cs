using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRISUI
{
    class Staff
    {
        public string FamilyName { get; set; }

        public string GivenName { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            //For the purposes of this week's demonstration this returns only the name
            return Title + " " + GivenName + " " + FamilyName;
        }
    }
}
