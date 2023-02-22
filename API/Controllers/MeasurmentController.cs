using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aqua_Sharp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurmentController : ControllerBase
    {
        private readonly IMeasurmentService _measurmentService;

        public MeasurmentController(IMeasurmentService measurmentService)
        {
            _measurmentService = measurmentService; 
        }
    }
}
