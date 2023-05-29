using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvestmentAppProd.Models;
using InvestmentAppProd.Data;

namespace InvestmentAppProd.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InvestmentController : Controller
    {
        private readonly IInvestmentRepository _context;

        public InvestmentController(IInvestmentRepository context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Investment>> FetchInvestments()
        {
            
            return Ok(_context.GetInvestments());
        }

        [HttpGet("name")]
        public ActionResult<Investment> FetchInvestment([FromQuery] string name)
        {
            var investment = _context.GetInvestment(name);
            return Ok(investment);
        }


        [HttpPost]
        public ActionResult<Investment> AddInvestment([FromBody] Investment investment)
        {
            // TODO: Cant find this in the requirements.
            if (investment.StartDate > DateTime.Now)
                return BadRequest("Investment Start Date cannot be in the future.");
            investment.CalculateValue();
            _context.InsertInvestment(investment);
            return CreatedAtAction("AddInvestment", investment.Name, investment);
        }

        [HttpPut("name")]
        public ActionResult UpdateInvestment([FromQuery] string name, [FromBody] Investment investment)
        {
            if (name != investment.Name)
                return BadRequest("Name does not match the Investment you are trying to update.");
            // TODO: Cant find this in the requirements.
            if (investment.StartDate > DateTime.Now)
                return BadRequest("Investment Start Date cannot be in the future.");

            investment.CalculateValue();
            _context.UpdateInvestment(investment);

            return NoContent();
        }

        [HttpDelete("name")]
        public ActionResult DeleteInvestment([FromQuery] string name)
        {
            _context.DeleteInvestment(name);
            return NoContent();
        }
    }
}
