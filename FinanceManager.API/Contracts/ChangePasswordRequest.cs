namespace FinanceManager.API.Contracts;

/// <summary>Corpo do PUT /api/users/password.</summary>
public record ChangePasswordRequest(string CurrentPassword, string NewPassword);
