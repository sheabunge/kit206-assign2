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

			CellColor[] colours = {CellColor.Yellow, CellColor.Yellow, CellColor.Orange, CellColor.Red, CellColor.Orange};

			var rows = new List<ColorGridRow> {
				new ColorGridRow("09:00", values, colours)
			};

			return new ObservableCollection<ColorGridRow>(rows);
		}

		public void UnitMap(Staff staff) {
			CurrentlySelected = staff;

			var rows = new List<ColorGridRow>();

			if (staff.Consultations == null) {
				_db.FetchStaffTeaching(CurrentlySelected);
			}
			foreach (var detail in staff.Consultations) {
				var dayNum = 0;
				while (dayNum < 6) {
					if (staff.Consultations.Day == staff.Classes.Day == 1) {
						if (staff.Consultations.Start == staff.Classes.Start) {
							rows.Add(new ColorGridRow(staff.Consultations.Start, "2", "CellColor.Red"));
						} else if (!(rows.Contains(staff.Consultations.Start, "1", "CellColor.Green") || rows.Contains(staff.Consultations.Start, "2", "CellColor.Red"))) {
							rows.Add(new ColorGridRow(staff.Consultations.Start, "2", "CellColor.Green"));
						} else {
							rows.Add(new ColorGridRow(staff.Consultations.Start, "2", "CellColor.White"));
						}
					}
				}
			}
		}
	}
}
