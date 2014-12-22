using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace HelloEf7.TraditionalDotNet.Models
{
    [Table("Category")]
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/TrackableEntities.Models")]
    public partial class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

		[DataMember]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(15)]
		[DataMember]
        public string CategoryName { get; set; }

		[DataMember]
        public ICollection<Product> Products { get; set; }
    }
}
