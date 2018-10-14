using HRIS.Control;
using HRIS.Teaching;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for StaffTab.xaml
	/// </summary>
	public partial class StaffTab : UserControl {
		private readonly StaffController controller;

		public StaffTab() {
			controller = (StaffController) Application.Current.FindResource("StaffController");
			InitializeComponent();
			StaffListPanel.StaffMemberSelected += LoadStaffDetails;
		}

		public void SelectStaffMember(object sender, EventArgs e) {
			if (e is SelectionChangedEventArgs sel) {
				SelectStaffMember(sel.AddedItems.Count == 0 ? null : (Staff) sel.AddedItems[0]);

			} else if (e is RoutedEventArgs) {
				var element = (FrameworkElement) sender;
				var unitClass = (UnitClass) element.DataContext;
				SelectStaffMember(unitClass.Staff);
			}
		}

		public void SelectStaffMember(Staff staff) {
			// attempt to select the identical staff object if it exists; otherwise just load the details directly
			if (StaffListPanel.StaffList.Items.Contains(staff)) {
				StaffListPanel.SelectedStaffMember = staff;
			} else {
				LoadStaffDetails(staff);
			}
		}

		private void LoadStaffDetails(object sender, EventArgs ea) {
			var e = (SelectionChangedEventArgs) ea;
			var staff = e.AddedItems.Count == 0 ? null : (Staff) e.AddedItems[0];
			LoadStaffDetails(staff);
		}

		private void LoadStaffDetails(Staff staff) {
			NoneSelected.Visibility = staff == null ? Visibility.Visible : Visibility.Hidden;
			StaffDetailsPanel.SetModel(staff);
		}
	}
}
