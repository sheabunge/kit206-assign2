using System.Windows;
using System.Windows.Controls;
using HRIS.Control;
using HRIS.Teaching;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for HeatMapTab.xaml
	/// </summary>
	public partial class HeatMapTab : UserControl {
		/// <summary>
		/// Controller class responsible for this section of the application
		/// </summary>
		private readonly HeatMapController controller;

		/// <summary>
		/// Initialise this section of the application
		/// </summary>
		public HeatMapTab() {
			controller = (HeatMapController) Application.Current.FindResource("HeatMapController");
			InitializeComponent();
		}
		private void SelectCampus(object sender, SelectionChangedEventArgs e) {
			controller.CurrentCampusFilter = (Campus) CampusFilter.SelectedItem;
			controller.UpdateRows();
		}
	}
}
