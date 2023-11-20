using System.Reflection;
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
				FieldInfo fi = typeof( T ).GetField( mi.Name.Substring( 4 ) )
					?? throw new XunitException( $"Could not fiend field to match {mi.Name}" );
				T? withed = mi.Invoke( original, new object?[] { fi.GetValue( different ) } ) as T;

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
			T actual = GetRandomEntity<T>();
			Assert.False( actual.Equals( null ) );
			Assert.False( actual.Equals( new object() ) );
		}

		public static void ObjectGetHashCode<T>() where T : class {
			T original = GetRandomEntity<T>();
			T different = GetDifferentEntity( original );
			T copy = UseCopyConstructor( original );

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
				FieldInfo fi = typeof( T ).GetField( mi.Name.Substring( 4 ) )
					?? throw new XunitException( $"Could not fiend field to match {mi.Name}" );

				AssertNotEqual(
					original.GetHashCode(),
					mi.Invoke( original, new object?[] { fi.GetValue( different ) } )?.GetHashCode(),
					$"GetHashCode: Changed {fi.Name} did not get new value"
				);
			}
		}

		public static void IEquatableEquals<T>() where T : class {
			// left.Equals( null ) is false
			T left = GetRandomEntity<T>();
			Assert.False( left is null );

			// relf-reference is true
			Assert.True( left.Equals( left ) );

			// Above cases don't force hash code creation
			FieldInfo hashFieldInfo = typeof( T )
				.GetField( "_hash", BindingFlags.NonPublic | BindingFlags.Instance )
				?? throw new XunitException( $"Could not find private _hash field" );

			Assert.Null( hashFieldInfo.GetValue( left ) );

			// hash code short circuit
			T right = UseCopyConstructor( left );
			hashFieldInfo.SetValue( right, left.GetHashCode() / 2 );
			Assert.False( left.Equals( right ) );

			// All values checked
			T different = GetDifferentEntity( left );
			right = UseCopyConstructor( left );
			foreach( MethodInfo witherMethodInfo in typeof( T ).GetMethods().Where( x => x.Name.StartsWith( "With" ) ) ) {
				FieldInfo fi = typeof( T ).GetField( witherMethodInfo.Name.Substring( 4 ) )
					?? throw new XunitException( $"Could not fiend field to match {witherMethodInfo.Name}" );

				T asdf = witherMethodInfo.Invoke( left, new object?[] { fi.GetValue( different ) } ) as T
					?? throw new XunitException( $"Wither returned null value" );
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
					)
					?? throw new XunitException( $"Could not find op_Equality" );

			Assert.True( (bool?)op.Invoke( null, new object?[] { null, null } ) ?? false, "null == null was false" );
			Assert.True( (bool?)op.Invoke( null, new object?[] { left, left } ) ?? false, "left == left was false" );
			Assert.True( (bool?)op.Invoke( null, new object?[] { left, copy } ) ?? false, "left == copy was false" );

			Assert.False( (bool?)op.Invoke( null, new object?[] { left, null } )  ?? true, "left == null was true" );
			Assert.False( (bool?)op.Invoke( null, new object?[] { null, right } ) ?? true, "null == right was true" );
			Assert.False( (bool?)op.Invoke( null, new object?[] { left, right } ) ?? true, "left == right was true" );
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
					)
					?? throw new XunitException( $"Could not find op_Inequality" );

			Assert.False( (bool?)op.Invoke( null, new object?[] { null, null } ) ?? true, "null != null was true" );
			Assert.False( (bool?)op.Invoke( null, new object?[] { left, left } ) ?? true, "left != left was true" );
			Assert.False( (bool?)op.Invoke( null, new object?[] { left, copy } ) ?? true, "left != copy was true" );

			Assert.True( (bool?)op.Invoke( null, new object?[] { left, null } )  ?? false, "left != null was false" );
			Assert.True( (bool?)op.Invoke( null, new object?[] { null, right } ) ?? false, "null != right was false" );
			Assert.True( (bool?)op.Invoke( null, new object?[] { left, right } ) ?? false, "left != right was false" );
		}

		private static void AssertEqual( object? expected, object? actual, string userMessage ) {
			try {
				Assert.Equal( expected, actual );
			} catch( EqualException ) {
				throw EqualException.ForMismatchedValues( 
					expected,
					actual,
					userMessage
				);
			}
		}

		private static void AssertNotEqual( object? expected, object? actual, string userMessage ) {
			try {
				Assert.NotEqual( expected, actual );
			} catch( NotEqualException ) {
				throw NotEqualException.ForEqualValues(
					expected?.ToString() ?? "<NULL>",
					actual?.ToString() ?? "<NULL>",
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
				object? originalValue = fi.GetValue( original );
				object? differentValue = fi.GetValue( different );

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

				MethodInfo with = typeof( T ).GetMethod( $"With{fi.Name}" )
					?? throw new XunitException( $"Could not find wither to match field {fi.Name}" );
				different = with.Invoke( different, new object?[] { differentValue } ) as T
					?? throw new XunitException( $"Wither returned null value" );
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
