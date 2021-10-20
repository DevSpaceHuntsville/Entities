using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	public class SessionJsonConverter : JsonConverter {
		public override bool CanConvert( Type objectType ) => 
			objectType.IsAssignableFrom( typeof( Session ) );

		public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer ) {
			Session output = existingValue as Session ?? new Session();
			Func<JsonConverter, object, object> f = ( c, i ) => c.ReadJson( reader, i.GetType(), i, serializer );

			while( reader.Read() ) {
				switch( reader.TokenType ) {
					case JsonToken.StartObject:
						continue;

					case JsonToken.PropertyName:
						switch( reader.Value.ToString().ToUpperInvariant() ) {
							case "ID":
								output = output.WithId( reader.ReadAsInt32().GetValueOrDefault() );
								break;

							case "USERID":
								output = output.WithUserId( reader.ReadAsInt32().GetValueOrDefault() );
								break;

							case "TITLE":
								output = output.WithTitle( reader.ReadAsString() );
								break;

							case "ABSTRACT":
								output = output.WithAbstract( reader.ReadAsString() );
								break;

							case "NOTES":
								output = output.WithNotes( reader.ReadAsString() );
								break;

							case "SESSIONLENGTH":
								output = output.WithSessionLength( reader.ReadAsInt32().GetValueOrDefault() );
								break;

							case "LEVEL":
								output = output.WithLevel(
									f( new TagJsonConverter(), output.Level ) as Tag
								);
								break;

							case "CATEGORY":
								output = output.WithCategory(
									f( new TagJsonConverter(), output.Category ) as Tag
								);
								break;

							case "ACCEPTED":
								output = output.WithAccepted( reader.ReadAsBoolean() );
								break;

							case "TAGS":
								if( JsonToken.Null == reader.TokenType ) {
									reader.Read();
									output = output.WithTags( null );
									break;
								}

								List<Tag> tags = new List<Tag>();
								while( reader.Read() ) {
									switch( reader.TokenType ) {
										case JsonToken.StartArray:
											continue;

										case JsonToken.StartObject:
											tags.Add(
												f( new TagJsonConverter(), output.Category ) as Tag
											);
											break;

										default: break;
									}

									if( JsonToken.EndArray == reader.TokenType )
										break;
								}

								output = output.WithTags( tags );
								break;

							case "TIMESLOT":
								output = output.WithTimeSlot(
									f( new TimeSlotJsonConverter(), output.TimeSlot ) as TimeSlot
								);
								break;

							case "ROOM":
								output = output.WithRoom(
									f( new RoomJsonConverter(), output.Room ) as Room
								);
								break;

							case "EVENTID":
								output = output.WithEventId( reader.ReadAsInt32().GetValueOrDefault() );
								break;

							case "SESSIONIZEID":
								output = output.WithSessionizeId( reader.ReadAsInt32() );
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
			if( value is Session o ) {
				TagJsonConverter tagConverter = new TagJsonConverter();

				writer.WriteStartObject();

					writer.WritePropertyName( "id" );
					writer.WriteValue( o.Id );

					writer.WritePropertyName( "userId" );
					writer.WriteValue( o.UserId );

					writer.WritePropertyName( "title" );
					writer.WriteValue( o.Title );

					writer.WritePropertyName( "abstract" );
					writer.WriteValue( o.Abstract );

					writer.WritePropertyName( "notes" );
					writer.WriteValue( o.Notes );

					writer.WritePropertyName( "sessionLength" );
					writer.WriteValue( o.SessionLength );

					writer.WritePropertyName( "level" );
					tagConverter.WriteJson( writer, o.Level, serializer );

					writer.WritePropertyName( "category" );
					tagConverter.WriteJson( writer, o.Category, serializer );

					writer.WritePropertyName( "accepted" );
					writer.WriteValue( o.Accepted );

					writer.WritePropertyName( "tags" );
					writer.WriteStartArray();

						foreach( Tag tag in o.Tags )
							tagConverter.WriteJson( writer, tag, serializer );

					writer.WriteEndArray();

					writer.WritePropertyName( "timeSlot" );
					new TimeSlotJsonConverter().WriteJson( writer, o.TimeSlot, serializer );

					writer.WritePropertyName( "room" );
					new RoomJsonConverter().WriteJson( writer, o.Room, serializer );

					writer.WritePropertyName( "eventId" );
					writer.WriteValue( o.EventId );

					writer.WritePropertyName( "sessionizeId" );
					writer.WriteValue( o.SessionizeId );

				writer.WriteEndObject();
			} else {
				writer.WriteNull();
			}
		}
	}
}
