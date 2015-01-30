using System;
using Xunit;

namespace MyTests
{
    public class SimpleTests
    {
        [Fact]
        public void PassingTest()
        {
            Assert.True(true);
        }

        [Fact]
        public void FailingTest()
        {
            Assert.True(false);
        }
    }
}
