using System.Collections.Generic;
using PaymentGateways.Data.Models;

namespace LibraryApp.Data.ViewModels
{
    public class TransactionBundle
    {
        public Balance Balance { get; set; }
        public List<BalanceTransaction> Transactions { get; set; }
        
        public List<Customer> Customers { get; internal set; }
    }
}
