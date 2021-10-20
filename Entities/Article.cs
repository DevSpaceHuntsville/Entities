using System;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	[JsonConverter( typeof( ArticleJsonConverter ) )]
	public class Article : IEquatable<Article> {
		public readonly int Id;
		public readonly string Title;
		public readonly string Body;
		public readonly DateTime PublishDate;
		public readonly DateTime ExpireDate;

		public Article (
			int id = default,
			string title = default,
			string body = default,
			DateTime publishdate = default,
			DateTime expiredate = default
		) {
			this.Id = id;
			this.Title = title;
			this.Body = body;
			this.PublishDate = publishdate;
			this.ExpireDate = expiredate;
		}

		public Article( Article copy )
			: this(
				copy?.Id ?? default,
				copy?.Title,
				copy?.Body,
				copy?.PublishDate ?? default,
				copy?.ExpireDate ?? default
		) { }

		public Article WithId( int id ) =>
			new Article( id, this.Title, this.Body, this.PublishDate, this.ExpireDate );
		public Article WithTitle( string title ) =>
			new Article( this.Id, title, this.Body, this.PublishDate, this.ExpireDate );
		public Article WithBody( string body ) =>
			new Article( this.Id, this.Title, body, this.PublishDate, this.ExpireDate );
		public Article WithPublishDate( DateTime publishdate ) =>
			new Article( this.Id, this.Title, this.Body, publishdate, this.ExpireDate );
		public Article WithExpireDate( DateTime expiredate ) =>
			new Article( this.Id, this.Title, this.Body, this.PublishDate, expiredate );

		public override bool Equals( object obj ) {
			if( obj is Article that )
				return this.Equals( that );

			return base.Equals( obj );
		}

		private int? _hash = null;
		private const int _bigPrime = 36107;
		private const int _littlePrime = 4211;
		public override int GetHashCode() {
			Func<object, int> SafeHashCode = ( obj ) =>
				obj is object ish
				? ish.GetHashCode()
				: 0;

			if( !_hash.HasValue ) {
				unchecked {
					_hash = _bigPrime;

					_hash = _hash * _littlePrime + SafeHashCode( this.Id );
					_hash = _hash * _littlePrime + SafeHashCode( this.Title );
					_hash = _hash * _littlePrime + SafeHashCode( this.Body );
					_hash = _hash * _littlePrime + SafeHashCode( this.PublishDate );
					_hash = _hash * _littlePrime + SafeHashCode( this.ExpireDate );
				}
			}

			return _hash.Value;
		}

		public override string ToString() =>
			this.Title;

		public bool Equals( Article that ) {
			if( ReferenceEquals( that, null ) )
				return false;

			return
				ReferenceEquals( this, that )
				|| (
					this.GetHashCode() == that.GetHashCode()
					&& this.Id == that.Id
					&& this.Title == that.Title
					&& this.Body == that.Body
					&& this.PublishDate == that.PublishDate
					&& this.ExpireDate == that.ExpireDate
				);
		}

		public static bool operator ==( Article left, Article right ) =>
			ReferenceEquals( left, null )
				? ReferenceEquals( right, null )
				: left.Equals( right );

		public static bool operator !=( Article left, Article right ) =>
			!( left == right );
	}
}