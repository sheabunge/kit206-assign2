using System;
using System.Windows;
using System.Windows.Controls;
using HRIS.Control;
using HRIS.Teaching;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for UnitTab.xaml
	/// </summary>
	public partial class UnitTab : UserControl {
		private readonly UnitController controller;

		private readonly Panel UnitDetails;

		public UnitTab() {
			controller = (UnitController) Application.Current.FindResource("UnitController");
			InitializeComponent();

			UnitDetails = (Panel) UnitDetailsPanel.FindName("UnitDetails");
			UnitDetails.Visibility = Visibility.Hidden;

			UnitListPanel.UnitSelected += SelectUnit;
		}

		private void SelectUnit(object sender, EventArgs e) {
			var unit = UnitListPanel.SelectedUnit;

			if (unit == null) {
				UnitDetails.Visibility = Visibility.Hidden;
				NoneSelected.Visibility = Visibility.Visible;
				return;
			}

			NoneSelected.Visibility = Visibility.Hidden;
			UnitDetails.DataContext = unit;
			UnitDetails.Visibility = Visibility.Visible;

			controller.SelectUnit(unit);
		}
	}
}
