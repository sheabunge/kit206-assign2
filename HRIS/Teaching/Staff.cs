﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HRIS.Teaching {
	public class Staff {
		public int ID { get; set; }

		public string FamilyName { get; set; }

		public string GivenName { get; set; }

		public string Title { get; set; }

		public Campus Campus { get; set; }

		public string Room { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }

		public string Photo { get; set; }

		public Category Category { get; set; }

		public List<Event> Consultations { get; set; }

		public List<UnitClass> Classes { get; set; }

		public List<Unit> UnitsTeaching { get; set; }

		public List<Unit> UnitsCoordinating { get; set; }

		public string FullName => $"{Title} {GivenName} {FamilyName}";

		public Availability CurrentAvalability => Availability(DateTime.Now).Item1;

		public override string ToString() {
			return FullName;
		}

		private static IEnumerable<Event> CurrentEvents(IEnumerable<Event> events, DateTime when) {

			if (events == null) {
				return events;
			}

			var results = from _event in events
			              where _event.Day == when.DayOfWeek &&
			                    _event.Start <= when.TimeOfDay && when.TimeOfDay <= _event.End
			              select _event;
			
			return results;
		}

		public Tuple<Availability, Event> Availability(DateTime when) {
			Availability availability = Teaching.Availability.Free;
			Event currentEvent = null;

			var consultations = CurrentEvents(Consultations, when);
			if (consultations != null && consultations.Any()) {
				availability = Teaching.Availability.Consulting;
				currentEvent = consultations.First();

			} else {
				var classes = CurrentEvents(Classes, when);
				if (classes != null && classes.Any()) {
					availability = Teaching.Availability.Teaching;
					currentEvent = classes.First();
				}
			}

			return Tuple.Create(availability, currentEvent);
		}
	}
}
