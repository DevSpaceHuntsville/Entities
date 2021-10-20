using System;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class ArticleTests : Helpers.StandardEntityTests<Article> {
		[Fact]
		public void DefaultCtor() {
			Article actual = new Article();

			Assert.NotNull( actual );
			Assert.Equal( 0, actual.Id );
			Assert.Null( actual.Title );
			Assert.Null( actual.Body );
			Assert.Equal( DateTime.MinValue, actual.PublishDate );
			Assert.Equal( DateTime.MinValue, actual.ExpireDate );
		}

		public override void ObjectToString() {
			Article actual = Helpers.RandomEntity.Article;
			Assert.Equal( actual.Title, actual.ToString() );
		}
	}
}
