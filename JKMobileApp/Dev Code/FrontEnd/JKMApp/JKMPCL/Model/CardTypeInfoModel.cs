using JKMPCL.Services;

namespace JKMPCL.Model
{
    /// <summary>
    /// Class Name      : AlertModel
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 31 Jan 2018
    /// Purpose         : DTO for credit card type
    /// Revision : 
    /// </summary>
    public class CardTypeInfoModel
    {
        public string Regex { get; set; }
        public int CardNumberLength { get; set; }
        public UtilityPCL.CardType CardType { get; set; }
    }
}
