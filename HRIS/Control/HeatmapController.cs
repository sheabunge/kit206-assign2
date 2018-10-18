using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HRIS.Database;
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

		private List<Tuple<Event, Campus>> RawClass { get; set; }
		private List<Tuple<Event, Campus>> RawConsult { get; set; }

		public HeatMapController() {
			ClassRows = new ObservableCollection<ColorGridRow>();
			ConsultRows = new ObservableCollection<ColorGridRow>();
			CurrentCampusFilter = Campus.Any;

			var _db = new DatabaseAdapter();
			RawClass = _db.FetchAllClasses();
			RawConsult = _db.FetchAllConsultations();
			UpdateRows();
		}

		const int FIRST_HOUR = 9;
		const int LAST_HOUR = 16;
		const int HOUR_RANGE = LAST_HOUR - FIRST_HOUR + 1;

		const int FIRST_DAY = 1;
		const int LAST_DAY = 5;
		const int DAY_RANGE = LAST_DAY - FIRST_DAY + 1;

		ColorGridRow[] GenRows(EventTable events) {
			var result = new ColorGridRow[HOUR_RANGE];

			for (int hour = FIRST_HOUR; hour <= LAST_HOUR; hour++) {
				var values = new string[5];
				var colors = new CellColor[5];
				for (int day = FIRST_DAY; day <= LAST_DAY; day++) {
					var freq = events.FrequencyAt(hour, (DayOfWeek) day);
					if (freq != 0) {
						values[day - FIRST_DAY] = freq.ToString();
						colors[day - FIRST_DAY] = CellColor.Yellow;
					}
				}
				result[hour - FIRST_HOUR] = new ColorGridRow($"{hour}:00", values, colors);
			}

			return result;
		}

		private void UpdateRowsOf(List<Tuple<Event, Campus>> rawEvents, ObservableCollection<ColorGridRow> dest) {
			var selected =
				from Tuple<Event, Campus> campusEvent in rawEvents
				where CurrentCampusFilter == Campus.Any || CurrentCampusFilter == campusEvent.Item2
				select campusEvent.Item1;
			var events = new EventTable(selected);
			var rows = GenRows(events);

			dest.Clear();
			foreach (var row in rows) {
				dest.Add(row);
			}
		}
		
		public void UpdateRows() {
			UpdateRowsOf(RawClass, ClassRows);
			UpdateRowsOf(RawConsult, ConsultRows);
		}
	}
}
