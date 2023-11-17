using Newtonsoft.Json;

namespace DevSpace.Common.Entities.Test {
	public class CompanyJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = @"[
{'id':2015,'name':'DevSpace 2015','address':'Address 2015','phone':'(123) 456-2015','website':'https://www.website.com/2015','twitter':'@twit2015'},
{'id':2016,'name':'DevSpace 2016','address':'Address 2016','phone':'(123) 456-2016','website':'https://www.website.com/2016','twitter':'@twit2016'},
{'id':2017,'name':'DevSpace 2017','address':'Address 2017','phone':'(123) 456-2017','website':'https://www.website.com/2017','twitter':'@twit2017'},
{'id':2018,'name':'DevSpace 2018','address':'Address 2018','phone':'(123) 456-2018','website':'https://www.website.com/2018','twitter':'@twit2018'},
{'id':2019,'name':'DevSpace 2019','address':'Address 2019','phone':'(123) 456-2019','website':'https://www.website.com/2019','twitter':'@twit2019'},
{'id':2020,'name':'DevSpace 2020','address':'Address 2020','phone':'(123) 456-2020','website':'https://www.website.com/2020','twitter':'@twit2020'}
]";
			Assert.Equal(
				expected: Enumerable.Range( 2015, 6 ).Select( CreateCompany ),
				actual: JsonConvert.DeserializeObject<IEnumerable<Company>>( json )
			);
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			string json = "{'twitter':'@twit2015','website':'https://www.website.com/2015','address':'Address 2015','phone':'(123) 456-2015','name':'DevSpace 2015','id':2015}";
			Assert.Equal(
				expected: CreateCompany( 2015 ),
				actual: JsonConvert.DeserializeObject<Company>( json )
			);
		}

		[Fact]
		public void JsonDeserializer_NullTwitter() {
			string json = "{'id':2015,'name':'DevSpace 2015','address':'Address 2015','phone':'(123) 456-2015','website':'https://www.website.com/2015','twitter':null}";
			Assert.Equal(
				expected: CreateCompany( 2015 ).WithTwitter( null ),
				actual: JsonConvert.DeserializeObject<Company>( json )
			);
		}

		[Fact]
		public void JsonDeserializer_MissingTwitter() {
			string json = "{'id':2015,'name':'DevSpace 2015','address':'Address 2015','phone':'(123) 456-2015','website':'https://www.website.com/2015'}";
			Assert.Equal(
				expected: CreateCompany( 2015 ).WithTwitter( null ),
				actual: JsonConvert.DeserializeObject<Company>( json )
			);
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			string expected = @"[{""id"":2015,""name"":""DevSpace 2015"",""address"":""Address 2015"",""phone"":""(123) 456-2015"",""website"":""https://www.website.com/2015"",""twitter"":""@twit2015""},{""id"":2016,""name"":""DevSpace 2016"",""address"":""Address 2016"",""phone"":""(123) 456-2016"",""website"":""https://www.website.com/2016"",""twitter"":""@twit2016""},{""id"":2017,""name"":""DevSpace 2017"",""address"":""Address 2017"",""phone"":""(123) 456-2017"",""website"":""https://www.website.com/2017"",""twitter"":""@twit2017""},{""id"":2018,""name"":""DevSpace 2018"",""address"":""Address 2018"",""phone"":""(123) 456-2018"",""website"":""https://www.website.com/2018"",""twitter"":""@twit2018""},{""id"":2019,""name"":""DevSpace 2019"",""address"":""Address 2019"",""phone"":""(123) 456-2019"",""website"":""https://www.website.com/2019"",""twitter"":""@twit2019""},{""id"":2020,""name"":""DevSpace 2020"",""address"":""Address 2020"",""phone"":""(123) 456-2020"",""website"":""https://www.website.com/2020"",""twitter"":""@twit2020""}]";
			Assert.Equal(
				expected,
				actual: JsonConvert.SerializeObject(
					Enumerable.Range( 2015, 6 ).Select( CreateCompany )
				)
			);
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			string expected = @"[
  {
    ""id"": 2015,
    ""name"": ""DevSpace 2015"",
    ""address"": ""Address 2015"",
    ""phone"": ""(123) 456-2015"",
    ""website"": ""https://www.website.com/2015"",
    ""twitter"": ""@twit2015""
  },
  {
    ""id"": 2016,
    ""name"": ""DevSpace 2016"",
    ""address"": ""Address 2016"",
    ""phone"": ""(123) 456-2016"",
    ""website"": ""https://www.website.com/2016"",
    ""twitter"": ""@twit2016""
  },
  {
    ""id"": 2017,
    ""name"": ""DevSpace 2017"",
    ""address"": ""Address 2017"",
    ""phone"": ""(123) 456-2017"",
    ""website"": ""https://www.website.com/2017"",
    ""twitter"": ""@twit2017""
  },
  {
    ""id"": 2018,
    ""name"": ""DevSpace 2018"",
    ""address"": ""Address 2018"",
    ""phone"": ""(123) 456-2018"",
    ""website"": ""https://www.website.com/2018"",
    ""twitter"": ""@twit2018""
  },
  {
    ""id"": 2019,
    ""name"": ""DevSpace 2019"",
    ""address"": ""Address 2019"",
    ""phone"": ""(123) 456-2019"",
    ""website"": ""https://www.website.com/2019"",
    ""twitter"": ""@twit2019""
  },
  {
    ""id"": 2020,
    ""name"": ""DevSpace 2020"",
    ""address"": ""Address 2020"",
    ""phone"": ""(123) 456-2020"",
    ""website"": ""https://www.website.com/2020"",
    ""twitter"": ""@twit2020""
  }
]";
			Assert.Equal(
				expected,
				actual: JsonConvert.SerializeObject(
					Enumerable.Range( 2015, 6 ).Select( CreateCompany ),
					Formatting.Indented
				)
			);
		}

		[Fact]
		public void JsonSerializer_IncludeNull() {
			string expected = @"{""id"":2015,""name"":""DevSpace 2015"",""address"":""Address 2015"",""phone"":""(123) 456-2015"",""website"":""https://www.website.com/2015"",""twitter"":null}";
			Assert.Equal(
				expected,
				actual: JsonConvert.SerializeObject(
					CreateCompany( 2015 ).WithTwitter( null ),
					new JsonSerializerSettings {
						NullValueHandling = NullValueHandling.Include
					}
				)
			);
		}

		[Fact]
		public void JsonSerializer_IgnoreNull() {
			string expected = @"{""id"":2015,""name"":""DevSpace 2015"",""address"":""Address 2015"",""phone"":""(123) 456-2015"",""website"":""https://www.website.com/2015""}";
			Assert.Equal(
				expected,
				actual: JsonConvert.SerializeObject(
					CreateCompany( 2015 ).WithTwitter( null ),
					new JsonSerializerSettings {
						NullValueHandling = NullValueHandling.Ignore
					}
				)
			);
		}

		internal static Company CreateCompany( int i ) =>
			new Company(
				id: i,
				name: $"DevSpace {i}",
				address: $"Address {i}",
				phone: $"(123) 456-{i}",
				website: $"https://www.website.com/{i}",
				twitter: $"@twit{i}"
			);

		internal static string CompanyToJson( Company x ) =>
			$"{{" +
				$"'id':{x.Id}," +
				$"'name':{( null == x.Name ? "null" : $"'{x.Name}'" )}," +
				$"'address':{( null == x.Address ? "null" : $"'{x.Address}'" )}," +
				$"'phone':{( null == x.Phone ? "null" : $"'{x.Phone}'" )}," +
				$"'website':{( null == x.Website ? "null" : $"'{x.Website}'" )}," +
				$"'twitter':{( null == x.Twitter ? "null" : $"'{x.Twitter}'" )}" +
			$"}}";
	}
}
