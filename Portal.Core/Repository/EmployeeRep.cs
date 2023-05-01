using Microsoft.EntityFrameworkCore;
using Portal.Core.Interface;
using Portal.Infrastructure.Database;
using Portal.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Repository
{
    public class EmployeeRep : IEmployeeRep
    {
        private readonly ApplicationContext db;

        public EmployeeRep(ApplicationContext db)
        {
            this.db = db;
        }
        public async Task<List<Employee>> GetAsync(Expression<Func<Employee, bool>> filter)
        {
            var data = await db.Employees.Include(emp => emp.Department)
                                         .Include(emp => emp.District)
                                         .ThenInclude(emp => emp.City)
                                         .ThenInclude(emp => emp.country)
                                         .Where(filter).ToListAsync();
            return data;
        }

        public async Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter)
        {
            var data = await db.Employees.Include(emp => emp.Department).Where(filter).SingleOrDefaultAsync();

            if (data == null)
                throw new NullReferenceException("");

            return data;
        }
        public async Task CreateAsync(Employee obj)
        {

            await db.Employees.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee obj)
        {
            obj.IsUpdated = true;
            obj.UpdateOn = DateTime.Now;

            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        public async Task DeleteAsync(Employee obj)
        {
            obj.IsDeleted = true;
            obj.DeletedOn = DateTime.Now;
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
