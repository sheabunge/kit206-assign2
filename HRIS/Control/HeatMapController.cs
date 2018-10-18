﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Media;
using HRIS.Database;
using HRIS.Teaching;
using HRIS.View;

namespace HRIS.Control {
	/// <summary>
	/// Controller responsible for the Hash Maps section of the application
	/// </summary>
	public class HeatMapController {
		/// <summary>
		/// Campus location currently being used to filter the heat amp
		/// </summary>
		public Campus CurrentCampusFilter { get; set; } = Campus.Any;

		/// <summary>
		/// Rows of data for the class times heat map
		/// </summary>
		private ObservableCollection<ColorGridRow> ClassRows { get; } = new ObservableCollection<ColorGridRow>();

		/// <summary>
		/// Rows of data for the consultation times heat map
		/// </summary>
		private ObservableCollection<ColorGridRow> ConsultRows { get; } = new ObservableCollection<ColorGridRow>();

		/// <summary>
		/// Method to retrieve the rows of data for the class times heat map
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<ColorGridRow> GetClassRows() => ClassRows;

		/// <summary>
		/// Method to retrieve the rows of data for the consultation times heat map
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<ColorGridRow> GetConsultRows() => ConsultRows;

		/// <summary>
		/// Raw data on class times
		/// </summary>
		private List<Tuple<Event, Campus>> ClassData { get; set; }

		/// <summary>
		/// Raw data on consultation times
		/// </summary>
		private List<Tuple<Event, Campus>> ConsultationData { get; set; }

		/// <summary>
		/// First hour in the day to display
		/// </summary>
		private const int FirstHour = 9;

		/// <summary>
		/// Final hour in the day to display
		/// </summary>
		private const int LastHour = 16;

		/// <summary>
		/// Number of hours in the day to display
		/// </summary>
		private const int HourRange = LastHour - FirstHour + 1;

		/// <summary>
		/// First day of the week to display
		/// </summary>
		private const int FirstDay = 1;

		/// <summary>
		/// Last day of the week to display
		/// </summary>
		private const int LastDay = 5;

		/// <summary>
		/// Number of days of the week to display
		/// </summary>
		private const int DayRange = LastDay - FirstDay + 1;

		/// <summary>
		/// Color to display when 1 class is on
		/// </summary>
		private readonly Color ClassLow = Color.FromRgb(32, 128, 32);

		/// <summary>
		/// Color to display when ClassThreshold classes are on
		/// </summary>
		private readonly Color ClassHigh = Color.FromRgb(64, 255, 64);

		/// <summary>
		/// Color to display when 1 staff member is consulting
		/// </summary>
		private readonly Color ConsultLow = Color.FromRgb(32, 128, 32);

		/// <summary>
		/// Color to display when more than 1 staff member is consulting
		/// </summary>
		private readonly Color ConsultHigh = Color.FromRgb(255, 0, 0);

		/// <summary>
		/// heat where heat map stops fading and displays everything the same
		/// </summary>
		private const int ClassThreshold = 5;

		/// <summary>
		/// Class constructor
		/// </summary>
		public HeatMapController() {
			var db = new DatabaseAdapter();
			ClassData = db.FetchAllClasses();
			ConsultationData = db.FetchAllConsultations();
			UpdateRows();
		}

		/// <summary>
		/// Generate rows for display in the heat map
		/// </summary>
		/// <param name="frequencies"></param>
		/// <returns></returns>
		private static IEnumerable<ColorGridRow> GenRows(EventFrequencyTable frequencies, Color full, Color nearEmpty, int threshold) {
			var result = new ColorGridRow[HourRange];

			for (var hour = FirstHour; hour <= LastHour; hour++) {
				var values = new string[DayRange];
				var colors = new Color[DayRange];

				for (var day = FirstDay; day <= LastDay; day++) {
					var freq = frequencies[(DayOfWeek) day, hour];

					if (freq == 0) {
						continue;
					}

					values[day - FirstDay] = freq.ToString();
					var color = full - nearEmpty;
					var weight = (Single) (freq - 1) / (Single) threshold;
					if (weight <= 1.0) {
						color *= weight;
						color += nearEmpty;
					} else {
						color = full;
					}
					color.A = 255;
					colors[day - FirstDay] = color;
				}

				var time = DateTime.Today + new TimeSpan(hour, 0, 0);
				result[hour - FirstHour] = new ColorGridRow(time.ToString("hh:mm tt"), values, colors);
			}

			return result;
		}

		/// <summary>
		/// Update the rows of a heat map to match the current filters
		/// </summary>
		/// <param name="events"></param>
		/// <param name="dest"></param>
		private void UpdateRowsOf(IEnumerable<Tuple<Event, Campus>> events, ICollection<ColorGridRow> dest, Color full, Color nearEmpty, int threshold) {

			var selected =
				from Tuple<Event, Campus> campusEvent in events
				where CurrentCampusFilter == Campus.Any || CurrentCampusFilter == campusEvent.Item2
				select campusEvent.Item1;

			var frequencies = new EventFrequencyTable(selected);
			GenRows(frequencies, full, nearEmpty, threshold).ToList().ForEach(dest.Add);
		}

		/// <summary>
		/// Update the rows of all heat maps to match the current filters
		/// </summary>
		public void UpdateRows() {
			UpdateRowsOf(ClassData, ClassRows, ClassLow, ClassHigh, ClassThreshold);
			UpdateRowsOf(ConsultationData, ConsultRows, ConsultLow, ConsultHigh, 2);
		}
	}
}
