using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
				.ForEach( fi => {
					try {
						Assert.Equal(
							fi.GetValue( original ),
							fi.GetValue( copy )
						);
					} catch( EqualException ) {
						throw new AssertActualExpectedException(
							fi.GetValue( original ),
							fi.GetValue( copy ),
							$"Field {fi.Name} was different in the copy\nexpected"
						);
					}
				} );

			typeof( T )
				.GetProperties()
				.ToList()
				.ForEach( pi => {
					try {
						Assert.Equal(
							pi.GetValue( original ),
							pi.GetValue( copy )
						);
					} catch( EqualException ) {
						throw new AssertActualExpectedException(
							pi.GetValue( original ),
							pi.GetValue( copy ),
							$"Field {pi.Name} was different in the copy\nexpected"
						);
					}
				}  );
		}

		public static void OperatorEquals<T>() where T : class {
			T left = GetRandomEntity<T>();
			T right = GetRandomEntity<T>();

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
			T right = GetRandomEntity<T>();

			T copy = UseCopyConstructor( left );
			
			MethodInfo op =
				typeof( Event )
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

		private static T GetRandomEntity<T>() where T : class =>
			typeof( RandomEntity )
				.GetProperty(
					typeof( T ).Name,
					BindingFlags.Public | BindingFlags.Static
				)
				?.GetValue( null )
				as T
			?? throw new Exception( $"Could not find RandomEntity creator for {typeof( T ).Name}" );

		private static T UseCopyConstructor<T>( T original ) where T : class =>
			typeof( T )
				.GetConstructor( new[] { typeof( T ) } )
				?.Invoke( new object[] { original } )
				as T
			?? throw new Exception( $"Could not find Copy Constructor for {typeof( T ).Name}" );
	}
}
