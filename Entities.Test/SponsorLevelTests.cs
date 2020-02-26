using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class SponsorLevelTests : Helpers.StandardEntityTests<SponsorLevel> {
		[Fact]
		public void DefaultCtor() {
			SponsorLevel actual = new SponsorLevel();

			Assert.NotNull( actual );
			Assert.Equal( 0, actual.Id );
			Assert.Equal( 0, actual.DisplayOrder );
			Assert.Null( actual.Name );
			Assert.Equal( 0, actual.Cost );
			Assert.False( actual.DisplayInEmails );
			Assert.False( actual.DisplayInSidebar );
			Assert.Equal( 0, actual.Tickets );
			Assert.Equal( 0, actual.Discount );
			Assert.False( actual.PreConEmail );
			Assert.False( actual.MidConEmail );
			Assert.False( actual.PostConEmail );
		}

		public override void ObjectToString() {
			SponsorLevel actual = Helpers.RandomEntity.SponsorLevel;
			Assert.Equal( actual.Name, actual.ToString() );
		}
	}
}
