using Braintree;

namespace LibraryApp.Data.Services
{
    public class BraintreeService : IBraintreeService
    {
        private readonly IBraintreeGateway braintreeGateway;

        public BraintreeService(IBraintreeGateway braintreeGateway)
        {
            this.braintreeGateway = braintreeGateway;
        }

        public string GenerateToken()
        {
            return braintreeGateway.ClientToken.Generate();
        }
    }
}
