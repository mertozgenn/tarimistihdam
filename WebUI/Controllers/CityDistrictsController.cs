﻿using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class CityDistrictsController : Controller
    {
        private ICityDistrictService _cityDistrictService;

        public CityDistrictsController(ICityDistrictService cityDistrictService)
        {
            _cityDistrictService=cityDistrictService;
        }

        public JsonResult GetDistricts([FromQuery] int cityId)
        {
            var districts = _cityDistrictService.GetDistrictsByCityId(cityId).Data;
            return Json(districts);
        }
    }
}