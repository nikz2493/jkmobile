using System.Collections.Generic;

namespace JKMWindowsService.Model
{
    /// <summary>
    /// Class Name      : APIResponse
    /// Author          : Pratik Soni
    /// Creation Date   : 23 Feb 2018
    /// Purpose         : To get API response. 
    /// Revision        : 
    /// </summary> 
    public class APIResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public string Information { get; set; }
        public string BadRequest { get; set; }
    }
}
