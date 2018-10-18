using HRIS.Database;
using HRIS.Teaching;
using HRIS.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace HRIS.Control {
	/// <summary>
	/// Controller responsible for the Staff Details section of the application
	/// </summary>
	public class StaffController {
		/// <summary>
		/// Instance of the DatabaseAdapter
		/// </summary>
		private readonly DatabaseAdapter _db;

		/// <summary>
		/// Complete list of staff members in the system
		/// </summary>
		public List<Staff> CompleteList { get; private set; }

		/// <summary>
		/// List of units filtered to match the current visibility controls
		/// </summary>
		public ObservableCollection<Staff> VisibleList { get; private set; }

		/// <summary>
		/// Value currently being used to restrict the visible units to those with a specified name
		/// </summary>
		public string CurrentNameFilter { get; set; }

		/// <summary>
		/// Value currently being used to restrict the visible units to those in a specified category
		/// </summary>
		public Category CurrentCategoryFilter { get; set; }

		/// <summary>
		/// Currently selected individual staff member
		/// </summary>
		public Staff CurrentlySelected { get; private set; }

		public ObservableCollection<ColorGridRow> AvalabilityTable { get; } = new ObservableCollection<ColorGridRow>();

		public ObservableCollection<ColorGridRow> GetAvalabilityTable() => AvalabilityTable;

		/// <summary>
		/// Method for retrieving the visible list of staff members
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<Staff> GetVisibleList() => VisibleList;

		/// <summary>
		/// Class constructor; initialises class properties
		/// </summary>
		public StaffController() {
			_db = new DatabaseAdapter();
			CompleteList = _db.FetchBasicStaffDetails();
			VisibleList = new ObservableCollection<Staff>(CompleteList);

			CurrentNameFilter = "";
			CurrentCategoryFilter = Category.Any;
		}

		/// <summary>
		/// Select a specific item from the complete list of staff members.
		/// Loads additional basic details and teaching associations from the database
		/// </summary>
		/// <param name="staff"></param>
		public void SelectItem(Staff staff) {
			CurrentlySelected = staff;

			// if the staff email is not set, then the complete details need to be filled in
			if (staff.Email == null) {
				_db.CompleteStaffDetails(CurrentlySelected);
			}

			// if the staff consultations list is not set, then the associations need to be retrieved
			if (staff.Consultations == null) {
				_db.FetchStaffTeaching(CurrentlySelected);
			}
		}

		/// <summary>
		/// Helper method for checking whether a string contains a substring, ignoring case
		/// </summary>
		/// <param name="haystack">Full string to search within</param>
		/// <param name="needle">Substring to search for</param>
		/// <returns>Whether the substring was found in the full string</returns>
		private static bool StringContains(string haystack, string needle) {
			return haystack.IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		/// <summary>
		/// Apply the current values of the filter properties to the complete staff list
		/// </summary>
		public void ApplyFilters() {
			VisibleList.Clear();

			var selected =
				from Staff staff in CompleteList
				where
					(CurrentCategoryFilter == Category.Any || CurrentCategoryFilter == staff.Category) &&
					(StringContains(staff.GivenName, CurrentNameFilter) || StringContains(staff.FamilyName, CurrentNameFilter))
				orderby staff.FamilyName, staff.GivenName
				select staff;

			selected.ToList().ForEach(VisibleList.Add);
		}

		/// <summary>
		/// Generate the data for displaying a timetable of staff avalability
		/// </summary>
		public void GenerateAvalabilityTable() {
			const int firstHour = 9;
			const int lastHour = 16;
			const int firstDay = (int) DayOfWeek.Monday;
			const int lastDay = (int) DayOfWeek.Friday;

			var teachingColor = Colors.CornflowerBlue;
			var consultingColor = Colors.IndianRed;

			AvalabilityTable.Clear();

			if (CurrentlySelected == null) {
				return;
			}

			var baseDateTime = DateTime.Today.AddDays(-(int) DateTime.Today.DayOfWeek + firstDay);

			for (var hour = firstHour; hour <= lastHour; hour++) {
				var time = baseDateTime.AddHours(hour);

				var row = new ColorGridRow {
					Time = time
				};

				for (var day = 0; day < 5; day++) {
					Console.WriteLine(time);

					var color = Colors.White;
					var avalability = CurrentlySelected.Availability(time).Item1;

					if (Availability.Teaching == avalability) {
						color = teachingColor;
					} else if (Availability.Consulting == avalability) {
						color = consultingColor;
					}

					Console.WriteLine($"{time.DayOfWeek} is {color}");
					row.Colors[day] = new SolidColorBrush(color);
					time = time.AddDays(1);
				}

				AvalabilityTable.Add(row);
			}
		}
	}
}
