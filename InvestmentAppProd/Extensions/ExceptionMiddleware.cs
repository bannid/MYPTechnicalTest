using GlobalErrorHandling.Models;
using InvestmentAppProd.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace GlobalErrorHandling.Extensions
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    string message = "";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var pathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    if (contextFeature != null)
                    {
                        if (pathFeature != null)
                        {
                            switch (pathFeature.Error)
                            {
                                case InvestmentBaseException:
                                    {
                                        InvestmentBaseException ex = (InvestmentBaseException)pathFeature.Error;
                                        message = ex.Message;
                                        context.Response.StatusCode = (int)ex.StatusCode;
                                        break;
                                    }
                                default:
                                    {
                                        message = "Internal Server Error";
                                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                        context.Response.ContentType = "application/json";
                                        break;
                                    }
                            }
                        }
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = message
                        }.ToString());
                    }
                });
            });
        }
    }
}