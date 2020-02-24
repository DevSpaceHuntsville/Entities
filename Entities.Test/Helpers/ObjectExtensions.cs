using System.Reflection;

namespace DevSpace.Common.Entities.Test.Helpers {
	public static class ObjectExtensions {
		public static object GetPrivateField( this object obj, string fieldName ) =>
			obj.GetType()
				.GetField(
					fieldName,
					BindingFlags.Instance | BindingFlags.NonPublic
				)
				.GetValue( obj );

		public static T GetPrivateField<T>( this object obj, string fieldName ) =>
			(T)obj.GetType()
				.GetField(
					fieldName,
					BindingFlags.Instance | BindingFlags.NonPublic
				)
				.GetValue( obj );

		public static void SetPublicField( this object obj, string fieldName, object value ) =>
			obj.GetType()
				.GetField(
					fieldName,
					BindingFlags.Instance | BindingFlags.Public
				)
				.SetValue( obj, value );

		public static void SetPrivateField( this object obj, string fieldName, object value ) =>
			obj.GetType()
				.GetField(
					fieldName,
					BindingFlags.Instance | BindingFlags.NonPublic
				)
				.SetValue( obj, value );

		public static void SetPublicProperty( this object obj, string propertyName, object value ) =>
			obj.GetType()
				.GetProperty(
					propertyName,
					BindingFlags.Instance | BindingFlags.Public
				)
				.SetValue( obj, value );

		public static void SetPrivateProperty( this object obj, string propertyName, object value ) =>
			obj.GetType()
				.GetProperty(
					propertyName,
					BindingFlags.Instance | BindingFlags.NonPublic
				)
				.SetValue( obj, value );
	}
}
