using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for ColorGrid.xaml
	/// </summary>
	public partial class ColorGrid : UserControl {
		public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
			"Rows", typeof(ObservableCollection<ColorGridRow>), typeof(ColorGrid),
			new PropertyMetadata(new ObservableCollection<ColorGridRow>(), OnRowsChanged)
		);

		public ObservableCollection<ColorGridRow> Rows {
			get => GetValue(RowsProperty) as ObservableCollection<ColorGridRow>;
			set => SetValue(RowsProperty, value);
		}

		public ColorGrid() {
			InitializeComponent();
		}

		private static void OnRowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var control = (ColorGrid) d;
			control.OnRowsChanged(e);
		}

		private void OnRowsChanged(DependencyPropertyChangedEventArgs e) {
			TableMap.ItemsSource = e.NewValue as ObservableCollection<ColorGridRow>;
		}
	}
}
