using System;
using System.Collections.Generic;
using System.Text;

namespace JKMPCL.Model
{
    /// <summary>
    /// Class Name      : CreatePasswordModel
    /// Author          : Hiren Patel
    /// Creation Date   : 05 Dec 2017
    /// Purpose         : 
    /// Revision        : 
    /// </summary>
    public class CreatePasswordModel
    {
        public string CreatePassword { get; set; }
        public string VerifyPassword { get; set; }
        public string EmailId { get; set; }
    }

    /// <summary>
    /// Class Name      : PutCreatePasswordModel
    /// Author          : Hiren Patel
    /// Creation Date   : 05 Dec 2017
    /// Purpose         : 
    /// Revision        : 
    /// </summary>
    public class PutCreatePasswordModel
    {
        public string CustomerId { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
