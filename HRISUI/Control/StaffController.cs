using HRIS.Teaching;

namespace HRIS.Control {
	public class StaffController {
		public string CurrentNameFilter { get; private set; }

		public string CurrentCategoryFilter { get; private set; }

		public void LoadStaff() { }

		public void FilterBy(Category category) { }

		public void FilterByName(string name) { }

		public void ShowStaffDetails(Staff staff) { }
	}
}
