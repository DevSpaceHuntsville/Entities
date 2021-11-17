using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class UserJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = "[{'id':1,'emailaddress':'Email1@email.com','displayname':'Display Name 1','bio':'Bio 1','twitter':'twitter1','website':'https://www.website.com/1','blog':'https://www.blog.com/1','profilepicture':'https://www.picture.com/1','permissions':1,'sessiontoken':'00000001-0000-0000-0000-000000000001','sessionexpires':'2020-11-02T00:00:00Z','sessionizeid':null},{'id':2,'emailaddress':'Email2@email.com','displayname':'Display Name 2','bio':'Bio 2','twitter':'twitter2','website':'https://www.website.com/2','blog':'https://www.blog.com/2','profilepicture':'https://www.picture.com/2','permissions':2,'sessiontoken':'00000001-0000-0000-0000-000000000002','sessionexpires':'2020-11-03T00:00:00Z','sessionizeid':null},{'id':3,'emailaddress':'Email3@email.com','displayname':'Display Name 3','bio':'Bio 3','twitter':'twitter3','website':'https://www.website.com/3','blog':'https://www.blog.com/3','profilepicture':'https://www.picture.com/3','permissions':3,'sessiontoken':'00000001-0000-0000-0000-000000000003','sessionexpires':'2020-11-04T00:00:00Z','sessionizeid':null}]";
			IEnumerable<User> expected = Enumerable.Range( 1, 3 ).Select( i => CreateUser( (byte)i ) );
			IEnumerable<User> actual = JsonConvert.DeserializeObject<IEnumerable<User>>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			string json = "{'sessionexpires':'2020-11-02T00:00:00Z','blog':'https://www.blog.com/1','displayname':'Display Name 1','twitter':'twitter1','permissions':1,'website':'https://www.website.com/1','id':1,'profilepicture':'https://www.picture.com/1','bio':'Bio 1','sessiontoken':'00000001-0000-0000-0000-000000000001','sessionizeid':null,'emailaddress':'Email1@email.com'}";
			User expected = CreateUser( 1 );
			User actual = JsonConvert.DeserializeObject<User>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			IEnumerable<User> data = Enumerable.Range( 1, 3 ).Select(  i => CreateUser( (byte)i )  );
			string expected = "[{\"id\":1,\"emailaddress\":\"Email1@email.com\",\"displayname\":\"Display Name 1\",\"bio\":\"Bio 1\",\"twitter\":\"twitter1\",\"website\":\"https://www.website.com/1\",\"blog\":\"https://www.blog.com/1\",\"profilepicture\":\"https://www.picture.com/1\",\"permissions\":1,\"sessiontoken\":\"00000001-0000-0000-0000-000000000001\",\"sessionexpires\":\"2020-11-02T00:00:00Z\",\"sessionizeid\":null},{\"id\":2,\"emailaddress\":\"Email2@email.com\",\"displayname\":\"Display Name 2\",\"bio\":\"Bio 2\",\"twitter\":\"twitter2\",\"website\":\"https://www.website.com/2\",\"blog\":\"https://www.blog.com/2\",\"profilepicture\":\"https://www.picture.com/2\",\"permissions\":2,\"sessiontoken\":\"00000001-0000-0000-0000-000000000002\",\"sessionexpires\":\"2020-11-03T00:00:00Z\",\"sessionizeid\":null},{\"id\":3,\"emailaddress\":\"Email3@email.com\",\"displayname\":\"Display Name 3\",\"bio\":\"Bio 3\",\"twitter\":\"twitter3\",\"website\":\"https://www.website.com/3\",\"blog\":\"https://www.blog.com/3\",\"profilepicture\":\"https://www.picture.com/3\",\"permissions\":3,\"sessiontoken\":\"00000001-0000-0000-0000-000000000003\",\"sessionexpires\":\"2020-11-04T00:00:00Z\",\"sessionizeid\":null}]";
			string actual = JsonConvert.SerializeObject( data );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			IEnumerable<User> data = Enumerable.Range( 1, 3 ).Select(  i => CreateUser( (byte)i )  );
			string expected = @"[
  {
    ""id"": 1,
    ""emailaddress"": ""Email1@email.com"",
    ""displayname"": ""Display Name 1"",
    ""bio"": ""Bio 1"",
    ""twitter"": ""twitter1"",
    ""website"": ""https://www.website.com/1"",
    ""blog"": ""https://www.blog.com/1"",
    ""profilepicture"": ""https://www.picture.com/1"",
    ""permissions"": 1,
    ""sessiontoken"": ""00000001-0000-0000-0000-000000000001"",
    ""sessionexpires"": ""2020-11-02T00:00:00Z"",
    ""sessionizeid"": null
  },
  {
    ""id"": 2,
    ""emailaddress"": ""Email2@email.com"",
    ""displayname"": ""Display Name 2"",
    ""bio"": ""Bio 2"",
    ""twitter"": ""twitter2"",
    ""website"": ""https://www.website.com/2"",
    ""blog"": ""https://www.blog.com/2"",
    ""profilepicture"": ""https://www.picture.com/2"",
    ""permissions"": 2,
    ""sessiontoken"": ""00000001-0000-0000-0000-000000000002"",
    ""sessionexpires"": ""2020-11-03T00:00:00Z"",
    ""sessionizeid"": null
  },
  {
    ""id"": 3,
    ""emailaddress"": ""Email3@email.com"",
    ""displayname"": ""Display Name 3"",
    ""bio"": ""Bio 3"",
    ""twitter"": ""twitter3"",
    ""website"": ""https://www.website.com/3"",
    ""blog"": ""https://www.blog.com/3"",
    ""profilepicture"": ""https://www.picture.com/3"",
    ""permissions"": 3,
    ""sessiontoken"": ""00000001-0000-0000-0000-000000000003"",
    ""sessionexpires"": ""2020-11-04T00:00:00Z"",
    ""sessionizeid"": null
  }
]";
			string actual = JsonConvert.SerializeObject( data, Formatting.Indented );
			Assert.Equal( expected, actual );
		}

		internal static DateTime ChosenDate =>
			new DateTime( 2020, 11, 01, 00, 00, 00, DateTimeKind.Utc );

		internal static User CreateUser( byte i ) =>
			new User(
				i,
				$"Email{i}@email.com",
				$"Display Name {i}",
				$"Bio {i}",
				$"twitter{i}",
				$"https://www.website.com/{i}",
				$"https://www.blog.com/{i}",
				$"https://www.picture.com/{i}",
				i,
				CreateFixedGuid( 1, i ),
				ChosenDate.AddDays( i ),
				0x1 == i % 0x1 ? (Guid?)CreateFixedGuid( 2, i ) : null
			);
		
		private static Guid CreateFixedGuid( byte h, byte i ) =>
			new Guid( new byte[] { h, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, i } );

		internal static string UserToJson( User x ) =>
			$"{{'id':{x.Id},'emailaddress':'{x.EmailAddress}','displayname':'{x.DisplayName}','bio':'{x.Bio}','twitter':'{x.Twitter}','website':'{x.Website}','blog':'{x.Blog}','profilepicture':'{x.ProfilePicture}','permissions':{x.Permissions},'sessiontoken':'{x.SessionToken}','sessionexpires':'{x.SessionExpires:yyyy-MM-ddTHH:mm:ssZ}','sessionizeid':{(x.SessionizeId.HasValue ? $"'{x.SessionizeId}'" : "null")}}}";
	}
}
