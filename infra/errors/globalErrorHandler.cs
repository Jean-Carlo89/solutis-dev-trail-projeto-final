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


                 HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                 string errorTitle = "Erro Interno do Servidor";
                 string errorMessage = "Ocorreu um erro inesperado.";


                 if (exception is DevError devError)
                 {

                     statusCode = devError.StatusCode;
                     errorTitle = "Erro de Regra de Negócio";
                     errorMessage = devError.Message;
                 }
                 else
                 {

                     errorMessage = exception?.Message;
                 }

                 context.Response.StatusCode = (int)statusCode;
                 context.Response.ContentType = "application/json";

                 await context.Response.WriteAsJsonAsync(new
                 {
                     title = errorTitle,
                     status = (int)statusCode,

                 });
             });
            });
        }
    }
}