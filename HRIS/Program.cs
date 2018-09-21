using System;
using System.Linq;
using System.Runtime.InteropServices;
using HRIS.Database;

namespace HRIS {
    class Program {
        static void Main(string[] args) {
            
            var db = new DatabaseAdapter();

            var units = db.FetchUnits();
            
            var classes = db.FetchClasses(units.Single(unit => unit.Code == "KIT107"));

            foreach (var unitclass in classes) {
                Console.WriteLine(unitclass.Room);
            }
        }
    }
}
