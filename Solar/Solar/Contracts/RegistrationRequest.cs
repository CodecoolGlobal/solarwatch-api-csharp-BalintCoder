using System.ComponentModel.DataAnnotations;

namespace Solar.Contracts;

public record RegistrationRequest(
    [Required] string Email,
    [Required] string Username,
    [Required] string Password);