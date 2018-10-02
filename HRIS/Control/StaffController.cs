using HRIS.Database;
using HRIS.Teaching;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HRIS.Control {
	public class StaffController {
		private readonly List<Staff> _staffList;

		public List<Staff> StaffList => _staffList;

		private readonly ObservableCollection<Staff> _viewableStaff;

		public ObservableCollection<Staff> VisibleStaff => _viewableStaff;

		public StaffController() {
			var db = new DatabaseAdapter();
			_staffList = db.FetchBasicStaffDetails();
			_viewableStaff = new ObservableCollection<Staff>(_staffList);
		}

		public ObservableCollection<Staff> GetViewableList() {
			return VisibleStaff;
		}

		private void SetViewableStaff(IEnumerable<Staff> selected) {
			_viewableStaff.Clear();
			selected.ToList().ForEach(_viewableStaff.Add);
		}

		public void FilterByCategory(Category category) {
			var selected = from Staff staff in _staffList
				where category == Category.Any || category == staff.Category
				select staff;

			SetViewableStaff(selected);
		}

		public void FilterByName(string name) {
			var selected = from Staff staff in _staffList
				where staff.ToString().Contains(name)
				select staff;

			SetViewableStaff(selected);
		}

		public void ShowStaffDetails(Staff staff) { }
	}
}
