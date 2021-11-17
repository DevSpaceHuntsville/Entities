using System;
using Newtonsoft.Json;

namespace DevSpace.Common.Entities {
	[JsonConverter( typeof( UserJsonConverter ) )]
	public class User : IEquatable<User> {
		public readonly int Id;
		public readonly string EmailAddress;
		public readonly string DisplayName;
		public readonly string Bio;
		public readonly string Twitter;
		public readonly string Website;
		public readonly string Blog;
		public readonly string ProfilePicture;
		public readonly byte Permissions;
		public readonly Guid SessionToken;
		public readonly DateTime SessionExpires;
		public readonly Guid? SessionizeId;

		public User (
			int id = default,
			string emailaddress = default,
			string displayname = default,
			string bio = default,
			string twitter = default,
			string website = default,
			string blog = default,
			string profilepicture = default,
			byte permissions = default,
			Guid sessiontoken = default,
			DateTime sessionexpires = default,
			Guid? sessionizeid = default
		) {
			this.Id = id;
			this.EmailAddress = emailaddress;
			this.DisplayName = displayname;
			this.Bio = bio;
			this.Twitter = twitter;
			this.Website = website;
			this.Blog = blog;
			this.ProfilePicture = profilepicture;
			this.Permissions = permissions;
			this.SessionToken = sessiontoken;
			this.SessionExpires = sessionexpires;
			this.SessionizeId = sessionizeid;
		}

		public User( User copy )
			: this(
				copy?.Id ?? default,
				copy?.EmailAddress,
				copy?.DisplayName,
				copy?.Bio,
				copy?.Twitter,
				copy?.Website,
				copy?.Blog,
				copy?.ProfilePicture,
				copy?.Permissions ?? default,
				copy?.SessionToken ?? default,
				copy?.SessionExpires ?? default,
				copy?.SessionizeId
			) { }

		public User WithId( int id ) =>
			new User( id, this.EmailAddress, this.DisplayName, this.Bio, this.Twitter, this.Website, this.Blog, this.ProfilePicture, this.Permissions, this.SessionToken, this.SessionExpires, this.SessionizeId );
		public User WithEmailAddress( string emailaddress ) =>
			new User( this.Id, emailaddress, this.DisplayName, this.Bio, this.Twitter, this.Website, this.Blog, this.ProfilePicture, this.Permissions, this.SessionToken, this.SessionExpires, this.SessionizeId );
		public User WithDisplayName( string displayname ) =>
			new User( this.Id, this.EmailAddress, displayname, this.Bio, this.Twitter, this.Website, this.Blog, this.ProfilePicture, this.Permissions, this.SessionToken, this.SessionExpires, this.SessionizeId );
		public User WithBio( string bio ) =>
			new User( this.Id, this.EmailAddress, this.DisplayName, bio, this.Twitter, this.Website, this.Blog, this.ProfilePicture, this.Permissions, this.SessionToken, this.SessionExpires, this.SessionizeId );
		public User WithTwitter( string twitter ) =>
			new User( this.Id, this.EmailAddress, this.DisplayName, this.Bio, twitter, this.Website, this.Blog, this.ProfilePicture, this.Permissions, this.SessionToken, this.SessionExpires, this.SessionizeId );
		public User WithWebsite( string website ) =>
			new User( this.Id, this.EmailAddress, this.DisplayName, this.Bio, this.Twitter, website, this.Blog, this.ProfilePicture, this.Permissions, this.SessionToken, this.SessionExpires, this.SessionizeId );
		public User WithBlog( string blog ) =>
			new User( this.Id, this.EmailAddress, this.DisplayName, this.Bio, this.Twitter, this.Website, blog, this.ProfilePicture, this.Permissions, this.SessionToken, this.SessionExpires, this.SessionizeId );
		public User WithProfilePicture( string profilepicture ) =>
			new User( this.Id, this.EmailAddress, this.DisplayName, this.Bio, this.Twitter, this.Website, this.Blog, profilepicture, this.Permissions, this.SessionToken, this.SessionExpires, this.SessionizeId );
		public User WithPermissions( byte permissions ) =>
			new User( this.Id, this.EmailAddress, this.DisplayName, this.Bio, this.Twitter, this.Website, this.Blog, this.ProfilePicture, permissions, this.SessionToken, this.SessionExpires, this.SessionizeId );
		public User WithSessionToken( Guid sessiontoken ) =>
			new User( this.Id, this.EmailAddress, this.DisplayName, this.Bio, this.Twitter, this.Website, this.Blog, this.ProfilePicture, this.Permissions, sessiontoken, this.SessionExpires, this.SessionizeId );
		public User WithSessionExpires( DateTime sessionexpires ) =>
			new User( this.Id, this.EmailAddress, this.DisplayName, this.Bio, this.Twitter, this.Website, this.Blog, this.ProfilePicture, this.Permissions, this.SessionToken, sessionexpires, this.SessionizeId );
		public User WithSessionizeId( Guid? sessionizeid ) =>
			new User( this.Id, this.EmailAddress, this.DisplayName, this.Bio, this.Twitter, this.Website, this.Blog, this.ProfilePicture, this.Permissions, this.SessionToken, this.SessionExpires, sessionizeid );

