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
	/// Interaction logic for StaffDetailsView.xaml
	/// </summary>
	public partial class StaffDetailsView : UserControl {
		private readonly StaffController controller;

		public event EventHandler LoadUnitTimetable;

		public StaffDetailsView() {
			controller = (StaffController) Application.Current.FindResource("StaffController");
			InitializeComponent();
			StaffDetails.Visibility = Visibility.Hidden;
		}

		public void SetModel(Staff staff) {
			if (staff == null) {
				// hide the details section if no staff member is selected
				StaffDetails.Visibility = Visibility.Hidden;
			} else {
				controller.SelectItem(staff);
				StaffDetails.DataContext = staff;
				StaffDetails.Visibility = Visibility.Visible;
			}
		}

		private void DeselectItem(object sender, RoutedEventArgs e) {
			var item = (ListBoxItem) sender;
			item.IsSelected = false;
		}

		private void LoadUnitTimetableHandler(object sender, RoutedEventArgs e) {
			LoadUnitTimetable?.Invoke(sender, e);
		}
	}
}
