using System;
using System.Collections.Generic;

namespace Demo006.DatabaseWorker.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public int Quantity { get; set; }

    public int Price { get; set; }

    public DateTime Created { get; set; }
}
