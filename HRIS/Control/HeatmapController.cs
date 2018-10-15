using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

			CellColour[] colours = {CellColour.Yellow, CellColour.Yellow, CellColour.Orange, CellColour.Red, CellColour.Orange};

			var rows = new List<ColorGridRow> {
				new ColorGridRow("09:00", values, colours)
			};

			return new ObservableCollection<ColorGridRow>(rows);
		}
	}
}
