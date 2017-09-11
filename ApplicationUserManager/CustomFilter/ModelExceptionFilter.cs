using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using UserAppService.Exceptions;

namespace UserAppService.CustomFilter
{
    public class ModelExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            HttpError error = null;
            HttpResponseMessage response = null;
            //if (context.Exception is EntityNotFoundException)
            //{
            //    var e = (EntityNotFoundException)context.Exception;
            //    error = new HttpError
            //    {
            //        {"StatusCode", HttpStatusCode.NotFound},
            //        {"Message", String.Format(ResourcesFiles.LocalizedText.EntityNotFound, e.Entity, e.EntityId) }
            //        //{"Message", ResourcesFiles.LocalizedText.EntityNotFound + $"{e.Entity} " + $"{e.EntityId}"}
            //    };
            //    response = context.Request.CreateErrorResponse(HttpStatusCode.NotFound, error);
            //}
            //else 
            if (context.Exception is UserFriendlyException)
            {
                var e = (UserFriendlyException)context.Exception;
                error = new HttpError
                {
                    {"StatusCode", HttpStatusCode.Conflict},
                    {"Message", e.Message}
                };
                response = context.Request.CreateErrorResponse(HttpStatusCode.Conflict, error);
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                var e = (UnauthorizedAccessException)context.Exception;
                error = new HttpError
                {
                    {"StatusCode", HttpStatusCode.Unauthorized},
                    //{"Message", ResourceFiles.LocalizedText.InsufficientPermission + e.Message}
                    {"Message", "InsufficientPermission"}
                };
                response = context.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, error);
            }
            
            else
            {
                error = new HttpError
                {
                    {"StatusCode", HttpStatusCode.InternalServerError},
                    {"Message", context.Exception.Message}
                };
                response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error);
            }
            throw new HttpResponseException(response);
        }
    }
}