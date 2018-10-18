using System;
using System.Windows.Media;

namespace HRIS.View {
	/// <summary>
	/// Represents a single row of a coloured grid
	/// </summary>
	public class ColorGridRow {
		/// <summary>
		/// The time value for this row, displayed on the y-axis
		/// </summary>
		public DateTime Time { get; set; }

		/// <summary>
		/// List of values to display in the row cells
		/// </summary>
		public object[] Values { get; } = new object[5];

		/// <summary>
		/// List of colours to use for the cell backgrounds
		/// </summary>
		public Brush[] Colors { get; } = new Brush[5];
	}
}
