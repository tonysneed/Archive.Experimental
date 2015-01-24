using NUnit.Framework;
using System;

namespace HelloNUnit
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void FailingTest ()
		{
			Assert.Fail ("This is a failing test.");
		}

		[Test ()]
		public void PassingTest ()
		{
			Assert.Pass ("This is a passing test.");
		}
		}
}

