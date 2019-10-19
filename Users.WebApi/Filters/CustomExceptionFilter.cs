namespace Users.WebApi.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    using BLL.Exceptions;

    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ResourceNotFoundException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            else if (context.Exception is ResourceHasConflictException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Conflict);
            }
            else
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}