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
		/// <summary>
		/// Dependency property for setting rows data to display
		/// </summary>
		public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
			"Rows", typeof(ObservableCollection<ColorGridRow>), typeof(ColorGrid),
			new PropertyMetadata(new ObservableCollection<ColorGridRow>())
		);

		/// <summary>
		/// Wrapper for the Row property
		/// </summary>
		public ObservableCollection<ColorGridRow> Rows {
			get => GetValue(RowsProperty) as ObservableCollection<ColorGridRow>;
			set => SetValue(RowsProperty, value);
		}

		/// <summary>
		/// Dependency property to set the height and width of cells
		/// </summary>
		public static readonly DependencyProperty CellSizeProperty = DependencyProperty.Register(
			"CellSize", typeof(int), typeof(ColorGrid), new PropertyMetadata(30, CellSizeChanged)
		);

		/// <summary>
		/// Wrapper for the CellSize property
		/// </summary>
		public int CellSize {
			get => (int) GetValue(CellSizeProperty);
			set => SetValue(CellSizeProperty, value);
		}

		/// <summary>
		/// Static handler to update the size of cells when changed
		/// </summary>
		/// <param name="d"></param>
		/// <param name="e"></param>
		private static void CellSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var control = (ColorGrid) d;
			control.CellSizeChanged(e);
		}

		/// <summary>
		/// Handler to update the size of cells when changed
		/// </summary>
		/// <param name="d"></param>
		/// <param name="e"></param>
		private void CellSizeChanged(DependencyPropertyChangedEventArgs e) {
			TableMap.MinColumnWidth = (int) e.NewValue;
			TableMap.MinRowHeight = (int) e.NewValue;
		}

		/// <summary>
		/// Class constructor
		/// </summary>
		public ColorGrid() {
			InitializeComponent();

			var dayNames = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames;

			for (var day = 1; day < 6; day++) {
				TableMap.Columns[day].Header = dayNames[day];
			}
		}

		/// <summary>
		/// Handler for DataGridCells which should be unselectable
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeselectCell(object sender, RoutedEventArgs e) {
			var cell = (DataGridCell) sender;
			cell.IsSelected = false;
		}
	}
}
