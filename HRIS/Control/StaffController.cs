using HRIS.Database;
using HRIS.Teaching;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HRIS.Control {
	public class StaffController {

        private List<Staff> staffmembers;

        public List<Staff> StaffList { get => staffmembers; private set { } }

        private ObservableCollection<Staff> viewableStaff;
        public ObservableCollection<Staff> VisibleStaff { get => viewableStaff; private set { } }

		public StaffController() {
            var db = new DatabaseAdapter();
            staffmembers = db.FetchBasicStaffDetails();

            viewableStaff = new ObservableCollection<Staff>(staffmembers);
        }

        public ObservableCollection<Staff> GetViewableList() {
            return VisibleStaff;
        }

        public void FilterBy(Category category) {
            var selected = from Staff staff in staffmembers
                           where category == Category.Any || category == staff.Category
                           select staff;

            viewableStaff.Clear();
            selected.ToList().ForEach(viewableStaff.Add);
        }

		public void FilterByName(string name) { }

		public void ShowStaffDetails(Staff staff) { }
	}
}
