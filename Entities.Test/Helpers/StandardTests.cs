using System;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace DevSpace.Common.Entities.Test.Helpers {
	internal static class StandardTests {
		public static void CopyConstructor<T>() where T : class {
			T original = GetRandomEntity<T>();
			T copy = UseCopyConstructor( original );

			Assert.False( 
				ReferenceEquals( original, copy ),
				"Copy Constructor did not create unique reference"
			);

			typeof( T )
				.GetFields()
				.ToList()
				.ForEach( fi => AssertEqual(
					fi.GetValue( original ),
					fi.GetValue( copy ),
					$"Field {fi.Name} was different in the copy"
				) );

			typeof( T )
				.GetProperties()
				.ToList()
				.ForEach( pi => AssertEqual(
					pi.GetValue( original ),
					pi.GetValue( copy ),
					$"Property {pi.Name} was different in the copy"
				) );
		}

		public static void AllWith<T>() where T : class {
			T original = GetRandomEntity<T>();
			T different = GetDifferentEntity( original );

			foreach( MethodInfo mi in typeof( T ).GetMethods().Where( x => x.Name.StartsWith( "With" ) ) ) {
				FieldInfo fi = typeof( T ).GetField( mi.Name.Substring( 4 ) );
				T withed = mi.Invoke( original, new object[] { fi.GetValue( different ) } ) as T;

				Assert.False(
					ReferenceEquals( original, withed ),
					$"With{fi.Name} did not produce new reference"
				);

				AssertNotEqual(
					fi.GetValue( original ),
					fi.GetValue( withed ),
					$"With{fi.Name} did not get new value"
				);

				typeof( T )
					.GetFields()
					.Except( new[] { fi } )
					.ToList()
					.ForEach( f =>
						AssertEqual(
							f.GetValue( original ),
							f.GetValue( withed ),
							$"With{fi.Name} did not retain value for field {f.Name}"
						)
					);

				typeof( T )
					.GetProperties()
					.ToList()
					.ForEach( p =>
						AssertEqual(
							p.GetValue( original ),
							p.GetValue( withed ),
							$"With{fi.Name} did not retain value for field {p.Name}"
						)
					);
			}
		}

		public static void ObjectEquals<T>() where T : class {
			T actual = GetRandomEntity<T>() as T;
			Assert.False( actual.Equals( null ) );
			Assert.False( actual.Equals( new object() ) );
		}

		public static void ObjectGetHashCode<T>() where T : class {
			T original = GetRandomEntity<T>();
			T different = GetDifferentEntity( original );
			T copy = UseCopyConstructor<T>( original );

			AssertEqual(
				original.GetHashCode(),
				original.GetHashCode(),
				"GetHashCode is not determanistic"
			);

			AssertEqual(
				original.GetHashCode(),
				copy.GetHashCode(),
				"GetHashCode: copies do not create same hash code"
			);

			foreach( MethodInfo mi in typeof( T ).GetMethods().Where( x => x.Name.StartsWith( "With" ) ) ) {
				FieldInfo fi = typeof( T ).GetField( mi.Name.Substring( 4 ) );

				AssertNotEqual(
					original.GetHashCode(),
					mi.Invoke( original, new object[] { fi.GetValue( different ) } ).GetHashCode(),
					$"GetHashCode: Changed {fi.Name} did not get new value"
				);
			}
		}

		public static void IEquatableEquals<T>() where T : class {
			// left.Equals( null ) is false
			T left = GetRandomEntity<T>();
			Assert.False( left.Equals( (T)null ) );

			// relf-reference is true
			Assert.True( left.Equals( left ) );

			// Above cases don't force hash code creation
			Assert.Null(
				typeof( T )
					.GetField( "_hash", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance )
					.GetValue( left )
			);

			// hash code short circuit
			T right = UseCopyConstructor<T>( left );
			typeof( T )
				.GetField( "_hash", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance )
				.SetValue( right, left.GetHashCode() / 2 );
			Assert.False( left.Equals( right ) );

			// All values checked
			T different = GetDifferentEntity( left );
			right = UseCopyConstructor<T>( left );
			foreach( MethodInfo mi in typeof( T ).GetMethods().Where( x => x.Name.StartsWith( "With" ) ) ) {
				FieldInfo fi = typeof( T ).GetField( mi.Name.Substring( 4 ) );

				T asdf = mi.Invoke( left, new object[] { fi.GetValue( different ) } ) as T;
				asdf.SetPrivateField( "_hash", left.GetPrivateField( "_hash" ) );

				Assert.False(
					left.Equals( asdf ),
					$"IEqualable<{typeof( T ).Name}>.Equals: Changed {fi.Name} did not get make false"
				);
			}
		}

		public static void OperatorEquals<T>() where T : class {
			T left = GetRandomEntity<T>();
			T right = GetDifferentEntity( left );

			T copy = UseCopyConstructor( left );
			
			MethodInfo op =
				typeof( T )
					.GetMethod(
						"op_Equality",
						BindingFlags.Public | BindingFlags.Static
					);

			Assert.True( (bool)op.Invoke( null, new object[] { null, null } ), "null == null was false" );
			Assert.True( (bool)op.Invoke( null, new object[] { left, left } ), "left == left was false" );
			Assert.True( (bool)op.Invoke( null, new object[] { left, copy } ), "left == copy was false" );

			Assert.False( (bool)op.Invoke( null, new object[] { left, null } ), "left == null was true" );
			Assert.False( (bool)op.Invoke( null, new object[] { null, right } ), "null == right was true" );
			Assert.False( (bool)op.Invoke( null, new object[] { left, right } ), "left == right was true" );
		}

		public static void OperatorNotEquals<T>() where T : class {
			T left = GetRandomEntity<T>();
			T right = GetDifferentEntity( left );

			T copy = UseCopyConstructor( left );
			
			MethodInfo op =
				typeof( T )
					.GetMethod(
						"op_Inequality",
						BindingFlags.Public | BindingFlags.Static
					);

			Assert.False( (bool)op.Invoke( null, new object[] { null, null } ), "null != null was true" );
			Assert.False( (bool)op.Invoke( null, new object[] { left, left } ), "left != left was true" );
			Assert.False( (bool)op.Invoke( null, new object[] { left, copy } ), "left != copy was true" );

			Assert.True( (bool)op.Invoke( null, new object[] { left, null } ) , "left != null was false" );
			Assert.True( (bool)op.Invoke( null, new object[] { null, right } ), "null != right was false" );
			Assert.True( (bool)op.Invoke( null, new object[] { left, right } ), "left != right was false" );
		}

		private static void AssertEqual( object expected, object actual, string userMessage ) {
			try {
				Assert.Equal( expected, actual );
			} catch( EqualException ) {
				throw new AssertActualExpectedException(
					expected,
					actual,
					userMessage
				);
			}
		}

		private static void AssertNotEqual( object expected, object actual, string userMessage ) {
			try {
				Assert.NotEqual( expected, actual );
			} catch( NotEqualException ) {
				throw new AssertActualExpectedException(
					expected,
					actual,
					userMessage
				);
			}
		}

		private static T GetRandomEntity<T>() where T : class =>
			typeof( RandomEntity )
				.GetProperty(
					typeof( T ).Name,
					BindingFlags.Public | BindingFlags.Static
				)
				?.GetValue( null )
				as T
			?? throw new Exception( $"Could not find RandomEntity creator for {typeof( T ).Name}" );

		private static T GetDifferentEntity<T>( T original ) where T : class {
			T different = GetRandomEntity<T>();

			foreach( FieldInfo fi in typeof( T ).GetFields( BindingFlags.Public | BindingFlags.Instance ) ) {
				object originalValue = fi.GetValue( original );
				object differentValue = fi.GetValue( different );

				if( typeof( bool? ) == fi.FieldType ) {
					bool? o = (bool?)originalValue;
					differentValue = o.HasValue ? !o.Value : true;
				} else {
					while(
						originalValue?.Equals( differentValue )
						??
						null == differentValue
					) {
						differentValue = fi.GetValue( GetRandomEntity<T>() );
					}
				}

				MethodInfo with = typeof( T ).GetMethod( $"With{fi.Name}" );
				different = with.Invoke( different, new object[] { differentValue } ) as T;
			}

			return different;
		}

		private static T UseCopyConstructor<T>( T original ) where T : class =>
			typeof( T )
				.GetConstructor( new[] { typeof( T ) } )
				?.Invoke( new object[] { original } )
				as T
			?? throw new Exception( $"Could not find Copy Constructor for {typeof( T ).Name}" );
	}
}
