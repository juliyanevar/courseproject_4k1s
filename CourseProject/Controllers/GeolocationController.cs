using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using FreeGeoIPCore;
using MetOfficeDataPoint;
using MetOfficeDataPoint.Models;
using MetOfficeDataPoint.Models.GeoCoordinate;
using CourseProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace CourseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GeolocationController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public GeolocationController(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [HttpGet]
        [Route("Get")]
        public IActionResult Get(double longitude, double latitude)
        {
            FreeGeoIPClient ipClient = new FreeGeoIPClient();

            string ipAddress = Geolocation.GetRequestIP(_httpContextAccessor);

            FreeGeoIPCore.Models.Location location = ipClient.GetLocation(ipAddress).Result;

            GeoCoordinate coordinate = new GeoCoordinate();

            // If location is provided then use over IP address
            if (longitude == 0 && latitude == 0)
            {
                coordinate.Longitude = location.Longitude;
                coordinate.Latitude = location.Latitude;
            }
            else
            {
                coordinate.Longitude = longitude;
                coordinate.Latitude = latitude;
            }

            return Ok();
        }
    }
}
