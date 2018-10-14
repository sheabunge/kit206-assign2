namespace HRIS.Teaching {
	public class Unit {
		public string Code { get; set; }

		public string Title { get; set; }

		public Staff Coordinator { get; set; }

		public override string ToString() {
			return $"{Code} {Title}";
		}

		public override bool Equals(object obj) {
			return obj is Unit other && other.Code == Code;
		}

		public override int GetHashCode() {
			return Code.GetHashCode();
		}
	}
}
