using System;
using System.Collections.Generic;
using System.Text;

namespace DevSpace.Common.Entities.Test.Helpers {
	public static class RandomEntity {
		private static Random random = new Random();

		public static Event Event =>
			new Event(
				id: random.Next(),
				name: random.NextString( 16 )
			);
	}
}
