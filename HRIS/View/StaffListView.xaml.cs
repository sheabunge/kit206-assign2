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
	/// Interaction logic for StaffListView.xaml
	/// </summary>
	public partial class StaffListView : UserControl {
		private readonly StaffController controller;

		public event EventHandler StaffMemberSelected;

		public Staff SelectedStaffMember {
			get {
				return (Staff) StaffList.SelectedItem;
			}
			set {
				StaffList.SelectedItem = value;
			}
		}

		public StaffListView() {
			controller = (StaffController) Application.Current.FindResource("StaffController");
			InitializeComponent();
		}

		private void SelectCategory(object sender, SelectionChangedEventArgs e) {
			controller.CurrentCategoryFilter = (Category) CategoryFilter.SelectedItem;
			controller.ApplyFilters();
		}

		private void FilterByName(object sender, TextChangedEventArgs e) {
			controller.CurrentNameFilter = NameFilter.Text;
			controller.ApplyFilters();
		}

		private void StaffMemberSelectedHandler(object sender, SelectionChangedEventArgs e) {
			StaffMemberSelected?.Invoke(sender, e);
		}
	}
}
