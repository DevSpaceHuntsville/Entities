using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class TimeSlotJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = "[{'id':1,'starttime':'2020-11-02T00:00:00Z','endtime':'2020-11-02T01:00:00Z'},{'id':2,'starttime':'2020-11-03T00:00:00Z','endtime':'2020-11-03T02:00:00Z'},{'id':3,'starttime':'2020-11-04T00:00:00Z','endtime':'2020-11-04T03:00:00Z'}]";
			IEnumerable<TimeSlot> expected = Enumerable.Range( 1, 3 ).Select( CreateTimeSlot );
			IEnumerable<TimeSlot> actual = JsonConvert.DeserializeObject<IEnumerable<TimeSlot>>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			string json = "{'endtime':'2020-11-02T01:00:00Z','id':1,'starttime':'2020-11-02T00:00:00Z'}";
			TimeSlot expected = CreateTimeSlot( 1 );
			TimeSlot actual = JsonConvert.DeserializeObject<TimeSlot>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			IEnumerable<TimeSlot> data = Enumerable.Range( 1, 3 ).Select( CreateTimeSlot );
			string expected = "[{\"id\":1,\"starttime\":\"2020-11-02T00:00:00Z\",\"endtime\":\"2020-11-02T01:00:00Z\"},{\"id\":2,\"starttime\":\"2020-11-03T00:00:00Z\",\"endtime\":\"2020-11-03T02:00:00Z\"},{\"id\":3,\"starttime\":\"2020-11-04T00:00:00Z\",\"endtime\":\"2020-11-04T03:00:00Z\"}]";
			string actual = JsonConvert.SerializeObject( data );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			IEnumerable<TimeSlot> data = Enumerable.Range( 1, 3 ).Select( CreateTimeSlot );
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
			string actual = JsonConvert.SerializeObject( data, Formatting.Indented );
			Assert.Equal( expected, actual );
		}

		internal static DateTime ChosenDate =>
			new DateTime( 2020, 11, 01, 00, 00, 00, DateTimeKind.Utc );

		internal static TimeSlot CreateTimeSlot( int i ) =>
			new TimeSlot( i, ChosenDate.AddDays( i ), ChosenDate.AddDays( i ).AddHours( i ) );

		internal static string TimeSlotToJson( TimeSlot x ) =>
			$"{{'id':{x.Id},'starttime':'{x.StartTime:yyyy-MM-ddTHH:mm:ssZ}','endtime':'{x.EndTime:yyyy-MM-ddTHH:mm:ssZ}'}}";
	}
}
