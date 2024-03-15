namespace Books.Api.Models;

public class BookDto
{
    public long Id { get; set; }

    public string Title { get; set; }

    public decimal Price { get; set; }

    public int Bookstand { get; set; }

    public int Shelf { get; set; }

    public required AuthorDto Author { get; set; }
}
