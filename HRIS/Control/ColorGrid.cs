using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HRIS.Control {
	class RowData {
		public String Time {
			get {
				return "T";
			}
		}
		public String[] Values {
			get {
				String[] values = { "A", "B", "C", "D", "E" };
				return values;
			}
		}
		public Brush[] Colours {
			get {
				Brush brush = null;
				Brush[] values = { brush, brush, brush, brush, brush };
				return values;
			}
		}
		public RowData() {
		}
	}
	class ColorGrid {
		public ObservableCollection<RowData> GetRows() {
			RowData row = new RowData();
			List<RowData> rows = new List<RowData>();
			rows.Add(row);
			return new ObservableCollection<RowData>(rows);
		}

		public ColorGrid() {
		}
	}
}
