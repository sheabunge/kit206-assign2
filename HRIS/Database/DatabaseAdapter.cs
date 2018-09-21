using System.Collections.Generic;
using HRIS.Teaching;

namespace HRIS.Database {
	public class SchoolDBAdapter {

		List<Staff> FetchBasicStaffDetails() {
			return new List<Staff>();
		}

		Staff CompleteStaffDetails(Staff staff) {
			return staff;
		}

		List<Unit> FetchUnits() {
			return new List<Unit>();
		}

		List<UnitClass> FetchClasses(Unit unit) {
			return new List<UnitClass>();
		}
	}
}