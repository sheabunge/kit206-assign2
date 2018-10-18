using System.Collections.Generic;
using System.Collections.ObjectModel;
using HRIS.Teaching;
using HRIS.View;

namespace HRIS.Control {
	/// <summary>
	/// Controller responsible for the Hash Maps section of the application
	/// </summary>
	public class HeatMapController {
		public Campus CurrentCampusFilter { get; set; }

		public ObservableCollection<ColorGridRow> GetClassRows() {
			string[] values = { "1", "1", "2", "5", "3" };

			CellColor[] colours = { CellColor.Yellow, CellColor.Yellow, CellColor.Orange, CellColor.Red, CellColor.Orange };

			var rows = new List<ColorGridRow> {
				new ColorGridRow("09:00", values, colours)
			};

			return new ObservableCollection<ColorGridRow>(rows);
		}
		public ObservableCollection<ColorGridRow> GetConsultRows() {
			return GetClassRows();
		}
		public string[] GetCampusValues() {
			string[] values = { "Hobart", "Launceston" };  // where is this meant to come from?
			return values;
		}

		public void ApplyFilters() {
		}
	}
}
