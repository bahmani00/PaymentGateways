using System;

namespace PaymentGateways.Data.Models
{
    public class BalanceTransaction
    {
        public long Fee { get; set; }
        
        public string Status { get; set; }
        
        public decimal? ExchangeRate { get; set; }
        
        public string Currency { get; set; }
        
        public DateTime Created { get; set; }
        
        public DateTime AvailableOn { get; set; }
        
        public long Amount { get; set; }
        
        public string Id { get; set; }
        public string Type { get; set; }
        
        public string Description { get; set; }

        public static explicit operator BalanceTransaction(Stripe.BalanceTransaction transaction){
            return new BalanceTransaction{
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Created = transaction.Created,
                AvailableOn = transaction.AvailableOn,
                Currency = transaction.Currency,
                Status = transaction.Status,
            };
        }
    }
}