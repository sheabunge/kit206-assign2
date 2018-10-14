using HRIS.Control;
using System;
using System.Windows;
using System.Windows.Controls;
using HRIS.Teaching;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		public MainWindow() {
			InitializeComponent();
			StaffTabContent.StaffDetailsPanel.LoadUnitTimetable += LoadUnitTimetable;
			UnitTabContent.UnitDetailsPanel.LoadStaffDetails += LoadStaffDetails;
		}

		public void LoadUnitTimetable(object sender, EventArgs e) {
			var element = sender as FrameworkElement;
			Console.WriteLine("Loading unit timetable from " + sender + " " + element?.Name);
			Navigation.SelectedItem = UnitsTabItem;
			UnitTabContent.SelectUnit(sender, e);
		}

		public void LoadStaffDetails(object sender, EventArgs e) {
			Navigation.SelectedItem = StaffTabItem;
			StaffTabContent.SelectStaffMember(sender, e);
		}
	}
}
