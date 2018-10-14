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
		/// <summary>
		/// Controller class responsible for this section of the application
		/// </summary>
		private readonly UnitController controller;

		/// <summary>
		/// Initialise this component
		/// </summary>
		public UnitTab() {
			controller = (UnitController) Application.Current.FindResource("UnitController");
			InitializeComponent();
			UnitListPanel.UnitSelectedEvent += LoadUnitTimetable;
		}

		/// <summary>
		/// Respond to a request to select a specific unit in the list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void SelectUnit(object sender, EventArgs e) {
			if (e is SelectionChangedEventArgs sel) {
				SelectUnit(sel.AddedItems.Count == 0 ? null : (Unit) sel.AddedItems[0]);
			} else if (e is RoutedEventArgs) {
				var element = (Hyperlink) sender;
				var unit = (Unit) element.DataContext;
				SelectUnit(unit);
			}
		}

		/// <summary>
		/// Select a specific unit in the list
		/// </summary>
		/// <param name="unit">Unit to select</param>
		public void SelectUnit(Unit unit) {
			UnitListPanel.SelectedUnit = unit;
		}

		/// <summary>
		/// Load the timetable view for a unit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="ea"></param>
		private void LoadUnitTimetable(object sender, EventArgs ea) {
			var e = (SelectionChangedEventArgs) ea;
			var unit = e.AddedItems.Count == 0 ? null : (Unit) e.AddedItems[0];
			NoneSelected.Visibility = unit == null ? Visibility.Visible : Visibility.Hidden;
			UnitDetailsPanel.SetModel(unit);
		}
	}
}
