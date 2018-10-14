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

		public event EventHandler LoadStaffDetails;

		public UnitTimetableView() {
			controller = (UnitController) Application.Current.FindResource("UnitController");
			InitializeComponent();
		}

		private void FilterClassesByCampus(object sender, SelectionChangedEventArgs e) {
			if (CampusFilter.SelectedItem != null) {
				controller.FilterClassesByCampus((Campus) CampusFilter.SelectedItem);
			}
		}

		private void RedirectToStaffDetails(object sender, RoutedEventArgs e) {
			LoadStaffDetails?.Invoke(sender, e);
		}

		private void UnselectCell(object sender, RoutedEventArgs e) {
			var cell = (DataGridCell) sender;
			cell.IsSelected = false;
		}
	}
}
