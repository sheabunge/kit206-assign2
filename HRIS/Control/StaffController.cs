using HRIS.Database;
using HRIS.Teaching;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HRIS.Control {
	public class StaffController {
		public List<Staff> CompleteList { get; private set; }

		public ObservableCollection<Staff> VisibleList { get; private set; }

		public string CurrentNameFilter { get; set; }

		public Category CurrentCategoryFilter { get; set; }

		private readonly DatabaseAdapter _db;

		public StaffController() {
			_db = new DatabaseAdapter();
			CompleteList = _db.FetchBasicStaffDetails();
			VisibleList = new ObservableCollection<Staff>(CompleteList);
			CurrentNameFilter = "";
			CurrentCategoryFilter = Category.Any;
		}

		public void CompleteStaffDetails(Staff staff) {
			_db.CompleteStaffDetails(staff);
		}

		public ObservableCollection<Staff> GetVisibleList() {
			return VisibleList;
		}

		public void ApplyFilters() {
			VisibleList.Clear();

			var selected =
				from Staff staff in CompleteList
				where
					(CurrentCategoryFilter == Category.Any || CurrentCategoryFilter == staff.Category) &&
					(CurrentNameFilter == "" || staff.ToString().IndexOf(CurrentNameFilter, StringComparison.OrdinalIgnoreCase) >= 0)
				select staff;

			selected.ToList().ForEach(VisibleList.Add);
		}
	}
}
