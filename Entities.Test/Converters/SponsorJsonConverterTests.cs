using Newtonsoft.Json;

namespace DevSpace.Common.Entities.Test {
	public class SponsorJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			string json = $"[{string.Join( ",", Enumerable.Range( 2015, 6 ).Select( CreateSponsor ).Select( x => SponsorToJson( x ) ) )}]";
			Assert.Equal(
				expected: Enumerable.Range( 2015, 6 ).Select( CreateSponsor ),
				actual: JsonConvert.DeserializeObject<IEnumerable<Sponsor>>( json )
			);
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			string json = $"{{" +
				$"'sponsoringCompany':{CompanyJsonConverterTests.CompanyToJson( CreateSponsor( 2015 ).SponsoringCompany )}," +
				$"'sponsoredEvent':{EventJsonConverterTests.EventToJson( CreateSponsor( 2015 ).SponsoredEvent )}," +
				$"'id':{CreateSponsor( 2015 ).Id}," +
				$"'sponsorshipLevel':{SponsorLevelJsonConverterTests.SponsorLevelToJson( CreateSponsor( 2015 ).SponsorshipLevel )}" +
			$"}}";
			Assert.Equal(
				expected: CreateSponsor( 2015 ),
				actual: JsonConvert.DeserializeObject<Sponsor>( json )
			);
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			Assert.Equal(
				expected: SponsorToJson( CreateSponsor( 1 ) ).Replace( '\'', '\"' ),
				actual: JsonConvert.SerializeObject( CreateSponsor( 1 ) ),
				ignoreCase: false,
				ignoreLineEndingDifferences: true,
				ignoreWhiteSpaceDifferences: true
			);
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			string expected = "[\r\n" + string.Join( ",\r\n", Enumerable.Range( 2015, 6 ).Select( CreateSponsor ).Select( x => $@"  {{
    ""id"": {x.Id},
    ""sponsoredEvent"": {{
      ""id"": {x.SponsoredEvent.Id},
      ""name"": ""{x.SponsoredEvent.Name}"",
      ""startdate"": ""{EventJsonConverterTests.DateTimeToJsonString( x.SponsoredEvent.StartDate )}"",
      ""enddate"": ""{EventJsonConverterTests.DateTimeToJsonString( x.SponsoredEvent.EndDate )}""
    }},
    ""sponsoringCompany"": {{
      ""id"": {x.SponsoringCompany.Id},
      ""name"": ""{x.SponsoringCompany.Name}"",
      ""address"": ""{x.SponsoringCompany.Address}"",
      ""phone"": ""{x.SponsoringCompany.Phone}"",
      ""website"": ""{x.SponsoringCompany.Website}"",
      ""twitter"": ""{x.SponsoringCompany.Twitter}""
    }},
    ""sponsorshipLevel"": {{
      ""id"": {x.SponsorshipLevel.Id},
      ""displayorder"": {x.SponsorshipLevel.DisplayOrder},
      ""name"": ""{x.SponsorshipLevel.Name}"",
      ""cost"": {x.SponsorshipLevel.Cost},
      ""displaylink"": {x.SponsorshipLevel.DisplayLink.ToString().ToLower()},
      ""displayinemails"": {x.SponsorshipLevel.DisplayInEmails.ToString().ToLower()},
      ""displayinsidebar"": {x.SponsorshipLevel.DisplayInSidebar.ToString().ToLower()},
      ""tickets"": {x.SponsorshipLevel.Tickets},
      ""discount"": {x.SponsorshipLevel.Discount},
      ""timeonscreen"": {x.SponsorshipLevel.TimeOnScreen},
      ""preconemail"": {x.SponsorshipLevel.PreConEmail.ToString().ToLower()},
      ""midconemail"": {x.SponsorshipLevel.MidConEmail.ToString().ToLower()},
      ""postconemail"": {x.SponsorshipLevel.PostConEmail.ToString().ToLower()}
    }}
  }}" ) ) + "\r\n]";
			Assert.Equal(
				expected,
				actual: JsonConvert.SerializeObject(
					Enumerable.Range( 2015, 6 ).Select( CreateSponsor ),
					Formatting.Indented
				),
				ignoreCase: false,
				ignoreLineEndingDifferences: true,
				ignoreWhiteSpaceDifferences: true
			);
		}

		private Sponsor CreateSponsor( int i ) =>
			new Sponsor(
				id: i,
				sponsoredevent: EventJsonConverterTests.CreateEvent( i ),
				sponsoringcompany: CompanyJsonConverterTests.CreateCompany( i ),
				sponsorshiplevel: SponsorLevelJsonConverterTests.CreateSponsorLevel( i )
			);

		private string SponsorToJson( Sponsor x ) =>
			$"{{" +
				$"'id':{x.Id}," +
				$"'sponsoredEvent':{EventJsonConverterTests.EventToJson( x.SponsoredEvent )}," +
				$"'sponsoringCompany':{CompanyJsonConverterTests.CompanyToJson( x.SponsoringCompany )}," +
				$"'sponsorshipLevel':{SponsorLevelJsonConverterTests.SponsorLevelToJson( x.SponsorshipLevel )}" +
			$"}}";
	}
}
