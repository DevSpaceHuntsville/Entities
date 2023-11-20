using System;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	[JsonConverter( typeof( TimeSlotJsonConverter ) )]
	public class TimeSlot : IEquatable<TimeSlot> {
		public readonly int Id;
		public readonly DateTime StartTime;
		public readonly DateTime EndTime;

		public TimeSlot (
			int id = default,
			DateTime starttime = default,
			DateTime endtime = default
		) {
			this.Id = id;
			this.StartTime = starttime;
			this.EndTime = endtime;
		}

		public TimeSlot( TimeSlot copy )
			: this(
				copy?.Id ?? default,
				copy?.StartTime ?? default,
				copy?.EndTime ?? default
			) { }

		public TimeSlot WithId( int id ) =>
			new TimeSlot( id, this.StartTime, this.EndTime );
		public TimeSlot WithStartTime( DateTime starttime ) =>
			new TimeSlot( this.Id, starttime, this.EndTime );
		public TimeSlot WithEndTime( DateTime endtime ) =>
			new TimeSlot( this.Id, this.StartTime, endtime );

		public override bool Equals( object obj ) {
			if( obj is TimeSlot that )
				return this.Equals( that );

			return base.Equals( obj );
		}

		private int? _hash = null;
		private const int _bigPrime = 24943;
		private const int _littlePrime = 5573;
		public override int GetHashCode() {
			static int SafeHashCode( object obj ) =>
				obj is object ish
				? ish.GetHashCode()
				: 0;

			if( !_hash.HasValue ) {
				unchecked {
					_hash = _bigPrime;

					_hash = _hash * _littlePrime + SafeHashCode( this.Id );
					_hash = _hash * _littlePrime + SafeHashCode( this.StartTime );
					_hash = _hash * _littlePrime + SafeHashCode( this.EndTime );
				}
			}

			return _hash.Value;
		}

		public override string ToString() =>
			$"{StartTime} - {EndTime}";

		public bool Equals( TimeSlot that ) {
			if( that is null )
				return false;

			return
				ReferenceEquals( this, that )
				|| (
					this.GetHashCode() == that.GetHashCode()
					&& this.Id == that.Id
					&& this.StartTime == that.StartTime
					&& this.EndTime == that.EndTime
				);
		}

		public static bool operator ==( TimeSlot left, TimeSlot right ) =>
			left is null
				? right is null
				: left.Equals( right );

		public static bool operator !=( TimeSlot left, TimeSlot right ) =>
			!( left == right );
	}
}