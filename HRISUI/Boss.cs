using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRISUI
{
    class Boss
    {
        public Boss()
        {
        }
        public List<Staff> GetViewableList() {
            var list = new List<Staff>();

            var bob = new Staff();
            bob.GivenName = "B.";
            bob.FamilyName = "Harris";
            bob.Title = "Dr.";
            list.Add(bob);

            var jane = new Staff();
            jane.GivenName = "J.";
            jane.FamilyName = "Elliot";
            jane.Title = "Mrs.";
            list.Add(jane);

            return list;
        }
    }
}
