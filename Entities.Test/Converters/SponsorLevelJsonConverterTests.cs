using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class SponsorLevelJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			IEnumerable<SponsorLevel> expected = Enumerable.Range( 2015, 6 )
				.Select( i =>
					new SponsorLevel(
						id: i,
						displayorder: i,
						name: $"Sponsor Level {i}",
						cost: i,
						displayinemails: false,
						displayinsidebar: false,
						tickets: i,
						discount: i,
						preconemail: false,
						midconemail: false,
						postconemail: false
					)
				);

			string json = $"[{string.Join( ",", expected.Select( x => SponsorLevelToJson( x ) ) )}]";

			IEnumerable<SponsorLevel> actual = JsonConvert.DeserializeObject<IEnumerable<SponsorLevel>>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			SponsorLevel expected = CreateSponsorLevel( 2015 );
			string json = $@"{{'postconemail':{expected.PostConEmail.ToString().ToLower()},'midconemail':{expected.MidConEmail.ToString().ToLower()},'preconemail':{expected.PreConEmail.ToString().ToLower()},'cost':{expected.Cost},'displayinsidebar':{expected.DisplayInSidebar.ToString().ToLower()},'displayinemails':{expected.DisplayInEmails.ToString().ToLower()},'tickets':{expected.Tickets},'discount':{expected.Discount},'name':'{expected.Name}','displayorder':{expected.DisplayOrder},'id':{expected.Id}}}";;

			SponsorLevel actual = JsonConvert.DeserializeObject<SponsorLevel>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			IEnumerable<SponsorLevel> data = Enumerable.Range( 2015, 6 ).Select( i => CreateSponsorLevel( i ) );
			string expected = $"[{string.Join( ",", data.Select( x => SponsorLevelToJson( x ) ) )}]".Replace( '\'', '\"' );

			string actual = JsonConvert.SerializeObject( data );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			IEnumerable<SponsorLevel> data = Enumerable.Range( 2015, 6 ).Select( i => CreateSponsorLevel( i ) );
			string expected = "[\r\n" + string.Join( ",\r\n", data.Select( x => $@"  {{
    ""id"": {x.Id},
    ""displayorder"": {x.DisplayOrder},
    ""name"": ""{x.Name}"",
    ""cost"": {x.Cost},
    ""displayinemails"": {x.DisplayInEmails.ToString().ToLower()},
    ""displayinsidebar"": {x.DisplayInSidebar.ToString().ToLower()},
    ""tickets"": {x.Tickets},
    ""discount"": {x.Discount},
    ""preconemail"": {x.PreConEmail.ToString().ToLower()},
    ""midconemail"": {x.MidConEmail.ToString().ToLower()},
    ""postconemail"": {x.PostConEmail.ToString().ToLower()}
  }}" ) ) + "\r\n]";

			string actual = JsonConvert.SerializeObject( data, Formatting.Indented );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializer_IncludeNull() {
			SponsorLevel data = CreateSponsorLevel( 2015 ).WithName( null );
			string expected = SponsorLevelToJson( data ).Replace( '\'', '\"' );

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
			SponsorLevel data = CreateSponsorLevel( 2015 ).WithName( null );
			string expected = SponsorLevelToJson( data )
				.Replace( ",'name':null", string.Empty )
				.Replace( '\'', '\"' );

			string actual = JsonConvert.SerializeObject(
				data,
				new JsonSerializerSettings {
					NullValueHandling = NullValueHandling.Ignore
				}
			);
			Assert.Equal( expected, actual );
		}

		internal static SponsorLevel CreateSponsorLevel( int i ) =>
			new SponsorLevel(
				id: i,
				displayorder: i,
				name: $"Sponsor Level {i}",
				cost: i,
				displayinemails: ( i & 0x01 ) > 0,
				displayinsidebar: ( i & 0x02 ) > 0,
				tickets: i,
				discount: i,
				preconemail: ( i & 0x04 ) > 0,
				midconemail: ( i & 0x08 ) > 0,
				postconemail: ( i & 0x10 ) > 0
			);

		internal static string SponsorLevelToJson( SponsorLevel x ) =>
			$"{{'id':{x.Id},'displayorder':{x.DisplayOrder},'name':{(null == x.Name ? "null" : $"'{x.Name}'")},'cost':{x.Cost},'displayinemails':{x.DisplayInEmails.ToString().ToLower()},'displayinsidebar':{x.DisplayInSidebar.ToString().ToLower()},'tickets':{x.Tickets},'discount':{x.Discount},'preconemail':{x.PreConEmail.ToString().ToLower()},'midconemail':{x.MidConEmail.ToString().ToLower()},'postconemail':{x.PostConEmail.ToString().ToLower()}}}";
	}
}
