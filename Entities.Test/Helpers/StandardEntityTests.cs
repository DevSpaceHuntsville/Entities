namespace DevSpace.Common.Entities.Test.Helpers {
	[System.Diagnostics.CodeAnalysis.SuppressMessage(
		"CodeQuality",
		"IDE0079:Remove unnecessary suppression",
		Justification = "Suppression is in xUnit Analyzer. Line 24 for details"
	)]
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

// Justification: This is being flagged here because it's abstract
//	Thus, the issue is that the super classes don't mark it
#pragma warning disable xUnit1013 // Public method should be marked as test
		[Fact]
		public abstract void ObjectToString();
#pragma warning restore xUnit1013 // Public method should be marked as test

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
