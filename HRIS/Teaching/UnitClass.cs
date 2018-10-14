namespace HRIS.Teaching {
	/// <summary>
	/// Represents the different types of a unit class
	/// </summary>
	public enum UnitClassType {
		Lecture,
		Tutorial,
		Practical,
		Workshop,
	}

	/// <summary>
	/// Represents a teaching class associated with a unit
	/// </summary>
	public class UnitClass : Event {
		/// <summary>
		/// Type of class. See the UnitClassType enum
		/// </summary>
		public UnitClassType Type { get; set; }

<<<<<<< HEAD
		/// <summary>
		/// Unit this class is associated with
		/// </summary>
=======
>>>>>>> Use actual objects to represent staff and unit properties instead of primatives
		public Unit Unit { get; set; }

		/// <summary>
		/// Physical room location where this class takes place
		/// </summary>
		public string Room { get; set; }

		/// <summary>
		/// Campus location where this class takes place
		/// </summary>
		public Campus Campus { get; set; }

<<<<<<< HEAD
		/// <summary>
		/// The staff member that teaches this class
		/// </summary>
=======
>>>>>>> Use actual objects to represent staff and unit properties instead of primatives
		public Staff Staff { get; set; }
	}
}
