using System;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class EventTests {
		[Fact]
		public void DefaultCtor() {
			Event actual = new Event();

			Assert.NotNull( actual );
			Assert.Equal( 0, actual.Id );
			Assert.Null( actual.Name );
		}

		[Fact]
		public void CopyCtor() =>
			Helpers.StandardTests.CopyConstructor<Event>();

		[Fact]
		public void WithId() {
			Event expected = Helpers.RandomEntity.Event;
			Event actual = expected.WithId( expected.Id + 1 );

			Assert.NotEqual( expected, actual );
			Assert.NotEqual( expected.Id, actual.Id );
			Assert.Equal( expected.Name, actual.Name );
		}

		[Fact]
		public void WithName() {
			Event expected = Helpers.RandomEntity.Event;
			Event actual = expected.WithName( Helpers.RandomEntity.Event.Name );

			Assert.NotEqual( expected, actual );
			Assert.Equal( expected.Id, actual.Id );
			Assert.NotEqual( expected.Name, actual.Name );
		}

		[Fact]
		public void ObjectEquals() {
			Event actual = Helpers.RandomEntity.Event;
			Assert.False( actual.Equals( null ) );
			Assert.False( actual.Equals( new object() ) );
		}

		[Fact]
		public void ObjectGetHashCode() {
			Event actual = Helpers.RandomEntity.Event;
			int expected = actual.GetHashCode();

			Assert.Equal( expected, actual.GetHashCode() );
			Assert.Equal( expected, new Event( actual ).GetHashCode() );

			Assert.NotEqual( expected, new Event( actual ).WithId( actual.Id + 1 ).GetHashCode() );
			Assert.NotEqual( expected, new Event( actual ).WithName( Helpers.RandomEntity.Event.Name ).GetHashCode() );
		}

		[Fact]
		public void ObjectToString() {
			Event actual = Helpers.RandomEntity.Event;
			Assert.Equal( actual.Name, actual.ToString() );
		}

		[Fact]
		public void IEquatableEquals() {
			// left.Equals( null ) is false
			Event left = Helpers.RandomEntity.Event;
			Assert.False( left.Equals( (Event)null ) );

			// relf-reference is true
			Assert.True( left.Equals( left ) );

			// Above cases don't force hash code creation
			Assert.Null(
				typeof( Event )
					.GetField( "_hash", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance )
					.GetValue( left )
			);

			// hash code short circuit
			Event right = new Event( left );
			typeof( Event )
				.GetField( "_hash", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance )
				.SetValue( right, left.GetHashCode() / 2 );
			Assert.False( left.Equals( right ) );

			// All values checked
			right = new Event( left );
			Assert.False( left.Equals( right.WithId( left.Id + 1 ) ) );
			Assert.False( left.Equals( right.WithName( Helpers.RandomEntity.Event.Name ) ) );
		}

		[Fact]
		public void OperatorEquals() =>
			Helpers.StandardTests.OperatorEquals<Event>();

		[Fact]
		public void OperatorNotEquals() =>
			Helpers.StandardTests.OperatorNotEquals<Event>();
	}
}
