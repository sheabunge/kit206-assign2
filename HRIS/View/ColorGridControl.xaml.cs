using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	/// Interaction logic for ColorGridControl.xaml
	/// </summary>
	public partial class ColorGridControl : UserControl {
		public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
			"Rows", typeof(ObservableCollection<ColorGridRow>), typeof(ColorGridControl),
			new PropertyMetadata(new ObservableCollection<ColorGridRow>(), OnRowsChanged)
		);

		public ObservableCollection<ColorGridRow> Rows {
			get => GetValue(RowsProperty) as ObservableCollection<ColorGridRow>;
			set => SetValue(RowsProperty, value);
		}

		public ColorGridControl() {
			InitializeComponent();
		}

		private static void OnRowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var control = (ColorGridControl) d;
			control.OnRowsChanged(e);
		}

		private void OnRowsChanged(DependencyPropertyChangedEventArgs e) {
			TableMap.ItemsSource = e.NewValue as ObservableCollection<ColorGridRow>;
		}
	}
}
