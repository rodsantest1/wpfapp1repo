using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SimpleMathController : ControllerBase
    {
        [HttpGet("{input1}/{input2}")]

        public int Add(string input1, string input2)
        {
            var sum = Class1.AddNumbers(input1, input2);

            return sum;
        }


    }
}
