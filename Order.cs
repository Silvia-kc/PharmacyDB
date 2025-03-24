using System;
using System.Collections.Generic;

namespace PharmacyDB.Data.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? MedicineId { get; set; }

    public string SupplierName { get; set; } = null!;

    public DateOnly OrderDate { get; set; }

    public int QuantityOrdered { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Medicine? Medicine { get; set; }
}
