using JKMServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Utility;

namespace JKMServices.BLL
{
    /// <summary>
    /// Class Name      : ServiceBase
    /// Author          : Pratik Soni
    /// Creation Date   : 16 Dec 2017
    /// Purpose         : Base class for all BLL files.
    /// Revision        :
    /// </summary>
    /// <returns></returns>
    public class ServiceBase
    {
        /// <summary>
        /// PRIVATE CONSTANT STIRNGS
        /// </summary>
        protected const string STATUS = "STATUS";
        protected const string SUCCESS = "SUCCESS";
        protected const string DATA = "DATA";
        protected const string FAIL = "FAIL";
        protected const string MESSAGE = "MESSAGE";
        protected const string BADREQUEST = "BADREQUEST";
        protected const string INFORMATION = "INFORMATION";

        protected ServiceBase() { }

        /// <summary>
        /// Method Name     : GenerateServiceResponse - overloaded method
        /// Author          : Pratik Soni
        /// Creation Date   : 16 Feb 2018
        /// Purpose         : Generate Service Response base on T object
        /// Revision        : 
        /// </summary>
        protected string GenerateServiceResponse<T>(T dtoValue)
        {
            List<DTO.ServiceResponse<T>> serviceResponse = new List<DTO.ServiceResponse<T>>
            {
                new DTO.ServiceResponse<T> { Data = dtoValue }
            };
            return General.ConvertToJson<DTO.ServiceResponse<T>>(serviceResponse.FirstOrDefault());
        }

        /// <summary>
        /// Method Name     : GenerateServiceResponse - overloaded method
        /// Author          : Pratik Soni
        /// Creation Date   : 16 Feb 2018
        /// Purpose         : Generate Service Response base on T object
        /// Revision        : 
        /// </summary>
        protected string GenerateServiceResponse<T>(string messageType = "", string messageValue = "")
        {
            string message = string.Empty;
            string information = string.Empty;
            string badRequest = string.Empty;

            switch (messageType.ToUpper())
            {
                case INFORMATION:
                    information = messageValue;
                    break;
                case MESSAGE:
                    message = messageValue;
                    break;
                case BADREQUEST:
                    badRequest = messageValue;
                    break;
            }

            List<DTO.ServiceResponse<T>> serviceResponse = new List<DTO.ServiceResponse<T>>
            {
                new DTO.ServiceResponse<T> { Information = information, Message = message, BadRequest = badRequest}
            };
            return General.ConvertToJson<DTO.ServiceResponse<T>>(serviceResponse.FirstOrDefault());
        }

        /// <summary>
        /// Method Name     : GetFinalResponseForPostService
        /// Author          : Pratik Soni
        /// Creation Date   : 30 Jan 2018
        /// Purpose         : Generate Service Response for POST service based on T object
        /// Revision        : 
        /// </summary>
        /// <returns>service response in string object</returns>
        protected string GetFinalResponseForPostService<T>(ServiceResponse<T> crmResponse)
        {
            if (crmResponse.Information == "204 - NoContent")
            {
                return GenerateServiceResponse<Payment>(DATA, "Saved Successfully");
            }
            else if (!string.IsNullOrEmpty(crmResponse.Information))
            {
                return GenerateServiceResponse<Payment>(INFORMATION, crmResponse.Information);
            }
            else if (!string.IsNullOrEmpty(crmResponse.Message))
            {
                return GenerateServiceResponse<Payment>(MESSAGE, crmResponse.Message);
            }
            return GenerateServiceResponse<Payment>(BADREQUEST, "Bad Request");
        }

        /// <summary>
        /// Method Name     : GetConditionalResponseForDocument
        /// Author          : Pratik Soni
        /// Creation Date   : 13 Feb 2018
        /// Purpose         : To return the formatted service response for PDF
        /// Revision        : 
        /// </summary>
        /// <returns>service response in string object</returns>
        protected string GetConditionalResponseForDocument(byte[] fileContentInByteArray)
        {
            string fileContent = string.Empty;
            if (fileContentInByteArray.Length > 0)
            {
                fileContent = Convert.ToBase64String(fileContentInByteArray);
                if (string.IsNullOrEmpty(fileContent))
                {
                    return GenerateServiceResponse<string>(MESSAGE, "Service Unavailable.");
                }
                return GenerateServiceResponse(fileContent);
            }
            else
            {
                return GenerateServiceResponse<string>(INFORMATION, "No document found for given Move.");
            }
        }
    }
}
