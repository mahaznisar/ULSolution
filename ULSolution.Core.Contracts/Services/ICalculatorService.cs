
using ULSolution.Core.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ULSolution.Core.Contracts.Services
{
    public interface ICalculatorService
    {
    
        Response<string> EvaluateExpression(string expression);

  
    }
}
