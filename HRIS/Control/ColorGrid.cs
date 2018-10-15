using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HRIS.Control {
	class RowData {
		public String Time { get; private set; }
		public String[] Values { get; private set; }
		public Brush[] Colours { get; private set; }
		public RowData(String time, String[] values, Brush[] colours) {
			Time = time;
			Values = values;
			Colours = colours;
		}
	}
	class ColorGrid {
		public ObservableCollection<RowData> GetRows() {
			String[] values = { "A", "B", "C", "D", "E" };
			Brush brush = new Brush();
			Brush[] colours = { brush, brush, brush, brush, brush };
			RowData row = new RowData("T", values, colours);
			List<RowData> rows = new List<RowData>();
			rows.Add(row);
			return new ObservableCollection<RowData>(rows);
		}

		public ColorGrid() {
		}
	}
}
