using System;
using System.Collections.Generic;
using HRIS.Teaching;
using MySql.Data.MySqlClient;

namespace HRIS.Database {
	/// <summary>
	/// Adapter class used for fetching data from the MySQL database
	/// </summary>
	public class DatabaseAdapter {
		/// <summary>
		/// Database name
		/// </summary>
		private const string DB = "kit206";

		/// <summary>
		/// Name of user with permissions to access the database
		/// </summary>
		private const string User = "kit206";

		/// <summary>
		/// Password associated with the authorised user
		/// </summary>
		private const string Pass = "kit206";

		/// <summary>
		/// Remote server location of the database
		/// </summary>
		private const string Server = "alacritas.cis.utas.edu.au";

		/// <summary>
		/// Instance of data storage class used for caching certain types of data
		/// </summary>
		private readonly DataStore dataStore = DataStore.Instance;

		/// <summary>
		/// MySQL connection object
		/// </summary>
		private MySqlConnection Connection;

		/// <summary>
		/// Class constructor
		/// </summary>
		public DatabaseAdapter() {
			var details = $"Database={DB};Data Source={Server};User Id={User};Password={Pass};SslMode=none;";
			Connection = new MySqlConnection(details);
		}

		/// <summary>
		/// Parse a string value into its matching enumerated value
		/// </summary>
		/// <param name="value">String value to convert</param>
		/// <typeparam name="T">Enum type to convert to</typeparam>
		/// <returns>Converted enumerated value</returns>
		private static T ParseEnum<T>(string value) {
			return (T) Enum.Parse(typeof(T), value);
		}

		/// <summary>
		/// Fetch the basic details required to display a list of staff members
		/// </summary>
		/// <returns></returns>
		public List<Staff> FetchBasicStaffDetails() {
			MySqlDataReader reader = null;
			var staffList = new List<Staff>();

			try {
				Connection.Open();
				var command = new MySqlCommand("SELECT id, given_name, family_name, title, campus, category FROM staff", Connection);
				reader = command.ExecuteReader();

				while (reader.Read()) {
					// attempt to fetch this staff member from the data storage, if it exists
					var staff = dataStore.GetStaffMember(reader.GetInt32("id"));

					// fill in additional data
					staff.GivenName = reader.GetString("given_name");
					staff.FamilyName = reader.GetString("family_name");
					staff.Title = reader.GetString("title");
					staff.Campus = ParseEnum<Campus>(reader.GetString("campus"));
					staff.Category = ParseEnum<Category>(reader.GetString("category"));

					staffList.Add(staff);
				}
			} finally {
				reader?.Close();
				Connection?.Close();
			}

			return staffList;
		}

		/// <summary>
		/// Fetch and update a Staff object with all available basic information
		/// </summary>
		/// <param name="staff">Staff member to update</param>
		public void CompleteStaffDetails(Staff staff) {
			MySqlDataReader reader = null;

			try {
				Connection.Open();

				var command = new MySqlCommand("SELECT * FROM staff WHERE id = @staffid", Connection);
				command.Parameters.AddWithValue("@staffid", staff.ID.ToString());
				reader = command.ExecuteReader();

				if (reader.Read()) {
					staff.GivenName = reader.GetString("given_name");
					staff.FamilyName = reader.GetString("family_name");
					staff.Title = reader.GetString("title");
					staff.Campus = ParseEnum<Campus>(reader.GetString("campus"));
					staff.Room = reader.GetString("room");
					staff.Email = reader.GetString("email");
					staff.Phone = reader.GetString("phone");
					staff.Photo = reader.GetString("photo");
					staff.Category = ParseEnum<Category>(reader.GetString("category"));
				}
			} finally {
				reader?.Close();
				Connection?.Close();
			}
		}

