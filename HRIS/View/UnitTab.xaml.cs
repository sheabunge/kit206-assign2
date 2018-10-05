using System.Windows;
using System.Windows.Controls;
using HRIS.Control;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for UnitTab.xaml
	/// </summary>
	public partial class UnitTab : UserControl {
		private readonly UnitController controller;

		public UnitTab() {
			controller = (UnitController) Application.Current.FindResource("UnitController");
			InitializeComponent();
		}

		private void FilterList(object sender, TextChangedEventArgs e) {
			controller.CurrentFilter = SearchBox.Text;
			controller.ApplyFilter();
		}
	}
}
