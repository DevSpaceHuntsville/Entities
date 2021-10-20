using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class TagTests : Helpers.StandardEntityTests<Tag> {
		[Fact]
		public void DefaultCtor() {
			Tag actual = new Tag();

			Assert.NotNull( actual );
			Assert.Equal( 0, actual.Id );
			Assert.Null( actual.Text );
		}

		public override void ObjectToString() {
			Tag actual = Helpers.RandomEntity.Tag;
			Assert.Equal( actual.Text, actual.ToString() );
		}
	}
}
