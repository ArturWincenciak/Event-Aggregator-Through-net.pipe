using TeoVincent.EventAggregator.Common.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TeoVincent.EventAggregator.Common.UnitTests.Events.Mock;

namespace TeoVincent.EventAggregator.Common.UnitTests.Events
{
    [TestClass]
	public class AEventTest
	{
		internal AEvent CreateEvent()
		{
			AEvent target = new SimpleEventMock();
			return target;
		}

		#region Equals(AEvent) Method Test

		[TestMethod()]
		public void Equals_Events_With_The_Same_Type_Test()
		{
			// 1) arrange
            AEvent target = CreateEvent();
			AEvent other = new SimpleEventMock();

			bool expected = false;
			bool actual;

            // act
			actual = target.Equals(other);

            // assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void Equals_Event_With_The_Same_Ids_Test()
		{
			AEvent target = CreateEvent();
			AEvent other = new SimpleEventMock();
			other.ID = target.ID;

			bool expected = true;
			bool actual;

			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void Equals_Event_After_Copy_References_Test()
		{
			AEvent target = CreateEvent();
			AEvent other = target;

			bool expected = true;
			bool actual;

			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void Equals_Event_With_The_Same_Ids_But_With_Different_Types_Test()
		{
			AEvent target = CreateEvent();
			target.ID = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
			AEvent other = new OtherSimpleEventMock();
			other.ID = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

			bool expected = false;
			bool actual;

			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region Equals(object) Method Test

		[TestMethod()]
		public void Equals_Object_Test()
		{
			AEvent target = CreateEvent();
			object obj = new SimpleEventMock();

			bool expected = false;
			bool actual;

			actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void Equals_Object_With_The_Same_Ids_Test()
		{
			AEvent target = CreateEvent();
			object other = new SimpleEventMock();
			((AEvent)other).ID = target.ID;

			bool expected = true;
			bool actual;

			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region GetHashCode Method Test

		[TestMethod()]
		public void Get_Hash_Code_Form_Event_Test()
		{
			AEvent target = CreateEvent();

			int expected = target.ID.GetHashCode();
			int actual;

			actual = target.GetHashCode();
			Assert.AreEqual(expected, actual);
		}

		#endregion
	}
}
