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
		ObservableCollection<ColorGridRow> ClassRows { get; set; }
		ObservableCollection<ColorGridRow> ConsultRows { get; set; }

		public ObservableCollection<ColorGridRow> GetClassRows() => ClassRows;
		public ObservableCollection<ColorGridRow> GetConsultRows() => ConsultRows;

		public HeatMapController() {
			ClassRows = new ObservableCollection<ColorGridRow>();
			ConsultRows = new ObservableCollection<ColorGridRow>();
			CurrentCampusFilter = Campus.Any;
			UpdateRows();
		}
		
		public void UpdateRows() {
			string[] values = { CurrentCampusFilter.ToString(), "", "", "", "" };

			CellColor[] colours = { CellColor.Yellow, CellColor.Yellow, CellColor.Orange, CellColor.Red, CellColor.Orange };

			var rows = new List<ColorGridRow> {
				new ColorGridRow("09:00", values, colours)
			};
			ClassRows.Clear();
			rows.ForEach(ClassRows.Add);
			ConsultRows.Clear();
			rows.ForEach(ConsultRows.Add);
		}
	}
}
