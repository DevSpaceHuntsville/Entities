using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class RoomTests : Helpers.StandardEntityTests<Room> {
		[Fact]
		public void DefaultCtor() {
			Room actual = new Room();

			Assert.NotNull( actual );
			Assert.Equal( 0, actual.Id );
			Assert.Null( actual.DisplayName );
		}

		public override void ObjectToString() {
			Room actual = Helpers.RandomEntity.Room;
			Assert.Equal( actual.DisplayName, actual.ToString() );
		}
	}
}
