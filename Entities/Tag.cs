using System;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	[JsonConverter( typeof( TagJsonConverter ) )]
	public class Tag : IEquatable<Tag> {
		public readonly int Id;
		public readonly string Text;

		public Tag (
			int id = default,
			string text = default
		) {
			this.Id = id;
			this.Text = text;
		}

		public Tag( Tag copy )
			: this( copy?.Id ?? default, copy?.Text ) { }

		public Tag WithId( int id ) =>
			new Tag( id, this.Text );
		public Tag WithText( string text ) =>
			new Tag( this.Id, text );

		public override bool Equals( object obj ) {
			if( obj is Tag that )
				return this.Equals( that );

			return base.Equals( obj );
		}

		private int? _hash = null;
		private const int _bigPrime = 13327;
		private const int _littlePrime = 4903;
		public override int GetHashCode() {
			Func<object, int> SafeHashCode = ( obj ) =>
				obj is object ish
				? ish.GetHashCode()
				: 0;

			if( !_hash.HasValue ) {
				unchecked {
					_hash = _bigPrime;

					_hash = _hash * _littlePrime + SafeHashCode( this.Id );
					_hash = _hash * _littlePrime + SafeHashCode( this.Text );
				}
			}

			return _hash.Value;
		}

		public override string ToString() =>
			this.Text;

		public bool Equals( Tag that ) {
			if( ReferenceEquals( that, null ) )
				return false;

			return
				ReferenceEquals( this, that )
				|| (
					this.GetHashCode() == that.GetHashCode()
					&& this.Id == that.Id
					&& this.Text == that.Text
				);
		}

		public static bool operator ==( Tag left, Tag right ) =>
			ReferenceEquals( left, null )
				? ReferenceEquals( right, null )
				: left.Equals( right );

		public static bool operator !=( Tag left, Tag right ) =>
			!( left == right );
	}
}