using System;
using DistributedTrace.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DistributedTrace.Tests
{
    [TestClass]
    public class TracePathTests
    {
        [TestMethod]
        public void TracePath_Tests()
        {
            var path = new TreePath();
            Assert.AreEqual(path.Value, string.Empty);

            path.Push("Call");
            Assert.AreEqual(path.Value, "Call");

            path.Push("Service");
            Assert.AreEqual(path.Value, "Call/Service");

            path.Pop();
            Assert.AreEqual(path.Value, "Call");

            path.Pop();
            Assert.AreEqual(path.Value, string.Empty);
        }
    }
}
