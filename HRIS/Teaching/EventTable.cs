using System;
using System.Collections.Generic;

namespace HRIS.Teaching {
	public class EventTable {

		private int[][] frequency { get; set; }

		public int FrequencyAt(int hour, DayOfWeek dayofweek) => frequency[(int) dayofweek][hour];

		const int HOURS = 24;

		public EventTable(IEnumerable<Event> events) {
			frequency = new int[HOURS][];
			for (int i = 0; i < HOURS; i++) {
				frequency[i] = new int[7];
			}

			foreach (var ev in events) {
				for (var hour = Math.Floor(ev.Start.TotalHours); hour < Math.Ceiling(ev.End.TotalHours); hour++) {
					frequency[(int) ev.Day][(int) hour]++;
				}
			}
		}
	}
}
