using System;
using System.Collections.Generic;
using System.Linq;

namespace HRIS.Teaching {
	/// <summary>
	/// Represents a member of the staff
	/// </summary>
	public class Staff {
		/// <summary>
		/// The unique identifier for this staff member
		/// </summary>
		public int ID { get; }

		/// <summary>
		/// The staff member's surname
		/// </summary>
		public string FamilyName { get; set; }

		/// <summary>
		/// The staff member's first name
		/// </summary>
		public string GivenName { get; set; }

		/// <summary>
		/// The staff member's title (e.g. Mr, Ms)
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The campus where the staff member is primarily based
		/// </summary>
		public Campus Campus { get; set; }

		/// <summary>
		/// The physical location on campus where the staff member works
		/// </summary>
		public string Room { get; set; }

		/// <summary>
		/// The contact email for the staff member
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// The contact phone number for the staff member
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// A URL to a photograph of the staff member
		/// </summary>
		public string Photo { get; set; }

		/// <summary>
		/// The department where the staff member works
		/// </summary>
		public Category Category { get; set; }

		/// <summary>
		/// A list of weekly times when the staff member provides student consultation
		/// </summary>
		public List<Event> Consultations { get; set; }

		/// <summary>
		/// A list of classes that are taught by the staff member
		/// </summary>
		public List<UnitClass> Classes { get; set; }

		/// <summary>
		/// A list of units that are taught by the staff member
		/// </summary>
		public List<Unit> UnitsTeaching { get; set; }

		/// <summary>
		/// A list of units that are coordinated by the staff member
		/// </summary>
		public List<Unit> UnitsCoordinating { get; set; }

		/// <summary>
		/// The staff member's full name in standard format
		/// </summary>
		public string FullName => $"{Title} {GivenName} {FamilyName}";

		/// <summary>
		/// The staff member's full name in formal list format
		/// </summary>
		public string FormalName => $"{FamilyName}, {GivenName} ({Title})";

		/// <summary>
		/// Information about the staff member's current avalability
		/// Item1 is an enumerated value
		/// Item2 is a detailed textual description
		/// </summary>
		public Tuple<Availability, string> CurrentAvalability => Availability(DateTime.Now);

		/// <summary>
		/// Generate a string representation of this staff member
		/// </summary>
		/// <returns></returns>
		public override string ToString() => FormalName;

		/// <summary>
		/// Class constructor
		/// Ensures every Staff object at least has an ID property
		/// </summary>
		/// <param name="staffID">Unique identifier</param>
		public Staff(int staffID) {
			ID = staffID;
		}

		/// <summary>
		/// Internally-used method for filtering a list of events to those that occur during a specified date and time
		/// </summary>
		/// <param name="events">List of events to filter</param>
		/// <param name="when">Current date and time</param>
		/// <returns>Filtered list of units</returns>
		private static IEnumerable<Event> CurrentEvents(IEnumerable<Event> events, DateTime when) {
			if (events == null) {
				return events;
			}

			var results = from e in events
				where e.Day == when.DayOfWeek &&
				      e.Start <= when.TimeOfDay && !(when.TimeOfDay >= e.End)
				select e;

			return results;
		}

		/// <summary>
		/// Retrieve information on a staff member's availability during a specified date and time
		/// </summary>
		/// <param name="when">Current date and time</param>
		/// <returns>A tuple with Item1 being an enumerated value, and Item2 being a more detailed description</returns>
		public Tuple<Availability, string> Availability(DateTime when) {
			// find any consultation times currently occurring
			var consultations = CurrentEvents(Consultations, when);

			if (consultations != null && consultations.Any()) {
				return Tuple.Create(Teaching.Availability.Consulting, "Consulting");
			}

			// if none were discovered, look for classes currently occurring
			var classes = CurrentEvents(Classes, when);
			if (classes != null && classes.Any()) {
				// use the extra detail from the current class to build a more detailed description
				var currentClass = (UnitClass) classes.First();
				return Tuple.Create(
					Teaching.Availability.Teaching,
					$"Teaching {currentClass.Unit.Code} in Room {currentClass.Room}"
				);
			}

			// otherwise, the staff member is freely available
			return Tuple.Create(Teaching.Availability.Free, "Free");
		}

		/// <summary>
		/// Determine whether one staff member is identical to another
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj) {
			return obj is Staff other && other.ID == ID;
		}

		/// <summary>
		/// Generate a hash code for this staff member
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode() {
			return ID.GetHashCode();
		}
	}
}
