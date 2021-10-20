using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class TagJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = "[{'id':1,'text':'Text 1'},{'id':2,'text':'Text 2'},{'id':3,'text':'Text 3'}]";
			IEnumerable<Tag> expected = Enumerable.Range( 1, 3 ).Select( CreateTag );
			IEnumerable<Tag> actual = JsonConvert.DeserializeObject<IEnumerable<Tag>>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			string json = "{'text':'Text 1','id':1}";
			Tag expected = CreateTag( 1 );
			Tag actual = JsonConvert.DeserializeObject<Tag>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			IEnumerable<Tag> data = Enumerable.Range( 1, 3 ).Select( CreateTag );
			string expected = "[{\"id\":1,\"text\":\"Text 1\"},{\"id\":2,\"text\":\"Text 2\"},{\"id\":3,\"text\":\"Text 3\"}]";
			string actual = JsonConvert.SerializeObject( data );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			IEnumerable<Tag> data = Enumerable.Range( 1, 3 ).Select( CreateTag );
			string expected = @"[
  {
    ""id"": 1,
    ""text"": ""Text 1""
  },
  {
    ""id"": 2,
    ""text"": ""Text 2""
  },
  {
    ""id"": 3,
    ""text"": ""Text 3""
  }
]";
			string actual = JsonConvert.SerializeObject( data, Formatting.Indented );
			Assert.Equal( expected, actual );
		}

		internal static Tag CreateTag( int i ) =>
			new Tag( i, $"Text {i}" );

		internal static string TagToJson( Tag x ) =>
			$"{{'id':{x.Id},'text':{( null == x.Text ? "null" : $"'{x.Text}'" )}}}";
	}
}
