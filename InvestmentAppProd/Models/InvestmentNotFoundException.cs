using System;
using System.Net;

namespace InvestmentAppProd.Models
{
    public class InvestmentNotFoundException : InvestmentBaseException
    {
        public InvestmentNotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity) : base(message, statusCode)
        {

        }

    }
}