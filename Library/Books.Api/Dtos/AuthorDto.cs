using System.ComponentModel.DataAnnotations;

namespace Books.Api.Models;

public class AuthorDto
{
    [Required]
    [MaxLength(100)]
    public required string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public required string LastName { get; set; }
}
