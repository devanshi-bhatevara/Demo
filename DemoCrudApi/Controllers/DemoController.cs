using DemoCrudApi.Dtos;
using DemoCrudApi.Models;
using DemoCrudApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IService _service;
        public DemoController(IService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var response = _service.GetDemos();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("GetDemoById/{id}")]

        public IActionResult GetDemoById(int id)
        {
            var response = _service.GetDemo(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult AddDemo(AddDemoDto demoDto)
        {

            var demo = new Demo()

            {
                Name = demoDto.Name,
                Age = demoDto.Age
            };

            var result = _service.AddDemo(demo);
            return !result.Success ? BadRequest(result) : Ok(result);

        }

        [HttpPut("Modify")]

        public IActionResult UpdateDemo(DemoDto demoDto)
        {
            var demo = new Demo()

            {
                Id = demoDto.Id,
                Name = demoDto.Name,
                Age = demoDto.Age
            };

            var response = _service.ModifyDemo(demo);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }

        }

        [HttpDelete("Remove/{id}")]

        public IActionResult Remove(int id)
        {
            if (id > 0)
            {
                var response = _service.RemoveDemo(id);
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            else
            {
                var response = new ServiceResponse<string>();
                response.Success = false;
                response.Message = "Enter correct data please";
                return BadRequest(response);
            }

        }
    }
}
