using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HRIS.Database;
using HRIS.Teaching;

namespace HRIS.Control {
	public class UnitController {
		public List<Unit> CompleteList { get; private set; }

		public ObservableCollection<Unit> VisibleList { get; private set; }

		public string CurrentFilter { get; set; }

		private readonly DatabaseAdapter _db;

		public UnitController() {
			_db = new DatabaseAdapter();
			CompleteList = _db.FetchAllUnits();
			VisibleList = new ObservableCollection<Unit>(CompleteList);
			CurrentFilter = "";
		}

		public ObservableCollection<Unit> GetVisibleList() {
			return VisibleList;
		}

		public List<UnitClass> GetClasses(Unit unit) {
			return _db.FetchUnitClasses(unit);
		}

		public void ApplyFilter() {
			VisibleList.Clear();

			var selected =
				from Unit unit in CompleteList
				where unit.ToString().IndexOf(CurrentFilter, StringComparison.OrdinalIgnoreCase) >= 0
				orderby unit
				select unit;

			selected.ToList().ForEach(VisibleList.Add);
		}

		public void UnitsFor(Staff staff) { }
	}
}
