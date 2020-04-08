using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

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
			IEnumerable<Company> expected = Enumerable.Range( 2015, 6 )
				.Select( i =>
					new Company(
						id: i,
						name: $"DevSpace {i}",
						address: $"Address {i}",
						phone: $"(123) 456-{i}",
						website: $"https://www.website.com/{i}",
						twitter: $"@twit{i}"
					)
				);

			IEnumerable<Company> actual = JsonConvert.DeserializeObject<IEnumerable<Company>>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			string json = "{'twitter':'@twit2015','website':'https://www.website.com/2015','address':'Address 2015','phone':'(123) 456-2015','name':'DevSpace 2015','id':2015}";
			Company expected = new Company(
				id: 2015,
				name: $"DevSpace 2015",
				address: $"Address 2015",
				phone: $"(123) 456-2015",
				website: $"https://www.website.com/2015",
				twitter: $"@twit2015"
			);

			Company actual = JsonConvert.DeserializeObject<Company>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonDeserializer_NullTwitter() {
			string json = "{'id':2015,'name':'DevSpace 2015','address':'Address 2015','phone':'(123) 456-2015','website':'https://www.website.com/2015','twitter':null}";
			Company expected = new Company(
				id: 2015,
				name: $"DevSpace 2015",
				address: $"Address 2015",
				phone: $"(123) 456-2015",
				website: $"https://www.website.com/2015",
				twitter: null
			);
			Company actual = JsonConvert.DeserializeObject<Company>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonDeserializer_MissingTwitter() {
			string json = "{'id':2015,'name':'DevSpace 2015','address':'Address 2015','phone':'(123) 456-2015','website':'https://www.website.com/2015'}";
			Company expected = new Company(
				id: 2015,
				name: $"DevSpace 2015",
				address: $"Address 2015",
				phone: $"(123) 456-2015",
				website: $"https://www.website.com/2015",
				twitter: null
			);
			Company actual = JsonConvert.DeserializeObject<Company>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			string expected = @"[{""id"":2015,""name"":""DevSpace 2015"",""address"":""Address 2015"",""phone"":""(123) 456-2015"",""website"":""https://www.website.com/2015"",""twitter"":""@twit2015""},{""id"":2016,""name"":""DevSpace 2016"",""address"":""Address 2016"",""phone"":""(123) 456-2016"",""website"":""https://www.website.com/2016"",""twitter"":""@twit2016""},{""id"":2017,""name"":""DevSpace 2017"",""address"":""Address 2017"",""phone"":""(123) 456-2017"",""website"":""https://www.website.com/2017"",""twitter"":""@twit2017""},{""id"":2018,""name"":""DevSpace 2018"",""address"":""Address 2018"",""phone"":""(123) 456-2018"",""website"":""https://www.website.com/2018"",""twitter"":""@twit2018""},{""id"":2019,""name"":""DevSpace 2019"",""address"":""Address 2019"",""phone"":""(123) 456-2019"",""website"":""https://www.website.com/2019"",""twitter"":""@twit2019""},{""id"":2020,""name"":""DevSpace 2020"",""address"":""Address 2020"",""phone"":""(123) 456-2020"",""website"":""https://www.website.com/2020"",""twitter"":""@twit2020""}]";
			IEnumerable<Company> data = Enumerable.Range( 2015, 6 )
				.Select( i =>
					new Company(
						id: i,
						name: $"DevSpace {i}",
						address: $"Address {i}",
						phone: $"(123) 456-{i}",
						website: $"https://www.website.com/{i}",
						twitter: $"@twit{i}"
					)
				);

			string actual = JsonConvert.SerializeObject( data );
			Assert.Equal( expected, actual );
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
			IEnumerable<Company> data = Enumerable.Range( 2015, 6 )
				.Select( i =>
					new Company(
						id: i,
						name: $"DevSpace {i}",
						address: $"Address {i}",
						phone: $"(123) 456-{i}",
						website: $"https://www.website.com/{i}",
						twitter: $"@twit{i}"
					)
				);

			string actual = JsonConvert.SerializeObject( data, Formatting.Indented );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializer_IncludeNull() {
			string expected = @"{""id"":2015,""name"":""DevSpace 2015"",""address"":""Address 2015"",""phone"":""(123) 456-2015"",""website"":""https://www.website.com/2015"",""twitter"":null}";
			Company data = new Company(
				id: 2015,
				name: $"DevSpace 2015",
				address: $"Address 2015",
				phone: $"(123) 456-2015",
				website: $"https://www.website.com/2015",
				twitter: null
			);

			string actual = JsonConvert.SerializeObject(
				data,
				new JsonSerializerSettings {
					NullValueHandling = NullValueHandling.Include
				}
			);
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializer_IgnoreNull() {
			string expected = @"{""id"":2015,""name"":""DevSpace 2015"",""address"":""Address 2015"",""phone"":""(123) 456-2015"",""website"":""https://www.website.com/2015""}";
			Company data = new Company(
				id: 2015,
				name: $"DevSpace 2015",
				address: $"Address 2015",
				phone: $"(123) 456-2015",
				website: $"https://www.website.com/2015",
				twitter: null
			);

			string actual = JsonConvert.SerializeObject(
				data,
				new JsonSerializerSettings {
					NullValueHandling = NullValueHandling.Ignore
				}
			);
			Assert.Equal( expected, actual );
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
