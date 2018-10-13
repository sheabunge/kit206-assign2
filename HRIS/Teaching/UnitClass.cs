namespace HRIS.Teaching {

	public enum UnitClassType {
		Lecture,
		Tutorial,
		Practical,
		Workshop,
	}

	public class UnitClass : Event {
		public UnitClassType Type { get; set; }

		public Unit Unit { get; set; }

		public string Room { get; set; }

		public Campus Campus { get; set; }

		public Staff Staff { get; set; }
	}
}
