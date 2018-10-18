using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for ColorGrid.xaml
	/// </summary>
	public partial class ColorGrid : UserControl {
		public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
			"Rows", typeof(ObservableCollection<ColorGridRow>), typeof(ColorGrid),
			new PropertyMetadata(new ObservableCollection<ColorGridRow>())
		);

		public ObservableCollection<ColorGridRow> Rows {
			get => GetValue(RowsProperty) as ObservableCollection<ColorGridRow>;
			set => SetValue(RowsProperty, value);
		}

		public static readonly DependencyProperty CellSizeProperty = DependencyProperty.Register(
			"CellSize", typeof(int), typeof(ColorGrid), new PropertyMetadata(30, CellSizeChanged)
		);

		public int CellSize {
			get => (int) GetValue(CellSizeProperty);
			set => SetValue(CellSizeProperty, value);
		}

		private static void CellSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var control = (ColorGrid) d;
			control.CellSizeChanged(e);
		}

		private void CellSizeChanged(DependencyPropertyChangedEventArgs e) {
			TableMap.MinColumnWidth = (int) e.NewValue;
			TableMap.MinRowHeight = (int) e.NewValue;
		}

		public ColorGrid() {
			InitializeComponent();

			var dayNames = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames;

			for (var day = 1; day < 6; day++) {
				TableMap.Columns[day].Header = dayNames[day];
			}
		}
	}
}
