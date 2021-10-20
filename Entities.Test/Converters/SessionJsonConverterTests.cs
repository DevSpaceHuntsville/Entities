using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class SessionJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			IEnumerable<Session> expected = Enumerable.Range( 1, 6 ).Select( i => CreateSession( i ) );

			string json = $"[{string.Join( ",", expected.Select( x => SessionToJson( x ) ) )}]";

			IEnumerable<Session> actual = JsonConvert.DeserializeObject<IEnumerable<Session>>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			Session expected = CreateSession( 1 );
			string json = null;
			//$"{{" +
			//	$"'sponsoringCompany':{CompanyJsonConverterTests.CompanyToJson( expected.SessioningCompany )}," +
			//	$"'sponsoredEvent':{EventJsonConverterTests.EventToJson( expected.SessionedEvent )}," +
			//	$"'id':{expected.Id}," +
			//	$"'sponsorshipLevel':{SessionLevelJsonConverterTests.SessionLevelToJson( expected.SessionshipLevel )}" +
			//$"}}";

			Session actual = JsonConvert.DeserializeObject<Session>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			Session data = CreateSession( 1 );
			string actual = JsonConvert.SerializeObject( data );
			string expected = SessionToJson( data ).Replace( '\'', '\"' );
			Assert.Equal( expected, actual );
		}

		//[Fact]
		//public void JsonSerializerFormattingIndented() {
		//	IEnumerable<Session> data = Enumerable.Range( 1, 6 ).Select( i => CreateSession( i ) );
		//	string expected = "[\r\n" + string.Join( ",\r\n", data.Select( x => $@"  {{
  //  ""id"": {x.Id},
  //  ""sponsoredEvent"": {{
  //    ""id"": {x.SessionedEvent.Id},
  //    ""name"": ""{x.SessionedEvent.Name}"",
  //    ""startdate"": ""{EventJsonConverterTests.DateTimeToJsonString( x.SessionedEvent.StartDate )}"",
  //    ""enddate"": ""{EventJsonConverterTests.DateTimeToJsonString( x.SessionedEvent.EndDate )}""
  //  }},
  //  ""sponsoringCompany"": {{
  //    ""id"": {x.SessioningCompany.Id},
  //    ""name"": ""{x.SessioningCompany.Name}"",
  //    ""address"": ""{x.SessioningCompany.Address}"",
  //    ""phone"": ""{x.SessioningCompany.Phone}"",
  //    ""website"": ""{x.SessioningCompany.Website}"",
  //    ""twitter"": ""{x.SessioningCompany.Twitter}""
  //  }},
  //  ""sponsorshipLevel"": {{
  //    ""id"": {x.SessionshipLevel.Id},
  //    ""displayorder"": {x.SessionshipLevel.DisplayOrder},
  //    ""name"": ""{x.SessionshipLevel.Name}"",
  //    ""cost"": {x.SessionshipLevel.Cost},
  //    ""displaylink"": {x.SessionshipLevel.DisplayLink.ToString().ToLower()},
  //    ""displayinemails"": {x.SessionshipLevel.DisplayInEmails.ToString().ToLower()},
  //    ""displayinsidebar"": {x.SessionshipLevel.DisplayInSidebar.ToString().ToLower()},
  //    ""tickets"": {x.SessionshipLevel.Tickets},
  //    ""discount"": {x.SessionshipLevel.Discount},
  //    ""timeonscreen"": {x.SessionshipLevel.TimeOnScreen},
  //    ""preconemail"": {x.SessionshipLevel.PreConEmail.ToString().ToLower()},
  //    ""midconemail"": {x.SessionshipLevel.MidConEmail.ToString().ToLower()},
  //    ""postconemail"": {x.SessionshipLevel.PostConEmail.ToString().ToLower()}
  //  }}
  //}}" ) ) + "\r\n]";

		//	string actual = JsonConvert.SerializeObject( data, Formatting.Indented );
		//	Assert.Equal( expected, actual );
		//}

		private Session CreateSession( int i ) =>
			new Session(
				id: i,
				userid: 100 + i,
				title: $"Title {i}",
				@abstract: $"Abstract {i}",
				notes: $"Notes {i}",
				sessionlength: 0x1 == ( i & 0x1 ) ? 30 : 60,
				level: TagJsonConverterTests.CreateTag( ( i % 3 ) + 1 ),
				category: TagJsonConverterTests.CreateTag( ( i % 5 ) + 1 ),
				accepted: 0x1 == ( i & 0x1 ),
				tags: Enumerable.Range( 1, i ).Select( j => TagJsonConverterTests.CreateTag( j ) ),
				timeslot: TimeSlotJsonConverterTests.CreateTimeSlot( i ),
				room: RoomJsonConverterTests.CreateRoom( i ),
				eventid: 2020 + i,
				sessionizeid: 0x3 == ( i & 0x3 ) ? (int?)i : null
			);

		private string SessionToJson( Session x ) =>
			$"{{" +
				$"'id':{x.Id}," +
				$"'userId':{x.UserId}," +
				$"'title':'{x.Title}'," +
				$"'abstract':'{x.Abstract}'" +
				$"'notes':'{x.Notes}'," +
				$"'sessionlength':'{x.SessionLength}'," +
				$"'level':{TagJsonConverterTests.TagToJson( x.Level )}," +
				$"'category':{TagJsonConverterTests.TagToJson( x.Category )}," +
				$"'accepted':{(x.Accepted.HasValue ? x.Accepted.Value.ToString().ToLower() : "null")}," +
				$"'tags':[{string.Join( ",", x.Tags.Select( t => TagJsonConverterTests.TagToJson( t ) ) )}]," +
				$"'timeslot':{TimeSlotJsonConverterTests.TimeSlotToJson( x.TimeSlot )}," +
				$"'room':{RoomJsonConverterTests.RoomToJson( x.Room )}," +
				$"'eventid':{x.EventId}," +
				$"'sessionizeid':{(x.SessionizeId.HasValue ? x.SessionizeId.ToString() : "null" )}" +
			$"}}";
	}
}
