using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Serialization.Dcs.Inheritance
{
    [DataContract(Name = "Person")]
    public class OtherPerson : PersonBase
    {
        [DataMember]
        public string Name { get; set; }
    }
}
