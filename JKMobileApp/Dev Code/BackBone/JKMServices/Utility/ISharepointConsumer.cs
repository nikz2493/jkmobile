using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public interface ISharepointConsumer
    {
        byte[] HandleResult(string filePath);
        List<string> GetDocumentList(string filePath);
    }
}
