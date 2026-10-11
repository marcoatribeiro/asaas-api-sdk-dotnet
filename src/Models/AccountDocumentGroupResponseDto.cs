using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account document group response
/// </summary>
public class AccountDocumentGroupResponseDto
{
    /// <summary>
    /// Unique document group identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Document group status. One of <c>NOT_SENT</c>, <c>PENDING</c>, <c>APPROVED</c>, <c>REJECTED</c> or
    /// <c>IGNORED</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Type of documents. One of <c>IDENTIFICATION</c>, <c>SOCIAL_CONTRACT</c>, <c>ENTREPRENEUR_REQUIREMENT</c>,
    /// <c>MINUTES_OF_ELECTION</c> or <c>CUSTOM</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Document group title
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Document group description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Who is responsible for sending these documents
    /// </summary>
    [JsonPropertyName("responsible")]
    public AccountDocumentResponsibleResponseDto? Responsible { get; set; }

    /// <summary>
    /// URL for onboarding
    /// </summary>
    [JsonPropertyName("onboardingUrl")]
    public string? OnboardingUrl { get; set; }

    /// <summary>
    /// Onboarding URL expiration date
    /// </summary>
    [JsonPropertyName("onboardingUrlExpirationDate")]
    public string? OnboardingUrlExpirationDate { get; set; }

    /// <summary>
    /// Documents that have already been sent with their respective identifiers
    /// </summary>
    [JsonPropertyName("documents")]
    public List<AccountDocumentGetResponseDto>? Documents { get; set; }
}
