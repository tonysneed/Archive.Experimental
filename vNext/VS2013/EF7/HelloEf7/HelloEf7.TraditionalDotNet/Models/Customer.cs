using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace HelloEf7.TraditionalDotNet.Models
{
    [Table("Customer")]
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/TrackableEntities.Models")]
    public partial class Customer
    {
        [StringLength(5)]
		[DataMember]
        public string CustomerId { get; set; }

        [Required]
        [StringLength(40)]
		[DataMember]
        public string CompanyName { get; set; }

        [StringLength(30)]
		[DataMember]
        public string ContactName { get; set; }

        [StringLength(15)]
		[DataMember]
        public string City { get; set; }

        [StringLength(15)]
		[DataMember]
        public string Country { get; set; }
    }
}
