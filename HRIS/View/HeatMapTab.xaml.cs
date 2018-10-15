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

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for HeatMapTab.xaml
	/// </summary>
	public partial class HeatMapTab : UserControl {
		ColorGrid controller { get; set; }

		/// <summary>
		/// Class constructor
		/// </summary>
		public HeatMapTab() {
			controller = (ColorGrid) Application.Current.FindResource("ColorGrid");
			InitializeComponent();
		}
	}
}
