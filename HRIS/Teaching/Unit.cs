namespace HRIS.Teaching {
	public class Unit {
		public string Code { get; set; }

		public string Title { get; set; }

		public int CoordinatorID { get; set; }

		public override string ToString() {
			return $"{Code} {Title}";
		}

		public override bool Equals(object obj) {
			Unit other = (Unit) obj;
			return other.Code == Code;
		}
	}
}
