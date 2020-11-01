using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class SponsorJsonConverterTests {
		[Fact]
		public void JsonDeserializer() {
			IEnumerable<Sponsor> expected = Enumerable.Range( 2015, 6 ).Select( i => CreateSponsor( i ) );

			string json = $"[{string.Join( ",", expected.Select( x => SponsorToJson( x ) ) )}]";

			IEnumerable<Sponsor> actual = JsonConvert.DeserializeObject<IEnumerable<Sponsor>>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonDeserializer_ItemsOutOfOrder() {
			Sponsor expected = CreateSponsor( 2015 );
			string json = $"{{" +
				$"'sponsoringCompany':{CompanyJsonConverterTests.CompanyToJson( expected.SponsoringCompany )}," +
				$"'sponsoredEvent':{EventJsonConverterTests.EventToJson( expected.SponsoredEvent )}," +
				$"'id':{expected.Id}," +
				$"'sponsorshipLevel':{SponsorLevelJsonConverterTests.SponsorLevelToJson( expected.SponsorshipLevel )}" +
			$"}}";

			Sponsor actual = JsonConvert.DeserializeObject<Sponsor>( json );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingNone() {
			Sponsor data = CreateSponsor( 1 );
			string actual = JsonConvert.SerializeObject( data );
			string expected = SponsorToJson( data ).Replace( '\'', '\"' );
			Assert.Equal( expected, actual );
		}

		[Fact]
		public void JsonSerializerFormattingIndented() {
			IEnumerable<Sponsor> data = Enumerable.Range( 2015, 6 ).Select( i => CreateSponsor( i ) );
			string expected = "[\r\n" + string.Join( ",\r\n", data.Select( x => $@"  {{
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

			string actual = JsonConvert.SerializeObject( data, Formatting.Indented );
			Assert.Equal( expected, actual );
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
