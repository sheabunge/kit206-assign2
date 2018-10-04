using HRIS.Control;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
<<<<<<< HEAD
	public partial class MainWindow : Window {}
=======
	public partial class MainWindow : Window {
		private readonly StaffController _staffController;

		public MainWindow() {
			_staffController = (StaffController) Application.Current.FindResource("StaffController");
			InitializeComponent();
		}

		private void SelectCategory(object sender, SelectionChangedEventArgs e) {
			_staffController.CurrentCategoryFilter = (Teaching.Category) CategoryFilter.SelectedItem;
			_staffController.ApplyFilters();
		}

		private void FilterByName(object sender, TextChangedEventArgs e) {
			_staffController.CurrentNameFilter = NameFilter.Text;
			_staffController.ApplyFilters();
		}
	}
>>>>>>> f4f2277fcbc28811f70691402ab65fdfe16b64bb
}
