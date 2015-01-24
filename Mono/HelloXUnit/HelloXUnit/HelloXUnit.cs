using System;
using Xunit;

namespace HelloXUnit
{
	public class XUnitTests
	{
		[Fact]
		public void PassingTest()
		{
			Assert.True (true);
		}

		[Fact]
		public void FailingTest()
		{
			Assert.True (false);
		}
	}
}

