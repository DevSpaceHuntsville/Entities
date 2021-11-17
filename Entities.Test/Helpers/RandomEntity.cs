using System;
using System.Linq;

namespace DevSpace.Common.Entities.Test.Helpers {
	public static class RandomEntity {
		private static Random random = new Random();

		public static Article Article =>
			new Article(
				id: random.Next(),
				title: random.NextString( 16 ),
				body: random.NextString( 16 ),
				publishdate: random.NextDateTime(),
				expiredate: random.NextDateTime()
			);

		public static Room Room =>
			new Room(
				id: random.Next(),
				displayname: random.NextString( 16 )
			);

		public static TimeSlot TimeSlot =>
			new TimeSlot(
				id: random.Next(),
				starttime: random.NextDateTime(),
				endtime: random.NextDateTime()
			);

		public static Tag Tag =>
			new Tag(
				id: random.Next(),
				text: random.NextString( 59 )
			);

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

		public static Session Session =>
			new Session(
				id: random.Next(),
				userid: random.Next(),
				title: random.NextString( 5, 250 ),
				@abstract: random.NextString( 20, 500 ),
				notes: random.NextBool() ? null : random.NextString( 20, 5000 ),
				sessionlength: random.NextBool() ? 60 : 30,
				level: RandomEntity.Tag,
				category: RandomEntity.Tag,
				accepted: random.NextBool() ? (bool?)null : random.NextBool(),
				tags: Enumerable.Range( 0, 5 ).Select( _ => RandomEntity.Tag ).ToList(),
				timeslot: RandomEntity.TimeSlot,
				room: RandomEntity.Room,
				eventid: random.Next(),
				sessionizeid: random.NextBool() ? (int?)null : random.Next()
			);

		public static User User =>
			new User(
				bio: random.NextString( 5, 5000 ),
				displayname: random.NextString( 5, 50 ),
				emailaddress: random.NextString( 5, 50 ),
				id: random.Next(),
				permissions: (byte)random.Next( 0, byte.MaxValue ),
				twitter: random.NextString( 1, 16 ),
				website: random.NextString( 16, 1000 ),
				sessiontoken: Guid.NewGuid(),
				sessionexpires: random.NextDateTime(),
				blog: random.NextString( 16, 1000 ),
				profilepicture: random.NextString( 16, 1000 ),
				sessionizeid: random.NextBool() ? (Guid?)Guid.NewGuid() : null
			);
	}
}
