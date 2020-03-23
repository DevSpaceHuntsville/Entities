using System;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	public class CompanyJsonConverter : JsonConverter {
		public override bool CanConvert( Type objectType ) =>
			typeof( Company ) == objectType;

		public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer ) {
			if( "Company" != objectType.Name )
				return null;

			Company c = existingValue as Company ?? new Company();

			int id = default;
			string name = default;
			string address = default;
			string phone = default;
			string website = default;
			string twitter = default;

			while( reader.Read() ) {
				switch( reader.TokenType ) {
					case JsonToken.StartObject:
						continue;

					case JsonToken.PropertyName:
						if( "id".Equals( reader.Value.ToString(), StringComparison.InvariantCultureIgnoreCase ) )
							id = reader.ReadAsInt32().GetValueOrDefault();
						else
							switch( reader.Value.ToString().ToLowerInvariant() ) {
								case "name":
									name = reader.ReadAsString();
									break;

								case "address":
									address = reader.ReadAsString();
									break;

								case "phone":
									phone = reader.ReadAsString();
									break;

								case "website":
									website = reader.ReadAsString();
									break;

								case "twitter":
									twitter = reader.ReadAsString();
									break;

								default:
									reader.Read();
									break;
							}
							
						break;

					default:
						break;
				}

				if( JsonToken.EndObject == reader.TokenType )
					break;
			}

			return c
				.WithId( id )
				.WithName( name )
				.WithAddress( address )
				.WithPhone( phone )
				.WithWebsite( website )
				.WithTwitter( twitter );
		}

		public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer ) {
			if( value is Company c ) {
				writer.WriteStartObject();
					writer.WritePropertyName( nameof( c.Id ).ToLowerInvariant() );
					writer.WriteValue( c.Id );

					writer.WritePropertyName( nameof( c.Name ).ToLowerInvariant() );
					writer.WriteValue( c.Name );

					writer.WritePropertyName( nameof( c.Address ).ToLowerInvariant() );
					writer.WriteValue( c.Address );

					writer.WritePropertyName( nameof( c.Phone ).ToLowerInvariant() );
					writer.WriteValue( c.Phone );

					writer.WritePropertyName( nameof( c.Website ).ToLowerInvariant() );
					writer.WriteValue( c.Website );

					if( !string.IsNullOrWhiteSpace( c.Twitter ) ||
						NullValueHandling.Include == serializer.NullValueHandling ) {
						writer.WritePropertyName( nameof( c.Twitter ).ToLowerInvariant() );
						writer.WriteValue( c.Twitter );
					}
				writer.WriteEndObject();
			} else {
				writer.WriteNull();
			}
		}
	}
}
