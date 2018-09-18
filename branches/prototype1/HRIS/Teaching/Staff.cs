using System;

namespace HRIS.Teaching {
	public class Staff {
		public int ID { get; set; }

		public string FamilyName { get; set; }

		public string GivenName { get; set; }

		public string Title { get; set; }

		public string Room { get; set; }

		public string Email { get; set; }

		public string Photo { get; set; }

		public Category Category { get; set; }

		public Availability Availability(DateTime when) {
			return Teaching.Availability.Free;
		}
	}
}