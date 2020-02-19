using Automapper.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Extensions.AspNetMVC
{
    public static class ResultExtension
    {
        public static ObjectResult AsActionResult<TSuccess>(this Result<TSuccess> result)
            where TSuccess : class
        {
            if (result.IsSuccess)
            {
                return new ObjectResult(result.Value) 
                { 
                    StatusCode = result.StatusCode ?? 200 
                };
            }

            return new ObjectResult(result.Errors) 
            { 
                StatusCode = result.StatusCode ?? 400
            };
        }

        public static ObjectResult AsActionResult<TSuccess, TProjection>(this Result<TSuccess> result) where TProjection : class
          where TSuccess : class
        {
            if (result.IsSuccess)
            {
                return new ObjectResult(result.Value.As<TProjection>())
                {
                    StatusCode = result.StatusCode ?? 200
                };
            }

            return new ObjectResult(result.Errors)
            {
                StatusCode = result.StatusCode ?? 400
            };
        }
    }
}