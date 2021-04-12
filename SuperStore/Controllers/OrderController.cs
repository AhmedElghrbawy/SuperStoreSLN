using Microsoft.AspNetCore.Mvc;
using SuperStore.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetUserOrderHistoryAsync(this.User);
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeOrder()
        {
            await _orderService.MakeAnOrderAsync(this.User);
            return RedirectToAction("Index");
        }
    }
}
