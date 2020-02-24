using Xunit;

namespace DevSpace.Common.Entities.Test.Helpers {
	public abstract class StandardEntityTests<T> where T : class {
		[Fact]
		public virtual void CopyCtor() =>
			StandardTests.CopyConstructor<T>();

		[Fact]
		public virtual void WithAll() =>
			StandardTests.AllWith<T>();

		[Fact]
		public virtual void ObjectEquals() =>
			StandardTests.ObjectEquals<T>();

		[Fact]
		public virtual void ObjectGetHashCode() =>
			StandardTests.ObjectGetHashCode<T>();

		[Fact]
		public abstract void ObjectToString();

		[Fact]
		public virtual void IEquatableEquals() =>
			StandardTests.IEquatableEquals<T>();

		[Fact]
		public virtual void OperatorEquals() =>
			StandardTests.OperatorEquals<T>();

		[Fact]
		public virtual void OperatorNotEquals() =>
			StandardTests.OperatorNotEquals<T>();
	}
}
