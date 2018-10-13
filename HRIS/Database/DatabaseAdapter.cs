using System;
using System.Collections.Generic;
using HRIS.Teaching;
using MySql.Data.MySqlClient;

namespace HRIS.Database {
	public class DatabaseAdapter {
		private const string DB = "kit206";
		private const string User = "kit206";
		private const string Pass = "kit206";
		private const string Server = "alacritas.cis.utas.edu.au";

		private MySqlConnection Connection;

		public DatabaseAdapter() {
			var details = $"Database={DB};Data Source={Server};User Id={User};Password={Pass};SslMode=none;";
			Connection = new MySqlConnection(details);
		}

		private static T ParseEnum<T>(string value) {
			return (T) Enum.Parse(typeof(T), value);
		}

		public List<Staff> FetchBasicStaffDetails() {
			MySqlDataReader reader = null;
			var staff = new List<Staff>();

			try {
				Connection.Open();
				var command = new MySqlCommand("SELECT id, given_name, family_name, title, campus, category FROM staff", Connection);
				reader = command.ExecuteReader();

				while (reader.Read()) {
					var staffMember = new Staff {
						ID = reader.GetInt32("id"),
						GivenName = reader.GetString("given_name"),
						FamilyName = reader.GetString("family_name"),
						Title = reader.GetString("title"),
						Campus = ParseEnum<Campus>(reader.GetString("campus")),
						Category = ParseEnum<Category>(reader.GetString("category")),
					};

					staff.Add(staffMember);
				}
			} finally {
				reader?.Close();
				Connection?.Close();
			}

			return staff;
		}

		public void CompleteStaffDetails(Staff staff) {
			MySqlDataReader reader = null;

			try {
				Connection.Open();

				var command = new MySqlCommand("SELECT * FROM staff WHERE id = @staffid", Connection);
				command.Parameters.AddWithValue("@staffid", staff.ID.ToString());
				reader = command.ExecuteReader();

				if (reader.Read()) {
					staff.ID = reader.GetInt32("id");
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

		private UnitClass ReadUnitClass(MySqlDataReader reader) {
			return new UnitClass {
				UnitCode = reader.GetString("unit_code"),
				Campus = ParseEnum<Campus>(reader.GetString("campus")),
				Day = ParseEnum<DayOfWeek>(reader.GetString("day")),
				Start = reader.GetTimeSpan("start"),
				End = reader.GetTimeSpan("end"),
				Type = ParseEnum<UnitClassType>(reader.GetString("type")),
				Room = reader.GetString("room"),
				StaffID = reader.GetInt32("staff"),
			};
		}

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
				command = new MySqlCommand("SELECT * FROM class WHERE staff = @staffid", Connection);
				command.Parameters.AddWithValue("@staffid", staff.ID.ToString());
				reader = command.ExecuteReader();

				staff.Classes = new List<UnitClass>();
				staff.UnitsTeaching = new List<Unit>();

				while (reader.Read()) {
					var unitClass = ReadUnitClass(reader);
					staff.Classes.Add(unitClass);
					teachingUnitCodes.Add(unitClass.UnitCode);
				}

				reader.Close();

				// next, fetch the units that are coordinated by this staff member
				command = new MySqlCommand("SELECT code, title FROM unit WHERE coordinator = @staffid", Connection);
				command.Parameters.AddWithValue("@staffid", staff.ID.ToString());
				reader = command.ExecuteReader();

				staff.UnitsCoordinating = new List<Unit>();

				while (reader.Read()) {
					var unit = new Unit {
						Code = reader.GetString("code"),
						Title = reader.GetString("title"),
						CoordinatorID = staff.ID,
					};

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
						staff.UnitsTeaching.Add(new Unit {
							Code = reader.GetString("code"),
							Title = reader.GetString("title"),
							CoordinatorID = staff.ID,
						});
					}
				}
			} finally {
				reader?.Close();
				Connection.Close();
			}
		}

		public List<Unit> FetchAllUnits() {
			MySqlDataReader reader = null;
			var units = new List<Unit>();

			try {
				Connection.Open();
				var command = new MySqlCommand("SELECT code, title, coordinator FROM unit ORDER BY code, title", Connection);
				reader = command.ExecuteReader();

				while (reader.Read()) {
					units.Add(new Unit {
						Code = reader.GetString("code"),
						Title = reader.GetString("title"),
						CoordinatorID = reader.GetInt32("coordinator"),
					});
				}
			} finally {
				reader?.Close();
				Connection?.Close();
			}

			return units;
		}

		public List<UnitClass> FetchUnitClasses(Unit unit) {
			MySqlDataReader reader = null;
			var classes = new List<UnitClass>();

			try {
				Connection.Open();
				var command = new MySqlCommand("SELECT * FROM class WHERE unit_code = @unitcode", Connection);
				command.Parameters.AddWithValue("@unitcode", unit.Code);
				reader = command.ExecuteReader();

				while (reader.Read()) {
					classes.Add(ReadUnitClass(reader));
				}
			} finally {
				reader?.Close();
				Connection?.Close();
			}

			return classes;
		}
	}
}
