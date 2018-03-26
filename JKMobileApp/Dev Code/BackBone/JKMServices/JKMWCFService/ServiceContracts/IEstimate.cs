using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace JKMWCFService
{
    /// <summary>
    /// Interface Name      : IEstimate
    /// Author              : Pratik Soni
    /// Creation Date       : 12 Jan 2018
    /// Purpose             : Gets list of estimates and their data for the specified customer
    /// Revision            :
    /// </summary>
    /// <returns></returns>
    [ServiceContract]
    public interface IEstimate
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/{customerId}")]
        string GetEstimateData(string customerId);

        [OperationContract]
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "/{moveId}/file")]
        string GetEstimatePDF(string moveId);

        [OperationContract]
        [WebInvoke(Method = "PUT",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "/{moveId}")]
        string PutEstimateData(string moveId, EstimateDetail estimateDetail);

    }

    [DataContract]
    public class EstimateDetail
    {
        /* Address fields */
        [DataMember]
        public string CustomOriginAddress { get; set; }
        [DataMember]
        public string CustomDestinationAddress { get; set; }

        /* Service Date fields */
        [DataMember]
        public string PackStartDate { get; set; }
        [DataMember]
        public string LoadStartDate { get; set; }
        [DataMember]
        public string MoveStartDate { get; set; }

        /* Valuation fields */
        [DataMember]
        public string ExcessValuation { get; set; }
        [DataMember]
        public string ValuationDeductible { get; set; }
        [DataMember]
        public string ValuationCost { get; set; }
        [DataMember]
        public string ServiceCode { get; set; } //Mocked for sprint 4 because no field mapped to JIM database yet

        [DataMember]
        public string WhatMattersMost { get; set; }

        [DataMember]
        public bool IsServiceDate { get; set; }
        [DataMember]
        public bool IsAddressEdited { get; set; }
        [DataMember]
        public bool IsValuationEdited { get; set; }
        [DataMember]
        public bool IsWhatMatterMostEdited { get; set; }
    }
}