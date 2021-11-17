using System;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class UserTests : Helpers.StandardEntityTests<User> {
		[Fact]
		public void DefaultCtor() {
			User actual = new User();

			Assert.NotNull( actual );
			Assert.Equal( 0, actual.Id );
			Assert.Null( actual.DisplayName );
			Assert.Null( actual.Bio );
			Assert.Null( actual.EmailAddress);
			Assert.Null( actual.Twitter );
			Assert.Null( actual.Website );
			Assert.Null( actual.Blog );
			Assert.Null( actual.ProfilePicture );
			Assert.Null( actual.SessionizeId );
			Assert.Equal( 0, actual.Permissions );
			Assert.Equal( DateTime.MinValue, actual.SessionExpires );
			Assert.Equal( Guid.Empty, actual.SessionToken );
		}

		public override void ObjectToString() {
			User actual = Helpers.RandomEntity.User;
			Assert.Equal( actual.DisplayName, actual.ToString() );
		}
	}
}
