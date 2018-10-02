using System;
using System.Collections.Generic;

namespace HRIS.Teaching {
	public class Staff {
		public int ID { get; set; }

		public string FamilyName { get; set; }

		public string GivenName { get; set; }

		public string Title { get; set; }

		public Campus Campus { get; set; }

		public string Room { get; set; }

		public string Email { get; set; }

		public string Photo { get; set; }

		public Category Category { get; set; }
		
		public List<Event> Consultations { get; set; }

        public override string ToString() {
            return $"{Title} {GivenName} {FamilyName}";
        }

        public Availability Availability(DateTime when) {
			return Teaching.Availability.Free;
		}
	}
}