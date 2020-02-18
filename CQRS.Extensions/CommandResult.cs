using System;
using System.Collections.Generic;

namespace CQRS.Extensions
{
    public class CommandResult
    {
        public static CommandResult<TSuccess, object> Success<TSuccess>(TSuccess value, int statusCode = 201) where TSuccess : class 
            => CommandResult<TSuccess, object>.Success(value, statusCode);

        public static CommandResult<object, TError> Fail<TError>(TError error, int? statusCode) where TError : class
            => CommandResult<object, TError>.Fail(error, statusCode);

        public static CommandResult<object, TError> Fail<TError>(int? statusCode) where TError : class 
            => CommandResult<object, TError>.Fail(null, statusCode);
    }

    public class CommandResult<TSuccess, TError>
        where TSuccess : class
        where TError : class
    {
        public TSuccess Value { get; private set; }

        public bool IsSuccess { get; private set; }

        public List<TError> Errors { get; private set; }

        public int? StatusCode { get; private set; }

        private CommandResult()
        {
            this.IsSuccess = true;
            this.Errors = new List<TError> { };
        }

        private CommandResult(TError error, int? statusCode)
        {
            this.Errors = new List<TError> { error };
            this.IsSuccess = false;
            this.StatusCode = statusCode;
        }

        public static CommandResult<TSuccess, TError> Success(TSuccess value, int statusCode = 201) => new CommandResult<TSuccess, TError> { Value = value, StatusCode = statusCode };

        public static CommandResult<TSuccess, TError> Fail(TError error, int? statusCode) => new CommandResult<TSuccess, TError>(error, statusCode);

        public static CommandResult<TSuccess, TError> Fail(int? statusCode) => new CommandResult<TSuccess, TError>(null, statusCode);

    }
}
