using JKMPCL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace JKMPCL.Services
{
    /// <summary>
    /// Class Name      : MyDocument
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 09 Feb 2018
    /// Purpose         : to get set my document details from service.
    /// Revision        :  
    /// </summary>
    public class MyDocument
    {
        private readonly MyDocumentAPIService myDocumentAPIService;

        //Constructor
        public MyDocument()
        {
            myDocumentAPIService = new MyDocumentAPIService();
        }

        /// <summary>
        /// Method Name     : GetDocumentList
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 09 Feb 2018
        /// Purpose         : call service to get list of documents for customer
        /// Revision        :  
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<APIResponse<List<DocumentModel>>> GetDocumentList(string customerID)
        {
            APIResponse<List<DocumentModel>> response = new APIResponse<List<DocumentModel>>() { STATUS = false };
            response = await myDocumentAPIService.GetDocumentList(customerID);
            if ((response.STATUS) && ((response.DATA is null) || (response.DATA != null && response.DATA.Count <= 0)))
            {
                response.Message = Resource.msgNoDocumentFound;
                response.STATUS = false;
            }
            return response;
        }

        /// <summary>
        /// Method Name     : GetEstimatePDF
        /// Author          : Vivek Bhavsar
        /// Creation Date   : 09 Feb 2018
        /// Purpose         : get pdf for input document id
        /// Revision        : 
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public async Task<APIResponse<GetDocumentPDF>> GetEstimatePDF(string documentID)
        {
            APIResponse<GetDocumentPDF> response = await myDocumentAPIService.GetDocumentPDF(documentID);
            return response;
        }
    }
}
