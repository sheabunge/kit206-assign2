﻿namespace HRIS.Teaching {
	/// <summary>
	/// Represents a teaching unit
	/// </summary>
	public class Unit {
		/// <summary>
		/// Uniquely identifiable unit code
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Descriptive unit title
		/// </summary>
		public string Title { get; set; }

<<<<<<< HEAD
		/// <summary>
		/// Staff member coordinating the unit
		/// </summary>
=======
>>>>>>> Use actual objects to represent staff and unit properties instead of primatives
		public Staff Coordinator { get; set; }

		/// <summary>
		/// Retrieve a string representation of the unit
		/// </summary>
		/// <returns>The unit's full code and title</returns>
		public override string ToString() {
			return $"{Code} {Title}";
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
