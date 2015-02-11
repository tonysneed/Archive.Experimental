using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TrackableEntities;

namespace MyClient
{
    [DataContract(IsReference = true)]
    [JsonObject(IsReference = true)]
    public abstract class MyEntityBase : ITrackable, IMergeable
    {
        [DataMember]
        [JsonProperty] // Not needed
        ICollection<string> ITrackable.ModifiedProperties { get; set; }

        [DataMember]
        [JsonProperty] // Not needed
        TrackingState ITrackable.TrackingState { get; set; }

        [DataMember]
        [JsonProperty] // Not needed
        Guid IMergeable.EntityIdentifier { get; set; }

        // Public property wrappers

        [JsonIgnore]
        public TrackingState TrackingState
        {
            get { return ((ITrackable)this).TrackingState; }
            set { ((ITrackable)this).TrackingState = value; }
        }

        [JsonIgnore]
        public ICollection<string> ModifiedProperties
        {
            get { return ((ITrackable)this).ModifiedProperties; }
            set { ((ITrackable)this).ModifiedProperties = value; }
        }

        [JsonIgnore]
        public Guid EntityIdentifier
        {
            get { return ((IMergeable)this).EntityIdentifier; }
            set { ((IMergeable)this).EntityIdentifier = value; }
        }
    }
}
