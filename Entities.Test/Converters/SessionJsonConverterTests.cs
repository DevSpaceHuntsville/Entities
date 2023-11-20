using Newtonsoft.Json;

namespace DevSpace.Common.Entities.Test {
	public class SessionJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = $"[{string.Join( ",", Enumerable.Range( 1, 6 ).Select( CreateSession ).Select( x => SessionToJson( x ) ) )}]";
			Assert.Equal(
				expected: Enumerable.Range( 1, 6 ).Select( CreateSession ),
				JsonConvert.DeserializeObject<IEnumerable<Session>>( json )
			);
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			string json = @"{
	'notes': 'Notes 1',
	'title': 'Title 1',
	'userId': 101,
	'sessionizeId': null,
	'category': {
		'id': 2,
		'text': 'Text 2'
	},
	'tags': [
		{
			'id': 1,
			'text': 'Text 1'
		}
	],
	'level': {
		'id': 2,
		'text': 'Text 2'
	},
	'id': 1,
	'accepted': true,
	'room': {
		'id': 1,
		'displayname': 'Display Name 1'
	},
	'timeSlot': {
		'id': 1,
		'starttime': '11/2/2020 12: 00: 00 AM',
		'endtime': '11/2/2020 1: 00: 00 AM'
	},
	'eventId': 2021,
	'sessionLength': 30,
	'abstract': 'Abstract 1'
}";
			Assert.Equal(
				expected: CreateSession( 1 ),
				actual: JsonConvert.DeserializeObject<Session>( json )
			);
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			Assert.Equal(
				expected: SessionToJson( CreateSession( 1 ) ).Replace( '\'', '\"' ),
				actual: JsonConvert.SerializeObject( CreateSession( 1 ) ),
				ignoreCase: false,
				ignoreLineEndingDifferences: true,
				ignoreWhiteSpaceDifferences: true
			);
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			string expected = "[\r\n" + string.Join( ",\r\n", Enumerable.Range( 1, 6 ).Select( CreateSession ).Select( x => $@"  {{
    ""id"": {x.Id},
    ""userId"": {x.UserId},
    ""title"": ""{x.Title}"",
    ""abstract"": ""{x.Abstract}"",
    ""notes"": ""{x.Notes}"",
    ""sessionLength"": {x.SessionLength},
    ""level"": {{
      ""id"": {x.Level.Id},
      ""text"": ""{x.Level.Text}""
    }},
    ""category"": {{
      ""id"": {x.Category.Id},
      ""text"": ""{x.Category.Text}""
    }},
    ""accepted"": {x.Accepted?.ToString().ToLower() ?? "null"},
    ""tags"": [
{string.Join( @",
", x.Tags.Select( t => $@"      {{
        ""id"": {t.Id},
        ""text"": ""{t.Text}""
      }}" ) )}
    ],
    ""timeSlot"": {{
      ""id"": {x.TimeSlot.Id},
      ""starttime"": ""{x.TimeSlot.StartTime:yyyy-MM-ddTHH:mm:ssZ}"",
      ""endtime"": ""{x.TimeSlot.EndTime:yyyy-MM-ddTHH:mm:ssZ}""
    }},
    ""room"": {{
      ""id"": {x.Room.Id},
      ""displayname"": ""{x.Room.DisplayName}""
    }},
    ""eventId"": {x.EventId},
    ""sessionizeId"": {x.SessionizeId?.ToString() ?? "null"}
  }}" ) ) + "\r\n]";
			Assert.Equal(
				expected,
				actual: JsonConvert.SerializeObject(
					Enumerable.Range( 1, 6 ).Select( i => CreateSession( i ) ),
					Formatting.Indented
				),
				ignoreCase: false,
				ignoreLineEndingDifferences: true,
				ignoreWhiteSpaceDifferences: true
			);
		}

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

		private static string SessionToJson( Session x ) =>
			$"{{" +
				$"'id':{x.Id}," +
				$"'userId':{x.UserId}," +
				$"'title':'{x.Title}'," +
				$"'abstract':'{x.Abstract}'," +
				$"'notes':'{x.Notes}'," +
				$"'sessionLength':{x.SessionLength}," +
				$"'level':{TagJsonConverterTests.TagToJson( x.Level )}," +
				$"'category':{TagJsonConverterTests.TagToJson( x.Category )}," +
				$"'accepted':{(x.Accepted.HasValue ? x.Accepted.Value.ToString().ToLower() : "null")}," +
				$"'tags':[{string.Join( ",", x.Tags.Select( t => TagJsonConverterTests.TagToJson( t ) ) )}]," +
				$"'timeSlot':{TimeSlotJsonConverterTests.TimeSlotToJson( x.TimeSlot )}," +
				$"'room':{RoomJsonConverterTests.RoomToJson( x.Room )}," +
				$"'eventId':{x.EventId}," +
				$"'sessionizeId':{(x.SessionizeId.HasValue ? x.SessionizeId.ToString() : "null" )}" +
			$"}}";
	}
}
