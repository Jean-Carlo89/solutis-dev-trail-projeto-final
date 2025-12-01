using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace SeuProjeto.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
             {
                 var exceptionHandlerPathFeature =
                     context.Features.Get<IExceptionHandlerPathFeature>();

                 var exception = exceptionHandlerPathFeature?.Error;

                 string errorMessage = null;
                 string errorTitle = "Erro Interno do Servidor";
                 HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

                 if (exception is DevError devError)
                 {

                     statusCode = devError.StatusCode;
                     errorTitle = "Erro de Regra de Negócio";
                     errorMessage = devError.Message;
                 }
                 else
                 {

                     errorMessage = null;
                 }

                 context.Response.StatusCode = (int)statusCode;
                 context.Response.ContentType = "application/json";


                 object responseBody;

                 if (errorMessage != null)
                 {

                     responseBody = new
                     {
                         title = errorTitle,
                         status = (int)statusCode,
                         message = errorMessage
                     };
                 }
                 else
                 {

                     responseBody = new
                     {
                         title = errorTitle,
                         status = (int)statusCode
                     };
                 }

                 await context.Response.WriteAsJsonAsync(responseBody);
             });
            });
        }
    }
}