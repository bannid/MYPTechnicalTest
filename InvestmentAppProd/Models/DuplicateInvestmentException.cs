using System;
using System.Net;

namespace InvestmentAppProd.Models
{
    public class DuplicateInvestmentFoundException : InvestmentBaseException
    {
        public DuplicateInvestmentFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity) : base(message, statusCode)
        {
            
        }

    }
}