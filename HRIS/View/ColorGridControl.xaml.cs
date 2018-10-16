using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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
