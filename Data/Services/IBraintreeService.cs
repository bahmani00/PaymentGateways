using Braintree;
using System;

namespace LibraryApp.Data.Services
{
    public interface IBraintreeService
    {
        string GenerateToken();

        Result<Transaction> SubmitForSettlement(Guid itemId, int userId, decimal price, string nonce);

    }
}
