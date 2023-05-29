using System;
using System.Net;

namespace InvestmentAppProd.Models
{
    public class InvestmentBaseException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public InvestmentBaseException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}