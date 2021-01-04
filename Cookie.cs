using System.Runtime.Serialization;

namespace NFAuthenicationKeyCli
{
    [DataContract]
    class Cookie
    {
        public Cookie(string[] values)
        {
            Domain = values[0];
            Secure = bool.Parse(values[1]);
            Path = values[2];
            HttpOnly = bool.Parse(values[3]);
            Expires = long.Parse(values[4]);
            Name = values[5];
            Value = values[6];
        }

        [DataMember(Name = "domain")]
        public string Domain { get; set; }
        
        [DataMember(Name = "secure")]
        public bool Secure { get; set; }
        
        [DataMember(Name = "path")]
        public string Path { get; set; }
        
        [DataMember(Name = "httpOnly")]
        public bool HttpOnly { get; set; }
        
        [DataMember(Name = "expires")]
        public long Expires { get; set; }
        
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}