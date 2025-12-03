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
    /// Response body content
    /// </summary>
    public string? ResponseBody { get; }

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
