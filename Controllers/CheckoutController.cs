using LibraryApp.Data.Services;
using LibraryApp.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IPaymentGatewayService _PaymentGatewayService;

        public CheckoutController(IBookService bookService, IPaymentGatewayService paymentGatewayService)
        {
            _bookService = bookService;
            _PaymentGatewayService = paymentGatewayService;
        }

        public IActionResult Purchase(string id)
        {
            var book = _bookService.GetById(id);
            if (book == null) return NotFound();

            var product = new BookPurchaseVM
            {
                Id = book.Id,
                Description = book.Description,
                Author = book.Author,
                Thumbnail = book.Thumbnail,
                Title = book.Title,
                Price = book.Price,
                Nonce = "" //?
            };

            ViewBag.ClientToken = _PaymentGatewayService.GenerateToken(product);

            return View(product);
        }

        [HttpPost]
        public IActionResult Create(BookPurchaseVM model)
        {
            var book = _bookService.GetById(model.Id);
            var result = _PaymentGatewayService
                .SubmitForSettlement(book.Id, book.Title, 1, book.Price, model.Nonce);

            if (result.IsSucceeded)
                return View("Success");

            return View("Failure");
        }

        public IActionResult OurPlans()
        {
            var plans = _PaymentGatewayService.GetAllPlans();

            return View(plans);
        }

        public IActionResult SubscribeToPlan(string id)
        {
            //it should come from customer's card
            var customerPaymentMethodToken = "MelodyB_d69s97r";
            
            var result = _PaymentGatewayService.SubscribeTo(id, customerPaymentMethodToken);
            if (result.IsSucceeded)
                return View("Subscribed");

            return View("NotSubscribed");
        }
    }
}