using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class EventJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = "[{'id':2015,'name':'DevSpace 2015','startdate':'2026-05-09T00:00:00-05:00','enddate':'2031-11-14T00:00:00-06:00'},{'id':2016,'name':'DevSpace 2016','startdate':'2026-05-10T00:00:00-05:00','enddate':'2031-11-16T00:00:00-06:00'},{'id':2017,'name':'DevSpace 2017','startdate':'2026-05-11T00:00:00-05:00','enddate':'2031-11-18T00:00:00-06:00'},{'id':2018,'name':'DevSpace 2018','startdate':'2026-05-12T00:00:00-05:00','enddate':'2031-11-20T00:00:00-06:00'},{'id':2019,'name':'DevSpace 2019','startdate':'2026-05-13T00:00:00-05:00','enddate':'2031-11-22T00:00:00-06:00'},{'id':2020,'name':'DevSpace 2020','startdate':'2026-05-14T00:00:00-05:00','enddate':'2031-11-24T00:00:00-06:00'}]";
			IEnumerable<Event> expected = Enumerable.Range( 2015, 6 ).Select( CreateEvent );
			IEnumerable<Event> actual = JsonConvert.DeserializeObject<IEnumerable<Event>>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			string json = "{'enddate':'2031-11-14T00:00:00-06:00','name':'DevSpace 2015','id':2015,'startdate':'2026-05-09T00:00:00-05:00'}";
			Event expected = CreateEvent( 2015 );
			Event actual = JsonConvert.DeserializeObject<Event>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			IEnumerable<Event> data = Enumerable.Range( 2015, 6 ).Select( CreateEvent );
			string expected = "[{\"id\":2015,\"name\":\"DevSpace 2015\",\"startdate\":\"2026-05-09T00:00:00-05:00\",\"enddate\":\"2031-11-14T00:00:00-06:00\"},{\"id\":2016,\"name\":\"DevSpace 2016\",\"startdate\":\"2026-05-10T00:00:00-05:00\",\"enddate\":\"2031-11-16T00:00:00-06:00\"},{\"id\":2017,\"name\":\"DevSpace 2017\",\"startdate\":\"2026-05-11T00:00:00-05:00\",\"enddate\":\"2031-11-18T00:00:00-06:00\"},{\"id\":2018,\"name\":\"DevSpace 2018\",\"startdate\":\"2026-05-12T00:00:00-05:00\",\"enddate\":\"2031-11-20T00:00:00-06:00\"},{\"id\":2019,\"name\":\"DevSpace 2019\",\"startdate\":\"2026-05-13T00:00:00-05:00\",\"enddate\":\"2031-11-22T00:00:00-06:00\"},{\"id\":2020,\"name\":\"DevSpace 2020\",\"startdate\":\"2026-05-14T00:00:00-05:00\",\"enddate\":\"2031-11-24T00:00:00-06:00\"}]";
			string actual = JsonConvert.SerializeObject( data );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			IEnumerable<Event> data = Enumerable.Range( 2015, 6 ).Select( CreateEvent );
			string expected = @"[
  {
    ""id"": 2015,
    ""name"": ""DevSpace 2015"",
    ""startdate"": ""2026-05-09T00:00:00-05:00"",
    ""enddate"": ""2031-11-14T00:00:00-06:00""
  },
  {
    ""id"": 2016,
    ""name"": ""DevSpace 2016"",
    ""startdate"": ""2026-05-10T00:00:00-05:00"",
    ""enddate"": ""2031-11-16T00:00:00-06:00""
  },
  {
    ""id"": 2017,
    ""name"": ""DevSpace 2017"",
    ""startdate"": ""2026-05-11T00:00:00-05:00"",
    ""enddate"": ""2031-11-18T00:00:00-06:00""
  },
  {
    ""id"": 2018,
    ""name"": ""DevSpace 2018"",
    ""startdate"": ""2026-05-12T00:00:00-05:00"",
    ""enddate"": ""2031-11-20T00:00:00-06:00""
  },
  {
    ""id"": 2019,
    ""name"": ""DevSpace 2019"",
    ""startdate"": ""2026-05-13T00:00:00-05:00"",
    ""enddate"": ""2031-11-22T00:00:00-06:00""
  },
  {
    ""id"": 2020,
    ""name"": ""DevSpace 2020"",
    ""startdate"": ""2026-05-14T00:00:00-05:00"",
    ""enddate"": ""2031-11-24T00:00:00-06:00""
  }
]";
			string actual = JsonConvert.SerializeObject( data, Formatting.Indented );
			Assert.Equal( expected, actual );
		}

		internal static Event CreateEvent( int i ) =>
			new Event( i, $"DevSpace {i}", DateTime.Today.AddDays( i ), DateTime.Today.AddDays( i * 2 ) );

		internal static string EventToJson( Event x ) =>
			$"{{'id':{x.Id},'name':{( null == x.Name ? "null" : $"'{x.Name}'" )},'startdate':'{DateTimeToJsonString( x.StartDate )}','enddate':'{DateTimeToJsonString( x.EndDate )}'}}";

		internal static string DateTimeToJsonString( DateTime dt ) =>
			dt.ToString( "yyyy-MM-ddTHH:mm:sszzz" );
	}
}
