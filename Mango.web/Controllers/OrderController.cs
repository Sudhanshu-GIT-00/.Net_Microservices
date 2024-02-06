﻿using Mango.web.Models;
using Mango.web.Service.IService;
using Mango.web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.web.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;
		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		public IActionResult OrderIndex()
		{
			return View();
		}
		public async Task<IActionResult> OrderDetail(int orderId)
		{
			OrderHeaderDto orderHeaderDto = new OrderHeaderDto();
			string userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

			var response = await _orderService.GetOrder(orderId);
			if (response != null && response.IsSuccess)
			{
				orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));

			}
			if (!User.IsInRole(SD.RoleAdmin) && userId != orderHeaderDto.UserId)
			{
				return NotFound();
			}
			return View(orderHeaderDto);
		}

		[HttpPost("OrderReadyForPickup")]
		public async Task<IActionResult> OrderReadyForPickup(int orderId)
		{
            var response = await _orderService.UpdateOrderStatus(orderId,SD.Status_ResdyForPickup);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status update successfully";
				return RedirectToAction(nameof(OrderDetail),new { orderId = orderId });

            }            
            return View();
        }
		[HttpPost("CancelOrder")]
		public async Task<IActionResult> CancelOrder(int orderId)
		{
            var response = await _orderService.UpdateOrderStatus(orderId,SD.Status_Cancelled);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status update successfully";
				return RedirectToAction(nameof(OrderDetail),new { orderId = orderId });

            }            
            return View();
        }
		[HttpPost("CompleteOrder")]
		public async Task<IActionResult> CompleteOrder(int orderId)
		{
            var response = await _orderService.UpdateOrderStatus(orderId,SD.Status_Completed);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status update successfully";
				return RedirectToAction(nameof(OrderDetail),new { orderId = orderId });

            }            
            return View();
        }

        [HttpGet]
		public IActionResult GetAll()
		{
			List<OrderHeaderDto> list;
			string userId = "";
			if (!User.IsInRole(SD.RoleAdmin))
			{
				userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
			}
			ResponseDto response = _orderService.GetAllOrder(userId).GetAwaiter().GetResult();
			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result));
			}
			else
			{
				list = new List<OrderHeaderDto>();
			}
			return Json(new { data = list });
		}
	}
}