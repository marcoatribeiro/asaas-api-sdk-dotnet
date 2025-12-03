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
    /// Document group status
    /// </summary>
    [JsonPropertyName("status")]
    public AccountDocumentStatus? Status { get; set; }

    /// <summary>
    /// Type of documents
    /// </summary>
    [JsonPropertyName("type")]
    public AccountDocumentType? Type { get; set; }

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
