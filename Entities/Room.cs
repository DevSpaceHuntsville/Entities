using System;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	[JsonConverter( typeof( RoomJsonConverter ) )]
	public class Room : IEquatable<Room> {
		public readonly int Id;
		public readonly string DisplayName;

		public Room (
			int id = default,
			string displayname = default
		) {
			this.Id = id;
			this.DisplayName = displayname;
		}

		public Room( Room copy )
			: this(
				copy?.Id ?? default,
				copy?.DisplayName
		) { }

		public Room WithId( int id ) =>
			new Room( id, this.DisplayName );
		public Room WithDisplayName( string displayname ) =>
			new Room( this.Id, displayname );

		public override bool Equals( object obj ) {
			if( obj is Room that )
				return this.Equals( that );

			return base.Equals( obj );
		}

		private int? _hash = null;
		private const int _bigPrime = 31379;
		private const int _littlePrime = 6151;
		public override int GetHashCode() {
			Func<object, int> SafeHashCode = ( obj ) =>
				obj is object ish
				? ish.GetHashCode()
				: 0;

			if( !_hash.HasValue ) {
				unchecked {
					_hash = _bigPrime;

					_hash = _hash * _littlePrime + SafeHashCode( this.Id );
					_hash = _hash * _littlePrime + SafeHashCode( this.DisplayName );
				}
			}

			return _hash.Value;
		}

		public override string ToString() =>
			this.DisplayName;

		public bool Equals( Room that ) {
			if( ReferenceEquals( that, null ) )
				return false;

			return
				ReferenceEquals( this, that )
				|| (
					this.GetHashCode() == that.GetHashCode()
					&& this.Id == that.Id
					&& this.DisplayName == that.DisplayName
				);
		}

		public static bool operator ==( Room left, Room right ) =>
			ReferenceEquals( left, null )
				? ReferenceEquals( right, null )
				: left.Equals( right );

		public static bool operator !=( Room left, Room right ) =>
			!( left == right );
	}
}