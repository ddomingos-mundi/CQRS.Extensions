using Automapper.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CQRS.Extensions.AspNetMVC
{
    public static class CommandResultExtension
    {
        public static ObjectResult AsCreatedResult<TSuccess, TError>(this CommandResult<TSuccess, TError> commandResult)
            where TSuccess : class
            where TError : class
        {
            if (commandResult.IsSuccess)
                return new ObjectResult(commandResult.Value) { StatusCode = StatusCodes.Status201Created };

            return new ObjectResult(commandResult.Errors) { StatusCode = commandResult.StatusCode ?? StatusCodes.Status412PreconditionFailed };
        }
        public static ObjectResult AsCreatedResultWithProjection<TSuccess, TError>(this CommandResult<TSuccess, TError> commandResult)
            where TSuccess : class
            where TError : class
        {
            if (commandResult.IsSuccess)
                return new ObjectResult(commandResult.Value.As<TSuccess>()) { StatusCode = StatusCodes.Status201Created };

            return new ObjectResult(commandResult.Errors) { StatusCode = commandResult.StatusCode ?? StatusCodes.Status412PreconditionFailed };
        }
        public static ObjectResult AsOKResult<TSuccess, TError>(this CommandResult<TSuccess, TError> commandResult)
            where TSuccess : class
            where TError : class
        {
            if (commandResult.IsSuccess)
                return new ObjectResult(commandResult.Value) { StatusCode = StatusCodes.Status200OK };

            return new ObjectResult(commandResult.Errors) { StatusCode = commandResult.StatusCode ?? StatusCodes.Status412PreconditionFailed };
        }

        public static ObjectResult AsOKResultWithProjection<TSuccess, TError>(this CommandResult<TSuccess, TError> commandResult)
            where TSuccess : class
            where TError : class
        {
            if (commandResult.IsSuccess)
                return new ObjectResult(commandResult.Value.As<TSuccess>()) { StatusCode = StatusCodes.Status200OK };

            return new ObjectResult(commandResult.Errors) { StatusCode = commandResult.StatusCode ?? StatusCodes.Status412PreconditionFailed };
        }

        public static CommandResult<TSuccess, TError> AddError<TSuccess, TError>(this CommandResult<TSuccess, TError> command, TError error)
            where TSuccess : class
            where TError : class
        {
            command.Errors.Add(error);

            return command;
        }
    }
}
