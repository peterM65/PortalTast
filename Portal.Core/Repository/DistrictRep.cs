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
    public class DistrictRep: IDistrictRep
    {
        private readonly ApplicationContext db;
        public DistrictRep(ApplicationContext db)
        {
            this.db = db;
        }
        public async Task<List<District>> GetAsync(Expression<Func<District, bool>> filter)
        {
            var data = await db.Districts.Include(dis => dis.City).Where(filter).ToListAsync();
            return data;
        }

        public async Task<District> GetByIdAsync(Expression<Func<District, bool>> filter)
        {
            var data = await db.Districts.Include(dis => dis.City).Where(filter).SingleOrDefaultAsync();

            if (data == null)
                throw new NullReferenceException("");

            return data;
        }
    }
}
