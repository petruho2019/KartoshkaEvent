using System.Net;

namespace KartoshkaEvent.Domain.Common.Utils
{
    public record Error
    {
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
    public record Success<T>
    {
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
    public record Success
    {
        public HttpStatusCode StatusCode { get; set; }
    }
    public class Result<T>
    {
        public bool IsSuccess { get; set; } = false;
        public Success<T>? Success { get; set; }
        public Error? Error { get; set; }

        public static Result<T> FromError(Error error)
            => new() { Error = new() { ErrorMessage = error.ErrorMessage, StatusCode = error.StatusCode } };

        public static Result<T> Created(T data)
            => new() { Success = new() { Data = data, StatusCode = HttpStatusCode.Created }, IsSuccess = true };
        public static Result<T> Ok(T data)
            => new() { Success = new() { Data = data, StatusCode = HttpStatusCode.OK }, IsSuccess = true };
        public static Result<T> NoContent(T data)
            => new() { Success = new() { Data = data, StatusCode = HttpStatusCode.NoContent }, IsSuccess = true };
        public static Result<T> BadRequest(string errorMessage)
            => new() { Error = new() { ErrorMessage = errorMessage, StatusCode = HttpStatusCode.BadRequest } };
        
    }
    public class Result
    {
        public bool IsSuccess { get; set; }
        public Success? Success { get; set; }
        public Error? Error { get; set; }

        public static Result FromError(Error error)
            => new() { Error = new() { ErrorMessage = error.ErrorMessage, StatusCode = error.StatusCode } };

        public static Result NoContent()
            => new(){ Success = new() { StatusCode = HttpStatusCode.NoContent }, IsSuccess = true };
        public static Result Ok()
            => new() { Success = new() { StatusCode = HttpStatusCode.OK }, IsSuccess = true };
        public static Result Created()
            => new() { Success = new() { StatusCode = HttpStatusCode.Created }, IsSuccess = true };
        public static Result BadRequest(string errorMessage)
            => new() { Error = new() { ErrorMessage = errorMessage, StatusCode = HttpStatusCode.BadRequest } };
        
    }
}
