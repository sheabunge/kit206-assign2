using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HRIS.Control {
	enum CellColour {
		White,
		Red,
		Orange,
		Yellow,
	}
	class RowData {
		public String Time { get; private set; }
		public String[] Values { get; private set; }
		public Brush[] Colours { get; private set; }
		public RowData(String time, String[] values, CellColour[] colours) {
			Time = time;
			Values = values;
			Colours = new Brush[colours.Length];
			for (int i = 0; i < colours.Length; i++) {
				Brush next = null;
				switch (colours[i]) {
					case CellColour.White:
						next = Brushes.White;
						break;
					case CellColour.Red:
						next = Brushes.Red;
						break;
					case CellColour.Orange:
						next = Brushes.Orange;
						break;
					case CellColour.Yellow:
						next = Brushes.Yellow;
						break;
				}
				Colours[i] = next;
			}
		}
	}
	class ColorGrid {
		public ObservableCollection<RowData> GetRows() {
			String[] values = { "1", "1", "2", "5", "3" };
			CellColour[] colours = { CellColour.Yellow, CellColour.Yellow, CellColour.Orange, CellColour.Red, CellColour.Orange };
			RowData row = new RowData("09:00", values, colours);
			List<RowData> rows = new List<RowData>();
			rows.Add(row);
			return new ObservableCollection<RowData>(rows);
		}

		public ColorGrid() {
		}
	}
}
