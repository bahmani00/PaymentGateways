using System;

namespace PaymentGateways.Data.Models
{
    public class Charge {
        public string CustomerId { get; set; } 
        public long Amount { get; set; }
        public string ReceiptNumber { get; set; } 
        public string Currency { get; set; } 
        public DateTime Created { get; set; } 

        public static explicit operator Charge(Stripe.Charge charge){
            return new Charge{
                CustomerId = charge.CustomerId,
                Amount = charge.Amount,
                ReceiptNumber = charge.ReceiptNumber,
                Currency = charge.Currency,
                Created = charge.Created,
            };
        }             
    }
}