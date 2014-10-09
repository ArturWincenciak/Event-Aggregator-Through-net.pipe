using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TeoVincent.RootAndNodesPattern.UnitTests
{
    [TestClass]
    public class RootUnitTest
    {
        class RootMock : Root
        {
            public RootMock(string a_name)
                : base(a_name)
            {
            }
        }
        
        [TestMethod]
        public void TestRootConstructor()
        {
            var target = new RootMock("test");
            Assert.IsNotNull(target);
        }
    }
}
