using System;
using System.Collections.Generic;

namespace PharmacyDB.Data.Models;

public partial class Medicine
{
    public int MedicineId { get; set; }

    public string? Name { get; set; }

    public string Manufacturer { get; set; } = null!;

    public decimal Price { get; set; }

    public int QuantityInStock { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
