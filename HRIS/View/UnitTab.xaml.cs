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

		public void SelectUnit(object sender, EventArgs e) {
			SelectUnit(sender, (SelectionChangedEventArgs) e);
		}

		public void SelectUnit(object sender, SelectionChangedEventArgs e) {
			SelectUnit(e.AddedItems.Count > 0 ? (Unit) e.AddedItems[0] : null);
		}

		public void SelectUnit(Unit unit) {

			if (unit == null) {
				UnitDetails.Visibility = Visibility.Hidden;
				NoneSelected.Visibility = Visibility.Visible;
				return;
			}

			controller.SelectUnit(unit);
			UnitListPanel.SelectedUnit = unit;

			NoneSelected.Visibility = Visibility.Hidden;
			UnitDetails.DataContext = unit;
			UnitDetails.Visibility = Visibility.Visible;
		}
	}
}
