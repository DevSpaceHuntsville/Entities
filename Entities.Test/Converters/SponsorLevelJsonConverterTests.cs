using Newtonsoft.Json;

namespace DevSpace.Common.Entities.Test {
	public class SponsorLevelJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			IEnumerable<SponsorLevel> expected = Enumerable.Range( 2015, 6 )
				.Select( CreateSponsorLevel );
			Assert.Equal(
				expected,
				actual: JsonConvert.DeserializeObject<IEnumerable<SponsorLevel>>(
					$"[{string.Join( ",", expected.Select( SponsorLevelToJson ) )}]"
				)
			);
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			SponsorLevel expected = CreateSponsorLevel( 2015 );
			string json = $@"{{'postconemail':{expected.PostConEmail.ToString().ToLower()},'midconemail':{expected.MidConEmail.ToString().ToLower()},'preconemail':{expected.PreConEmail.ToString().ToLower()},'cost':{expected.Cost},'displayinsidebar':{expected.DisplayInSidebar.ToString().ToLower()},'displayinemails':{expected.DisplayInEmails.ToString().ToLower()},'tickets':{expected.Tickets},'discount':{expected.Discount},'name':'{expected.Name}','displayorder':{expected.DisplayOrder},'id':{expected.Id},'timeonscreen':{expected.TimeOnScreen},'displaylink':{expected.DisplayLink.ToString().ToLower()}}}";
			
			Assert.Equal(
				expected,
				actual: JsonConvert.DeserializeObject<SponsorLevel>( json )
			);
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			Assert.Equal(
				expected: $"[{string.Join( ",", Enumerable.Range( 2015, 6 ).Select( CreateSponsorLevel ).Select( SponsorLevelToJson ) )}]".Replace( '\'', '\"' ),
				actual: JsonConvert.SerializeObject( Enumerable.Range( 2015, 6 ).Select( CreateSponsorLevel ) ),
				ignoreCase: false,
				ignoreLineEndingDifferences: true,
				ignoreWhiteSpaceDifferences: true
			);
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			IEnumerable<SponsorLevel> data = Enumerable.Range( 2015, 6 ).Select( CreateSponsorLevel );
			string expected = "[\r\n" + string.Join( ",\r\n", data.Select( x => $@"  {{
    ""id"": {x.Id},
    ""displayorder"": {x.DisplayOrder},
    ""name"": ""{x.Name}"",
    ""cost"": {x.Cost},
    ""displaylink"": {x.DisplayLink.ToString().ToLower()},
    ""displayinemails"": {x.DisplayInEmails.ToString().ToLower()},
    ""displayinsidebar"": {x.DisplayInSidebar.ToString().ToLower()},
    ""tickets"": {x.Tickets},
    ""discount"": {x.Discount},
    ""timeonscreen"": {x.TimeOnScreen},
    ""preconemail"": {x.PreConEmail.ToString().ToLower()},
    ""midconemail"": {x.MidConEmail.ToString().ToLower()},
    ""postconemail"": {x.PostConEmail.ToString().ToLower()}
  }}" ) ) + "\r\n]";
			Assert.Equal(
				expected,
				actual: JsonConvert.SerializeObject( data, Formatting.Indented ),
				ignoreCase: false,
				ignoreLineEndingDifferences: true,
				ignoreWhiteSpaceDifferences: true
			);
		}

		[Fact]
		public void JsonSerializer_IncludeNull() {
			SponsorLevel data = CreateSponsorLevel( 2015 ).WithName( null );
			Assert.Equal(
				expected: SponsorLevelToJson( data ).Replace( '\'', '\"' ),
				actual: JsonConvert.SerializeObject(
					data,
					new JsonSerializerSettings {
						NullValueHandling = NullValueHandling.Include
					}
				),
				ignoreCase: false,
				ignoreLineEndingDifferences: true,
				ignoreWhiteSpaceDifferences: true
			);
		}

		[Fact]
		public void JsonSerializer_IgnoreNull() {
			SponsorLevel data = CreateSponsorLevel( 2015 ).WithName( null );
			Assert.Equal(
				expected: SponsorLevelToJson( data )
					.Replace( ",'name':null", string.Empty )
					.Replace( '\'', '\"' ),
				actual: JsonConvert.SerializeObject(
					data,
					new JsonSerializerSettings {
						NullValueHandling = NullValueHandling.Ignore
					}
				),
				ignoreCase: false,
				ignoreLineEndingDifferences: true,
				ignoreWhiteSpaceDifferences: true
			);
		}

		internal static SponsorLevel CreateSponsorLevel( int i ) =>
			new SponsorLevel(
				id: i,
				displayorder: i,
				name: $"Sponsor Level {i}",
				cost: i,
				displaylink: ( i & 0x20 ) > 0,
				displayinemails: ( i & 0x01 ) > 0,
				displayinsidebar: ( i & 0x02 ) > 0,
				tickets: i,
				discount: i,
				timeonscreen: i,
				preconemail: ( i & 0x04 ) > 0,
				midconemail: ( i & 0x08 ) > 0,
				postconemail: ( i & 0x10 ) > 0
			);

		internal static string SponsorLevelToJson( SponsorLevel x ) =>
			$"{{'id':{x.Id},'displayorder':{x.DisplayOrder},'name':{(null == x.Name ? "null" : $"'{x.Name}'")},'cost':{x.Cost},'displaylink':{x.DisplayLink.ToString().ToLower()},'displayinemails':{x.DisplayInEmails.ToString().ToLower()},'displayinsidebar':{x.DisplayInSidebar.ToString().ToLower()},'tickets':{x.Tickets},'discount':{x.Discount},'timeonscreen':{x.TimeOnScreen},'preconemail':{x.PreConEmail.ToString().ToLower()},'midconemail':{x.MidConEmail.ToString().ToLower()},'postconemail':{x.PostConEmail.ToString().ToLower()}}}";
	}
}
