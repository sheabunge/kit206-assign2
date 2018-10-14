using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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

			UnitListPanel.UnitSelected += LoadUnitTimetable;
		}

		public void SelectUnit(object sender, EventArgs e) {
			if (e is SelectionChangedEventArgs sel) {
				Console.WriteLine("before select unit");
				SelectUnit(sel.AddedItems.Count == 0 ? null : (Unit) sel.AddedItems[0]);
			} else if (e is RoutedEventArgs) {
				var element = (Hyperlink) sender;
				var unit = (Unit) element.DataContext;
				SelectUnit(unit);
			}
		}

		public void SelectUnit(Unit unit) {
			Console.WriteLine("Selecting unit "  + unit);
			UnitListPanel.SelectedUnit = unit;
		}

		private void LoadUnitTimetable(object sender, EventArgs ea) {
			var e = (SelectionChangedEventArgs) ea;

			if (e.AddedItems.Count == 0) {
				UnitDetails.Visibility = Visibility.Hidden;
				NoneSelected.Visibility = Visibility.Visible;
				return;
			}

			var unit = (Unit) e.AddedItems[0];

			controller.SelectUnit(unit);
			NoneSelected.Visibility = Visibility.Hidden;
			UnitDetails.DataContext = unit;
			UnitDetails.Visibility = Visibility.Visible;
		}
	}
}
