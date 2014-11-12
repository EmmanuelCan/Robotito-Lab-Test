using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace RestJsonRobotitoLabContract
{
    [DataContract]
    public class Response
    {
        [DataMember(Name = "Users")]
        public User[] users { get; set; }
    }


    [DataContract]
    public class User
    {
        [DataMember(Name = "UserName")]
        public string userName { get; set; }
        [DataMember(Name = "Email")]
        public string email { get; set; }
        [DataMember(Name = "Phone")]
        public string phone { get; set; }
    }

   
}
