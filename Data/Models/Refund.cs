using System;

namespace PaymentGateways.Data.Models
{
    public class Refund {
        public string Reason { get; set; } 
        public long? Amount { get; set; }
        public string ReceiptNumber { get; set; } 
        public string Status { get; set; } 
        public string Currency { get; set; } 
        public DateTime Created { get; set; } 

        public static explicit operator Refund(Stripe.Refund refund){
            return new Refund{
                Reason = refund.Reason,
                Amount = refund.Amount,
                ReceiptNumber = refund.ReceiptNumber,
                Status = refund.Status,
                Currency = refund.Currency,
                Created = refund.Created,
            };
        }             
    }
}