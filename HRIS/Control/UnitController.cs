using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HRIS.Database;
using HRIS.Teaching;

namespace HRIS.Control {
	public class UnitController {
		private readonly DatabaseAdapter _db;

		public List<Unit> UnitsList { get; private set; }

		public ObservableCollection<Unit> VisibleUnits { get; private set; }

		public string CurrentFilter { get; set; }

		public List<UnitClass> UnitClasses { get; private set; }

		public ObservableCollection<UnitClass> VisibleUnitClasses { get; private set; }

		public ObservableCollection<UnitClass> GetUnitClasses() => VisibleUnitClasses;

		public UnitController() {
			_db = new DatabaseAdapter();
			UnitsList = _db.FetchAllUnits();
			VisibleUnits = new ObservableCollection<Unit>(UnitsList);
			CurrentFilter = "";

			VisibleUnitClasses = new ObservableCollection<UnitClass>();
		}

		public ObservableCollection<Unit> GetVisibleList() {
			return VisibleUnits;
		}

		public void SelectUnit(Unit unit) {
			UnitClasses = _db.FetchUnitClasses(unit);
			VisibleUnitClasses.Clear();
			UnitClasses.ForEach(VisibleUnitClasses.Add);
		}

		public void FilterClassesByCampus(Campus campus) {
			if (UnitClasses == null) {
				return;
			}

			VisibleUnitClasses.Clear();

			if (campus == Campus.Any) {
				UnitClasses.ForEach(VisibleUnitClasses.Add);
			} else {
				var results = from UnitClass unitclass in UnitClasses
					where unitclass.Campus == campus
					select unitclass;

				results.ToList().ForEach(VisibleUnitClasses.Add);
			}
		}

		public void ApplyFilter() {
			VisibleUnits.Clear();

			var selected =
				from Unit unit in UnitsList
				where unit.ToString().IndexOf(CurrentFilter, StringComparison.OrdinalIgnoreCase) >= 0
				orderby unit
				select unit;

			selected.ToList().ForEach(VisibleUnits.Add);
		}

		public void UnitsFor(Staff staff) { }
	}
}
