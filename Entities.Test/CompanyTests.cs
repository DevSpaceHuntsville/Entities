using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class CompanyTests : Helpers.StandardEntityTests<Company> {
		[Fact]
		public void DefaultCtor() {
			Company actual = new Company();

			Assert.NotNull( actual );
			Assert.Equal( 0, actual.Id );
			Assert.Null( actual.Name );
			Assert.Null( actual.Address );
			Assert.Null( actual.Phone );
			Assert.Null( actual.Website );
			Assert.Null( actual.Twitter );
		}

		public override void ObjectToString() {
			Company actual = Helpers.RandomEntity.Company;
			Assert.Equal( actual.Name, actual.ToString() );
		}
	}
}
