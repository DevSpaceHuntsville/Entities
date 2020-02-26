using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class SponsorTests : Helpers.StandardEntityTests<Sponsor> {
		[Fact]
		public void DefaultCtor() {
			Sponsor actual = new Sponsor();

			Assert.NotNull( actual );
			Assert.Equal( 0, actual.Id );
			Assert.Equal( new Event(), actual.SponsoredEvent );
			Assert.Equal( new Company(), actual.SponsoringCompany );
			Assert.Equal( new SponsorLevel(), actual.SponsorshipLevel );
		}

		public override void ObjectToString() {
			Sponsor actual = Helpers.RandomEntity.Sponsor;
			Assert.Equal( 
				$"{actual.SponsoringCompany.Name} is a {actual.SponsorshipLevel.Name} Sponsor of {actual.SponsoredEvent.Name}",
				actual.ToString()
			);
		}
	}
}
