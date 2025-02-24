using Microsoft.EntityFrameworkCore;

namespace backend.Services;

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