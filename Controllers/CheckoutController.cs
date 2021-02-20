using System;
using LibraryApp.Data.Services;
using LibraryApp.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IBookService _courseService;
        private readonly IBraintreeService braintreeService;

        public CheckoutController(IBookService courseService, IBraintreeService braintreeService)
        {
            _courseService = courseService;
            this.braintreeService = braintreeService;
        }

        public IActionResult Purchase(Guid id)
        {
            var book = _courseService.GetById(id);
            if (book == null) return NotFound();

            var data = new BookPurchaseVM
            {
                Id = book.Id,
                Description = book.Description,
                Author = book.Author,
                Thumbnail = book.Thumbnail,
                Title = book.Title,
                Price = book.Price,
                Nonce = ""
            };

            ViewBag.ClientToken = braintreeService.GenerateToken();

            return View(data);
        }
    }
}