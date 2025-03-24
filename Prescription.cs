using System;
using System.Collections.Generic;

namespace PharmacyDB.Data.Models;

public partial class Prescription
{
    public int Prescription1 { get; set; }

    public int? MedicineId { get; set; }

    public string DoctorName { get; set; } = null!;

    public string PatientName { get; set; } = null!;

    public DateOnly DateIssued { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Medicine? Medicine { get; set; }
}
