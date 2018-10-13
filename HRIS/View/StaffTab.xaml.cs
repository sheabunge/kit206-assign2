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

		private Panel StaffDetails;

		public StaffTab() {
			controller = (StaffController) Application.Current.FindResource("StaffController");
			InitializeComponent();

			StaffDetails = (Panel) StaffDetailsPanel.FindName("StaffDetails");
			StaffDetails.Visibility = Visibility.Hidden;

			StaffListPanel.StaffMemberSelected += SelectStaffMember;
		}

		private void SelectStaffMember(object sender, EventArgs e) {
			Staff staff = StaffListPanel.SelectedStaffMember;

			if (staff == null) {
				StaffDetails.Visibility = Visibility.Hidden;
				NoneSelected.Visibility = Visibility.Visible;
				return;
			}

			controller.SelectItem(staff);
			NoneSelected.Visibility = Visibility.Hidden;
			StaffDetails.DataContext = staff;
			StaffDetails.Visibility = Visibility.Visible;
		}
	}
}
