namespace Serialization.Dcs
{
    using System.Runtime.Serialization;

    [DataContract(Name = "DummyClass", Namespace = "")]
    public class DummyClass
    {
        [DataMember]
        public int SampleInt { get; set; }
    }

    [DataContract(Name = "SomeDummyClass", Namespace = "")]
    public class SomeDummyClass : DummyClass
    {
        [DataMember]
        public string SampleString { get; set; }
    }
}
