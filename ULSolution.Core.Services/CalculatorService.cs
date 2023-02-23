using ULSolution.Core.Contracts.Services;
using ULSolution.Core.Common.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ULSolution.Core.Services
{
    public class CalculatorService : ICalculatorService
    {
        public Response<string> EvaluateExpression(string expression)
        {
            return new Response<string>((string)Evaluate(expression),null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private  string Evaluate(string expression)
        {
            //Get List of Tokens
            var tokensList = GetTokens(expression);

            //Iterate and perform operation on list until list reduced to final single value.
            while (tokensList.Count > 1)
            {

                //Iterate over each item in list and perform operation* in each loop by using DMAS rule. 
                //*Perform arithmetic operation (/,*,+,-) and update operand1 with result and remove operator and operand2
                for (int i = 0; i < tokensList.Count(); i++)
                {
                    if (!char.IsDigit(tokensList[i][0]) && CanPerformOperation(tokensList, tokensList[i][0]))
                    {
                        switch (tokensList[i])
                        {
                            case ("/"):
                                tokensList[i - 1] = Convert.ToString(Convert.ToDecimal(tokensList[i - 1]) / Convert.ToDecimal(tokensList[i + 1]));
                                break;
                            case ("*"):
                                tokensList[i - 1] = Convert.ToString(Convert.ToDecimal(tokensList[i - 1]) * Convert.ToDecimal(tokensList[i + 1]));
                                break;
                            case ("+"):
                                tokensList[i - 1] = Convert.ToString(Convert.ToDecimal(tokensList[i - 1]) + Convert.ToDecimal(tokensList[i + 1]));
                                break;
                            case ("-"):
                                tokensList[i - 1] = Convert.ToString(Convert.ToDecimal(tokensList[i - 1]) - Convert.ToDecimal(tokensList[i + 1]));
                                break;
                        }
                        tokensList.RemoveAt(i + 1);
                        tokensList.RemoveAt(i);
                        break;
                    }

                }
            }

            return tokensList[0];

        }

        /// <summary>
        /// Iterate over each character of string to get digits and operators to Evaluate
        /// </summary>
        /// <param name="expression">String Expression</param>
        /// <returns>List of Tokens extracted from Expressoin</returns>
        private static List<string> GetTokens(string expression)
        {

            ValidateString(expression);

            char operation;
            int i = 0;
            List<string> tokensList = new List<string>();

            while (i < expression.Length)
            {
                int start = i;
                while (i < expression.Length && Char.IsDigit(expression[i]))
                {
                    i++;
                }
                string number = expression.Substring(start, i - start);
                tokensList.Add(number);
                if (i < expression.Length)
                {
                    operation = expression[i];
                    tokensList.Add(operation.ToString());
                }
                i++;

            }

            return tokensList;
        }


        /// <summary>
        ///This method validate string and throws exception if string is not in correct format.
        /// </summary>
        /// <param name="expression"></param>
        public static void ValidateString(string expression)
        {

            if (expression.Contains("."))
                throw new Exception("Decimals aren't allowed. Please enter integer value");
            if (expression.Contains("(") || expression.Contains(")"))
                throw new Exception("Application is not providing Brackets support. Please try again without brackets.");
            if (expression[0] == '-')
                throw new Exception("Oops! Only positive integers allowed.");
            else if (!char.IsDigit(expression[0]))
                throw new Exception("Input string was not in correct format.");

        }


        /// <summary>
        /// Method checks if passed operation allowed or Not. If validates DMAS Rule. 
        /// </summary>
        /// <param name="tokensList"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private static bool CanPerformOperation(List<string> tokensList, char operation)
        {
            switch (operation)
            {
                case '/':
                    return true;
                case '*':
                    return !tokensList.Any(s => s.Contains("/"));
                case '+':
                    return !(tokensList.Any(s => s.Contains("/") || s.Contains("*")));
                case '-':
                    return !(tokensList.Any(s => s.Contains("/") || s.Contains("*") || s.Contains("+")));
                default:
                    throw new Exception("Input string is in bad format.");

            }
        }

    }  
}
