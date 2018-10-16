using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HRIS.View {
	/// <summary>
	/// Represents the colour of a grid cell
	/// </summary>
	public enum CellColor {
		White,
		Red,
		Orange,
		Yellow,
	}

	/// <summary>
	/// Represents a single row of a coloured grid
	/// </summary>
	public class ColorGridRow {
		/// <summary>
		/// The time value for this row, displayed on the y-axis
		/// </summary>
		public string Time { get; private set; }

		/// <summary>
		/// List of values to display in the row cells
		/// </summary>
		public string[] Values { get; private set; }

		/// <summary>
		/// List of colours to use for the cell backgrounds
		/// </summary>
		public Brush[] Colors { get; private set; }

		/// <summary>
		/// Class constructor
		/// </summary>
		/// <param name="time"></param>
		/// <param name="values"></param>
		/// <param name="colors"></param>
		public ColorGridRow(string time, string[] values, IReadOnlyList<CellColor> colors) {
			Time = time;
			Values = values;
			Colors = new Brush[colors.Count];

			for (var i = 0; i < colors.Count; i++) {
				Brush next = null;
				switch (colors[i]) {
					case CellColor.White:
						next = Brushes.White;
						break;
					case CellColor.Red:
						next = Brushes.Red;
						break;
					case CellColor.Orange:
						next = Brushes.Orange;
						break;
					case CellColor.Yellow:
						next = Brushes.Yellow;
						break;
				}

				Colors[i] = next;
			}
		}
	}
}
