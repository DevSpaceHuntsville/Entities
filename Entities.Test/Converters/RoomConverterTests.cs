using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class RoomJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = "[{'id':1,'displayname':'Display Name 1'},{'id':2,'displayname':'Display Name 2'},{'id':3,'displayname':'Display Name 3'}]";
			IEnumerable<Room> expected = Enumerable.Range( 1, 3 ).Select( CreateRoom );
			IEnumerable<Room> actual = JsonConvert.DeserializeObject<IEnumerable<Room>>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			string json = "{'displayname':'Display Name 1','id':1}";
			Room expected = CreateRoom( 1 );
			Room actual = JsonConvert.DeserializeObject<Room>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			IEnumerable<Room> data = Enumerable.Range( 1, 3 ).Select( CreateRoom );
			string expected = "[{\"id\":1,\"displayname\":\"Display Name 1\"},{\"id\":2,\"displayname\":\"Display Name 2\"},{\"id\":3,\"displayname\":\"Display Name 3\"}]";
			string actual = JsonConvert.SerializeObject( data );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			IEnumerable<Room> data = Enumerable.Range( 1, 3 ).Select( CreateRoom );
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
			string actual = JsonConvert.SerializeObject( data, Formatting.Indented );
			Assert.Equal( expected, actual );
		}

		internal static Room CreateRoom( int i ) =>
			new Room( i, $"Display Name {i}" );

		internal static string RoomToJson( Room x ) =>
			$"{{'id':{x.Id},'displayname':{( null == x.DisplayName ? "null" : $"'{x.DisplayName}'" )}}}";
	}
}
