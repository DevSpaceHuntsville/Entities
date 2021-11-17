using System;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	public class UserJsonConverter : JsonConverter {
		private static readonly EntityJsonConverter<User> DefaultConverter;
		static UserJsonConverter() {
			DefaultConverter = new EntityJsonConverter<User>();
		}

		public override bool CanConvert( Type objectType ) =>
			DefaultConverter.CanConvert( objectType );
		public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer ) =>
			DefaultConverter.WriteJson( writer, value, serializer );

		public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer ) {
			User output = existingValue as User ?? new User();

			while( reader.Read() ) {
				switch( reader.TokenType ) {
					case JsonToken.StartObject:
						continue;

					case JsonToken.PropertyName:
						switch( reader.Value.ToString().ToUpperInvariant() ) {
							case "ID":
								output = output.WithId( reader.ReadAsInt32().GetValueOrDefault() );
								break;

							case "EMAILADDRESS":
								output = output.WithEmailAddress( reader.ReadAsString() );
								break;

							case "DISPLAYNAME":
								output = output.WithDisplayName( reader.ReadAsString() );
								break;

							case "BIO":
								output = output.WithBio( reader.ReadAsString() );
								break;

							case "TWITTER":
								output = output.WithTwitter( reader.ReadAsString() );
								break;

							case "WEBSITE":
								output = output.WithWebsite( reader.ReadAsString() );
								break;

							case "BLOG":
								output = output.WithBlog( reader.ReadAsString() );
								break;

							case "PROFILEPICTURE":
								output = output.WithProfilePicture( reader.ReadAsString() );
								break;

							case "PERMISSIONS":
								output = output.WithPermissions( Convert.ToByte( reader.ReadAsInt32().GetValueOrDefault() ) );
								break;
							case "SESSIONTOKEN":
								output = output.WithSessionToken( Guid.Parse( reader.ReadAsString() ?? Guid.Empty.ToString() ) );
								break;
							case "SESSIONEXPIRES":
								output = output.WithSessionExpires( reader.ReadAsDateTime().GetValueOrDefault() );
								break;
							case "SESSIONIZEID":
								output = output.WithSessionizeId( Guid.TryParse( reader.ReadAsString(), out Guid g ) ? (Guid?)g : null );
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
	}
}
