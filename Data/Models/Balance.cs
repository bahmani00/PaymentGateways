using System.Collections.Generic;
using System.Linq;

namespace PaymentGateways.Data.Models
{
    public class Balance {
        public List<BalanceAmount> Available { get; set; }
       
        public List<BalanceAmount> Pending { get; set; }

        public static explicit operator Balance(Stripe.Balance balance){
            return new Balance{
                Available = balance.Available.Select(x => (BalanceAmount)x).ToList(),
                Pending = balance.Pending.Select(x => (BalanceAmount)x).ToList()
            };
        }        
    }

    public class BalanceAmount {
        public long Amount { get; set; }
        public string Currency { get; set; } 

        public static explicit operator BalanceAmount(Stripe.BalanceAmount balance){
            return new BalanceAmount{
                Amount = balance.Amount,
                Currency = balance.Currency,
            };
        }             
    }
}