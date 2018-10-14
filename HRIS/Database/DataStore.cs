using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRIS.Teaching;

namespace HRIS.Database {
	class DataStore {

        public static DataStore Instance { get; } = new DataStore();

		private readonly Dictionary<int, Staff> staffMembers;

		private readonly Dictionary<string, Unit> units;

		private DataStore() {
			units = new Dictionary<string, Unit>();
			staffMembers = new Dictionary<int, Staff>();
		}

		public Staff GetStaffMember(int staffID) {
			if (!staffMembers.TryGetValue(staffID, out Staff staff)) {
				staff = new Staff { ID = staffID };
				staffMembers.Add(staff.ID, staff);
			}
			return staff;
		}

		public Unit GetUnit(string unitCode) {
			if (!units.TryGetValue(unitCode, out Unit unit)) {
				unit = new Unit { Code = unitCode };
				units.Add(unit.Code, unit);
			}
			return unit;
		}
	}
}
