using System;
using System.Collections.Generic;
using System.Text;

namespace DevSpace.Common.Entities.Test.Helpers {
	public static class RandomEntity {
		private static Random random = new Random();

		public static Event Event =>
			new Event(
				id: random.Next(),
				name: random.NextString( 16 ),
				startdate: random.NextDateTime(),
				enddate: random.NextDateTime()
			);

		public static Company Company =>
			new Company(
				id: random.Next(),
				name: random.NextString( 51 ),
				address: random.NextString( 201 ),
				phone: random.NextString( 21 ),
				website: random.NextString( 101 ),
				twitter: random.NextString( 21 )
			);

		public static SponsorLevel SponsorLevel =>
			new SponsorLevel(
				id: random.Next(),
				displayorder: random.Next(),
				name: random.NextString( 51 ),
				cost: random.Next(),
				displaylink: random.NextBool(),
				displayinemails: random.NextBool(),
				displayinsidebar: random.NextBool(),
				tickets: random.Next(),
				discount: random.Next(),
				timeonscreen: random.Next(),
				preconemail: random.NextBool(),
				midconemail: random.NextBool(),
				postconemail: random.NextBool()
			);

		public static Sponsor Sponsor =>
			new Sponsor(
				id: random.Next(),
				sponsoredevent: RandomEntity.Event,
				sponsoringcompany: RandomEntity.Company,
				sponsorshiplevel: RandomEntity.SponsorLevel
			);
	}
}
