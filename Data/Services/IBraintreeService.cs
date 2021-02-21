using Braintree;
using System;
using System.Collections.Generic;

namespace LibraryApp.Data.Services
{
    public interface IBraintreeService
    {
        string GenerateToken();

        Result<Transaction> SubmitForSettlement(Guid itemId, int userId, decimal price, string nonce);

        List<Plan> GetAllPlans();

        Result<Subscription> SubscribeTo(string id, string v);
    }
}
