using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WatanART.Models
{
    public class PaymentReceiptModel
    {
        [Display(Name = "Txn Response Code")]
        public string vpcTxnResponseCode { get; set; }
        [Display(Name = "Merch Txn Ref")]
        public string vpc_MerchTxnRef { get; set; }
        [Display(Name = "Order Info")]
        public string vpc_OrderInfo { get; set; }
        [Display(Name = "Merchant")]
        public string vpc_Merchant { get; set; }
        [Display(Name = "Amount")]
        public string vpc_Amount { get; set; }
        [Display(Name = "Message")]
        public string vpc_Message { get; set; }
        [Display(Name = "Receipt No")]
        public string vpc_ReceiptNo { get; set; }
        [Display(Name = "Acq Response Code")]
        public string vpc_AcqResponseCode { get; set; }
        [Display(Name = "Authorize Id")]
        public string vpc_AuthorizeId { get; set; }
        [Display(Name = "Batch No")]
        public string vpc_BatchNo { get; set; }
        [Display(Name = "Transaction No")]
        public string vpc_TransactionNo { get; set; }
        [Display(Name = "Card")]
        public string vpc_Card { get; set; }
        [Display(Name = "3DSECI")]
        public string vpc_3DSECI { get; set; }
        [Display(Name = "3DSXID")]
        public string vpc_3DSXID { get; set; }
        [Display(Name = "3DS enrolled")]
        public string vpc_3DSenrolled { get; set; }
        [Display(Name = "3DS status")]
        public string vpc_3DSstatus { get; set; }
        [Display(Name = "Ver Token")]
        public string vpc_VerToken { get; set; }
        [Display(Name = "Ver Type")]
        public string vpc_VerType { get; set; }
        [Display(Name = "Ver Security Level")]
        public string vpc_VerSecurityLevel { get; set; }
        [Display(Name = "Ver Status")]
        public string vpc_VerStatus { get; set; }
        [Display(Name = "Risk Overall Result")]
        public string vpc_RiskOverallResult { get; set; }
        [Display(Name = "Txn Reversal Result")]
        public string vpc_TxnReversalResult { get; set; }
        [Display(Name = "Txn Response Code Desc")]
        public string TxnResponseCodeDesc { get; set; }

        // Card Security Code Fields
        [Display(Name = "csc Result Code")]
        public string vpc_cscResultCode { get; set; }
        [Display(Name = "csc Code Desc")]
        public string cscResultCodeDesc { get; set; }
    }
}