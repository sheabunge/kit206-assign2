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
	public enum CellColour {
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
		public Brush[] Colours { get; private set; }

		/// <summary>
		/// Class constructor
		/// </summary>
		/// <param name="time"></param>
		/// <param name="values"></param>
		/// <param name="colours"></param>
		public ColorGridRow(string time, string[] values, IReadOnlyList<CellColour> colours) {
			Time = time;
			Values = values;
			Colours = new Brush[colours.Count];

			for (var i = 0; i < colours.Count; i++) {
				Brush next = null;
				switch (colours[i]) {
					case CellColour.White:
						next = Brushes.White;
						break;
					case CellColour.Red:
						next = Brushes.Red;
						break;
					case CellColour.Orange:
						next = Brushes.Orange;
						break;
					case CellColour.Yellow:
						next = Brushes.Yellow;
						break;
				}

				Colours[i] = next;
			}
		}
	}
}
