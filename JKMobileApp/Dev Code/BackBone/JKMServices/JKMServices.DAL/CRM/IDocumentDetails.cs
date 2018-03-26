using JKMServices.DTO;
using System.Collections.Generic;

namespace JKMServices.DAL.CRM
{
    public interface IDocumentDetails
    {
        ServiceResponse<List<Document>> GetDocumentList(string customerId);
    }
}
