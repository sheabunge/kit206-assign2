using System;
using System.Windows;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		/// <summary>
		/// Class constructor.
		/// Initialise the window and attach event listeners for user controls
		/// </summary>
		public MainWindow() {
			InitializeComponent();
			StaffTabContent.StaffDetailsPanel.LoadUnitTimetableEvent += LoadUnitTimetable;
			UnitTabContent.UnitDetailsPanel.LoadStaffDetailsEvent += LoadStaffDetails;
		}

		/// <summary>
		/// Respond to an event by loading the timetable view for a unit
		/// </summary>
		/// <param name="sender">UI element where this request originated</param>
		/// <param name="e">Arguments associated with the event</param>
		private void LoadUnitTimetable(object sender, EventArgs e) {
			Navigation.SelectedItem = UnitsTabItem;
			UnitTabContent.SelectUnit(sender, e);
		}

		/// <summary>
		/// Respond to an event by loading the details view for a staff member
		/// </summary>
		/// <param name="sender">UI element where this request originated</param>
		/// <param name="e">Arguments associated with the event</param>
		private void LoadStaffDetails(object sender, EventArgs e) {
			Navigation.SelectedItem = StaffTabItem;
			StaffTabContent.SelectStaffMember(sender, e);
		}
	}
}
