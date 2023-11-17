﻿using Newtonsoft.Json;

namespace DevSpace.Common.Entities.Test {
	public class EventJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = "[{'id':2015,'name':'DevSpace 2015','startdate':'2026-05-09T00:00:00Z','enddate':'2031-11-14T00:00:00Z'},{'id':2016,'name':'DevSpace 2016','startdate':'2026-05-10T00:00:00Z','enddate':'2031-11-16T00:00:00Z'},{'id':2017,'name':'DevSpace 2017','startdate':'2026-05-11T00:00:00Z','enddate':'2031-11-18T00:00:00Z'},{'id':2018,'name':'DevSpace 2018','startdate':'2026-05-12T00:00:00Z','enddate':'2031-11-20T00:00:00Z'},{'id':2019,'name':'DevSpace 2019','startdate':'2026-05-13T00:00:00Z','enddate':'2031-11-22T00:00:00Z'},{'id':2020,'name':'DevSpace 2020','startdate':'2026-05-14T00:00:00Z','enddate':'2031-11-24T00:00:00Z'}]";
			Assert.Equal(
				expected: Enumerable.Range( 2015, 6 ).Select( CreateEvent ),
				actual: JsonConvert.DeserializeObject<IEnumerable<Event>>( json )
			);
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			string json = "{'enddate':'2031-11-14T00:00:00Z','name':'DevSpace 2015','id':2015,'startdate':'2026-05-09T00:00:00Z'}";
			Assert.Equal(
				expected: CreateEvent( 2015 ),
				actual: JsonConvert.DeserializeObject<Event>( json )
			);
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			string expected = "[{\"id\":2015,\"name\":\"DevSpace 2015\",\"startdate\":\"2026-05-09T00:00:00Z\",\"enddate\":\"2031-11-14T00:00:00Z\"},{\"id\":2016,\"name\":\"DevSpace 2016\",\"startdate\":\"2026-05-10T00:00:00Z\",\"enddate\":\"2031-11-16T00:00:00Z\"},{\"id\":2017,\"name\":\"DevSpace 2017\",\"startdate\":\"2026-05-11T00:00:00Z\",\"enddate\":\"2031-11-18T00:00:00Z\"},{\"id\":2018,\"name\":\"DevSpace 2018\",\"startdate\":\"2026-05-12T00:00:00Z\",\"enddate\":\"2031-11-20T00:00:00Z\"},{\"id\":2019,\"name\":\"DevSpace 2019\",\"startdate\":\"2026-05-13T00:00:00Z\",\"enddate\":\"2031-11-22T00:00:00Z\"},{\"id\":2020,\"name\":\"DevSpace 2020\",\"startdate\":\"2026-05-14T00:00:00Z\",\"enddate\":\"2031-11-24T00:00:00Z\"}]";
			Assert.Equal(
				expected,
				actual: JsonConvert.SerializeObject(
					Enumerable.Range( 2015, 6 ).Select( CreateEvent )
				)
			);
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			string expected = @"[
  {
    ""id"": 2015,
    ""name"": ""DevSpace 2015"",
    ""startdate"": ""2026-05-09T00:00:00Z"",
    ""enddate"": ""2031-11-14T00:00:00Z""
  },
  {
    ""id"": 2016,
    ""name"": ""DevSpace 2016"",
    ""startdate"": ""2026-05-10T00:00:00Z"",
    ""enddate"": ""2031-11-16T00:00:00Z""
  },
  {
    ""id"": 2017,
    ""name"": ""DevSpace 2017"",
    ""startdate"": ""2026-05-11T00:00:00Z"",
    ""enddate"": ""2031-11-18T00:00:00Z""
  },
  {
    ""id"": 2018,
    ""name"": ""DevSpace 2018"",
    ""startdate"": ""2026-05-12T00:00:00Z"",
    ""enddate"": ""2031-11-20T00:00:00Z""
  },
  {
    ""id"": 2019,
    ""name"": ""DevSpace 2019"",
    ""startdate"": ""2026-05-13T00:00:00Z"",
    ""enddate"": ""2031-11-22T00:00:00Z""
  },
  {
    ""id"": 2020,
    ""name"": ""DevSpace 2020"",
    ""startdate"": ""2026-05-14T00:00:00Z"",
    ""enddate"": ""2031-11-24T00:00:00Z""
  }
]";
			Assert.Equal(
				expected,
				actual: JsonConvert.SerializeObject(
					Enumerable.Range( 2015, 6 ).Select( CreateEvent ),
					Formatting.Indented
				)
			);
		}

		internal static DateTime ChosenDate =>
			new DateTime( 2020, 11, 01, 00, 00, 00, DateTimeKind.Utc );

		internal static Event CreateEvent( int i ) =>
			new Event( i, $"DevSpace {i}", ChosenDate.AddDays( i ), ChosenDate.AddDays( i * 2 ) );

		internal static string EventToJson( Event x ) =>
			$"{{'id':{x.Id},'name':{( null == x.Name ? "null" : $"'{x.Name}'" )},'startdate':'{DateTimeToJsonString( x.StartDate )}','enddate':'{DateTimeToJsonString( x.EndDate )}'}}";

		internal static string DateTimeToJsonString( DateTime dt ) =>
			dt.ToString( "yyyy-MM-ddTHH:mm:ssZ" );
	}
}
