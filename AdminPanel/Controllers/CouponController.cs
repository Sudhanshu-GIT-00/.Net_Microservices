﻿using AdminPanel.Service.IService;
using AdminPanel.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using AdminPanel.Models;

namespace AdminPanel.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();

            ResponseDto? response = await _couponService.GetAllCouponsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"]=response?.Message;
            }
            return View(list);
        }
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponsAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "coupon Created Successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {
			ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);

			if (response != null && response.IsSuccess)
			{
				CouponDto? moddel = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(moddel);
			}
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }
        [HttpPost]
		public async Task<IActionResult> CouponDelete(CouponDto couponDto)
		{
			ResponseDto? response = await _couponService.DeleteCouponsAsync(couponDto.CouponId);

			if (response != null && response.IsSuccess)
			{
                TempData["Success"] = "coupon Deleted Successfully";
                return RedirectToAction(nameof(CouponIndex));
			}
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(couponDto);
		}
	}
}