namespace HRIS.Teaching {
	/// <summary>
	/// Represents a teaching unit
	/// </summary>
	public class Unit {
		/// <summary>
		/// Uniquely identifiable unit code
		/// </summary>
		public string Code { get; }

		/// <summary>
		/// Descriptive unit title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Staff member coordinating the unit
		/// </summary>
		public Staff Coordinator { get; set; }

		/// <summary>
		/// Retrieve a string representation of the unit
		/// </summary>
		/// <returns>The unit's full code and title</returns>
		public override string ToString() => $"{Code} {Title}";

		/// <summary>
		/// Class constructor
		/// Ensures every Unit object at least has a Code property
		/// </summary>
		/// <param name="unitCode">Unique code associated with this unit</param>
		public Unit(string unitCode) {
			Code = unitCode;
		}

		/// <summary>
		/// Determine whether one unit is identical to another
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj) {
			return obj is Unit other && other.Code == Code;
		}

		/// <summary>
		/// Generate a hash code for this unit
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode() {
			return Code.GetHashCode();
		}
	}
}
