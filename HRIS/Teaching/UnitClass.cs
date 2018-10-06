﻿namespace HRIS.Teaching {
	public enum UnitClassType {
		Tutorial,
		Lecture,
	}

	public class UnitClass : Event {
		public UnitClassType Type { get; set; }

		public string UnitCode { get; set; }

		public string Room { get; set; }

		public Campus Campus { get; set; }

		public int StaffID { get; set; }
	}
}
