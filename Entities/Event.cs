using System;

namespace Entities {
	public class Event : IEquatable<Event> {
		public readonly int Id;
		public readonly string Name;

		public Event (
			int id = default,
			string name = default
		) {
			this.Id = id;
			this.Name = name;
		}

		public Event( Event copy )
			: this( copy.Id, copy.Name ) { }

		public Event WithId( int id ) =>
			new Event( id, this.Name );
		public Event WithName( string name ) =>
			new Event( this.Id, name );

		public override bool Equals( object obj ) {
			if( obj is Event that )
				return this.Equals( that );

			return base.Equals( obj );
		}

		private int? _hash = null;
		private const int _bigPrime = 24359;
		private const int _littlePrime = 3673;
		public override int GetHashCode() {
			Func<object, int> SafeHashCode = ( obj ) =>
				obj is object ish
				? ish.GetHashCode()
				: 0;

			if( !_hash.HasValue ) {
				unchecked {
					_hash = _bigPrime;

					_hash = _hash * _littlePrime + SafeHashCode( this.Id );
					_hash = _hash * _littlePrime + SafeHashCode( this.Name );
				}
			}

			return _hash.Value;
		}

		public override string ToString() =>
			this.Name;

		public bool Equals( Event that ) {
			if( ReferenceEquals( that, null ) )
				return false;

			return
				ReferenceEquals( this, that )
				|| (
					this.GetHashCode() == that.GetHashCode()
					&& this.Id == that.Id
					&& this.Name == that.Name
				);
		}

		public static bool operator ==( Event left, Event right ) =>
			ReferenceEquals( left, null )
				? ReferenceEquals( right, null )
				: left.Equals( right );

		public static bool operator !=( Event left, Event right ) =>
			!( left == right );
	}
}