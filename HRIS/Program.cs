using System;
using System.Linq;
using System.Runtime.InteropServices;
using HRIS.Database;

namespace HRIS {
    class Program {
        static void Main(string[] args) {

            var db = new DatabaseAdapter();

            var staff = db.FetchBasicStaffDetails();

            var staffmember = staff[0];
            var avalability = staffmember.Availability(DateTime.Now);

            Console.Write(avalability);
        }
    }
}
