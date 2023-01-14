using ASP.NET.Core.Useful.Techniques.Common.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Core_useful_techniques.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(
            RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.ContentType = new MediaTypeHeaderValue(ContentTypes.ApplicationJson).ToString();

            try
            {
                await this.next(context);
            }
            catch (ArgumentException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(this.CreateResponseContent(ex.Message), Encoding.UTF8);
            }
            catch (InvalidOperationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(this.CreateResponseContent(ex.Message), Encoding.UTF8);
            }
            catch (DbUpdateException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(this.CreateResponseContent(Messages.DATABASE_ERROR));
            }
            catch (DirectoryNotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(this.CreateResponseContent(Messages.DIRECTORY_NOT_FOUND));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(this.CreateResponseContent(Messages.SERVER_ERROR));
            }
        }

        private string CreateResponseContent(string message)
        {
            return "{\"messages\":[\"" + message + "\"]}";
        }
    }
}
