using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class EventJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = "[{'id':2015,'name':'DevSpace 2015'},{'id':2016,'name':'DevSpace 2016'},{'id':2017,'name':'DevSpace 2017'},{'id':2018,'name':'DevSpace 2018'},{'id':2019,'name':'DevSpace 2019'},{'id':2020,'name':'DevSpace 2020'}]";
			IEnumerable<Event> expected = Enumerable.Range( 2015, 6 ).Select( i => new Event( i, $"DevSpace {i}" ) );
			IEnumerable<Event> actual = JsonConvert.DeserializeObject<IEnumerable<Event>>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			string json = "{'name':'DevSpace 2015','id':2015}";
			Event expected = new Event( 2015, "DevSpace 2015" );
			Event actual = JsonConvert.DeserializeObject<Event>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			IEnumerable<Event> data = Enumerable.Range( 2015, 6 ).Select( i => new Event( i, $"DevSpace {i}" ) );
			string expected = @"[{""id"":2015,""name"":""DevSpace 2015""},{""id"":2016,""name"":""DevSpace 2016""},{""id"":2017,""name"":""DevSpace 2017""},{""id"":2018,""name"":""DevSpace 2018""},{""id"":2019,""name"":""DevSpace 2019""},{""id"":2020,""name"":""DevSpace 2020""}]";
			string actual = JsonConvert.SerializeObject( data );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			IEnumerable<Event> data = Enumerable.Range( 2015, 6 ).Select( i => new Event( i, $"DevSpace {i}" ) );
			string expected = @"[
  {
    ""id"": 2015,
    ""name"": ""DevSpace 2015""
  },
  {
    ""id"": 2016,
    ""name"": ""DevSpace 2016""
  },
  {
    ""id"": 2017,
    ""name"": ""DevSpace 2017""
  },
  {
    ""id"": 2018,
    ""name"": ""DevSpace 2018""
  },
  {
    ""id"": 2019,
    ""name"": ""DevSpace 2019""
  },
  {
    ""id"": 2020,
    ""name"": ""DevSpace 2020""
  }
]";
			string actual = JsonConvert.SerializeObject( data, Formatting.Indented );
			Assert.Equal( expected, actual );
		}

	}
}
