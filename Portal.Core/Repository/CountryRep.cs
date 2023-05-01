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
    public class CountryRep:ICountryRep
    {
        private readonly ApplicationContext db;
        public CountryRep(ApplicationContext db)
        {
            this.db = db;
        }
        public async Task<List<Country>> GetAsync(Expression<Func<Country, bool>> filter)
        {
            var data = await db.Countries.Where(filter).ToListAsync();
            return data;
        }

        public async Task<Country> GetByIdAsync(Expression<Func<Country, bool>> filter)
        {
            var data = await db.Countries.Where(filter).SingleOrDefaultAsync();

            if (data == null)
                throw new NullReferenceException("");

            return data;
        }
    }
}
