namespace Serialization.Dcs
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(Name = "Child", Namespace = "")]
    public class Child
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Parent Parent { get; set; }
    }

    [DataContract(Name = "Parent", Namespace = "")]
    public class Parent
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<Child> Children { get; set; }
    }
}
