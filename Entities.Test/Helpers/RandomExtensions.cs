namespace DevSpace.Common.Entities.Test.Helpers {
	public static class RandomExtensions {
		public static bool NextBool( this Random random ) =>
			random.NextDouble() < 0.5;

		public static DateTime NextDateTime( this Random random ) =>
			random.NextDateTime( DateTime.Today, DateTime.MaxValue );
		public static DateTime NextDateTime( this Random random, DateTime maxLength ) =>
			random.NextDateTime( DateTime.Today, maxLength );
		public static DateTime NextDateTime( this Random random, DateTime minLength, DateTime maxLength ) {
			if( minLength > maxLength )
				(minLength, maxLength) = (maxLength, minLength);
			
			TimeSpan span = maxLength - minLength;
			return minLength + TimeSpan.FromMilliseconds( span.TotalMilliseconds * random.NextDouble() );
		}

		public static string NextString( this Random random ) =>
			random.NextString( 0, int.MaxValue );
		public static string NextString( this Random random, int maxLength ) =>
			random.NextString( 0, maxLength );
		public static string NextString( this Random random, int minLength, int maxLength ) {
			int length = random.Next( minLength, maxLength );
			return string.Join(
				string.Empty,
				Enumerable
					.Range( 0, length )
					.Select( i => random.Next( 0x61, 0x7B ) )
					.Select( Convert.ToChar )
			);
		}
	}
}
