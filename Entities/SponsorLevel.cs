using System;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	[JsonConverter( typeof( SponsorLevelJsonConverter ) )]
	public class SponsorLevel : IEquatable<SponsorLevel> {
		public readonly int Id;
		public readonly int DisplayOrder;
		public readonly string Name;
		public readonly int Cost;
		public readonly bool DisplayLink;
		public readonly bool DisplayInEmails;
		public readonly bool DisplayInSidebar;
		public readonly int Tickets;
		public readonly int Discount;
		public readonly int TimeOnScreen;
		public readonly bool PreConEmail;
		public readonly bool MidConEmail;
		public readonly bool PostConEmail;

		public SponsorLevel (
			int id = default,
			int displayorder = default,
			string name = default,
			int cost = default,
			bool displaylink = default,
			bool displayinemails = default,
			bool displayinsidebar = default,
			int tickets = default,
			int discount = default,
			int timeonscreen = default,
			bool preconemail = default,
			bool midconemail = default,
			bool postconemail = default
		) {
			this.Id = id;
			this.DisplayOrder = displayorder;
			this.Name = name;
			this.Cost = cost;
			this.DisplayLink = displaylink;
			this.DisplayInEmails = displayinemails;
			this.DisplayInSidebar = displayinsidebar;
			this.Tickets = tickets;
			this.Discount = discount;
			this.TimeOnScreen = timeonscreen;
			this.PreConEmail = preconemail;
			this.MidConEmail = midconemail;
			this.PostConEmail = postconemail;
		}

		public SponsorLevel( SponsorLevel copy )
			: this(
				copy?.Id ?? default,
				copy?.DisplayOrder ?? default,
				copy?.Name,
				copy?.Cost ?? default,
				copy?.DisplayLink ?? default,
				copy?.DisplayInEmails ?? default,
				copy?.DisplayInSidebar ?? default,
				copy?.Tickets ?? default,
				copy?.Discount ?? default,
				copy?.TimeOnScreen ?? default,
				copy?.PreConEmail ?? default,
				copy?.MidConEmail ?? default,
				copy?.PostConEmail ?? default
			) { }

		public SponsorLevel WithId( int id ) =>
			new SponsorLevel( id, this.DisplayOrder, this.Name, this.Cost, this.DisplayLink, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.TimeOnScreen, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithDisplayOrder( int displayorder ) =>
			new SponsorLevel( this.Id, displayorder, this.Name, this.Cost, this.DisplayLink, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.TimeOnScreen, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithName( string name ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, name, this.Cost, this.DisplayLink, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.TimeOnScreen, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithCost( int cost ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, cost, this.DisplayLink, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.TimeOnScreen, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithDisplayLink( bool displaylink ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, displaylink, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.TimeOnScreen, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithDisplayInEmails( bool displayinemails ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayLink, displayinemails, this.DisplayInSidebar, this.Tickets, this.Discount, this.TimeOnScreen, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithDisplayInSidebar( bool displayinsidebar ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayLink, this.DisplayInEmails, displayinsidebar, this.Tickets, this.Discount, this.TimeOnScreen, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithTickets( int tickets ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayLink, this.DisplayInEmails, this.DisplayInSidebar, tickets, this.Discount, this.TimeOnScreen, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithDiscount( int discount ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayLink, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, discount, this.TimeOnScreen, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithTimeOnScreen( int timeonscreen ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayLink, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, timeonscreen, this.PreConEmail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithPreConEmail( bool preconemail ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayLink, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.TimeOnScreen, preconemail, this.MidConEmail, this.PostConEmail );
		public SponsorLevel WithMidConEmail( bool midconemail ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayLink, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.TimeOnScreen, this.PreConEmail, midconemail, this.PostConEmail );
		public SponsorLevel WithPostConEmail( bool postconemail ) =>
			new SponsorLevel( this.Id, this.DisplayOrder, this.Name, this.Cost, this.DisplayLink, this.DisplayInEmails, this.DisplayInSidebar, this.Tickets, this.Discount, this.TimeOnScreen, this.PreConEmail, this.MidConEmail, postconemail );

		public override bool Equals( object obj ) {
			if( obj is SponsorLevel that )
				return this.Equals( that );

			return base.Equals( obj );
		}

		private int? _hash = null;
		private const int _bigPrime = 22877;
		private const int _littlePrime = 6217;
		public override int GetHashCode() {
			static int SafeHashCode( object obj ) =>
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
					_hash = _hash * _littlePrime + SafeHashCode( this.DisplayLink );
					_hash = _hash * _littlePrime + SafeHashCode( this.DisplayInEmails );
					_hash = _hash * _littlePrime + SafeHashCode( this.DisplayInSidebar );
					_hash = _hash * _littlePrime + SafeHashCode( this.Tickets );
					_hash = _hash * _littlePrime + SafeHashCode( this.Discount );
					_hash = _hash * _littlePrime + SafeHashCode( this.TimeOnScreen );
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
			if( that is null )
				return false;

			return
				ReferenceEquals( this, that )
				|| (
					this.GetHashCode() == that.GetHashCode()
					&& this.Id == that.Id
					&& this.DisplayOrder == that.DisplayOrder
					&& this.Name == that.Name
					&& this.Cost == that.Cost
					&& this.DisplayLink == that.DisplayLink
					&& this.DisplayInEmails == that.DisplayInEmails
					&& this.DisplayInSidebar == that.DisplayInSidebar
					&& this.Tickets == that.Tickets
					&& this.Discount == that.Discount
					&& this.TimeOnScreen == that.TimeOnScreen
					&& this.PreConEmail == that.PreConEmail
					&& this.MidConEmail == that.MidConEmail
					&& this.PostConEmail == that.PostConEmail
				);
		}

		public static bool operator ==( SponsorLevel left, SponsorLevel right ) =>
			left is null
				? right is null
				: left.Equals( right );

		public static bool operator !=( SponsorLevel left, SponsorLevel right ) =>
			!( left == right );
	}
}