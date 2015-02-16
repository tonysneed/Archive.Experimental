using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Dcs.Inheritance
{
    [DataContract]
    public class PersonBase
    {
        [DataMember]
        public string SimpleString { get; set; }

        [DataMember]
        public ICollection<string> StringCollection { get; set; }
    }
}
