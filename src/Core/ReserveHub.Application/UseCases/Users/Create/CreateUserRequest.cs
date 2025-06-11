using System.ComponentModel.DataAnnotations;

namespace ReserveHub.Application.UseCases.Users.Create;

public sealed record CreateUserRequest(
    [Required] 
    [MaxLength(50)] 
    string FirstName, 
    
    [Required] 
    [MaxLength(50)] 
    string LastName, 
    
    [Required] 
    [EmailAddress] 
    [MaxLength(100)] 
    string Email,
    
    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(
        pattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{5,}$",
        ErrorMessage = "Password must be at least 5 characters long and include uppercase, " +
                       "lowercase, number, and special character.")] 
    string Password);