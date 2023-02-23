namespace ULSolution.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ULSolution.Core.Contracts.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            this.calculatorService = calculatorService;
        }

        [AllowAnonymous]
        [HttpGet]
        public  IActionResult Get(string expression)
        {
            return this.Ok(this.calculatorService.EvaluateExpression(expression));
        }
    }
}