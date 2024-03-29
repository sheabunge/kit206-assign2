﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
		/// Color to use for displaying teaching timeslots in the avalability table
		/// </summary>
		public static SolidColorBrush TeachingColor => new SolidColorBrush(Colors.RoyalBlue);

		/// <summary>
		/// Color to use for displaying consulting timeslots in the avalability table
		/// </summary>
		public static SolidColorBrush ConsultingColor => new SolidColorBrush(Colors.IndianRed);

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
			UnloadAvailabilityTable();
		}

		/// <summary>
		/// Set the Staff model which is used for this details view
		/// </summary>
		/// <param name="staff">Staff object to use as model. null will reset the model</param>
		public void SetModel(Staff staff) {
			UnloadAvailabilityTable();

			if (staff == null) {
				// hide the details section if no staff member is selected
				StaffDetails.Visibility = Visibility.Hidden;
			} else {
				controller.SelectItem(staff);
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

		/// <summary>
		/// Handle generation or hiding of the availability table
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToggleAvailabilityTable(object sender, RoutedEventArgs e) {
			if (AvailabilityTableContainer.Visibility == Visibility.Visible) {
				UnloadAvailabilityTable();
			} else {
				LoadAvailabilityTable();
			}
		}

		/// <summary>
		/// Generate and show the availability table
		/// </summary>
		private void LoadAvailabilityTable() {
			controller.GenerateAvalabilityTable();
			AvailabilityTableContainer.Visibility = Visibility.Visible;
			AvailabilityTableButton.Content = "Hide Availability Table";
		}

		/// <summary>
		/// Unload and hide the availability table
		/// </summary>
		private void UnloadAvailabilityTable() {
			controller.AvalabilityTable.Clear();
			AvailabilityTableContainer.Visibility = Visibility.Collapsed;
			AvailabilityTableButton.Content = "Show Availability Table";
		}
	}
}
