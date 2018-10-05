using HRIS.Control;
using HRIS.Teaching;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HRIS.View
{
	/// <summary>
	/// Interaction logic for StaffTab.xaml
	/// </summary>
	public partial class StaffTab : UserControl {

		private readonly StaffController controller;

		public StaffTab() {
			controller = (StaffController) Application.Current.FindResource("StaffController");
			InitializeComponent();
		}

		private void SelectCategory(object sender, SelectionChangedEventArgs e) {
			controller.CurrentCategoryFilter = (Teaching.Category)CategoryFilter.SelectedItem;
			controller.ApplyFilters();
		}

		private void FilterByName(object sender, TextChangedEventArgs e) {
			controller.CurrentNameFilter = NameFilter.Text;
			controller.ApplyFilters();
		}

		private void SelectStaffMember(object sender, SelectionChangedEventArgs e) {
			Staff staff = (Staff) StaffList.SelectedItem;

			if (staff == null) {
				BasicDetails.Visibility = Visibility.Hidden;
				NoneSelected.Visibility = Visibility.Visible;
				return;
			}
		
			controller.CompleteStaffDetails(staff);
			NoneSelected.Visibility = Visibility.Hidden;
			BasicDetails.DataContext = staff;
			BasicDetails.Visibility = Visibility.Visible;
		}
	}
}
