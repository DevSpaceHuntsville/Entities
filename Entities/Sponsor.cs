using System;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	public class Sponsor : IEquatable<Sponsor> {
		public readonly int Id;
		public readonly Event SponsoredEvent;
		public readonly Company SponsoringCompany;
		public readonly SponsorLevel SponsorshipLevel;

		[JsonConstructor]
		public Sponsor (
			int id = default,
			Event sponsoredevent = default,
			Company sponsoringcompany = default,
			SponsorLevel sponsorshiplevel = default
		) {
			this.Id = id;
			this.SponsoredEvent = new Event( sponsoredevent );
			this.SponsoringCompany = new Company( sponsoringcompany );
			this.SponsorshipLevel = new SponsorLevel( sponsorshiplevel );
		}

		public Sponsor( Sponsor copy )
			: this(
				copy?.Id ?? default,
				copy?.SponsoredEvent,
				copy?.SponsoringCompany,
				copy?.SponsorshipLevel
			) { }

		public Sponsor WithId( int id ) =>
			new Sponsor( id, this.SponsoredEvent, this.SponsoringCompany, this.SponsorshipLevel );
		public Sponsor WithSponsoredEvent( Event sponsoredevent ) =>
			new Sponsor( this.Id, sponsoredevent, this.SponsoringCompany, this.SponsorshipLevel );
		public Sponsor WithSponsoringCompany( Company sponsoringcompany ) =>
			new Sponsor( this.Id, this.SponsoredEvent, sponsoringcompany, this.SponsorshipLevel );
		public Sponsor WithSponsorshipLevel( SponsorLevel sponsorshiplevel ) =>
			new Sponsor( this.Id, this.SponsoredEvent, this.SponsoringCompany, sponsorshiplevel );

		public override bool Equals( object obj ) {
			if( obj is Sponsor that )
				return this.Equals( that );

			return base.Equals( obj );
		}

		private int? _hash = null;
		private const int _bigPrime = 29983;
		private const int _littlePrime = 2243;
		public override int GetHashCode() {
			Func<object, int> SafeHashCode = ( obj ) =>
				obj is object ish
				? ish.GetHashCode()
				: 0;

			if( !_hash.HasValue ) {
				unchecked {
					_hash = _bigPrime;

					_hash = _hash * _littlePrime + SafeHashCode( this.Id );
					_hash = _hash * _littlePrime + SafeHashCode( this.SponsoredEvent );
					_hash = _hash * _littlePrime + SafeHashCode( this.SponsoringCompany );
					_hash = _hash * _littlePrime + SafeHashCode( this.SponsorshipLevel );
				}
			}

			return _hash.Value;
		}

		public override string ToString() =>
			$"{this.SponsoringCompany.Name} is a {this.SponsorshipLevel.Name} Sponsor of {this.SponsoredEvent.Name}";

		public bool Equals( Sponsor that ) {
			if( ReferenceEquals( that, null ) )
				return false;

			return
				ReferenceEquals( this, that )
				|| (
					this.GetHashCode() == that.GetHashCode()
					&& this.Id == that.Id
					&& this.SponsoredEvent == that.SponsoredEvent
					&& this.SponsoringCompany == that.SponsoringCompany
					&& this.SponsorshipLevel == that.SponsorshipLevel
				);
		}

		public static bool operator ==( Sponsor left, Sponsor right ) =>
			ReferenceEquals( left, null )
				? ReferenceEquals( right, null )
				: left.Equals( right );

		public static bool operator !=( Sponsor left, Sponsor right ) =>
			!( left == right );
	}
}