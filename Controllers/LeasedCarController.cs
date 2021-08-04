using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeasedCarService.Models;
using LeasedCarService.Repository;
using LeasedCarService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LeasedCarService.Controllers
{
    public class LeasedCarController : Controller
    {
        private readonly LeasedCarListService _LeasedCarListService;
        private IConfiguration _config;

        public LeasedCarController(IConfiguration config)
        {
            _config = config;
            _LeasedCarListService = new LeasedCarListService(_config);
        }
        [HttpPost]
        public IActionResult GetWorkingCarList([FromBody]WorkingCarListRequest Req)
        {
            try
            {
                if (_LeasedCarListService.Lookup(Req.ID, Req.PhoneNumber) == null) 
                {
                    return Json(new ApiError("204", "查無資料"));
                }
                return Json(_LeasedCarListService.Lookup(Req.ID, Req.PhoneNumber));
            }
            catch (Exception ex)
            {
                return Json(new ApiError("500", ex.Message));
            }
        }
    }
}
