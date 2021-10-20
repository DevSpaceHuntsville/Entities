using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DevSpace.Common.Entities.Test {
	public class TimeSlotTests : Helpers.StandardEntityTests<TimeSlot> {
		[Fact]
		public void DefaultCtor() {
			TimeSlot actual = new TimeSlot();

			Assert.NotNull( actual );
			Assert.Equal( 0, actual.Id );
			Assert.Equal( default, actual.StartTime );
			Assert.Equal( default, actual.EndTime );
		}

		public override void ObjectToString() {
			TimeSlot actual = Helpers.RandomEntity.TimeSlot;
			Assert.Equal( $"{actual.StartTime} - {actual.EndTime}", actual.ToString() );
		}
	}
}
