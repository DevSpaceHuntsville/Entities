using System;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	public class SponsorJsonConverter : JsonConverter {
		public override bool CanConvert( Type objectType ) => 
			objectType.IsAssignableFrom( typeof( Sponsor ) );

		public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer ) {
			Sponsor output = existingValue as Sponsor ?? new Sponsor();
			object CallBaseConverter( JsonConverter c, object o ) =>
				c.ReadJson( reader, o.GetType(), o, serializer );

			while( reader.Read() ) {
				switch( reader.TokenType ) {
					case JsonToken.StartObject:
						continue;

					case JsonToken.PropertyName:
						switch( reader.Value.ToString().ToUpperInvariant() ) {
							case "ID":
								output = output.WithId( reader.ReadAsInt32().GetValueOrDefault() );
								break;

							case "SPONSOREDEVENT":
								output = output.WithSponsoredEvent(
									CallBaseConverter( new EventJsonConverter(), output.SponsoredEvent ) as Event
								);
								break;

							case "SPONSORINGCOMPANY":
								output = output.WithSponsoringCompany(
									CallBaseConverter( new CompanyJsonConverter(), output.SponsoringCompany ) as Company
								);
								break;

							case "SPONSORSHIPLEVEL":
								output = output.WithSponsorshipLevel(
									CallBaseConverter( new SponsorLevelJsonConverter(), output.SponsorshipLevel ) as SponsorLevel
								);
								break;

							default:
								break;
						}
						continue;

					default:
						break;
				}

				if( JsonToken.EndObject == reader.TokenType )
					break;
			}

			return output;
		}

		public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer ) {
			if( value is Sponsor o ) {
				writer.WriteStartObject();

					writer.WritePropertyName( "id" );
					writer.WriteValue( o.Id );

					writer.WritePropertyName( "sponsoredEvent" );
					new EventJsonConverter().WriteJson( writer, o.SponsoredEvent, serializer );

					writer.WritePropertyName( "sponsoringCompany" );
					new CompanyJsonConverter().WriteJson( writer, o.SponsoringCompany, serializer );

					writer.WritePropertyName( "sponsorshipLevel" );
					new SponsorLevelJsonConverter().WriteJson( writer, o.SponsorshipLevel, serializer );

				writer.WriteEndObject();
			} else {
				writer.WriteNull();
			}
		}
	}
}
