﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using HRIS.Database;
using HRIS.Teaching;
using HRIS.View;

namespace HRIS.Control {
	/// <summary>
	/// Controller responsible for the Unit Timetables section of the application
	/// </summary>
	public class UnitController {
		/// <summary>
		/// Instance of the DatabaseAdapter
		/// </summary>
		private readonly DatabaseAdapter _db;

		/// <summary>
		/// Complete list of units in the system
		/// </summary>
		public List<Unit> UnitsList { get; private set; }

		/// <summary>
		/// List of units filtered to match the current search filter
		/// </summary>
		public ObservableCollection<Unit> VisibleUnits { get; private set; }

		/// <summary>
		/// Search text used to filter the unit list
		/// </summary>
		public string SearchText { get; set; }

		/// <summary>
		/// Currently selected unit in the model
		/// </summary>
		public Unit SelectedUnit { get; private set; }

		/// <summary>
		/// Complete list of classes associated with the currently selected Unit model
		/// </summary>
		public List<UnitClass> UnitClasses { get; private set; }

		/// <summary>
		/// List of classes associated with the currently selected Unit model filtered by campus
		/// </summary>
		public ObservableCollection<UnitClass> VisibleUnitClasses { get; private set; }

		/// <summary>
		/// Method for retrieving the currently visible list of units
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<Unit> GetVisibleList() => VisibleUnits;

		/// <summary>
		/// Method for retrieving the currently visible list of unit classes
		/// </summary>
		/// <returns>Collection of visible unit classes</returns>
		public ObservableCollection<UnitClass> GetUnitClasses() => VisibleUnitClasses;


		/// <summary>
		/// Rows of data for the consultation times heat map
		/// </summary>
		public ObservableCollection<ColorGridRow> ClashMap { get; } = new ObservableCollection<ColorGridRow>();

		/// <summary>
		/// Method to retrieve the rows of data for the class times heat map
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<ColorGridRow> GetClashMap() => ClashMap;

		/// <summary>
		/// Class constructor; initialises class properties
		/// </summary>
		public UnitController() {
			_db = new DatabaseAdapter();
			UnitsList = _db.FetchAllUnits();
			VisibleUnits = new ObservableCollection<Unit>(UnitsList);
			SearchText = "";
			VisibleUnitClasses = new ObservableCollection<UnitClass>();
		}

		/// <summary>
		/// Apply the current search text to the list of units
		/// </summary>
		public void ApplyFilter() {
			VisibleUnits.Clear();

			var selected =
				from Unit unit in UnitsList
				where unit.ToString().IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0
				orderby unit.ToString()
				select unit;

			selected.ToList().ForEach(VisibleUnits.Add);
		}

		/// <summary>
		/// Select a unit from the complete list.
		/// Retrieves and initialises the list of classes associated with the unit
		/// </summary>
		/// <param name="unit"></param>
		public void SelectUnit(Unit unit) {
			UnitClasses = _db.FetchUnitClasses(unit).OrderBy(c => c.StartDate).ThenBy(c => c.EndDate).ToList();
			SelectedUnit = unit;
			VisibleUnitClasses.Clear();
			UnitClasses.ForEach(VisibleUnitClasses.Add);
		}

		/// <summary>
		/// Filters the list of unit classes by their campus location
		/// </summary>
		/// <param name="campus"></param>
		public void FilterClassesByCampus(Campus campus) {
			if (UnitClasses == null) {
				return;
			}

			VisibleUnitClasses.Clear();

			if (campus == Campus.Any) {
				// if the filter is set to Any, we can just set all available classes as visible
				UnitClasses.ForEach(VisibleUnitClasses.Add);
			} else {
				// otherwise use Linq to filter the complete list, which is already ordered
				var results = from UnitClass unitClass in UnitClasses
					where unitClass.Campus == campus
					select unitClass;

				results.ToList().ForEach(VisibleUnitClasses.Add);
			}
		}

		/// <summary>
		/// Generate the clash map for the current unit and visible classes
		/// </summary>
		public void GenerateClashMap() {
			const int firstHour = 9;
			const int lastHour = 16;

			ClashMap.Clear();

			if (SelectedUnit == null) {
				return;
			}

			// begin our list of events from the visible unit classes
			var events = new List<Event>(VisibleUnitClasses);

			foreach (var e in VisibleUnitClasses) {
				var staff = e.Staff;

				// fetch the consultation times if necessary
				if (staff.Consultations == null) {
					_db.FetchStaffTeaching(e.Staff);
				}

				staff.Consultations.ForEach(events.Add);
			}

			var frequencies = new EventFrequencyTable(events);

			for (var hour = firstHour; hour <= lastHour; hour++) {
				var row = new ColorGridRow {
					Time = DateTime.Today.AddHours(hour),
				};

				for (var day = 0; day <= 5; day++) {
					var freq = frequencies[day + 1, hour];

					if (freq < 1) {
						continue;
					}

					if (freq == 1) {
						row.Colors[day] = UnitTimetableView.ActivityColor;
					} else {
						row.Colors[day] = UnitTimetableView.ClashColor;
						row.Values[day] = "Clash";
					}
				}

				ClashMap.Add(row);
			}
		}
	}
}
