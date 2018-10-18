using System;
using System.Collections.Generic;

namespace HRIS.Teaching {
	/// <summary>
	/// Class for calculating the frequency of events occuring during each hour of each day of the week
	/// </summary>
	public class EventFrequencyTable {
		/// <summary>
		/// Number of days during a week
		/// </summary>
		private const int DaysCount = 7;

		/// <summary>
		/// Number of hours during a day
		/// </summary>
		private const int HoursCount = 24;

		/// <summary>
		/// Multidimensional array for storing frequencies
		/// </summary>
		private int[,] Frequency { get; } = new int[DaysCount, HoursCount];

		/// <summary>
		/// Retrieve the number of events occuring during a given hour and day of the week
		/// </summary>
		/// <param name="day"></param>
		/// <param name="hour"></param>
		/// <returns></returns>
		public int this[DayOfWeek day, int hour] => Frequency[(int) day, hour];

		/// <summary>
		/// Retrieve the number of events occuring during a given hour and day of the week
		/// </summary>
		/// <param name="day"></param>
		/// <param name="hour"></param>
		/// <returns></returns>
		public int this[int day, int hour] => Frequency[day, hour];

		/// <summary>
		/// Construct the frequency table
		/// </summary>
		/// <param name="events"></param>
		public EventFrequencyTable(IEnumerable<Event> events) {
			// Loop through every event
			foreach (var ev in events) {
				// Increment the frequency for each hour when the event takes place
				for (var hour = Math.Floor(ev.Start.TotalHours); hour < Math.Ceiling(ev.End.TotalHours); hour++) {
					Frequency[(int) ev.Day, (int) hour]++;
				}
			}
		}
	}
}
