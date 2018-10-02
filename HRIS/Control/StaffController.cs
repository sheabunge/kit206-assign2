using HRIS.Database;
using HRIS.Teaching;
using System.Collections.Generic;

namespace HRIS.Control {
	public class StaffController {

        private List<Staff> StaffList;
		public string CurrentNameFilter { get; private set; }

		public string CurrentCategoryFilter { get; private set; }

		private void LoadStaff() {
            var db = new DatabaseAdapter();
            StaffList = db.FetchBasicStaffDetails();
        }

        public List<Staff> GetViewableList() {
            if (StaffList == null) {
                LoadStaff();
            }

            return StaffList;
        }

        public void FilterBy(Category category) { }

		public void FilterByName(string name) { }

		public void ShowStaffDetails(Staff staff) { }
	}
}
