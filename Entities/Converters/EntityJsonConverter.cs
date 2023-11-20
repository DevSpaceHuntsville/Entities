using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	public class EntityJsonConverter<T> : JsonConverter {
		public override bool CanConvert( Type objectType ) => 
			objectType.IsAssignableFrom( typeof( T ) );

		private static T With( T target, string prop, object value ) =>
			(T)target
				.GetType()
				.GetMethod( $"With{prop}", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase )
				.Invoke( target, new[] { Convert.ChangeType( value, target.GetType().GetField( prop, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase ).FieldType ) } );

		public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer ) {
			T output = (T)existingValue;

			if( null == output )
				output = default;

			if( null == output ) {
				ConstructorInfo ctor = typeof( T )
					.GetConstructors()
					.Single( ci => typeof( T ) == ci.GetParameters().First().ParameterType );

				output = (T)ctor
					.Invoke( new object[] { null } );
			}

			while( reader.Read() ) {
				switch( reader.TokenType ) {
					case JsonToken.StartObject:
						continue;

					case JsonToken.PropertyName:
						string prop = reader.Value.ToString();
						reader.Read();
						output = With( output, prop, reader.Value );
						break;

					default:
						break;
				}

				if( JsonToken.EndObject == reader.TokenType )
					break;
			}

			return output;
		}

		public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer ) {
			if( value is T o ) {
				writer.WriteStartObject();
				foreach( FieldInfo f in typeof( T ).GetFields() ) {
					//writer.WritePropertyName( f.Name.ToLowerInvariant() );
					//writer.WriteValue( f.GetValue( o ) );

					object v = f.GetValue( o );
					if( ( null != v ) ||
						NullValueHandling.Include == serializer.NullValueHandling ) {
						writer.WritePropertyName( f.Name.ToLowerInvariant() );
						writer.WriteValue( v );
					}
				}
				writer.WriteEndObject();
			} else {
				writer.WriteNull();
			}
		}
	}
}
