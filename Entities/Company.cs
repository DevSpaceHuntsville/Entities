using System;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	[JsonConverter( typeof( CompanyJsonConverter ) )]
	public class Company : IEquatable<Company> {
		public readonly int Id;
		public readonly string Name;
		public readonly string Address;
		public readonly string Phone;
		public readonly string Website;
		public readonly string Twitter;

		[JsonConstructor]
		public Company (
			int id = default,
			string name = default,
			string address = default,
			string phone = default,
			string website = default,
			string twitter = default
		) {
			this.Id = id;
			this.Name = name;
			this.Address = address;
			this.Phone = phone;
			this.Website = website;
			this.Twitter = twitter;
		}

		public Company( Company copy )
			: this(
				copy?.Id ?? default,
				copy?.Name,
				copy?.Address,
				copy?.Phone,
				copy?.Website,
				copy?.Twitter
			) { }

		public Company WithId( int id ) =>
			new Company( id, this.Name, this.Address, this.Phone, this.Website, this.Twitter );
		public Company WithName( string name ) =>
			new Company( this.Id, name, this.Address, this.Phone, this.Website, this.Twitter );
		public Company WithAddress( string address ) =>
			new Company( this.Id, this.Name, address, this.Phone, this.Website, this.Twitter );
		public Company WithPhone( string phone ) =>
			new Company( this.Id, this.Name, this.Address, phone, this.Website, this.Twitter );
		public Company WithWebsite( string website ) =>
			new Company( this.Id, this.Name, this.Address, this.Phone, website, this.Twitter );
		public Company WithTwitter( string twitter ) =>
			new Company( this.Id, this.Name, this.Address, this.Phone, this.Website, twitter );

		public override bool Equals( object obj ) {
			if( obj is Company that )
				return this.Equals( that );

			return base.Equals( obj );
		}

		private int? _hash = null;
		private const int _bigPrime = 12437;
		private const int _littlePrime = 9547;
		public override int GetHashCode() {
			static int SafeHashCode( object obj ) =>
				obj is object ish
				? ish.GetHashCode()
				: 0;

			if( !_hash.HasValue ) {
				unchecked {
					_hash = _bigPrime;

					_hash = _hash * _littlePrime + SafeHashCode( this.Id );
					_hash = _hash * _littlePrime + SafeHashCode( this.Name );
					_hash = _hash * _littlePrime + SafeHashCode( this.Address );
					_hash = _hash * _littlePrime + SafeHashCode( this.Phone );
					_hash = _hash * _littlePrime + SafeHashCode( this.Website );
					_hash = _hash * _littlePrime + SafeHashCode( this.Twitter );
				}
			}

			return _hash.Value;
		}

		public override string ToString() =>
			this.Name;

		public bool Equals( Company that ) {
			if( that is null )
				return false;

			return
				ReferenceEquals( this, that )
				|| (
					this.GetHashCode() == that.GetHashCode()
					&& this.Id == that.Id
					&& this.Name == that.Name
					&& this.Address == that.Address
					&& this.Phone == that.Phone
					&& this.Website == that.Website
					&& this.Twitter == that.Twitter
				);
		}

		public static bool operator ==( Company left, Company right ) =>
			left is null
				? right is null
				: left.Equals( right );

		public static bool operator !=( Company left, Company right ) =>
			!( left == right );
	}
}