using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
			controller = (HeatMapController) Application.Curent.FindResource("HeatMapsController");
			InitializeComponent();
		}
	}
}
