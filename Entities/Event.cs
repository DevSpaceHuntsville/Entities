using System;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	[JsonConverter( typeof( EventJsonConverter ) )]
	public class Event : IEquatable<Event> {
		public readonly int Id;
		public readonly string Name;
		public readonly DateTime StartDate;
		public readonly DateTime EndDate;

		public Event (
			int id = default,
			string name = default,
			DateTime startdate = default,
			DateTime enddate = default
		) {
			this.Id = id;
			this.Name = name;
			this.StartDate = startdate;
			this.EndDate = enddate;
		}

		public Event( Event copy )
			: this(
				copy?.Id ?? default,
				copy?.Name,
				copy?.StartDate ?? default,
				copy?.EndDate ?? default
			) { }

		public Event WithId( int id ) =>
			new Event( id, this.Name, this.StartDate, this.EndDate );
		public Event WithName( string name ) =>
			new Event( this.Id, name, this.StartDate, this.EndDate );
		public Event WithStartDate( DateTime startdate ) =>
			new Event( this.Id, this.Name, startdate, this.EndDate );
		public Event WithEndDate( DateTime enddate ) =>
			new Event( this.Id, this.Name, this.StartDate, enddate );

		public override bool Equals( object obj ) {
			if( obj is Event that )
				return this.Equals( that );

			return base.Equals( obj );
		}

		private int? _hash = null;
		private const int _bigPrime = 25981;
		private const int _littlePrime = 8081;
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
					_hash = _hash * _littlePrime + SafeHashCode( this.StartDate );
					_hash = _hash * _littlePrime + SafeHashCode( this.EndDate );
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
					&& this.StartDate == that.StartDate
					&& this.EndDate == that.EndDate
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