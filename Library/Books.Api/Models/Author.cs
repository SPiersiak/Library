using System;
using System.Collections.Generic;

namespace Books.Api.Models;

public partial class Author
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
