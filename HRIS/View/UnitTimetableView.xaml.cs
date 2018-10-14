using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HRIS.Control;
using HRIS.Teaching;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for UnitTimetableView.xaml
	/// </summary>
	public partial class UnitTimetableView : UserControl {
		private readonly UnitController controller;

		/// <summary>
		/// Event handler triggered by a request to load the details of a staff member
		/// </summary>
		public event EventHandler LoadStaffDetailsEvent;

		/// <summary>
		/// Initialise this component
		/// </summary>
		public UnitTimetableView() {
			controller = (UnitController) Application.Current.FindResource("UnitController");
			InitializeComponent();
			UnitDetails.Visibility = Visibility.Hidden;
		}

		/// <summary>
		/// Set the Unit model used for displaying the timetable
		/// </summary>
		/// <param name="staff">Unit object to use as model; null will reset the model</param>
		public void SetModel(Unit unit) {
			if (unit == null) {
				// hide the details section if no unit is selected
				UnitDetails.Visibility = Visibility.Hidden;
			} else {
				controller.SelectUnit(unit);
				UnitDetails.DataContext = unit;
				UnitDetails.Visibility = Visibility.Visible;
			}
		}

		/// <summary>
		/// Filter the list of classes displayed by their campus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FilterClassesByCampus(object sender, SelectionChangedEventArgs e) {
			if (CampusFilter.SelectedItem != null) {
				controller.FilterClassesByCampus((Campus) CampusFilter.SelectedItem);
			}
		}

		/// <summary>
		/// Redirect the user to viewing the details of a staff member
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RedirectToStaffDetails(object sender, RoutedEventArgs e) {
			LoadStaffDetailsEvent?.Invoke(sender, e);
		}

		/// <summary>
		/// Handler for DataGridCells which should be unselectable
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeselectCell(object sender, RoutedEventArgs e) {
			var cell = (DataGridCell) sender;
			cell.IsSelected = false;
		}
	}
}
