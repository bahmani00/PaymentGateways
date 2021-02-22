using LibraryApp.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class AdminController : Controller
    {
         private readonly IPaymentGatewayService _PaymentGatewayService;

        public AdminController(IPaymentGatewayService paymentGatewayService)
        {
            _PaymentGatewayService = paymentGatewayService;
        }
        
        public IActionResult Index()
        {
            var response = _PaymentGatewayService.GetTotalBalance();

            return View(response);
        }
    }
}