using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var sum = AddNumbers(input1, input2);

            return sum;
        }

        private int AddNumbers(string input1, string input2)
        {
            int.TryParse(input1, out int number1);
            int.TryParse(input2, out int number2);
            var sum = number1 + number2;

            return sum;
        }
    }
}
