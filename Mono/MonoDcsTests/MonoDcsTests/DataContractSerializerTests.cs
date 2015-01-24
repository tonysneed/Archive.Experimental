using NUnit.Framework;
using System.Runtime.Serialization;
using System;
using System.Reflection;
using System.Diagnostics;

namespace MonoDcsTests
{
	[DataContract]
	public class DummyClass
	{
		[DataMember]
		public int SampleInt {
			get;
			set;
		}
	}

	[TestFixture ()]
	public class DataContractSerializerTests
	{
		[Test ()]
		public void InitializeDataContractSerializerWithSettings ()
		{
			var settings = new DataContractSerializerSettings ();
			var dcs = new DataContractSerializer (typeof(DummyClass), settings);
			Assert.NotNull (dcs);
		}

		[Test ()]
		public void MonoRuntimeVersion ()
		{
			var version = GetMonoVersion();
			Assert.NotNull (version);
		}

		public string GetMonoVersion()
		{
			Type type = Type.GetType("Mono.Runtime");
			if (type != null)
			{
				var displayNameMethod = type.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);
				var version = (string)displayNameMethod.Invoke (null, null);
				return version;
			}
			return null;
		}
	}
}

