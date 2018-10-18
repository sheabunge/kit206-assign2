using System;
using System.Collections.Generic;

namespace HRIS.Teaching {
	public class EventTable {

		private int[,] Frequency { get; set; }

		const int HOURS = 24;

		const int DAYS = 7;

		public int FrequencyAt(int hour, DayOfWeek dayofweek) => Frequency[(int) dayofweek, hour];

		public EventTable(IEnumerable<Event> events) {
			Frequency = new int[DAYS, HOURS];

			foreach (var ev in events) {
				for (var hour = Math.Floor(ev.Start.TotalHours); hour < Math.Ceiling(ev.End.TotalHours); hour++) {
					Frequency[(int) ev.Day, (int) hour]++;
				}
			}
		}
	}
}
