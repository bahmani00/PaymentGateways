using System;

namespace PaymentGateways.Data.Models
{
    public class Dispute {
        public string Reason { get; set; } 
        public long? Amount { get; set; }
        public string Status { get; set; } 
        public string Currency { get; set; } 
        public DateTime Created { get; set; } 

        public static explicit operator Dispute(Stripe.Dispute dispute){
            return new Dispute{
                Reason = dispute.Reason,
                Amount = dispute.Amount,
                Status = dispute.Status,
                Currency = dispute.Currency,
                Created = dispute.Created,
            };
        }             
    }
}