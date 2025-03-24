// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PharmacyDB.Data.Models;

namespace PharmacyDB
{
    class Program
    {
        public static async Task Main (string[] args)
        {
            var context = new PharmacyDbContext();
            Console.WriteLine("Welecome to pharmacy!");
            while(true)
            {
                Console.WriteLine("Menu: ");
                Console.WriteLine("1.View all medecines");
                Console.WriteLine("2.Medicine quantity under 50");
                Console.WriteLine("3.Top 3 most expensive medicines in pharmacy");
                Console.WriteLine("4.View all employees");
                Console.WriteLine("5.View all order by supplier name");
                Console.WriteLine("6.Out of stock medicines");
                Console.WriteLine("7.Doctor who wrote prescriptions");
                Console.WriteLine("8.Patient name by doctor name");
                Console.WriteLine("9.Order total price");
                Console.WriteLine("10.Exit");
                int num=int.Parse(Console.ReadLine());
                if(num==10)
                {
                    break;
                }
                switch(num)
                {
                    case 1:
                        await ViewAllMedicines(context);
                        break;
                    case 2:
                        await MedicineQuantityUnder50(context);
                        break;
                    case 3:
                        await Top3MostExpensiveMedicinesInPharmacy(context);
                        break;
                    case 4:
                        await ViewAllEmployees(context);
                        break;
                    case 5:
                        await ViewAllOrderBySupplierName(context);
                        break;
                    case 6:
                        await OutOfStockMedicines(context);
                        break;
                    case 7:
                        await DoctorsWhoWrotePrescriptions(context);
                        break;
                    case 8:
                        await PatientNameByDoctorName(context);
                        break;
                    case 9:
                        await OrderTotalPrice(context);
                        break;
                    default:
                        Console.WriteLine("This option does not exist!");
                        break;
                }
            }
        }
        public static async Task ViewAllMedicines(PharmacyDbContext context)
        {
            var medicines = await context.Medicines.ToListAsync<Medicine>();
            foreach(var medicine in medicines)
            {
                Console.WriteLine(medicine.Name);
            }
        }
        public static async Task MedicineQuantityUnder50(PharmacyDbContext context)
        {
            var medicines=await context.Medicines.Where(m=>m.QuantityInStock<50)
                                                 .ToListAsync();
            foreach(var medicine in medicines)
            {
                Console.WriteLine(medicine.Name);
            }
        }
        public static async Task Top3MostExpensiveMedicinesInPharmacy(PharmacyDbContext context)
        {
            var medicines=await context.Medicines.OrderByDescending(m=>m.Price)
                                                 .Take(3)
                                                 .ToListAsync();
            foreach(var medicine in medicines)
            {
                Console.WriteLine($"{medicine.Name} - {medicine.Price}");
            }

        }
        public static async Task ViewAllEmployees(PharmacyDbContext context)
        {
            var employees = await context.Employees.ToListAsync<Employee>();
            foreach(var employee in employees)
            {
                Console.WriteLine($"{employee.Name} - {employee.Position}");
            }
        }
        public static async Task ViewAllOrderBySupplierName(PharmacyDbContext context)
        {
            Console.WriteLine("Enter supplier name: ");
            string supplierName=Console.ReadLine();
            var suppliers=await context.Orders.Where(o=>o.SupplierName==supplierName)
                                              .ToListAsync();
            if(suppliers.Count == 0 )
            {
                Console.WriteLine("This supplier name does not exist!");
            }
            else
            {
                foreach(var supplier in suppliers)
                {
                    Console.WriteLine($"{supplier.SupplierName} - Order date: {supplier.OrderDate}, Quantity: {supplier.QuantityOrdered}");
                }
            }
        }
        public static async Task OutOfStockMedicines(PharmacyDbContext context)
        {
            var medicines=await context.Medicines.Where(m=>m.QuantityInStock==0)
                                                 .ToListAsync();
            foreach (var medicine in medicines)
            {
                Console.WriteLine(medicine.Name);
            }
        }
        public static async Task DoctorsWhoWrotePrescriptions(PharmacyDbContext context)
        {
            var prescriptions=await context.Prescriptions.Select(p=>p.DoctorName)
                                                         .ToListAsync();
            foreach(var doctorName in prescriptions)
            {
                Console.WriteLine(doctorName);
            }
        }
        public static async Task PatientNameByDoctorName(PharmacyDbContext context)
        {
            Console.WriteLine("Enter doctor name: ");
            string doctorName=Console.ReadLine();
            var prescriptions=await context.Prescriptions.Where(p=>p.DoctorName==doctorName)
                                                         .Select(p=>p.PatientName)
                                                         .ToListAsync();
            foreach(var patientName in prescriptions)
            {
                Console.WriteLine($"Patient name: {patientName}");
            }
        }
        public static async Task<Decimal> OrderTotalPrice(PharmacyDbContext context)
        {
            var totalPrice = await context.Orders
                                          .Join(context.Medicines,
                                          order => order.MedicineId,
                                          medicine => medicine.MedicineId,
                                          (order, medicine) => new { order.QuantityOrdered, medicine.Price })
                                          .SumAsync(x => x.QuantityOrdered * x.Price);
            return totalPrice;
          
        }
    }
}