using System;
using LibraryApp.Data.Services;
using LibraryApp.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IBraintreeService _braintreeService;

        public CheckoutController(IBookService bookService, IBraintreeService braintreeService)
        {
            _bookService = bookService;
            _braintreeService = braintreeService;
        }

        public IActionResult Purchase(Guid id)
        {
            var book = _bookService.GetById(id);
            if (book == null) return NotFound();

            var data = new BookPurchaseVM
            {
                Id = book.Id,
                Description = book.Description,
                Author = book.Author,
                Thumbnail = book.Thumbnail,
                Title = book.Title,
                Price = book.Price,
                Nonce = "" //?
            };

            ViewBag.ClientToken = _braintreeService.GenerateToken();

            return View(data);
        }

        public IActionResult Create(BookPurchaseVM model)
        {
            var book = _bookService.GetById(model.Id);
            var result = _braintreeService
                .SubmitForSettlement(book.Id, 1, book.Price, model.Nonce);

            if (result.IsSuccess())
                return View("Success");

            return View("Failure");
        }

        public IActionResult OurPlans()
        {
            var plans = _braintreeService.GetAllPlans();

            return View(plans);
        }

        public IActionResult SubscribeToPlan(string id)
        {
            //it should come from customer's card
            var customerPaymentMethodToken = "MelodyB_d69s97r";
            var result = _braintreeService.SubscribeTo(id, customerPaymentMethodToken);
            if (result.IsSuccess())
                return View("Subscribed");

            return View("NotSubscribed");
        }
    }
}