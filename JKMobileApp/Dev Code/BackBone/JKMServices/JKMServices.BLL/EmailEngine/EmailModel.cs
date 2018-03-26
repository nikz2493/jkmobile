using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKMServices.BLL.Model
{
    public class VerificationEmailModel
    {
        public string MoveCoOrdinatorEmailId { get; set; }
        public int verificationCode { get; set; }
    }

    public class MoveStatusChangedEmailModel
    {
        public string MoveCoOrdinatorEmailId { get; set; }
        public string MoveNumber { get; set; }

        private String _CustomerContactNumber;
        public String CustomerContactNumber
        {
            get { return _CustomerContactNumber ?? String.Empty; }
            set { _CustomerContactNumber = value; }
        }

        private String _CustomerEmailAddress;
        public String CustomerEmailAddress
        {
            get { return _CustomerEmailAddress ?? String.Empty; }
            set { _CustomerEmailAddress = value; }
        }
    }

    public class EstimateDetailsUpdatedEmailModel
    {
        public string MoveCoOrdinatorEmailId { get; set; }
        public string MoveNumber { get; set; }
        public List<string> FieldList { get; set; }
    }   
}
