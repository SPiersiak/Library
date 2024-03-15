using System.ComponentModel.DataAnnotations;

namespace Books.Api.Models;

public class BookDto
{
    public long Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Title { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Bookstand { get; set; }

    [Required]
    public int Shelf { get; set; }

    public required AuthorDto Author { get; set; }
}
