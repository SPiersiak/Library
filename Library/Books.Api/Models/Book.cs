using System;
using System.Collections.Generic;

namespace Books.Api.Models;

public partial class Book
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public int Bookstand { get; set; }

    public int Shelf { get; set; }

    public long AuthorId { get; set; }

    public virtual Author Author { get; set; } = null!;
}