		public override bool Equals( object obj ) {
			if( obj is User that )
				return this.Equals( that );

			return base.Equals( obj );
		}

		private int? _hash = null;
		private const int _bigPrime = 12671;
		private const int _littlePrime = 4127;
		public override int GetHashCode() {
			Func<object, int> SafeHashCode = ( obj ) =>
				obj is object ish
				? ish.GetHashCode()
				: 0;

			if( !_hash.HasValue ) {
				unchecked {
					_hash = _bigPrime;

					_hash = _hash * _littlePrime + SafeHashCode( this.Id );
					_hash = _hash * _littlePrime + SafeHashCode( this.EmailAddress );
					_hash = _hash * _littlePrime + SafeHashCode( this.DisplayName );
					_hash = _hash * _littlePrime + SafeHashCode( this.Bio );
					_hash = _hash * _littlePrime + SafeHashCode( this.Twitter );
					_hash = _hash * _littlePrime + SafeHashCode( this.Website );
					_hash = _hash * _littlePrime + SafeHashCode( this.Blog );
					_hash = _hash * _littlePrime + SafeHashCode( this.ProfilePicture );
					_hash = _hash * _littlePrime + SafeHashCode( this.Permissions );
					_hash = _hash * _littlePrime + SafeHashCode( this.SessionToken );
					_hash = _hash * _littlePrime + SafeHashCode( this.SessionExpires );
					_hash = _hash * _littlePrime + SafeHashCode( this.SessionizeId );
				}
			}

			return _hash.Value;
		}

		public override string ToString() =>
			this.DisplayName;

		public bool Equals( User that ) {
			if( ReferenceEquals( that, null ) )
				return false;

			return
				ReferenceEquals( this, that )
				|| (
					this.GetHashCode() == that.GetHashCode()
					&& this.Id == that.Id
					&& this.EmailAddress == that.EmailAddress
					&& this.DisplayName == that.DisplayName
					&& this.Bio == that.Bio
					&& this.Twitter == that.Twitter
					&& this.Website == that.Website
					&& this.Blog == that.Blog
					&& this.ProfilePicture == that.ProfilePicture
					&& this.Permissions == that.Permissions
					&& this.SessionToken == that.SessionToken
					&& this.SessionExpires == that.SessionExpires
					&& this.SessionizeId == that.SessionizeId
				);
		}

		public static bool operator ==( User left, User right ) =>
			ReferenceEquals( left, null )
				? ReferenceEquals( right, null )
				: left.Equals( right );

		public static bool operator !=( User left, User right ) =>
			!( left == right );
	}
}