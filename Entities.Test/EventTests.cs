﻿using System;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class EventTests : Helpers.StandardEntityTests<Event> {
		[Fact]
		public void DefaultCtor() {
			Event actual = new Event();

			Assert.NotNull( actual );
			Assert.Equal( 0, actual.Id );
			Assert.Null( actual.Name );
			Assert.Equal( DateTime.MinValue, actual.StartDate );
			Assert.Equal( DateTime.MinValue, actual.EndDate );
		}

		public override void ObjectToString() {
			Event actual = Helpers.RandomEntity.Event;
			Assert.Equal( actual.Name, actual.ToString() );
		}
	}
}
