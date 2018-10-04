using HRIS.Database;
using HRIS.Teaching;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HRIS.Control {
	public class StaffController {
		private readonly List<Staff> _staffList;

		public List<Staff> StaffList => _staffList;

		private readonly ObservableCollection<Staff> _viewableStaff;

		public ObservableCollection<Staff> VisibleStaff => _viewableStaff;

		public string CurrentNameFilter { get; set; }

		public Category CurrentCategoryFilter { get; set; }

		public StaffController() {
			var db = new DatabaseAdapter();
			_staffList = db.FetchBasicStaffDetails();
			_viewableStaff = new ObservableCollection<Staff>(_staffList);
			CurrentNameFilter = "";
			CurrentCategoryFilter = Category.Any;
		}

		public ObservableCollection<Staff> GetViewableList() {
			return VisibleStaff;
		}

		public void ApplyFilters() {
			_viewableStaff.Clear();

			var selected =
				from Staff staff in _staffList
				where
					(CurrentCategoryFilter == Category.Any || CurrentCategoryFilter == staff.Category) &&
					(CurrentNameFilter == "" || staff.ToString().IndexOf(CurrentNameFilter, StringComparison.OrdinalIgnoreCase) >= 0)
				select staff;

			selected.ToList().ForEach(_viewableStaff.Add);
		}
	}
}
