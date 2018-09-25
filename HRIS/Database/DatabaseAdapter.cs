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
				var command = new MySqlCommand("SELECT * FROM staff");
				reader = command.ExecuteReader();

				while (reader.Read()) {
					var staffMember = new Staff {
						ID = reader.GetInt32("id"),
						GivenName = reader.GetString("given_name"),
						FamilyName = reader.GetString("family_name"),
						Title = reader.GetString("title"),
						Campus = ParseEnum<Campus>(reader.GetString("campus")),
						Room = reader.GetString("room"),
						Email = reader.GetString("email"),
						Photo = reader.GetString("photo"),
						Category = ParseEnum<Category>(reader.GetString("category")),
					};

					staff.Add(staffMember);
				}
			}
			finally {
				reader?.Close();
				Connection?.Close();
			}

			return staff;
		}

		public Staff CompleteStaffDetails(Staff staff) {
			MySqlDataReader reader = null;

			try {
				Connection.Open();

				var command = new MySqlCommand("SELECT * FROM staff WHERE id = @staffid");
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
					staff.Photo = reader.GetString("photo");
					staff.Category = ParseEnum<Category>(reader.GetString("category"));
				}

				reader.Close();

				command = new MySqlCommand("SELECT * FROM consultation WHERE staff_id = @staffid");
				command.Parameters.AddWithValue("@staffid", staff.ID.ToString());
				reader = command.ExecuteReader();

				while (reader.Read()) {
					staff.Consultations.Add(new Event {
						Day = ParseEnum<DayOfWeek>(reader.GetString("day")),
						Start = reader.GetTimeSpan("start"),
						End = reader.GetTimeSpan("end"),
					});
				}
			}
			finally {
				reader?.Close();
				Connection?.Close();
			}

			return staff;
		}

		public IEnumerable<Unit> FetchUnits() {
			MySqlDataReader reader = null;
			var units = new List<Unit>();

			try {
				Connection.Open();
				var command = new MySqlCommand("SELECT * FROM unit", Connection);
				reader = command.ExecuteReader();

				while (reader.Read()) {
					units.Add(new Unit {
						Code = reader.GetString("code"),
						Title = reader.GetString("title"),
						Coordinator = new Staff {ID = reader.GetInt32("coordinator")},
					});
				}
			}
			finally {
				reader?.Close();
				Connection?.Close();
			}

			return units;
		}

		public IEnumerable<UnitClass> FetchClasses(Unit unit) {
			MySqlDataReader reader = null;
			var classes = new List<UnitClass>();

			try {
				Connection.Open();
				var command = new MySqlCommand("SELECT * FROM class WHERE unit_code = @unitcode", Connection);
				command.Parameters.AddWithValue("@unitcode", unit.Code);
				reader = command.ExecuteReader();

				while (reader.Read()) {
					classes.Add(new UnitClass {
						Campus = ParseEnum<Campus>(reader.GetString("campus")),
						Day = ParseEnum<DayOfWeek>(reader.GetString("day")),
						Start = reader.GetTimeSpan("start"),
						End = reader.GetTimeSpan("end"),
						Type = ParseEnum<UnitClassType>(reader.GetString("type")),
						Room = reader.GetString("room"),
						Staff = new Staff {ID = reader.GetInt32("staff")},
					});
				}
			}
			finally {
				reader?.Close();
				Connection?.Close();
			}

			return classes;
		}
	}
}
