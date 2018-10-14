using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRIS.Teaching;

namespace HRIS.Database {
	/// <summary>
	/// Central location for storing model objects which are used in various sections of the application
	/// such as Staff members and Units
	/// </summary>
	internal class DataStore {
		/// <summary>
		/// This is a singleton class to ensure that each application section is accessing the same data.
		/// This property retrieves the single instance
		/// </summary>
		public static DataStore Instance { get; } = new DataStore();

		/// <summary>
		/// Dictionary for storing staff members.
		/// Staff objects are keyed by their ID for quick retrieval
		/// </summary>
		private readonly Dictionary<int, Staff> staffMembers;

		/// <summary>
		/// Dictionary for storing units.
		/// Unit objects are keyed by their code for quick retrieval
		/// </summary>
		private readonly Dictionary<string, Unit> units;

		/// <summary>
		/// Class constructor
		/// </summary>
		private DataStore() {
			units = new Dictionary<string, Unit>();
			staffMembers = new Dictionary<int, Staff>();
		}

		/// <summary>
		/// Retrieve the Staff object for a given staff ID.
		/// Will either return a previously-retrieved object, or construct a new one
		/// </summary>
		/// <param name="staffID">Staff ID</param>
		/// <returns>Staff object</returns>
		public Staff GetStaffMember(int staffID) {
			// if the specified staff member has already been retrieved, return it
			if (staffMembers.TryGetValue(staffID, out var staff)) {
				return staff;
			}

			// otherwise, construct a new Staff object using the given identifier
			staff = new Staff(staffID);
			staffMembers.Add(staff.ID, staff);
			return staff;
		}

		/// <summary>
		/// Retrieve the Unit object for a given unit code.
		/// Will either return a previously-retrieved object, or construct a new one
		/// </summary>
		/// <param name="unitCode">Unit Code</param>
		/// <returns>Unit object</returns>
		public Unit GetUnit(string unitCode) {
			// if the specified unit has already been retrieved, return it
			if (units.TryGetValue(unitCode, out var unit)) {
				return unit;
			}

			// otherwise, construct a new Unit object using the given unit code
			unit = new Unit(unitCode);
			units.Add(unit.Code, unit);
			return unit;
		}
	}
}
