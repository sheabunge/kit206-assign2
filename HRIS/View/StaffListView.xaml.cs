using System;
using System.Windows;
using System.Windows.Controls;
using HRIS.Control;
using HRIS.Teaching;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for StaffListView.xaml
	/// </summary>
	public partial class StaffListView : UserControl {
		/// <summary>
		/// Controller class responsible for this section of the application
		/// </summary>
		private readonly StaffController controller;

		/// <summary>
		/// Event handler that fires to redirect the user view a staff member's details
		/// </summary>
		public event EventHandler LoadStaffDetailsEvent;

		/// <summary>
		/// Retrieve or set the staff member currently selected in the list
		/// </summary>
		public Staff SelectedStaffMember {
			get => (Staff) StaffList.SelectedItem;
			set => StaffList.SelectedItem = value;
		}

		/// <summary>
		/// Initialise the component
		/// </summary>
		public StaffListView() {
			controller = (StaffController) Application.Current.FindResource("StaffController");
			InitializeComponent();
		}

		/// <summary>
		/// Filter the staff list to only show members in a particular category
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectCategory(object sender, SelectionChangedEventArgs e) {
			controller.CurrentCategoryFilter = (Category) CategoryFilter.SelectedItem;
			controller.ApplyFilters();
		}

		/// <summary>
		/// Filter the staff list to only show those with names matching specified search text
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FilterByName(object sender, TextChangedEventArgs e) {
			controller.CurrentNameFilter = NameFilter.Text;
			controller.ApplyFilters();
		}

		/// <summary>
		/// Handle a request to load the details for a specified staff member
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StaffMemberSelectedHandler(object sender, SelectionChangedEventArgs e) {
			LoadStaffDetailsEvent?.Invoke(sender, e);
		}
	}
}
