using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models.Enums;
using SalesWebMVC.Models;

namespace SalesWebMVC.Data
{
    public class SeedingService
    {
        private SalesWebMVCContext _context;

        public SeedingService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Department.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecord.Any())
            {
                return; // DB has been seeded
            }

            Department d1 = new Department(1, "Desenvolvimento");
            Department d2 = new Department(2, "Logistíca");
            Department d3 = new Department(3, "Apresentação");

            Seller s1 = new Seller(1, "Markus Knauer", "knauer.kar2009@gmail.com", new DateTime(1995, 08, 26), 2000.0, d1);
            Seller s2 = new Seller(2, "Laura Alves", "Laura.Alves2022@gmail.com", new DateTime(2018, 09, 13), 2500.0, d2);
            Seller s3 = new Seller(3, "Yasmin Andrade", "knauer.kar2009@gmail.com", new DateTime(1995, 08, 26), 2000.0, d3);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2022, 11, 15, 08, 35,45), 11000.0, SaleStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2022, 11, 15, 10, 45, 45), 11000.0, SaleStatus.Pending, s1);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2022, 11, 15, 10, 45, 45), 50000.0, SaleStatus.Pending, s1);

            SalesRecord r4 = new SalesRecord(4, new DateTime(2022, 11, 15, 08, 35, 45), 11000.0, SaleStatus.Billed, s2);
            SalesRecord r5 = new SalesRecord(5, new DateTime(2022, 11, 15, 10, 45, 45), 11000.0, SaleStatus.Pending, s2);
            SalesRecord r6 = new SalesRecord(6, new DateTime(2022, 11, 15, 10, 45, 45), 50000.0, SaleStatus.Pending, s2);

            SalesRecord r7 = new SalesRecord(7, new DateTime(2022, 11, 15, 08, 35, 45), 11000.0, SaleStatus.Billed, s3);
            SalesRecord r8 = new SalesRecord(8, new DateTime(2022, 11, 15, 10, 45, 45), 11000.0, SaleStatus.Pending, s3);
            SalesRecord r9 = new SalesRecord(9, new DateTime(2022, 11, 15, 10, 45, 45), 50000.0, SaleStatus.Pending, s3);

            _context.Department.AddRange(d1,d2, d3);

            _context.Seller.AddRange(s1,s2,s3);

            _context.SalesRecord.AddRange(
               r1, r2,r3,r4, r5, r6, r7, r8, r9
            );

            _context.SaveChanges();
        }
    }
}
