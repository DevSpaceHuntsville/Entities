using Newtonsoft.Json;

namespace DevSpace.Common.Entities.Test {
	public class TimeSlotJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = "[{'id':1,'starttime':'2020-11-02T00:00:00Z','endtime':'2020-11-02T01:00:00Z'},{'id':2,'starttime':'2020-11-03T00:00:00Z','endtime':'2020-11-03T02:00:00Z'},{'id':3,'starttime':'2020-11-04T00:00:00Z','endtime':'2020-11-04T03:00:00Z'}]";
			Assert.Equal(
				expected: Enumerable.Range( 1, 3 ).Select( CreateTimeSlot ),
				actual: JsonConvert.DeserializeObject<IEnumerable<TimeSlot>>( json )
			);
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			string json = "{'endtime':'2020-11-02T01:00:00Z','id':1,'starttime':'2020-11-02T00:00:00Z'}";
			Assert.Equal(
				expected: CreateTimeSlot( 1 ),
				actual: JsonConvert.DeserializeObject<TimeSlot>( json )
			);
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			Assert.Equal(
				expected: """[{"id":1,"starttime":"2020-11-02T00:00:00Z","endtime":"2020-11-02T01:00:00Z"},{"id":2,"starttime":"2020-11-03T00:00:00Z","endtime":"2020-11-03T02:00:00Z"},{"id":3,"starttime":"2020-11-04T00:00:00Z","endtime":"2020-11-04T03:00:00Z"}]""",
				JsonConvert.SerializeObject( Enumerable.Range( 1, 3 ).Select( CreateTimeSlot ) )
			);
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			string expected = @"[
  {
    ""id"": 1,
    ""starttime"": ""2020-11-02T00:00:00Z"",
    ""endtime"": ""2020-11-02T01:00:00Z""
  },
  {
    ""id"": 2,
    ""starttime"": ""2020-11-03T00:00:00Z"",
    ""endtime"": ""2020-11-03T02:00:00Z""
  },
  {
    ""id"": 3,
    ""starttime"": ""2020-11-04T00:00:00Z"",
    ""endtime"": ""2020-11-04T03:00:00Z""
  }
]";
			Assert.Equal(
				expected,
				actual: JsonConvert.SerializeObject(
					Enumerable.Range( 1, 3 ).Select( CreateTimeSlot ),
					Formatting.Indented
				)
			);
		}

		internal static DateTime ChosenDate =>
			new DateTime( 2020, 11, 01, 00, 00, 00, DateTimeKind.Utc );

		internal static TimeSlot CreateTimeSlot( int i ) =>
			new TimeSlot( i, ChosenDate.AddDays( i ), ChosenDate.AddDays( i ).AddHours( i ) );

		internal static string TimeSlotToJson( TimeSlot x ) =>
			$"{{'id':{x.Id},'starttime':'{x.StartTime:yyyy-MM-ddTHH:mm:ssZ}','endtime':'{x.EndTime:yyyy-MM-ddTHH:mm:ssZ}'}}";
	}
}
