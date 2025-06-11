using Microsoft.AspNetCore.Mvc;
using SharedKernel.Results;

namespace ReserveHub.API.Extensions;

internal static class ResultExtensions
{
    internal static ValidationProblemDetails ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot convert a successful result to problem details.");
        }
        return result.Error.Type switch
        {
            ErrorType.NotFound => CreateProblemDetails(result.Error, StatusCodes.Status400BadRequest),
            ErrorType.Validation => CreateProblemDetails(result.Error, StatusCodes.Status400BadRequest),
            ErrorType.Conflict => CreateProblemDetails(result.Error, StatusCodes.Status409Conflict),
            ErrorType.Failure => CreateProblemDetails(result.Error, StatusCodes.Status400BadRequest),
            _ => CreateProblemDetails(result.Error, StatusCodes.Status500InternalServerError)
        };
    }
    private static ValidationProblemDetails CreateProblemDetails(
        Error error,
        int? status = null,
        Error[]? errors = null)
            => errors is not null
            ? new ValidationProblemDetails
            {
                Title = "One or more validations occurred.",
                Type = error.Type.ToString(),
                Status = status,
                Detail = error.Description,
                Extensions = { { nameof(errors), errors } }
            }
            : new ValidationProblemDetails
            {
                Title = "One or more validations occurred.",
                Type = error.Type.ToString(),
                Status = status,
                Extensions = { { nameof(errors), new[] { error } } }
            };
}
