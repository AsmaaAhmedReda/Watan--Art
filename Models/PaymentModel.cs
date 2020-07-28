using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WatanART.Models
{
    public class PaymentModel
    {
        [Required]
        [Display(Name = "Order Info")]
        public string OrderInfo { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "Pleae Enter Correct Purchase amount")]
        [DataType(DataType.Currency)]
        [Display(Name = "Purchase Amount")]
        public string PurchaseAmount { get; set; }

        
    }
}