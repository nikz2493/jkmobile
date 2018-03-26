using System.Runtime.Serialization;

namespace JKMPCL.Model
{
    /// <summary>
    /// Class Name      : TokenResponseModel
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 30 Jan 2018
    /// Purpose         : model for Token generation
    /// Revision        :  
    /// </summary>
    [DataContract]
    public class TokenResponseModel
    {
        [DataMember]
        public string Token { get; set; }
    }
}
