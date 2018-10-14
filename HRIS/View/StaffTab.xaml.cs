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
		/// <summary>
		/// Controller class responsible for this section of the application
		/// </summary>
		private readonly StaffController controller;

		/// <summary>
		/// Initialise this section of the application
		/// </summary>
		public StaffTab() {
			controller = (StaffController) Application.Current.FindResource("StaffController");
			InitializeComponent();
			StaffListPanel.LoadStaffDetailsEvent += LoadLoadStaffDetails;
		}

		/// <summary>
		/// Respond to a request to select a specific staff member
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void SelectStaffMember(object sender, EventArgs e) {
			if (e is SelectionChangedEventArgs sel) {
				SelectStaffMember(sel.AddedItems.Count == 0 ? null : (Staff) sel.AddedItems[0]);
			} else if (e is RoutedEventArgs) {
				var element = (FrameworkElement) sender;
				var unitClass = (UnitClass) element.DataContext;
				SelectStaffMember(unitClass.Staff);
			}
		}

		/// <summary>
		/// Select a specific staff member to display its details
		/// </summary>
		/// <param name="staff">Staff member to select</param>
		public void SelectStaffMember(Staff staff) {
			// attempt to select the identical staff object if it exists; otherwise just load the details directly
			if (StaffListPanel.StaffList.Items.Contains(staff)) {
				StaffListPanel.SelectedStaffMember = staff;
			} else {
				LoadStaffDetails(staff);
			}
		}

		/// <summary>
		/// Respond to a request to display the details of a staff member
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="ea"></param>
		private void LoadLoadStaffDetails(object sender, EventArgs ea) {
			var e = (SelectionChangedEventArgs) ea;
			var staff = e.AddedItems.Count == 0 ? null : (Staff) e.AddedItems[0];
			LoadStaffDetails(staff);
		}

		/// <summary>
		/// Display the details of a staff member
		/// </summary>
		/// <param name="staff"></param>
		private void LoadStaffDetails(Staff staff) {
			NoneSelected.Visibility = staff == null ? Visibility.Visible : Visibility.Hidden;
			StaffDetailsPanel.SetModel(staff);
		}
	}
}
