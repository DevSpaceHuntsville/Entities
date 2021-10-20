using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class ArticleJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = "[{'id':1,'title':'Title 1','body':'Body 1','publishdate':'2020-11-02T00:00:00Z','expiredate':'2020-11-03T00:00:00Z'},{'id':2,'title':'Title 2','body':'Body 2','publishdate':'2020-11-03T00:00:00Z','expiredate':'2020-11-05T00:00:00Z'},{'id':3,'title':'Title 3','body':'Body 3','publishdate':'2020-11-04T00:00:00Z','expiredate':'2020-11-07T00:00:00Z'}]";
			IEnumerable<Article> expected = Enumerable.Range( 1, 3 ).Select( CreateArticle );
			IEnumerable<Article> actual = JsonConvert.DeserializeObject<IEnumerable<Article>>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			string json = "{'expiredate':'2020-11-03T00:00:00Z','title':'Title 1','id':1,'publishdate':'2020-11-02T00:00:00Z','body':'Body 1'}";
			Article expected = CreateArticle( 1 );
			Article actual = JsonConvert.DeserializeObject<Article>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			IEnumerable<Article> data = Enumerable.Range( 1, 3 ).Select( CreateArticle );
			string expected = "[{\"id\":1,\"title\":\"Title 1\",\"body\":\"Body 1\",\"publishdate\":\"2020-11-02T00:00:00Z\",\"expiredate\":\"2020-11-03T00:00:00Z\"},{\"id\":2,\"title\":\"Title 2\",\"body\":\"Body 2\",\"publishdate\":\"2020-11-03T00:00:00Z\",\"expiredate\":\"2020-11-05T00:00:00Z\"},{\"id\":3,\"title\":\"Title 3\",\"body\":\"Body 3\",\"publishdate\":\"2020-11-04T00:00:00Z\",\"expiredate\":\"2020-11-07T00:00:00Z\"}]";
			string actual = JsonConvert.SerializeObject( data );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			IEnumerable<Article> data = Enumerable.Range( 1, 3 ).Select( CreateArticle );
			string expected = @"[
  {
    ""id"": 1,
    ""title"": ""Title 1"",
    ""body"": ""Body 1"",
    ""publishdate"": ""2020-11-02T00:00:00Z"",
    ""expiredate"": ""2020-11-03T00:00:00Z""
  },
  {
    ""id"": 2,
    ""title"": ""Title 2"",
    ""body"": ""Body 2"",
    ""publishdate"": ""2020-11-03T00:00:00Z"",
    ""expiredate"": ""2020-11-05T00:00:00Z""
  },
  {
    ""id"": 3,
    ""title"": ""Title 3"",
    ""body"": ""Body 3"",
    ""publishdate"": ""2020-11-04T00:00:00Z"",
    ""expiredate"": ""2020-11-07T00:00:00Z""
  }
]";
			string actual = JsonConvert.SerializeObject( data, Formatting.Indented );
			Assert.Equal( expected, actual );
		}

		internal static DateTime ChosenDate =>
			new DateTime( 2020, 11, 01, 00, 00, 00, DateTimeKind.Utc );

		internal static Article CreateArticle( int i ) =>
			new Article( i, $"Title {i}", $"Body {i}", ChosenDate.AddDays( i ), ChosenDate.AddDays( i * 2 ) );

		internal static string ArticleToJson( Article x ) =>
			$"{{'id':{x.Id},'title':{( null == x.Title ? "null" : $"'{x.Title}'" )},'body':{( null == x.Body ? "null" : $"'{x.Body}'" )},'publishdate':'{DateTimeToJsonString( x.PublishDate)}','expiredate':'{DateTimeToJsonString( x.ExpireDate )}'}}";

		internal static string DateTimeToJsonString( DateTime dt ) =>
			dt.ToString( "yyyy-MM-ddTHH:mm:ssZ" );
	}
}
