using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using CarHistory.Services;

namespace CarHistory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VINController : ControllerBase
    {
        ParserService parserService = new ParserService();

        [HttpGet]
        public IActionResult Get()
        {
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var url = @"http://autoauctionhistory.com/search/" + id + "/";
            var result = parserService.GetInformationByVIN(url);
             
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return BadRequest();
        }
    }
}
