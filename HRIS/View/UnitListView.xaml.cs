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
	/// Interaction logic for UnitListView.xaml
	/// </summary>
	public partial class UnitListView : UserControl {
		/// <summary>
		/// Controller class responsible for this section of the application
		/// </summary>
		private readonly UnitController controller;

		/// <summary>
		/// Event handler that fires when a unit is selected to display its timetable
		/// </summary>
		public event EventHandler UnitSelectedEvent;

		/// <summary>
		/// Retrieve or set the unit currently selected in the list
		/// </summary>
		public Unit SelectedUnit {
			get => (Unit) UnitsList.SelectedItem;
			set => UnitsList.SelectedItem = value;
		}

		/// <summary>
		/// Initialise this component
		/// </summary>
		public UnitListView() {
			controller = (UnitController) Application.Current.FindResource("UnitController");
			InitializeComponent();
		}

		/// <summary>
		/// Filter the list of units displayed using search text
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FilterList(object sender, TextChangedEventArgs e) {
			controller.SearchText = SearchBox.Text;
			controller.ApplyFilter();
		}

		/// <summary>
		/// Trigger an event when a unit is selected to display its timetable
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UnitSelectedHandler(object sender, SelectionChangedEventArgs e) {
			UnitSelectedEvent?.Invoke(sender, e);
		}
	}
}
