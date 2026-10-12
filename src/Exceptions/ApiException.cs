using Asaas.Sdk.Models;

namespace Asaas.Sdk.Exceptions;

/// <summary>
/// Base exception for API errors
/// </summary>
public class ApiException : Exception
{
    /// <summary>
    /// HTTP status code of the error
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Response body content, exactly as received
    /// </summary>
    public string? ResponseBody { get; }

    /// <summary>
    /// The errors the Asaas reported in the documented <c>errors[]</c> array, in response order, each with its
    /// <see cref="ErrorItemDto.Code"/> and <see cref="ErrorItemDto.Description"/>. Empty when the response had none. Branch
    /// on the code (for example <c>invalid_dueDate</c>) instead of matching <see cref="Exception.Message"/>.
    /// </summary>
    public IReadOnlyList<ErrorItemDto> Errors { get; } = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class.
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="statusCode">HTTP status code</param>
    /// <param name="responseBody">Response body content</param>
    public ApiException(string message, int statusCode, string? responseBody = null) 
        : base(message)
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class with the errors the Asaas reported.
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="statusCode">HTTP status code</param>
    /// <param name="responseBody">Response body content</param>
    /// <param name="errors">Errors parsed from the response, in order</param>
    public ApiException(string message, int statusCode, string? responseBody, IEnumerable<ErrorItemDto> errors)
        : base(message)
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
        Errors = errors.ToList();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class.
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="statusCode">HTTP status code</param>
    /// <param name="responseBody">Response body content</param>
    /// <param name="innerException">Inner exception</param>
    public ApiException(string message, int statusCode, string? responseBody, Exception innerException) 
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }
}

/// <summary>
/// Exception for validation errors
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// List of validation errors
    /// </summary>
    public IReadOnlyList<string> Errors { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    /// <param name="errors">List of validation errors</param>
    public ValidationException(IEnumerable<string> errors) 
        : base("Validation failed")
    {
        Errors = errors.ToList();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    /// <param name="error">Single validation error</param>
    public ValidationException(string error) 
        : base("Validation failed")
    {
        Errors = new List<string> { error };
    }
}
