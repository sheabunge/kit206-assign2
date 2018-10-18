using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HRIS.Control;
using HRIS.Teaching;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for UnitTimetableView.xaml
	/// </summary>
	public partial class UnitTimetableView : UserControl {
		private readonly UnitController controller;

		/// <summary>
		/// Color to use for occupied slots in the clash map
		/// </summary>
		public static SolidColorBrush ActivityColor => new SolidColorBrush(Colors.LimeGreen);

		/// <summary>
		/// Color to use for clashes in the clash map
		/// </summary>
		public static SolidColorBrush ClashColor => new SolidColorBrush(Colors.IndianRed);

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
			UnloadClashMap();
		}

		/// <summary>
		/// Set the Unit model used for displaying the timetable
		/// </summary>
		/// <param name="unit">Unit object to use as model; null will reset the model</param>
		public void SetModel(Unit unit) {
			// reset controls
			UnloadClashMap();
			CampusFilter.SelectedIndex = 0;

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

				// refresh the clash map if it's loaded
				if (ClashMapContainer != null && ClashMapContainer.Visibility == Visibility.Visible) {
					controller.GenerateClashMap();
				}
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

		/// <summary>
		/// Handle generation or hiding of the clash map table
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToggleClashMap(object sender, RoutedEventArgs e) {
			if (ClashMapContainer.Visibility == Visibility.Visible) {
				UnloadClashMap();
			} else {
				LoadClashMap();
			}
		}

		/// <summary>
		/// Generate and show the clash map
		/// </summary>
		private void LoadClashMap() {
			controller.GenerateClashMap();
			ClashMapContainer.Visibility = Visibility.Visible;
			ClashMapButton.Content = "Hide Clash Table";
		}

		/// <summary>
		/// Unload and hide the clash map
		/// </summary>
		private void UnloadClashMap() {
			controller.ClashMap.Clear();
			ClashMapContainer.Visibility = Visibility.Collapsed;
			ClashMapButton.Content = "Show Clash Table";
		}
	}
}
