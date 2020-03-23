using System;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	public class EventJsonConverter : JsonConverter {
		public override bool CanConvert( Type objectType ) =>
			typeof( Event ) == objectType;

		public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer ) {
			if( "Event" != objectType.Name )
				return null;

			Event e = existingValue as Event ?? new Event();

			int id = default;
			string name = default;

			while( reader.Read() ) {
				switch( reader.TokenType ) {
					case JsonToken.StartObject:
						continue;

					case JsonToken.PropertyName:
						if( "id".Equals( reader.Value.ToString(), StringComparison.InvariantCultureIgnoreCase ) )
							id = reader.ReadAsInt32().GetValueOrDefault();
						else
							name = reader.ReadAsString();

						break;

					default:
						break;
				}

				if( JsonToken.EndObject == reader.TokenType )
					break;
			}

			return e
				.WithId( id )
				.WithName( name );
		}

		public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer ) {
			if( value is Event e ) {
				writer.WriteStartObject();
					writer.WritePropertyName( nameof( e.Id ).ToLowerInvariant() );
					writer.WriteValue( e.Id );

					writer.WritePropertyName( nameof( e.Name ).ToLowerInvariant() );
					writer.WriteValue( e.Name );
				writer.WriteEndObject();
			} else {
				writer.WriteNull();
			}
		}
	}
}
