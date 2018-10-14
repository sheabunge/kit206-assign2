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
		private readonly UnitController controller;

		public event EventHandler UnitSelected;

		public Unit SelectedUnit {
			get {
				return (Unit) UnitsList.SelectedItem;
			}
			set {
				UnitsList.SelectedItem = value;
			}
		}

		public UnitListView() {
			controller = (UnitController) Application.Current.FindResource("UnitController");
			InitializeComponent();
		}

		private void FilterList(object sender, TextChangedEventArgs e) {
			controller.CurrentFilter = SearchBox.Text;
			controller.ApplyFilter();
		}

		private void UnitSelectedHandler(object sender, SelectionChangedEventArgs e) {
			UnitSelected?.Invoke(sender, e);
		}
	}
}
