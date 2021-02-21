namespace LibraryApp.Data.Services
{
    public class PurchaseResult
    {
        public PurchaseResult(bool succeeded)
        {
            IsSucceeded = succeeded;
        }

        public bool IsSucceeded { get; }
    }
}
