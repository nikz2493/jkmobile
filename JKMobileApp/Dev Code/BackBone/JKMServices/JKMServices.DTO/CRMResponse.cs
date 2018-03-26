using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKMServices.DTO
{
    public class CRMResponse<T>
    {
        public List<T> value { get; set; }
    }
}
