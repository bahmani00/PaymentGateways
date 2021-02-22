using System.Collections.Generic;
using PaymentGateways.Data.Models;

namespace LibraryApp.Data.ViewModels
{
    public class DashboardVM
    {
        public Balance Balance { get; set; }
        public List<BalanceTransaction> Transactions { get; set; }
    }
}
