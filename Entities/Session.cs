using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	[JsonConverter( typeof( SessionJsonConverter ) )]
	public class Session : IEquatable<Session> {
		public readonly int Id;
		public readonly int UserId;
		public readonly string Title;
		public readonly string Abstract;
		public readonly string Notes;
		public readonly int SessionLength;
		public readonly Tag Level;
		public readonly Tag Category;
		public readonly bool? Accepted;
		public readonly IEnumerable<Tag> Tags;
		public readonly TimeSlot TimeSlot;
		public readonly Room Room;
		public readonly int EventId;
		public readonly int? SessionizeId;

		public Session (
			int id = default,
			int userid = default,
			string title = default,
			string @abstract = default,
			string notes = default,
			int sessionlength = default,
			Tag level = default,
			Tag category = default,
			bool? accepted = default,
			IEnumerable<Tag> tags = default,
			TimeSlot timeslot = default,
			Room room = default,
			int eventid = default,
			int? sessionizeid = default
		) {
			this.Id = id;
			this.UserId = userid;
			this.Title = title;
			this.Abstract = @abstract;
			this.Notes = notes;
			this.SessionLength = sessionlength;
			this.Level = level;
			this.Category = category;
			this.Accepted = accepted;
			this.Tags = this.Tags = tags?.Select( x => x ) ?? Enumerable.Empty<Tag>();
			this.TimeSlot = timeslot;
			this.Room = room;
			this.EventId = eventid;
			this.SessionizeId = sessionizeid;
		}

		public Session( Session copy )
			: this( copy.Id, copy.UserId, copy.Title, copy.Abstract, copy.Notes, copy.SessionLength, copy.Level, copy.Category, copy.Accepted, copy.Tags, copy.TimeSlot, copy.Room, copy.EventId, copy.SessionizeId ) { }

		public Session WithId( int id ) =>
			new Session( id, this.UserId, this.Title, this.Abstract, this.Notes, this.SessionLength, this.Level, this.Category, this.Accepted, this.Tags, this.TimeSlot, this.Room, this.EventId, this.SessionizeId );
		public Session WithUserId( int userid ) =>
			new Session( this.Id, userid, this.Title, this.Abstract, this.Notes, this.SessionLength, this.Level, this.Category, this.Accepted, this.Tags, this.TimeSlot, this.Room, this.EventId, this.SessionizeId );
		public Session WithTitle( string title ) =>
			new Session( this.Id, this.UserId, title, this.Abstract, this.Notes, this.SessionLength, this.Level, this.Category, this.Accepted, this.Tags, this.TimeSlot, this.Room, this.EventId, this.SessionizeId );
		public Session WithAbstract( string @abstract ) =>
			new Session( this.Id, this.UserId, this.Title, @abstract, this.Notes, this.SessionLength, this.Level, this.Category, this.Accepted, this.Tags, this.TimeSlot, this.Room, this.EventId, this.SessionizeId );
		public Session WithNotes( string notes ) =>
			new Session( this.Id, this.UserId, this.Title, this.Abstract, notes, this.SessionLength, this.Level, this.Category, this.Accepted, this.Tags, this.TimeSlot, this.Room, this.EventId, this.SessionizeId );
		public Session WithSessionLength( int sessionlength ) =>
			new Session( this.Id, this.UserId, this.Title, this.Abstract, this.Notes, sessionlength, this.Level, this.Category, this.Accepted, this.Tags, this.TimeSlot, this.Room, this.EventId, this.SessionizeId );
		public Session WithLevel( Tag level ) =>
			new Session( this.Id, this.UserId, this.Title, this.Abstract, this.Notes, this.SessionLength, level, this.Category, this.Accepted, this.Tags, this.TimeSlot, this.Room, this.EventId, this.SessionizeId );
		public Session WithCategory( Tag category ) =>
			new Session( this.Id, this.UserId, this.Title, this.Abstract, this.Notes, this.SessionLength, this.Level, category, this.Accepted, this.Tags, this.TimeSlot, this.Room, this.EventId, this.SessionizeId );
		public Session WithAccepted( bool? accepted ) =>
			new Session( this.Id, this.UserId, this.Title, this.Abstract, this.Notes, this.SessionLength, this.Level, this.Category, accepted, this.Tags, this.TimeSlot, this.Room, this.EventId, this.SessionizeId );
		public Session WithTags( IEnumerable<Tag> tags ) =>
			new Session( this.Id, this.UserId, this.Title, this.Abstract, this.Notes, this.SessionLength, this.Level, this.Category, this.Accepted, tags, this.TimeSlot, this.Room, this.EventId, this.SessionizeId );
		public Session WithTimeSlot( TimeSlot timeslot ) =>
			new Session( this.Id, this.UserId, this.Title, this.Abstract, this.Notes, this.SessionLength, this.Level, this.Category, this.Accepted, this.Tags, timeslot, this.Room, this.EventId, this.SessionizeId );
		public Session WithRoom( Room room ) =>
			new Session( this.Id, this.UserId, this.Title, this.Abstract, this.Notes, this.SessionLength, this.Level, this.Category, this.Accepted, this.Tags, this.TimeSlot, room, this.EventId, this.SessionizeId );
		public Session WithEventId( int eventid ) =>
			new Session( this.Id, this.UserId, this.Title, this.Abstract, this.Notes, this.SessionLength, this.Level, this.Category, this.Accepted, this.Tags, this.TimeSlot, this.Room, eventid, this.SessionizeId );
		public Session WithSessionizeId( int? sessionizeid ) =>
			new Session( this.Id, this.UserId, this.Title, this.Abstract, this.Notes, this.SessionLength, this.Level, this.Category, this.Accepted, this.Tags, this.TimeSlot, this.Room, this.EventId, sessionizeid );

		public override bool Equals( object obj ) {
			if( obj is Session that )
				return this.Equals( that );

			return base.Equals( obj );
		}

		private int? _hash = null;
		private const int _bigPrime = 31627;
		private const int _littlePrime = 9497;
		public override int GetHashCode() {
			static int SafeHashCode( object obj ) =>
				obj is object ish
				? ish.GetHashCode()
				: 0;

			if( !_hash.HasValue ) {
				unchecked {
					_hash = _bigPrime;

					_hash = _hash * _littlePrime + SafeHashCode( this.Id );
					_hash = _hash * _littlePrime + SafeHashCode( this.UserId );
					_hash = _hash * _littlePrime + SafeHashCode( this.Title );
					_hash = _hash * _littlePrime + SafeHashCode( this.Abstract );
					_hash = _hash * _littlePrime + SafeHashCode( this.Notes );
					_hash = _hash * _littlePrime + SafeHashCode( this.SessionLength );
					_hash = _hash * _littlePrime + SafeHashCode( this.Level );
					_hash = _hash * _littlePrime + SafeHashCode( this.Category );
					_hash = _hash * _littlePrime + SafeHashCode( this.Accepted );
					_hash = _hash * _littlePrime + SafeHashCode( this.TimeSlot );
					_hash = _hash * _littlePrime + SafeHashCode( this.Room );
					_hash = _hash * _littlePrime + SafeHashCode( this.EventId );
					_hash = _hash * _littlePrime + SafeHashCode( this.SessionizeId );

					foreach( Tag x in Tags.OrderBy( y => y.Id ) )
						_hash = _hash * _littlePrime + SafeHashCode( x );
				}
			}

			return _hash.Value;
		}

		public override string ToString() =>
			this.Title;

		public bool Equals( Session that ) {
			if( that is null )
				return false;

			return
				ReferenceEquals( this, that )
				|| (
					this.GetHashCode() == that.GetHashCode()
					&& this.Id == that.Id
					&& this.UserId == that.UserId
					&& this.Title == that.Title
					&& this.Abstract == that.Abstract
					&& this.Notes == that.Notes
					&& this.SessionLength == that.SessionLength
					&& this.Level == that.Level
					&& this.Category == that.Category
					&& this.Accepted == that.Accepted
					&& this.TimeSlot == that.TimeSlot
					&& this.Room == that.Room
					&& this.EventId == that.EventId
					&& this.SessionizeId == that.SessionizeId
					&& Enumerable.SequenceEqual(
						this.Tags.OrderBy( y => y.Id ),
						that.Tags.OrderBy( y => y.Id )
					)
				);
		}

		public static bool operator ==( Session left, Session right ) =>
			left is null
				? right is null
				: left.Equals( right );

		public static bool operator !=( Session left, Session right ) =>
			!( left == right );
	}
}