using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class SessionTests : Helpers.StandardEntityTests<Session> {
		[Fact]
		public void DefaultCtor() {
			Session actual = new Session();

			Assert.NotNull( actual );
			Assert.Equal( 0, actual.Id );
			Assert.Equal( 0, actual.UserId );
			Assert.Null( actual.Title );
			Assert.Null( actual.Abstract );
			Assert.Null( actual.Notes );
			Assert.Equal( 0, actual.SessionLength );
			Assert.Null( actual.Level );
			Assert.Null( actual.Category );
			Assert.Null( actual.Accepted );
			Assert.Empty( actual.Tags );
			Assert.Null( actual.TimeSlot );
			Assert.Null( actual.Room );
			Assert.Equal( 0, actual.EventId );
			Assert.Null( actual.SessionizeId );
		}

		[Fact]
		public override void ObjectToString() {
			Session actual = Helpers.RandomEntity.Session;
			Assert.Equal( 
				actual.Title,
				actual.ToString()
			);
		}
	}
}
