using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Control {
	class RowData {

	}
	class ColorGrid {
		public ObservableCollection<RowData> GetVisibleList() {
			RowData row = new RowData();
			List<RowData> rows = new List<RowData>();
			rows.Add(row);
			return new ObservableCollection<RowData>(rows);
		}
	}
}
