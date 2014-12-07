using System;
using TeoVincent.EA.Common.Events;
using TeoVincent.EA.Common.UnitTests.Events.Mock;
using Xunit;

namespace TeoVincent.EA.Common.UnitTests.Events
{
	public class AEventTest
	{
		internal AEvent CreateEvent()
		{
			AEvent target = new SimpleEventMock();
			return target;
		}

		#region Equals(AEvent) Method Test

		[Fact]
		public void Equal_Event_With_The_Same_Type_Test()
		{
			// 1) arrange
            AEvent target = CreateEvent();
			AEvent other = new SimpleEventMock();

            // 2) act
			bool actual = target.Equals(other);

            // 3) assert
            Assert.False(actual);
		}

        [Fact]
		public void Equal_Event_With_The_Same_Id_Test()
		{
            // 1) arrange
            AEvent target = CreateEvent();
			AEvent other = new SimpleEventMock();
			other.ID = target.ID;

            // 2) act
			bool actual = target.Equals(other);

            // 3) assert
            Assert.True(actual);
		}

        [Fact]
		public void Equal_Event_After_Copy_Reference_Test()
		{
            // 1) arrange
            AEvent target = CreateEvent();
			AEvent other = target;

            // 2) act
			bool actual = target.Equals(other);

            // 3) assert
            Assert.True(actual);
		}

        [Fact]
		public void Equal_Event_With_The_Same_Id_But_With_Different_Type_Test()
		{
            // 1) arrange
            AEvent target = CreateEvent();
			target.ID = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
			AEvent other = new OtherSimpleEventMock();
			other.ID = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            // 2) act
			bool actual = target.Equals(other);

            // 3) assert
            Assert.False(actual);
		}

		#endregion

		#region Equals(object) Method Test

        [Fact]
		public void Equal_Object_Test()
		{
            // 1) arrange
            AEvent target = CreateEvent();
			object obj = new SimpleEventMock();

            // 2) act
			bool actual = target.Equals(obj);

            // 3) assert
            Assert.False(actual);
		}

        [Fact]
		public void Equal_Object_With_The_Same_Id_Test()
		{
            // 1) arrange
            AEvent target = CreateEvent();
			object other = new SimpleEventMock();
			((AEvent)other).ID = target.ID;

            // 2) act
			bool actual = target.Equals(other);

            // 3) assert
            Assert.True(actual);
		}

		#endregion

		#region GetHashCode Method Test

        [Fact]
		public void Get_Hash_Code_For_Event_Test()
		{
            // 1) arrange
            AEvent target = CreateEvent();
			int expected = target.ID.GetHashCode();

            // 2) act
			int actual = target.GetHashCode();

            // 3) assert
            Assert.Equal(expected, actual);
		}

		#endregion
	}
}