		/// <summary>
		/// Fetch and update a staff object with associated other objects, such as Units, UnitClasses, and Events
		/// </summary>
		/// <param name="staff">Staff member to update</param>
		public void FetchStaffTeaching(Staff staff) {
			MySqlDataReader reader = null;

			try {
				Connection.Open();

				// begin by fetching staff consultation times
				var command = new MySqlCommand("SELECT day, start, end FROM consultation WHERE staff_id = @staffid", Connection);
				command.Parameters.AddWithValue("@staffid", staff.ID.ToString());
				reader = command.ExecuteReader();

				staff.Consultations = new List<Event>();

				while (reader.Read()) {
					staff.Consultations.Add(new Event {
						Day = ParseEnum<DayOfWeek>(reader.GetString("day")),
						Start = reader.GetTimeSpan("start"),
						End = reader.GetTimeSpan("end"),
					});
				}

				reader.Close();

				// while fetching classes, keep track of the units that are taught by this staff member
				var teachingUnitCodes = new HashSet<string>();

				// next, fetch the classes that are taught by this staff member
				const string sql = "SELECT unit_code, campus, day, start, end, type, room FROM class WHERE staff = @staffid";
				command = new MySqlCommand(sql, Connection);
				command.Parameters.AddWithValue("@staffid", staff.ID.ToString());
				reader = command.ExecuteReader();

				staff.Classes = new List<UnitClass>();
				staff.UnitsTeaching = new List<Unit>();
				.0000000000000000000000
				var units = new Dictionary<String, Unit>();

				while (reader.Read()) {
					var unit = dataStore.GetUnit(reader.GetString("unit_code"));

					var unitClass = new UnitClass {
						Unit = unit,
						Campus = ParseEnum<Campus>(reader.GetString("campus")),
						Day = ParseEnum<DayOfWeek>(reader.GetString("day")),
						Start = reader.GetTimeSpan("start"),
						End = reader.GetTimeSpan("end"),
						Type = ParseEnum<UnitClassType>(reader.GetString("type")),
						Room = reader.GetString("room"),
						Staff = staff,
					};

					staff.Classes.Add(unitClass);
					teachingUnitCodes.Add(unitClass.Unit.Code);
				}

				reader.Close();

				// next, fetch the units that are coordinated by this staff member
				command = new MySqlCommand("SELECT code, title FROM unit WHERE coordinator = @staffid", Connection);
				command.Parameters.AddWithValue("@staffid", staff.ID.ToString());
				reader = command.ExecuteReader();

				staff.UnitsCoordinating = new List<Unit>();

				while (reader.Read()) {
					var unit = dataStore.GetUnit(reader.GetString("code"));

					unit.Title = reader.GetString("title");
					unit.Coordinator = staff;

					staff.UnitsCoordinating.Add(unit);

					// if the staff member also teaches for this unit, add it to the Staff object
					if (teachingUnitCodes.Contains(unit.Code)) {
						staff.UnitsTeaching.Add(unit);
						teachingUnitCodes.Remove(unit.Code);
					}
				}

				reader.Close();

				// finally, fetch the units that are taught but not coordinated by the staff member
				if (teachingUnitCodes.Count > 0) {
					reader.Close();

					command = new MySqlCommand("SELECT code, title FROM unit WHERE coordinator = @staffid AND code IN (@unitcodes)", Connection);
					command.Parameters.AddWithValue("@staffid", staff.ID.ToString());
					command.Parameters.AddWithValue("@unitcodes", String.Join(",", teachingUnitCodes));

					reader = command.ExecuteReader();

					while (reader.Read()) {
						var unit = dataStore.GetUnit(reader.GetString("code"));
						unit.Title = reader.GetString("title");
						unit.Coordinator = staff;

						staff.UnitsTeaching.Add(unit);
					}
				}
			} finally {
				reader?.Close();
				Connection.Close();
			}
		}

		/// <summary>
		/// Fetch a list of all units with their basic information
		/// </summary>
		/// <returns>List of Unit obkects</returns>
		public List<Unit> FetchAllUnits() {
			MySqlDataReader reader = null;
			var units = new List<Unit>();

			try {
				Connection.Open();
				var command = new MySqlCommand("SELECT code, title, coordinator FROM unit ORDER BY code, title", Connection);
				reader = command.ExecuteReader();

				var staffMembers = new Dictionary<int, Staff>();

				while (reader.Read()) {
					// attempt to fetch this unit from the data storage if it has already been retrieved
					var unit = dataStore.GetUnit(reader.GetString("code"));
					unit.Title = reader.GetString("title");
					unit.Coordinator = dataStore.GetStaffMember(reader.GetInt32("coordinator"));
					units.Add(unit);
				}
			} finally {
				reader?.Close();
				Connection?.Close();
			}

			return units;
		}

		/// <summary>
		/// Fetch the complete list of classes associated with a unit
		/// </summary>
		/// <param name="unit">Unit to fetch classes for</param>
		/// <returns>List of UnitClass objects</returns>
		public List<UnitClass> FetchUnitClasses(Unit unit) {
			MySqlDataReader reader = null;
			var classes = new List<UnitClass>();

			try {
				Connection.Open();

				// fetch information from both the `class` and `staff` table, as very basic staff member information
				// such as title, given name and surname are required for display
				var command = new MySqlCommand(
					"SELECT c.campus, c.day, c.start, c.end, c.type, c.room, c.staff, " +
					"s.title, s.given_name, s.family_name FROM class AS c " +
					"JOIN staff AS s ON c.staff = s.id WHERE c.unit_code = @unitcode;",
					Connection
				);
				command.Parameters.AddWithValue("@unitcode", unit.Code);
				reader = command.ExecuteReader();

				var staffMembers = new Dictionary<int, Staff>();

				while (reader.Read()) {
					// attempt to read a staff object from the data storage, and fill in the necessary fields
					var staff = dataStore.GetStaffMember(reader.GetInt32("staff"));
					staff.Title = reader.GetString("title");
					staff.GivenName = reader.GetString("given_name");
					staff.FamilyName = reader.GetString("family_name");

					var unitClass = new UnitClass {
						Unit = unit,
						Campus = ParseEnum<Campus>(reader.GetString("campus")),
						Day = ParseEnum<DayOfWeek>(reader.GetString("day")),
						Start = reader.GetTimeSpan("start"),
						End = reader.GetTimeSpan("end"),
						Type = ParseEnum<UnitClassType>(reader.GetString("type")),
						Room = reader.GetString("room"),
						Staff = staff,
					};

					classes.Add(unitClass);
				}
			} finally {
				reader?.Close();
				Connection?.Close();
			}

			return classes;
		}
	}
}
