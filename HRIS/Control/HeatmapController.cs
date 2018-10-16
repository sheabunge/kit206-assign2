using System.Collections.Generic;
using System.Collections.ObjectModel;
using HRIS.View;

namespace HRIS.Control {
	/// <summary>
	/// Controller responsible for the Hash Maps section of the application
	/// </summary>
	public class HeatMapController {
		/// <summary>
		/// Generate a list of rows for displaying in the heat map grid
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<ColorGridRow> GetRows() {
			string[] values = {"1", "1", "2", "5", "3"};

			CellColor[] colours = {CellColor.Yellow, CellColor.Yellow, CellColor.Orange, CellColor.Red, CellColor.Orange};

			var rows = new List<ColorGridRow> {
				new ColorGridRow("09:00", values, colours)
			};

			return new ObservableCollection<ColorGridRow>(rows);
		}
	}
}
