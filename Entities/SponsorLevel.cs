using System;

namespace Entities {
	public class SponsorLevel : IEquatable<SponsorLevel> {
		public readonly int Id;
		public readonly int DisplayOrder;
		public readonly string Name;
		public readonly int Cost;
		public readonly bool DisplayInEmails;
		public readonly bool DisplayInSidebar;
		public readonly int Tickets;
		public readonly int Discount;
		public readonly bool PreConEmail;
		public readonly bool MidConEmail;
		public readonly bool PostConEmail;

		public SponsorLevel (
			int id = default,
			int displayorder = default,
			string name = default,
			int cost = default,
			bool displayinemails = default,
			bool displayinsidebar = default,
			int tickets = default,
			int discount = default,
			bool preconemail = default,
			bool midconemail = default,
			bool postconemail = default
		) {
			this.Id = id;
			this.DisplayOrder = displayorder;
			this.Name = name;
			this.Cost = cost;
			this.DisplayInEmails = displayinemails;
			this.DisplayInSidebar = displayinsidebar;
			this.Tickets = tickets;
			this.Discount = discount;
			this.PreConEmail = preconemail;
			this.MidConEmail = midconemail;
			this.PostConEmail = postconemail;
		}

		public SponsorLevel( SponsorLevel copy )
			: this( copy.Id, copy.DisplayOrder, copy.Name, copy.Cost, copy.DisplayInEmails, copy.DisplayInSidebar, copy.Tickets, copy.Discount, copy.PreConEmail, copy.MidConEmail, copy.PostConEmail ) { }

		public SponsorLevel WithId( int id ) =>
			new SponsorLevel( id, this.DisplayOrder, this.Name, this.Cost, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithDisplayOrder( int displayorder ) =>
			new SponsorLevel( this.Id, displayorder, this.Name, this.Cost, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithName( string name ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, name, this.Cost, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithCost( int cost ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, cost, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithDisplayInEmails( bool displayinemails ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, displayinemails, this.DisplayInSidebar, this.Tickets, this.Discount, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithDisplayInSidebar( bool displayinsidebar ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayInEmails, displayinsidebar, this.Tickets, this.Discount, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithTickets( int tickets ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayInEmails, this.DisplayInSidebar, tickets, this.Discount, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithDiscount( int discount ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, discount, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithPreConEmail( bool preconemail ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, preconemail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithMidConEmail( bool midconemail ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.PreConEmail, midconemail, this.PostConEmail );
		public SponsorLevel WithPostConEmail( bool postconemail ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.PreConEmail, this.MidConEmail, postconemail );

		public override bool Equals( object obj ) {
			if( obj is SponsorLevel that )
				return this.Equals( that );

			return base.Equals( obj );
		}

		private int? _hash = null;
		private const int _bigPrime = 37813;
		private const int _littlePrime = 2801;
		public override int GetHashCode() {
			Func<object, int> SafeHashCode = ( obj ) =>
				obj is object ish
				? ish.GetHashCode()
				: 0;

			if( !_hash.HasValue ) {
				unchecked {
					_hash = _bigPrime;

					_hash = _hash * _littlePrime + SafeHashCode( this.Id );
					_hash = _hash * _littlePrime + SafeHashCode( this.DisplayOrder );
					_hash = _hash * _littlePrime + SafeHashCode( this.Name );
					_hash = _hash * _littlePrime + SafeHashCode( this.Cost );
					_hash = _hash * _littlePrime + SafeHashCode( this.DisplayInEmails );
					_hash = _hash * _littlePrime + SafeHashCode( this.DisplayInSidebar );
					_hash = _hash * _littlePrime + SafeHashCode( this.Tickets );
					_hash = _hash * _littlePrime + SafeHashCode( this.Discount );
					_hash = _hash * _littlePrime + SafeHashCode( this.PreConEmail );
					_hash = _hash * _littlePrime + SafeHashCode( this.MidConEmail );
					_hash = _hash * _littlePrime + SafeHashCode( this.PostConEmail );
				}
			}

			return _hash.Value;
		}

		public override string ToString() =>
			this.Name;

		public bool Equals( SponsorLevel that ) {
			if( ReferenceEquals( that, null ) )
				return false;

			return
				ReferenceEquals( this, that )
				|| (
					this.GetHashCode() == that.GetHashCode()
					&& this.Id == that.Id
					&& this.DisplayOrder == that.DisplayOrder
					&& this.Name == that.Name
					&& this.Cost == that.Cost
					&& this.DisplayInEmails == that.DisplayInEmails
					&& this.DisplayInSidebar == that.DisplayInSidebar
					&& this.Tickets == that.Tickets
					&& this.Discount == that.Discount
					&& this.PreConEmail == that.PreConEmail
					&& this.MidConEmail == that.MidConEmail
					&& this.PostConEmail == that.PostConEmail
				);
		}

		public static bool operator ==( SponsorLevel left, SponsorLevel right ) =>
			ReferenceEquals( left, null )
				? ReferenceEquals( right, null )
				: left.Equals( right );

		public static bool operator !=( SponsorLevel left, SponsorLevel right ) =>
			!( left == right );
	}
}