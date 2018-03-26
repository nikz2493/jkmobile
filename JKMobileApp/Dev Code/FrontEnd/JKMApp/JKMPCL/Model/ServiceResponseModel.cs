using System.Collections.Generic;

namespace JKMPCL.Model
{
    /// <summary>
    /// Class Name      : APIResponse
    /// Author          : Hiren Patel
    /// Creation Date   : 5 Dec 2017
    /// Purpose         : To get API response. 
    /// Revision        : 
    /// </summary> 
    public class APIResponse<T>
    {
        public bool STATUS { get; set; }
        public T DATA { get; set; }
        public string Message { get; set; }
        public bool IsNoMove { get; set; }
    }

    public class ServiceResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class AlertServiceResponse
    {
        public List<AlertModel> AlertList { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
