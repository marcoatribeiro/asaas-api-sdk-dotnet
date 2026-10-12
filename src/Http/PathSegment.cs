using System.Runtime.CompilerServices;

namespace Asaas.Sdk.Http;

/// <summary>
/// Validates and encodes a caller-supplied value before it goes into a request path, so that it is always one path segment
/// and cannot change the route.
/// </summary>
/// <remarks>
/// The Asaas expects raw identifiers, so the value is encoded exactly once: an identifier that already contains
/// <c>%2F</c> is sent as <c>%252F</c> and reaches the Asaas as that literal text, never decoded twice.
/// </remarks>
internal static class PathSegment
{
    /// <summary>
    /// Encode <paramref name="value"/> as a single path segment.
    /// </summary>
    /// <param name="value">Raw identifier, token or provider</param>
    /// <param name="paramName">Name reported in the exception; filled in with the argument expression</param>
    /// <returns>The percent-encoded segment</returns>
    /// <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is empty, whitespace, or exactly <c>.</c> or <c>..</c>. <c>Uri.EscapeDataString</c> leaves
    /// dots alone and <see cref="Uri"/> then collapses them, so they have to be rejected.
    /// </exception>
    public static string Escape(string? value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, paramName);

        if (value is "." or "..")
        {
            throw new ArgumentException("A path segment cannot be a dot segment.", paramName);
        }

        return Uri.EscapeDataString(value);
    }
}
