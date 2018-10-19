using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HRIS.Control;
using HRIS.Teaching;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for HeatMapTab.xaml
	/// </summary>
	public partial class HeatMapTab : UserControl {
		/// <summary>
		/// Controller class responsible for this section of the application
		/// </summary>
		private readonly HeatMapController controller;

		/// <summary>
		/// Initialise this section of the application
		/// </summary>
		public HeatMapTab() {
			controller = (HeatMapController) Application.Current.FindResource("HeatMapController");
			InitializeComponent();

			var random = new Random();
			ColorPicker.SelectedIndex = random.Next(ColorPicker.Items.Count);
		}

		/// <summary>
		/// Update the timetable content when a campus location is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectCampus(object sender, SelectionChangedEventArgs e) {
			controller.CurrentCampusFilter = (Campus) CampusFilter.SelectedItem;
			controller.UpdateRows();
		}

		/// <summary>
		/// Update the timetable colors when a new color value is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectColor(object sender, SelectionChangedEventArgs e) {
			var brush = (SolidColorBrush) e.AddedItems[0];
			controller.PrimaryColor = brush.Color;
			controller.UpdateRows();
		}
	}
}
