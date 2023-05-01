using Microsoft.EntityFrameworkCore;
using Portal.Core.Interface;
using Portal.Core.Models;
using Portal.Infrastructure.Database;
using Portal.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Repository
{
    public class DepartmentRep : IDepartmentRep
    {
        private readonly ApplicationContext db;

        public DepartmentRep(ApplicationContext db)
        {
            this.db = db;
        }
        public async Task<List<Department>> GetAsync()
        {
            var data = await db.Departments.ToListAsync();
            return data;
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var data = await db.Departments.Where(a => a.Id == id).SingleOrDefaultAsync();

            if (data == null)
                throw new NullReferenceException("");

            return data;
        }
        public async Task CreateAsync(Department obj)
        {

            await db.Departments.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        public async Task DeleteAsync(Department obj)
        {
            var oldData = db.Departments.Find(obj.Id);

            db.Departments.Remove(oldData);
            await db.SaveChangesAsync();
        }

    }
}
