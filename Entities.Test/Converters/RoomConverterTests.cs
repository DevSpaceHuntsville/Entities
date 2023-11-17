using Newtonsoft.Json;

namespace DevSpace.Common.Entities.Test {
	public class RoomJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = "[{'id':1,'displayname':'Display Name 1'},{'id':2,'displayname':'Display Name 2'},{'id':3,'displayname':'Display Name 3'}]";
			Assert.Equal(
				expected: Enumerable.Range( 1, 3 ).Select( CreateRoom ),
				actual: JsonConvert.DeserializeObject<IEnumerable<Room>>( json )
			);
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			Assert.Equal(
				expected: CreateRoom( 1 ),
				actual: JsonConvert.DeserializeObject<Room>(
					"{'displayname':'Display Name 1','id':1}"
				)
			);
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			string expected = "[{\"id\":1,\"displayname\":\"Display Name 1\"},{\"id\":2,\"displayname\":\"Display Name 2\"},{\"id\":3,\"displayname\":\"Display Name 3\"}]";
			Assert.Equal(
				expected,
				actual: JsonConvert.SerializeObject(
					Enumerable.Range( 1, 3 ).Select( CreateRoom )
				)
			);
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			string expected = @"[
  {
    ""id"": 1,
    ""displayname"": ""Display Name 1""
  },
  {
    ""id"": 2,
    ""displayname"": ""Display Name 2""
  },
  {
    ""id"": 3,
    ""displayname"": ""Display Name 3""
  }
]";
			Assert.Equal(
				expected,
				actual: JsonConvert.SerializeObject(
					Enumerable.Range( 1, 3 ).Select( CreateRoom ),
					Formatting.Indented
				)
			);
		}

		internal static Room CreateRoom( int i ) =>
			new Room( i, $"Display Name {i}" );

		internal static string RoomToJson( Room x ) =>
			$"{{'id':{x.Id},'displayname':{( null == x.DisplayName ? "null" : $"'{x.DisplayName}'" )}}}";
	}
}
