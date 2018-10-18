using System;
using System.Windows;
using System.Windows.Controls;
using HRIS.Control;
using HRIS.Teaching;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for StaffDetailsView.xaml
	/// </summary>
	public partial class StaffDetailsView : UserControl {
		/// <summary>
		/// Controller class responsible for this section of the application
		/// </summary>
		private readonly StaffController controller;

		/// <summary>
		/// Event handler that fires to redirect the user to a unit timetable
		/// </summary>
		public event EventHandler LoadUnitTimetableEvent;

		/// <summary>
		/// Initialise this component
		/// </summary>
		public StaffDetailsView() {
			controller = (StaffController) Application.Current.FindResource("StaffController");
			InitializeComponent();
			StaffDetails.Visibility = Visibility.Hidden;
		}

		/// <summary>
		/// Set the Staff model which is used for this details view
		/// </summary>
		/// <param name="staff">Staff object to use as model. null will reset the model</param>
		public void SetModel(Staff staff) {
			if (staff == null) {
				// hide the details section if no staff member is selected
				StaffDetails.Visibility = Visibility.Hidden;
			} else {
				controller.SelectItem(staff);
				controller.GenerateAvalabilityTable();
				StaffDetails.DataContext = staff;
				StaffDetails.Visibility = Visibility.Visible;
			}
		}

		/// <summary>
		/// Event handler for ListBox elements which should be unselectable
		/// </summary>
		/// <param name="sender">ListBoxItem that has been selected</param>
		/// <param name="e">Associated event arguments</param>
		private void DeselectItem(object sender, RoutedEventArgs e) {
			var item = (ListBoxItem) sender;
			item.IsSelected = false;
		}

		/// <summary>
		/// Handler for a Hyperlink Click event that invokes an event to load
		/// the timetable view for a unit
		/// </summary>
		/// <param name="sender">UI Element where the request originated</param>
		/// <param name="e">Arguments associated with the event</param>
		private void LoadUnitTimetableHandler(object sender, RoutedEventArgs e) {
			LoadUnitTimetableEvent?.Invoke(sender, e);
		}
	}
}
