using JKMServices.DTO;
using System.Collections.Generic;

namespace JKMServices.DAL.CRM
{
    public interface IEstimateDetails
    {
        ServiceResponse<List<Estimate>> GetEstimateList(string customerId);
        ServiceResponse<List<Estimate>> GetEstimateData(List<string> estimateIdList);
        ServiceResponse<Estimate> PutEstimateData(string moveId, string jsonFormattedData);
        string GetEstimatePDF(string moveId);
    }
}