#region Licence
// The MIT License (MIT)
// 
// Copyright (c) 2014 TeoVincent Artur Wincenciak
// TeoVincent.EventAggregator2013
// TeoVincent.EventAggregator.Common.UnitTests
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
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
		public void EqualEventWith_The_Same_Type_Test()
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
		public void EqualEvent_With_The_Same_IdTest()
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
		public void EqualEvent_After_Copy_ReferenceTest()
		{
			AEvent target = CreateEvent();
			AEvent other = target;

			bool expected = true;
			bool actual;

			actual = target.Equals(other);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void EqualEvent_With_The_Same_IdBut_With_Different_TypeTest()
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
		public void EqualObject_Test()
		{
			AEvent target = CreateEvent();
			object obj = new SimpleEventMock();

			bool expected = false;
			bool actual;

			actual = target.Equals(obj);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void EqualObject_With_The_Same_IdTest()
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
		public void Get_Hash_Code_ForEvent_Test()
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
