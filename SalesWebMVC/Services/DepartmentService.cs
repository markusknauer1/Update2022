
using SalesWebMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context;

        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }

        // public List<Department> FindAll() // sincrono
        public async Task<List<Department>> FindAllAsync()
        {
            //return _context.Department.OrderBy(x => x.Name).ToList(); // sincrono
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();   //Async

        }
    }
}