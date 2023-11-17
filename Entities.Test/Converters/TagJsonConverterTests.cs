using Newtonsoft.Json;

namespace DevSpace.Common.Entities.Test {
	public class TagJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = "[{'id':1,'text':'Text 1'},{'id':2,'text':'Text 2'},{'id':3,'text':'Text 3'}]";
			Assert.Equal(
				expected: Enumerable.Range( 1, 3 ).Select( CreateTag ),
				actual: JsonConvert.DeserializeObject<IEnumerable<Tag>>( json )
			);
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			Assert.Equal(
				expected: CreateTag( 1 ),
				actual: JsonConvert.DeserializeObject<Tag>( "{'text':'Text 1','id':1}" )
			);
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			Assert.Equal(
				"""[{"id":1,"text":"Text 1"},{"id":2,"text":"Text 2"},{"id":3,"text":"Text 3"}]""",
				JsonConvert.SerializeObject( Enumerable.Range( 1, 3 ).Select( CreateTag ) )
			);
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
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
			Assert.Equal(
				expected,
				actual: JsonConvert.SerializeObject(
					Enumerable.Range( 1, 3 ).Select( CreateTag ),
					Formatting.Indented
				)
			);
		}

		internal static Tag CreateTag( int i ) =>
			new Tag( i, $"Text {i}" );

		internal static string TagToJson( Tag x ) =>
			$"{{'id':{x.Id},'text':{( null == x.Text ? "null" : $"'{x.Text}'" )}}}";
	}
}
