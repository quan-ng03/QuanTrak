using Microsoft.EntityFrameworkCore;

namespace backend.Services;

// API Key Authenticator Service Class to check with the saved key in docker env

public class ApiValidator
{
    public string ApiKey { get; }

    public ApiValidator (string apiKey)
    {
        ApiKey = apiKey;
    }

    public bool Validate(string? providedKey)
    {
        return !string.IsNullOrEmpty(providedKey) && providedKey == ApiKey;
    }
}